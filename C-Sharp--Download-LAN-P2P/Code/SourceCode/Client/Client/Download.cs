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
    public partial class Download : Form
    {
        private Socket S_loginConnect;
        private Socket S_clientListen;
        private Socket S_clientAccept;

        public int i_indexFile = 0;
        private bool b_clientListen;
        public Thread T_clientListen;

        private String S_portListen;
        private String UserName;
        public IPAddress IP;
        public String myPathSave;
        public DTOInfor infor = new DTOInfor();
        private FileInfo[] files;

        //Quan ly Thread Download :
        public List<Thread> L_threadDownload = new List<Thread>();
        private String nameThread;

        public Semaphore Sema_addClient = new Semaphore(0, 1);
        //Count Client connect download :
        private int downloadNumber;

        //get data from form Login :
        public void funData(TextBox txtForm1)
        {
            textBox8.Text = txtForm1.Text;
        }

        public Download(Socket login, String userName)
        {
            //Lấy thêm dữ liệu trong form Login :
            UserName = userName;
            this.S_loginConnect = login;
            b_clientListen = true;
            //Load data in form :
            InitializeComponent();
        }

        public void listBox()
        {
            dtgvFile.Rows.Clear();
            DirectoryInfo directory = new DirectoryInfo(BoxFolder.Text);
            directory.Refresh();
            files = directory.GetFiles();
            foreach (FileInfo file in files)
                dtgvFile.Rows.Add(file.Name, file.Length);
        }

        public void listClient()
        {
             List<DTOInfor> myList = BUSInfor.getListInfor();
            foreach (DTOInfor list in myList)
            {
                dtgvClient.Rows.Add(list.FileName, list.Size.ToString(), list.IpClient, list.PortClient.ToString(), list.FromClient, "Finish", list.Size.ToString(), list.Flag);
                i_indexFile++;
            }
        }

        public void checkDown(int i_index, String nameFile, String nameUser, long sizeDownload , String stateDown)
        {
            try
            {
                if (dtgvClient[0, i_index].Value.ToString() == nameFile && dtgvClient[4, i_index].Value.ToString() == nameUser)
                {
                    dtgvClient[7, i_index].Value = true;
                    dtgvClient[6, i_index].Value = (int)sizeDownload;
                    dtgvClient[5, i_index].Value = stateDown;
                }
                else
                    for (int i = 0; i <= i_index; i++)
                    {
                        if (nameFile == dtgvClient[0, i].Value.ToString())
                            if (nameUser == dtgvClient[2, i].Value.ToString())
                            {
                                dtgvClient[7, i].Value = true;
                                dtgvClient[6, i].Value = (int)sizeDownload;
                                dtgvClient[5, i].Value = stateDown;
                                break;
                            }
                    }
            }
            catch (Exception)
            {
            }
        }

        //Xuất các thông tin về Download file lên form Download :
        public void listClientLoad()
        {
            int r = this.dtgvSearch.CurrentCell.RowIndex;
            dtgvClient.Rows.Add(infor.FileName, infor.Size.ToString(), dtgvSearch.Rows[r].Cells[2].Value.ToString(), dtgvSearch.Rows[r].Cells[3].Value.ToString(), infor.FromClient, "Load ...", "0 byte", false);
        }

        public void listClientWait()
        {
            int r = this.dtgvSearch.CurrentCell.RowIndex;
            dtgvClient.Rows.Add(infor.FileName, infor.Size.ToString(), dtgvSearch.Rows[r].Cells[2].Value.ToString(), dtgvSearch.Rows[r].Cells[3].Value.ToString(), infor.FromClient, "Waiting", "0 byte", false);
        }

        //==============================================Listenning to Download :========================================================================================================

        //Load form download cùng với các dử liệu :
        private void Download_Load(object sender, EventArgs e)
        {
            //Đưa các thông tin cần thiết lên giao diện :
            BoxFolder.Text = "C:\\Share";
            BoxIP.Text = IP.ToString();
            BoxPort.Text = "8080";
            BoxSaveFile.Text = "C:\\Download";
            textBoxPath.Text = BoxSaveFile.Text;
            //Đặt các button cố dịnh :
            ButSearch.Enabled = false;
            ButClear.Enabled = true;
            ButDownload.Enabled = false;
            ButPause.Enabled = false;
            ButResume.Enabled = false;

            //Kiểm tra sự tồn tại của Folder : Download và Share. 
            DirectoryInfo myFolderShare = new DirectoryInfo("C:\\Share");
            if (myFolderShare.Exists == false)
                myFolderShare.Create();

            DirectoryInfo myFolderDown = new DirectoryInfo("C:\\Download");
            if (myFolderDown.Exists == false)
                myFolderDown.Create();

            //Performing list file wanna share
            listBox();
            listClient();
            //Default ip and port downloading :
            if (textBox8.Text == "" || textBox8.Text == "9090")
                textBox8.Text = "9090";
            string[] ipp = (S_loginConnect.LocalEndPoint.ToString()).Split(new char[] { ':' });
            textBox9.Text = ipp[0];
            textBox1.Text = ipp[1];

            S_portListen = textBox8.Text;

            //create thread listen from other client to download file :
            T_clientListen = new Thread(new ThreadStart(downloadListen));
            T_clientListen.IsBackground = true;
            T_clientListen.Start();
        }

        //Hàm chạy lắng nghe của Client :
        private void downloadListen()
        {
            //Tạo Socket lắng nghe download từ các Client :
            try
            {
                int i_Port = System.Convert.ToInt32(S_portListen, 10);
                S_clientListen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, i_Port);

                //Bind local IP Address : 
                S_clientListen.Bind(ipLocal);

                //Start listening :
                S_clientListen.Listen(10);

                //Start Server connnection Client :
                downloadNumber = 0;
                while (b_clientListen)
                {
                    downloadNumber++;
                    //Listening connection ...
                    S_clientAccept = S_clientListen.Accept();

                    //Create a Thread to process :
                    Thread T_threadListen;
                    T_threadListen = new Thread(new ThreadStart(ThreadListen));

                    //Start call thread run to listen Client connect
                    T_threadListen.Start();
                }
                Thread.CurrentThread.Abort();
            }
            catch (SocketException)
            {
                Thread.CurrentThread.Abort();
            }
        }

        //truyền dữ liệu vào thread lắng nghe :
        private void ThreadListen()
        {
            new ThreadListen(S_clientAccept, S_loginConnect, UserName, this);
            Thread.CurrentThread.Abort();
        }

        //==============================================Listenning to Download :========================================================================================================



        //==============================================Connection to Download :========================================================================================================

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (BoxIP.Text == "" || BoxPort.Text == "")
                {
                    MessageBox.Show("Input : IP and Port.", "MessageBox Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    ButDownload.Enabled = true;
                    return;
                }
                if (BoxSaveFile.Text == "" || BoxFileName.Text == "")
                {
                    MessageBox.Show("Input : Name File and Save File.", "MessageBox Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    return;
                }
                if (BoxSaveFile.Text == "C:\\")
                {
                    MessageBox.Show("Can't Save file in Folder C because it's limitted by Admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DirectoryInfo testFolder = new DirectoryInfo(BoxSaveFile.Text);
                if (testFolder.Exists == false)
                {
                    if (MessageBox.Show("Computer has not folder :" + "\"" + BoxSaveFile.Text + "\". \n" + "Do you want to create folder with this name ?", "MessageBox Error ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        testFolder.Create();
                    }
                    else
                    {
                        return;
                    }
                }
                FileInfo testFileExit;
                myPathSave = BoxSaveFile.Text;
                if (BoxSaveFile.Text.EndsWith("\\") == false)
                    testFileExit = new FileInfo(BoxSaveFile.Text + "\\" + BoxFileName.Text);
                else
                    testFileExit = new FileInfo(BoxSaveFile.Text + BoxFileName.Text);

                if (testFileExit.Exists == true)
                {
                    if (MessageBox.Show("You have file on computer with name" + "\"" + BoxSaveFile.Text + "\\" + BoxFileName.Text + "\". \n" + "Do you want to download again this file ?", "MessageBox Error ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                    else
                        try
                        {
                            testFileExit.Delete();
                        }
                        catch (IOException)
                        {
                            //MessageBox.Show(se.Message);
                        }
                }

                int r = this.dtgvSearch.CurrentCell.RowIndex;
                infor.FileName = dtgvSearch.Rows[r].Cells[0].Value.ToString();
                infor.Size = Convert.ToInt32(dtgvSearch.Rows[r].Cells[1].Value);
                infor.IpClient = dtgvSearch.Rows[r].Cells[2].Value.ToString();
                infor.PortClient = Convert.ToInt32(dtgvSearch.Rows[r].Cells[3].Value);
                infor.FromClient = dtgvSearch.Rows[r].Cells[4].Value.ToString();

                if (dtgvSearch.Rows[r].Cells[5].Value.ToString() == "False")
                {
                    if (MessageBox.Show("Do you want download the file ? \n Because Client share file offline", "MessageBox Stop Server", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        //Send String : //Offline @nameFile @size @Username @IPofMe @PortofMe @null.
                        byte[] Waiting = new byte[1024];
                        String Str_Waiting = "Offline@" + dtgvSearch.Rows[r].Cells[0].Value.ToString() + "@" + dtgvSearch.Rows[r].Cells[1].Value.ToString() + "@" + dtgvSearch.Rows[r].Cells[4].Value.ToString() + "@" + textBox9.Text + "@" + textBox8.Text + "@null";
                        byte[] byWaiting = System.Text.Encoding.ASCII.GetBytes(Str_Waiting);
                        this.S_loginConnect.Send(byWaiting);
                    }
                    return;
                }

                //Send information to server :
                //Download @nameFile @size @Username @Me @null
                byte[] byDownload = new byte[1024];
                String Str_download = "Download@" + dtgvSearch.Rows[r].Cells[0].Value.ToString() + "@" + dtgvSearch.Rows[r].Cells[1].Value.ToString() + "@" + dtgvSearch.Rows[r].Cells[4].Value.ToString() + "@" + UserName + "@null";
                byDownload = System.Text.Encoding.ASCII.GetBytes(Str_download);
                this.S_loginConnect.Send(byDownload);

                //Receive String "Start" from Server  : 
                int i_Start = this.S_loginConnect.Receive(byDownload);
                char[] chars_Start = new char[i_Start];
                System.Text.Decoder d_Start = System.Text.Encoding.UTF8.GetDecoder();
                int charLenStart = d_Start.GetChars(byDownload, 0, i_Start, chars_Start, 0);
                System.String S_Start = new System.String(chars_Start);
                if (S_Start == "Start")
                {
                    MessageBox.Show("Begin Download File : " + dtgvSearch.Rows[r].Cells[0].Value.ToString(), "Download File ", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    //begin create thread to download :
                    Thread T_clientDownload = new Thread(new ThreadStart(connectClient));
                    T_clientDownload.Start();

                    //Add Thread vao List danh sach cac Thread Download :
                    T_clientDownload.Name = infor.FileName + "@" + infor.FromClient + "@" + i_indexFile.ToString();
                    L_threadDownload.Add(T_clientDownload);
                    listClientLoad();
                    //phoi hop xu ly : doi thread T_clientDownload chay truoc.
                    Sema_addClient.WaitOne();
                    
                }
                else
                {
                    infor.Flag = false;
                    listClientWait();
                    i_indexFile++;
                    MessageBox.Show("Please, Waiting to Download File : " + dtgvSearch.Rows[r].Cells[0].Value.ToString(), "Waiting Download", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                }
                BUSInfor.Insert(infor);
                ButDownload.Enabled = true;
            }
            catch (SocketException)
            {
                ButDownload.Enabled = true;
            }
        }

        //Download ofline khi client share file offlien :
        private void waitDownload()
        {
            int r = this.dtgvSearch.CurrentCell.RowIndex;

            //Send String : Waiting @nameFile @size @Username @Me @null
            byte[] Waiting = new byte[1024];
            String Str_Waiting = "Waiting@" + dtgvSearch.Rows[r].Cells[0].Value.ToString() + "@" + dtgvSearch.Rows[r].Cells[1].Value.ToString() + "@" + dtgvSearch.Rows[r].Cells[4].Value.ToString() + "@" + UserName + "@null";
            byte[] byWaiting = System.Text.Encoding.ASCII.GetBytes(Str_Waiting);
            this.S_loginConnect.Send(byWaiting);
        }

        //connection to Client with IP and Port : 
        private void connectClient()
        {
            int r = this.dtgvSearch.CurrentCell.RowIndex;
            //truyền dữ liệu vào thread connect :
            new ThreadDownload(BoxIP.Text, BoxPort.Text, BoxSaveFile.Text, BoxFileName.Text, this.S_loginConnect, true, dtgvSearch.Rows[r].Cells[4].Value.ToString(), textBox8.Text, textBox9.Text, i_indexFile++, this, infor);
            Thread.CurrentThread.Abort();
        }

        //==============================================Connection to Download :========================================================================================================

        private void button8_Click_1(object sender, EventArgs e)
        {
            BoxFileName.Text = "";
            BoxIP.Text = "";
            BoxPort.Text = "";
            BoxSaveFile.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (BoxSearch.Text != "")
                ButSearch.Enabled = true;
            else
                ButSearch.Enabled = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                ButDownload.Enabled = true;
                ButSearch.Enabled = false;
                dtgvSearch.Rows.Clear();
                byte[] search;
                string command = "Next";
                String nameSearch = "";

                //send data search : file name .
                nameSearch = "Search@" + BoxSearch.Text + "@" + UserName + "@null";
                search = new byte[1024];
                search = Encoding.ASCII.GetBytes(nameSearch);
                this.S_loginConnect.Send(search);

                while (command == "Next")
                {
                    search = new byte[1024];
                    int i_DataSearch = this.S_loginConnect.Receive(search);
                    char[] chars_DataSearch = new char[i_DataSearch];
                    System.Text.Decoder d_DataSearch = System.Text.Encoding.UTF8.GetDecoder();
                    int charLenSearch = d_DataSearch.GetChars(search, 0, i_DataSearch, chars_DataSearch, 0);
                    System.String S_DataSearch = new System.String(chars_DataSearch);

                    //Get Name file, Size File, IP Client, Port Client :
                    string[] array = S_DataSearch.Split(new char[] { '@' });

                    // No Found file in Server : 
                    if (array[0] == "Null" || array[0] == "")
                    {
                        MessageBox.Show("No Found this File!", "Search File ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }

                    //Show on Table :
                    else
                    {
                        dtgvSearch.Rows.Add(array[0], array[1], array[2], array[3], array[4], Convert.ToBoolean(array[5]));
                        if (array[6] == "Next")
                        {
                            nameSearch = "Next";
                            search = new byte[1024];
                            search = Encoding.ASCII.GetBytes(nameSearch);
                            this.S_loginConnect.Send(search);
                        }
                        //Reveice : "End"
                        else
                        {
                            nameSearch = "End";
                            search = new byte[1024];
                            search = Encoding.ASCII.GetBytes(nameSearch);
                            this.S_loginConnect.Send(search);
                        }
                    }
                    command = array[6];
                }
            }
            catch (SocketException)
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                b_clientListen = false;
                dtgvSearch.Rows.Clear();
                //Gui chuoi thong bao ke thuc :
                String Quit = "Quit@" + UserName + "@" + IP.ToString() + "@null";
                byte[] byQuit = System.Text.Encoding.ASCII.GetBytes(Quit);
                this.S_loginConnect.Send(byQuit);

                //Doi chap nhan su cho phep ket thuc cua server :
                byQuit = new byte[1024];
                int i_Quit = this.S_loginConnect.Receive(byQuit);
                char[] chars_Quit = new char[i_Quit];
                System.Text.Decoder d_Quit = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d_Quit.GetChars(byQuit, 0, i_Quit, chars_Quit, 0);
                System.String S_Quit = new System.String(chars_Quit);

                int exit = 1;
                for (int i = 0; i < L_threadDownload.Count; i++)
                {
                    if (L_threadDownload[i].IsAlive == true)
                    {
                        if (MessageBox.Show("You have some Thread loading to download file ! \n You Terminating Thread Download File ?", "Thread Downlaod ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            for (int j = 0; j < L_threadDownload.Count; j++)
                            {
                                if (L_threadDownload[i].IsAlive == true)
                                {
                                    if (L_threadDownload[i].ThreadState == ThreadState.Suspended)
                                        L_threadDownload[i].Resume();
                                    L_threadDownload[i].Abort("Terminating Thread Downlaod");
                                    L_threadDownload[i].Join();
                                }
                            }
                        }
                        else
                        {
                            exit = 0;
                            break;
                        }
                    }
                }
                if (exit == 1)
                {
                    if (S_clientListen != null)
                    {
                        S_clientListen.Close();
                        T_clientListen.Abort();
                        b_clientListen = false;
                        while (T_clientListen.IsAlive)
                            Thread.Sleep(1);
                    }
                    this.Close();
                }
            }
            catch (SocketException)
            {
            }
        }

        private void dtgvSearch_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                int r = this.dtgvSearch.CurrentCell.RowIndex;

                BoxIP.Text = dtgvSearch.Rows[r].Cells[2].Value.ToString();
                BoxPort.Text = dtgvSearch.Rows[r].Cells[3].Value.ToString();
                BoxFileName.Text = dtgvSearch.Rows[r].Cells[0].Value.ToString();
            }
            catch (NullReferenceException)
            {
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (folder.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BoxFolder.Text = folder.SelectedPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            //sau khi add folder thi se auto cap nhat lai danh sach chia se :
            if (BoxFolder.Text[BoxFolder.Text.Length - 1] != '\\')
                BoxFolder.Text += '\\';
            listBox();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            try
            {
                ButUpload.Enabled = false;
                dtgvFile.Rows.Clear();
                if (files == null)
                    return;
                byte[] share;
                string Str_share = "Share@";
                foreach (FileInfo file in files)
                {
                    share = new byte[1024];
                    Str_share += file.Name + "@" + file.Length.ToString() + "@" + UserName;
                    share = System.Text.Encoding.ASCII.GetBytes(Str_share);
                    this.S_loginConnect.Send(share);
                    Str_share = "Share@";

                    //Receive "Next" to : 
                    byte[] Next = new byte[1024];
                    int i_Next = this.S_loginConnect.Receive(Next);
                    char[] chars_Next = new char[i_Next];
                    System.Text.Decoder d_Next = System.Text.Encoding.UTF8.GetDecoder();
                    int charLenNext = d_Next.GetChars(Next, 0, i_Next, chars_Next, 0);
                    System.String S_Next = new System.String(chars_Next);
                    if (S_Next != "Next")
                        break;
                    else
                        dtgvFile.Rows.Add(file.Name, file.Length, file.Exists);
                }
                //MessageBox.Show("Upload Complete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SocketException)
            {
            }
        }

        private void button1_TabIndexChanged(object sender, EventArgs e)
        {
            button1_Click_1(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox();
            ButUpload.Enabled = true;
        }

        private void btPathSave_Click(object sender, EventArgs e)
        {
            if (folder.ShowDialog() == DialogResult.OK)
            {
                BoxSaveFile.Text = folder.SelectedPath;
                textBoxPath.Text = BoxSaveFile.Text;
            }
        }

        private void Download_FormClosing(object sender, FormClosingEventArgs e)
        {
           /*if (S_clientListen != null)
            {
                S_clientListen.Close();
                T_clientListen.Abort();
                b_clientListen = false;
                while (T_clientListen.IsAlive)
                    Thread.Sleep(1);
            }*/
            button4_Click(sender, e);

        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }

        private void ButClearAll_Click(object sender, EventArgs e)
        {
            dtgvClient.Rows.Clear();
            i_indexFile = 0;
            BUSInfor.DeleteAll();
        }

        private void dtgvClient_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                int r = this.dtgvClient.CurrentCell.RowIndex;

                textBoxName.Text = dtgvClient.Rows[r].Cells[0].Value.ToString();
                textBoxIP.Text = dtgvClient.Rows[r].Cells[2].Value.ToString();
                textBoxPort.Text = dtgvClient.Rows[r].Cells[3].Value.ToString();
                String check = dtgvClient.Rows[r].Cells[7].Value.ToString();
                if (check == "False")
                {
                    ButPause.Enabled = true;
                    nameThread = textBoxName.Text + "@" + dtgvClient.Rows[r].Cells[4].Value.ToString() + "@" + r.ToString();
                }
                else
                {
                    ButPause.Enabled = false;
                    ButResume.Enabled = false;
                }
            }
            catch (NullReferenceException)
            {
            }
        }

        private void ButPause_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < L_threadDownload.Count; i++)
            {
                if (L_threadDownload[i].Name == nameThread)
                    if (L_threadDownload[i].IsAlive == true)
                    {
                        L_threadDownload[i].Suspend();
                        ButPause.Enabled = false;
                        ButResume.Enabled = true;
                        break;
                    }
            }
        }

        private void ButResume_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < L_threadDownload.Count; i++)
            {
                if (L_threadDownload[i].Name == nameThread)
                    if (L_threadDownload[i].IsAlive == true)
                    {
                        L_threadDownload[i].Resume();
                        ButResume.Enabled = false;
                        ButPause.Enabled = true;
                        break;
                    }
            }
        }

        private void Download_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Client.Visible = true;
        }
    }
}