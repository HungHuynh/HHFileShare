using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Client
{
    class ThreadListen
    {
        private Socket S_server;
        private Socket S_listen;
        private Download myDownload;
        private String Username;
        private String MeIp;
        private String MePort;
        private String nameFile;

        public ThreadListen(Socket temp,Socket Server,String username, Download down)
        {
            myDownload = down;
            this.S_server = Server;
            this.S_listen = temp;
            Username = username;
            myDownload = down;

            this.listenClient();
        }

        public void listenClient()
        {
            try
            {
                if (this.S_listen == null)
                {
                    Thread.CurrentThread.Interrupt();
                    return;
                }

               //Chờ nhận thông tin từ Client download hoặc Server báo hiệu dowownlaod :
                byte[] DataFile = new byte[1024];
                int i_DataFile = this.S_listen.Receive(DataFile);
                char[] chars_DataFile = new char[i_DataFile];

                System.Text.Decoder d_DataFile = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d_DataFile.GetChars(DataFile, 0, i_DataFile, chars_DataFile, 0);
                System.String S_DataFile = new System.String(chars_DataFile);

                //Waiting  : endWaiting @IPCLient @PorClient @NameFile @online @null.
                //Download : Download @FileName @null.
                string[] resu = S_DataFile.Split(new char[] { '@' });
                
                //Nhận thông tin của Server báo hiệu Client đã Online :
                if (resu[0] == "endWaiting")
                {
                    if (resu[5] != "null")
                        Username = resu[5];
                    new ThreadDownload(resu[1], resu[2], myDownload.myPathSave, resu[3], this.S_server, Convert.ToBoolean(resu[4]), Username, MeIp, MePort, myDownload.i_indexFile++, myDownload);
                    this.S_listen.Close();
                    Thread.CurrentThread.Interrupt();
                    return;
                }

                //Nhận thông tin từ Client download file : 
                nameFile = resu[1];

                //kiem tra xem file co ton tai tren Client khong ?
                FileInfo test = new FileInfo(nameFile);
                
                //Nếu file không tồn tại sẽ báo hiệu "Close" để kết thúc quá trình Download :
                if (test.Exists == false)
                {
                    String S_Close = "Close";
                    byte[] byClose = System.Text.Encoding.ASCII.GetBytes(S_Close);
                    this.S_listen.Send(byClose);

                    //Đóng kết nối với Client Download :
                    this.S_listen.Close();
                    Thread.CurrentThread.Abort();
                    return;
                }
                //File có tồn tại thì bắt đầu quá trình download :
                else
                {
                    //Gửi tín hiệu cho phép Download : "Accept"
                    String S_accept = "Accept";
                    byte[] byData = System.Text.Encoding.ASCII.GetBytes(S_accept);
                    this.S_listen.Send(byData);

                    //Bắt đầu truyền dữ liệu :
                    this.sendFile(nameFile, this.S_listen);

                    //Kết thúc quá trình truyền dữ liệu :
                    this.S_listen.Close();
                    Thread.CurrentThread.Interrupt();
                    return;
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void sendFile(String path, Socket client)
        {
            try
            {
                FileStream readFile = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader brFile = new BinaryReader(readFile);

                Int64 fileSize = readFile.Length;//File's size wanna send
                byte[] bLen = BitConverter.GetBytes(fileSize);
                client.Send(bLen, bLen.Length, SocketFlags.None);

                int byteSize = 0; //  A block's size send
                byte[] datasend = new byte[2048];
                while ((byteSize = brFile.Read(datasend, 0, datasend.Length)) > 0)
                {
                    client.Send(datasend, 0, byteSize, SocketFlags.None);
                }
                readFile.Close();
            }
            catch (Exception)
            {
                int i = nameFile.LastIndexOf("\\") + 1;
                String MyName = nameFile.Substring(i);
                String StrEn = "Download@Finish@" + MyName + "@" + Username + "@null";
                byte[] byStrEn = System.Text.Encoding.ASCII.GetBytes(StrEn);
                S_server.Send(byStrEn);
            }
        }
    }
}
