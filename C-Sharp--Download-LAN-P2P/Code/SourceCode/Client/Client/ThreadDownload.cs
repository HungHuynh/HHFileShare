using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System;

namespace Client
{
    class ThreadDownload
    {
        private Socket S_download;
        private Socket S_server;
        private String Str_nameFile;
        private String Str_sourse;
        private String IP_connect ;
        private String Port_connect;
        private String UserName;
        private String MePort;
        private String MeIP;
        private String stateDown;
        private Download me;
        private bool result;
        private bool C_online;
        private int i_index;
        private long sizeDownload;
        static Semaphore S_index = new Semaphore(1,1);
        private DTOInfor DataInfor = new DTOInfor();
        public ThreadDownload(String ip, String port, String sourse, String nameFile, Socket sockDownload,bool online,String username, String mePort, String meIp, int index,Download my)
        {
            C_online = online;
            IP_connect = ip;
            Port_connect = port;
            Str_sourse = sourse;
            Str_nameFile = nameFile;
            S_server = sockDownload;
            UserName = username;
            MePort = port;
            MeIP = ip;
            i_index = index;
            me = my;
            this.downloadFile();
        }

        public ThreadDownload(String ip, String port, String sourse, String nameFile, Socket sockDownload, bool online, String username, String mePort, String meIp, int index, Download my, DTOInfor data)
        {
            C_online = online;
            IP_connect = ip;
            Port_connect = port;
            Str_sourse = sourse;
            Str_nameFile = nameFile;
            S_server = sockDownload;
            UserName = username;
            MePort = port;
            MeIP = ip;
            i_index = index;
            me = my;
            DataInfor = data;
            me.Sema_addClient.Release();
            this.downloadFile();
        }

        public void downloadFile()
        {          
            try
            {
                //Kiểm tra path chứa file :
                if (Str_sourse == null)
                    Str_sourse = me.myPathSave;
                if (Str_sourse.EndsWith("\\") == false)
                    Str_sourse = Str_sourse + "\\";

                //kết nối đến Client share file :
                S_download = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                int i_Port = System.Convert.ToInt32(Port_connect, 10);
                S_download.Connect(IP_connect, i_Port);

                //Gửi tên file cho Client để download :
                //Download@FileName@null.
                String namefile = "Download@C:\\Share\\" + this.Str_nameFile+"@null";
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(namefile);
                this.S_download.Send(byData);

                //Nhận thông tin báo hiệu cho phép download : "Accept" hoặc "Close" 
                byte[] accept = new byte[10];
                int i_accept = this.S_download.Receive(accept);
                char[] chars_accept = new char[i_accept];
                System.Text.Decoder d_accept = System.Text.Encoding.UTF8.GetDecoder();
                int charLen_accept = d_accept.GetChars(accept, 0, i_accept, chars_accept, 0);
                String C_accept = new System.String(chars_accept);

                //Client share file báo hiệu không tồn tại file và đóng kết nối:
                if (C_accept == "Close")
                {
                    MessageBox.Show("You Can't Download File \" " + this.Str_nameFile + " \" because Client has not file !", "MessageBox Download", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                    S_download.Disconnect(true);
                    S_download.Close();
                    Thread.CurrentThread.Interrupt();
                    return;
                }

                //Client share file báo hiệu cho phép download :
                if (C_accept == "Accept")
                {
                    Str_sourse += Str_nameFile;
                    //Bắt đầu Download dữ liệu :
                    this.result = ReceiveFile(Str_sourse,S_download);

                    if (C_online == false)
                    {
                        //Gửi tín hiệu kết thúc download offline lên server : 
                        String StrEnd = "endOffline@Finish@null";
                        byte[] byStrEnd = System.Text.Encoding.ASCII.GetBytes(StrEnd);
                        S_server.Send(byStrEnd);
                        //Thông báo người dùng biết đã download offline xong :
                        MessageBox.Show("Download offline file \"" + this.Str_nameFile + "\" is successful!", "MessageBox Download", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                        S_download.Close();
                        Thread.CurrentThread.Interrupt();
                        return;
                    }

                    //Gửi tín hiệu kết thúc download lên server : 
                    //Ending : Download @Finish @fileName @username @null.
                    String StrEn = "Download@Finish@" + Str_nameFile + "@" + UserName + "@null";
                    byte[] byStrEn = System.Text.Encoding.ASCII.GetBytes(StrEn);
                    S_server.Send(byStrEn);

                    //Cap nhat du lieu cho file Access :
                    DataInfor.Flag = true;
                    DataInfor.Size = (int)sizeDownload;
                    BUSInfor.Update(DataInfor);

                    //Cap nhat len giao dien DataGridView :
                    //===========Mien Gang=======================
                    S_index.WaitOne();
                    me.checkDown(i_index, Str_nameFile, UserName, sizeDownload, stateDown);
                    S_index.Release();
                    //===========Mien Gang=======================

                    //Thông báo người dùng biết đã download xong :
                    if (result == true)
                    {
                       MessageBox.Show("Downloading file \"" + this.Str_nameFile + "\" is successful!", "MessageBox Download", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    }

                    //ngắt kết nối Client share file :
                    S_download.Close();
                    Thread.CurrentThread.Abort();
                    return;
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        //Quá trình download :
        public bool ReceiveFile(string path, Socket client)
        {
            // Receive size's file
            byte[] bLen = new byte[1024];
            client.Receive(bLen);
            Int64 fileSize = BitConverter.ToInt64(bLen, 0);

            // write block data into file have created 
            FileStream writeFile = new FileStream(path, FileMode.Append, FileAccess.Write);
            int byteSize = 0;
            byte[] buf = new byte[2048];
            client.ReceiveTimeout = 5000;
            try
            {
                while ((byteSize = client.Receive(buf, 0, buf.Length, SocketFlags.None)) > 0)
                {
                    writeFile.Write(buf, 0, byteSize);
                }
                sizeDownload = writeFile.Length;
                writeFile.Close();
                stateDown = "Finish";
                return true;
            }
            catch (SocketException)
            {
                sizeDownload = writeFile.Length;
                writeFile.Close();
                stateDown = "Disconnect";
                if (MessageBox.Show("Disconnect download file \"" + path + " \"\n" + "Do you want to download resume this file ?", "Download Resume", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    //Send String : Offline @nameFile @size @Username @IPofMe @PortofMe @null.
                    String Str_byDis = "Offline@" + Str_nameFile + "@0@" + UserName + "@" + MeIP + "@" + MePort + "@null";
                    byte[] byDis = System.Text.Encoding.ASCII.GetBytes(Str_byDis);
                    S_server.Send(byDis);
                    FileInfo myfile = new FileInfo(path);
                    myfile.Delete();
                }
            }
            return false;
        }
    }
}