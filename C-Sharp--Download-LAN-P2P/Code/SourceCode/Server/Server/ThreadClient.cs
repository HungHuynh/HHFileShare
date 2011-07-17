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
using BUS;
using DTO;

namespace Server
{
    public class ThreadClient
    {
        public Socket S_connectClient;
        private String User;
        private string[] Arr;
        private server myServer;

        //du lieu download offline :
        static int numThread = 10;
        static DTOClient LoginClient = new DTOClient();
        static DTOData DownData = new DTOData();
        static Thread[] T_downloadWait = new Thread[10];
        static Semaphore S_offlineDown = new Semaphore(1, 1);   //(x,y)

        // Du lieu cho dong bo cho download :
        static int i_Semaphore;
        static String nameSemaphore;
        static List<Semaphore> listSemaphore = new List<Semaphore>();
        static List<String> listNameSemaphore = new List<String>(); 
        static List<bool> listCheckFile = new List<bool>();
        static Queue<String>[] listDataSemaphore = new Queue<String>[10];

        //Semaphore dong bo hoa ve : truy xuat du lieu
        static Semaphore S_getClient = new Semaphore(1, 1);

        // Semaphore cho quá trình login, kiem tra hang doi trong ham testLogin() :
        static Semaphore S_Login = new Semaphore(1, 1);

        //Semaphore cho qua trinh update CLient :
        static Semaphore S_updateClient = new Semaphore(1, 1);

        //Semaphore cho qua trinh ghi gia tri i_Mutex :
        static Semaphore S_iSemaphore = new Semaphore(1, 1);

        public ThreadClient(Socket S_sockClient, server MyServer)
        {
            this.S_connectClient = S_sockClient;
            myServer = MyServer;
            for (int i = 0; i < 10; i++)
            {
                listDataSemaphore[i] = new Queue<string>();
            }
            string command = "";
            string username = "";
            string password = "";
            string numport = "";
            byte[] bufData;
            int exit = 0;
            try
            {
                if (this.S_connectClient != null)
                {
                    int i_DataFile = 0;
                    while (true)
                    {
                        //Receive name, size of file download : 
                        bufData = new byte[1024];
                        i_DataFile = S_connectClient.Receive(bufData);

                        char[] chars_DataFile = new char[i_DataFile];
                        System.Text.Decoder d_DataFile = System.Text.Encoding.UTF8.GetDecoder();
                        int charLen = d_DataFile.GetChars(bufData, 0, i_DataFile, chars_DataFile, 0);
                        System.String S_DataFile = new System.String(chars_DataFile);

                        Arr = S_DataFile.Split(new char[] { '@' });
                        if (Arr[0].Contains("\0"))
                        {
                            clientUpdate();
                            exit = 1;
                            break;
                        }

                        command = Arr[0];
                        username = Arr[1];
                        password = Arr[2];

                        switch (command)
                        {
                            //Register@UserName@UserPass@UserPort@null.
                            case "Register":
                                {
                                    if (BUSClient.Test(username) == true)
                                    {
                                        DTOClient client = new DTOClient();
                                        client.UserName = username;
                                        client.PassWord = password;

                                        //========Mien Gang===================
                                        S_updateClient.WaitOne();
                                        bool rs = BUSClient.Insert(client);
                                        S_updateClient.Release();
                                        //==========Mien Gang==================

                                        if (rs == false)
                                        {
                                            String S_Error = "Cannot create Account";
                                            byte[] byData = System.Text.Encoding.ASCII.GetBytes(S_Error);
                                            this.S_connectClient.Send(byData);
                                        }
                                        else
                                        {
                                            String S_Error = "Accept";
                                            byte[] byData = System.Text.Encoding.ASCII.GetBytes(S_Error);
                                            this.S_connectClient.Send(byData);
                                        }
                                    }
                                    else
                                    {
                                        String S_Error = "Username has existed!";
                                        byte[] byData = System.Text.Encoding.ASCII.GetBytes(S_Error);
                                        this.S_connectClient.Send(byData);
                                    }
                                    exit = 1;
                                }
                                break;

                            //"Login @UserName @UserPass @PortListen @ IPClient@null
                            case "Login":
                                {
                                    User = username;
                                    numport = Arr[3];
                                    if (BUSClient.TestLogin(username, password) == true)
                                    {
                                        String S_Login = "Login";
                                        byte[] byData = System.Text.Encoding.ASCII.GetBytes(S_Login);
                                        this.S_connectClient.Send(byData);

                                        DTOClient client = new DTOClient();
                                        client.UserName = username;
                                        client.PassWord = password;
                                        client.IPAdr = Arr[4];
                                        client.NumPort = Convert.ToInt32(numport, 10);
                                        client.FlagSta = true;
                                        BUSData.DeleteAll(username);

                                        //=========Mien Gang================
                                        S_updateClient.WaitOne();
                                        BUSClient.Update(client);
                                        S_updateClient.Release();
                                        //=========Mien Gang================

                                        LoginClient.IPAdr = Arr[4];
                                        LoginClient.NumPort = Convert.ToInt32(numport, 10);

                                        //Kiem tra xem co thread nao dang Wait Download ?
                                        Thread T_testLogin = new Thread(new ThreadStart(testLogin));
                                        T_testLogin.IsBackground = true;
                                        T_testLogin.Start();

                                    }
                                    else
                                    {
                                        String S_Error = "Bad";
                                        byte[] byData = System.Text.Encoding.ASCII.GetBytes(S_Error);
                                        this.S_connectClient.Send(byData);
                                    }
                                    numport = "";
                                }
                                break;

                            //@quit@username@IPClient@null.
                            case "Quit":
                                {
                                    try
                                    {
                                        //=================Mien Gang truy xuat du lieu =====================================
                                        S_getClient.WaitOne();
                                        List<DTOClient> listClient = BUSClient.getListClient();
                                        S_getClient.Release();
                                        //=================Mien Gang truy xuat du lieu ====================================

                                        foreach (DTOClient client in listClient)
                                            if (client.UserName.ToLower() == username.ToLower() && client.IPAdr == Arr[2])
                                            {
                                                client.FlagSta = false;

                                                //=========Mien Gang================
                                                S_updateClient.WaitOne();
                                                BUSClient.Update(client);
                                                S_updateClient.Release();
                                                //=========Mien Gang================

                                                String S_Error = "Quit";
                                                byte[] byData = System.Text.Encoding.ASCII.GetBytes(S_Error);
                                                this.S_connectClient.Send(byData);
                                                S_connectClient.Close();
                                                exit = 1;
                                                break;
                                            }
                                        if (exit == 0)
                                        {
                                            String S_No = "No";
                                            byte[] byS_No = System.Text.Encoding.ASCII.GetBytes(S_No);
                                            this.S_connectClient.Send(byS_No);
                                            S_connectClient.Close();
                                        }
                                    }
                                    catch (SocketException)
                                    {
                                    }
                                }
                                break;

                            //"Share@ FileName@ FileSize@ UserName@ null"
                            case "Share":
                                {
                                    //========================CS===============================
                                    S_getClient.WaitOne();
                                    List<DTOClient> listClient = BUSClient.getListClient();
                                    S_getClient.Release();
                                    //========================CS===============================

                                    string[] ipp = (S_connectClient.RemoteEndPoint.ToString()).Split(new char[] { ':' });
                                    DTOData share = new DTOData();

                                    foreach (DTOClient client in listClient)
                                        if (string.Compare(client.UserName, Arr[3], true) == 0)
                                        {
                                            if (BUSData.checkFile(username, client.IDClient) == true)
                                            {
                                                share.FileName = username;
                                                share.Size = Convert.ToInt32(password);
                                                share.IDClient = client.IDClient;

                                                //=======Truy xuat tai nguyn du lieu Data================
                                                S_updateClient.WaitOne();
                                                BUSData.Insert(share);
                                                S_updateClient.Release();
                                                //=======Truy xuat tai nguyn du lieu Data================
                                            }

                                            String S_Next = "Next";
                                            byte[] byNext = System.Text.Encoding.ASCII.GetBytes(S_Next);
                                            this.S_connectClient.Send(byNext);

                                            break;
                                        }
                                }
                                break;

                            //Search@FileName@null.
                            case "Search": // tien hanh search file do Client gửi len
                                {
                                    List<DTOData> listFile = BUSData.Search(username, password);
                                    int count = listFile.Count;
                                    string infor = "";

                                    // Khong tim duoc thi gui cho Client chuoi Null de thong bao
                                    if (count == 0)
                                    {
                                        infor = "Null@Null@Null@Null@Null@Null";
                                        byte[] byinfor = System.Text.Encoding.ASCII.GetBytes(infor);
                                        this.S_connectClient.Send(byinfor);
                                    }
                                    else
                                    {
                                        //Begin Search file in Clientt :
                                        foreach (DTOData file in listFile)
                                        {
                                            //Get information from Client :
                                            DTOClient client = BUSClient.Search(file.IDClient);
                                            if (client == null)
                                                count--;
                                            else
                                            {
                                                bufData = new byte[1024];
                                                //Information : file+size+IP+Port+Flag 
                                                if (count > 1)
                                                    infor = file.FileName + "@" + file.Size.ToString() + "@" + client.IPAdr + "@" + client.NumPort.ToString() + "@" + client.UserName + "@" + client.FlagSta.ToString() + "@Next";
                                                else
                                                    infor = file.FileName + "@" + file.Size.ToString() + "@" + client.IPAdr + "@" + client.NumPort.ToString() + "@" + client.UserName + "@" + client.FlagSta.ToString() + "@End";
                                                byte[] byinfor = System.Text.Encoding.ASCII.GetBytes(infor);
                                                this.S_connectClient.Send(byinfor);

                                                //Reveice information from Client :
                                                byte[] bysearch = new byte[1024];
                                                int i_DataSearch = this.S_connectClient.Receive(bysearch);

                                                char[] chars_DataSearch = new char[i_DataSearch];
                                                System.Text.Decoder d_DataSearch = System.Text.Encoding.UTF8.GetDecoder();
                                                int charLenSearch = d_DataSearch.GetChars(bysearch, 0, i_DataSearch, chars_DataSearch, 0);
                                                System.String S_DataSearch = new System.String(chars_DataSearch);

                                                //nhan duoc chuoi "Next"thi tiep tuc send thong tin file tim thay :
                                                if (S_DataSearch == "Next")
                                                {
                                                    infor = "";
                                                    count--;
                                                }
                                                //nhan chuoi "End" tuc la da gui het tat ca file da tim thay va thoat khoi vong lap :
                                                if (S_DataSearch == "End")
                                                    break;
                                            }
                                        }
                                    }
                                }
                                break;

                            //Start  : Download @nameFile @size @Username @Me @null.
                            //Ending : Download @Finish @fileName @username @null.
                            case "Download":
                                {
                                    //=================Mien Gang truy xuat du lieu ====================================
                                    S_getClient.WaitOne();
                                    List<DTOClient> listClient = BUSClient.getListClient();
                                    List<DTOData> listData = BUSData.getListData();
                                    S_getClient.Release();
                                    //=================Mien Gang truy xuat du lieu ====================================
                                    
                                    bool i_show = false;
                                    if (username == "Finish")
                                    {
                                        // Khi qua trinh download ket thuc :
                                        foreach (DTOClient client in listClient)
                                            if (client.UserName == Arr[3])
                                            {
                                                foreach (DTOData data in listData)
                                                    if (data.FileName == Arr[2] && data.IDClient == client.IDClient)
                                                    {
                                                        data.FlagSta = false;

                                                        //=======Mien Gang la truy xuat du lieu ===============
                                                        S_updateClient.WaitOne();
                                                        BUSData.Update(data);
                                                        S_updateClient.Release();
                                                        //=======Mien Gang la truy xuat du lieu ===============

                                                        String temp = Arr[2] + "-" + Arr[3];
                                                        for (int i = 0; i < listNameSemaphore.Count; i++)
                                                        {
                                                            if (temp == listNameSemaphore[i])
                                                            {
                                                                try
                                                                {
                                                                    listCheckFile[i] = false;
                                                                    listSemaphore[i].Release();
                                                                }
                                                                catch (AbandonedMutexException se)
                                                                {
                                                                    MessageBox.Show(se.Message);
                                                                }
                                                                break;
                                                            }
                                                        }
                                                    }
                                            }
                                    }
                                    //Download @nameFile @size @Username @Me @null.
                                    else
                                    {
                                        //Bat dau kiem tra file co trang thai nhu the nao ?
                                        foreach (DTOClient client in listClient)
                                            if (client.UserName == Arr[3])
                                            {
                                                foreach (DTOData data in listData)
                                                {
                                                    if (data.IDClient == client.IDClient)
                                                    {
                                                        // TIM KIEM TEN FILE :
                                                        if (data.FileName == Arr[1])
                                                        {
                                                            //kiem tra xem da tao mutes voi ten file nay chua ?
                                                            nameSemaphore = Arr[1] + "-" + Arr[3];
                                                            if (!check(nameSemaphore))
                                                            {
                                                                //neu chua thi tao Mutex voi ten file va UserName:
                                                                Semaphore S_mutex = new Semaphore(1, 1);
                                                                listSemaphore.Add(S_mutex);
                                                                listNameSemaphore.Add(nameSemaphore);
                                                                listCheckFile.Add(false);
                                                            }
                                                            //tim kiem ten Mutex ung voi ten file :
                                                            for (int i = 0; i < listNameSemaphore.Count; i++)
                                                            {
                                                                if (nameSemaphore == listNameSemaphore[i])
                                                                {
                                                                    //Neu ko co file nao downlaod thi bat dau download :
                                                                    if (listCheckFile[i] == false)
                                                                    {
                                                                        listSemaphore[i].WaitOne();
                                                                        //send tin hieu download cho client :
                                                                        String S_Start = "Start";
                                                                        byte[] byStart = System.Text.Encoding.ASCII.GetBytes(S_Start);
                                                                        this.S_connectClient.Send(byStart);
                                                                        listCheckFile[i] = true;
                                                                        data.FlagSta = true;

                                                                        //=======Mien Gang la truy xuat du lieu ===============
                                                                        S_updateClient.WaitOne();
                                                                        BUSData.Update(data);
                                                                        S_updateClient.Release();
                                                                        //=======Mien Gang la truy xuat du lieu ===============

                                                                        break;
                                                                    }

                                                                    //Neu da co Clien dang download thi tao thread vao hang doi :
                                                                    else
                                                                    {
                                                                        //tiem kiem ten mutex de vao hang doi mutex :
                                                                        //==============Mien Gang la bien i_Semaphore================================
                                                                        S_iSemaphore.WaitOne();
                                                                        DownData = data;
                                                                        for (i_Semaphore = 0; i_Semaphore < listNameSemaphore.Count; i_Semaphore++)
                                                                        {
                                                                            //Tim thay ten Mutex ung voi ten file-User :
                                                                            if (nameSemaphore == listNameSemaphore[i_Semaphore])
                                                                            {
                                                                                // endWaiting @IPCLient @PorClient @NameFile @online @ClientShare @null - IPMe @PortMe.
                                                                                foreach (DTOClient clientMe in listClient)
                                                                                    if (clientMe.UserName == Arr[4])
                                                                                    {
                                                                                        //Luu thong tin cua thread vao hang doi ung voi thread do :
                                                                                        String temp = "endWaiting@" + client.IPAdr + "@" + client.NumPort + "@" + data.FileName + "@true@" + Arr[3] +"@null-" + clientMe.IPAdr + "@" + clientMe.NumPort;
                                                                                        listDataSemaphore[i_Semaphore].Enqueue(temp);
                                                                                        break;
                                                                                    }

                                                                                //tao thread vao hang doi cua Mutex :
                                                                                Thread T_waitSemaphore = new Thread(new ThreadStart(waitDownload));
                                                                                T_waitSemaphore.Start();
                                                                                T_waitSemaphore.Join(100);
                                                                            }
                                                                        }
                                                                        S_iSemaphore.Release();
                                                                        //==============Mien Gang la bien i_Mutex================================
                                                                        //send tin hieu waiting cho client :
                                                                        String S_Wait = "Wait";
                                                                        byte[] byWait = System.Text.Encoding.ASCII.GetBytes(S_Wait);
                                                                        this.S_connectClient.Send(byWait);
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                i_show = true;
                                                break;
                                            }
                                    }

                                    if (i_show == true)
                                    {
                                        addDownload("Loading", "true");
                                    }
                                }
                                break;

                            //Download ofline :
                            //Offline @nameFile @size @Username @IPofMe @PortofMe @null.
                            case "Offline":
                                {
                                    for (int i = 0; i < numThread; i++)
                                    {
                                        if (T_downloadWait[i] == null)
                                        {
                                            Thread T_waiting = new Thread(new ThreadStart(beginWait));
                                            T_waiting.Name = Arr[3] + "@" + i.ToString();
                                            T_downloadWait[i] = T_waiting;
                                            myServer.DataWait[i][0] = Arr[1]; // name file
                                            myServer.DataWait[i][1] = Arr[3]; // name Client.
                                            myServer.DataWait[i][2] = Arr[4]; //IP Me
                                            myServer.DataWait[i][3] = Arr[5]; // Port Me

                                            addDownload("Waiting", "true");
                                            break;
                                        }
                                    }
                                }
                                break;

                            case "endOffline":
                                {
                                    S_offlineDown.Release();
                                }
                                break;
                        }
                        if (exit == 1)
                            break;
                    }
                }
            }
            catch (SocketException)
            {
                clientUpdate();
            }
        }

        //Bắt đầu báo hiệu Client share online và download : 
        // endWaiting @IPCLient @PorClient @NameFile @false @null.
        public void beginWait()
        {
            try
            {
                S_offlineDown.WaitOne();
                String[] stt = (Thread.CurrentThread.Name.Split(new char[] { '@' }));
                int i = System.Convert.ToInt16(stt[1], 10);
                String WaitFinish = "endWaiting@" + LoginClient.IPAdr + "@" + LoginClient.NumPort.ToString() + "@" + myServer.DataWait[i][0] + "@false@null";
                byte[] byWaitFinish = System.Text.Encoding.ASCII.GetBytes(WaitFinish);

                //kết nối lại Client qua port lắng nghe để gửi tín hiệu download :
                Socket S_clientWait = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                S_clientWait.Connect(myServer.DataWait[i][2], System.Convert.ToInt16(myServer.DataWait[i][3], 10));
                S_clientWait.Send(byWaitFinish);
                S_clientWait.Close();
            }
            catch (Exception)
            {
            }
        }

        //Gửi lại yêu cầu cho client khi thoát ra khoi Mutex :
        private void waitDownload()
        {
            try
            {
                int i_curentIndex = i_Semaphore;
                DTOData myData = DownData;
                listSemaphore[i_curentIndex].WaitOne();

                //cap nhat lai du lieu :
                listCheckFile[i_curentIndex] = true;
                myData.FlagSta = true;
                BUSData.Update(myData);

                String[] myMutex = (listDataSemaphore[i_curentIndex].Dequeue().Split(new char[] { '-' }));
                String[] myMe = (myMutex[1].Split(new char[] { '@' }));
                // endWaiting @IPCLient @PorClient @NameFile @online @ClientShare @null - IPMe @PortMe.
                byte[] byWaitDownload = System.Text.Encoding.ASCII.GetBytes(myMutex[0]);

                //kết nối lại Client qua port lắng nghe để gửi tín hiệu download khi thoát khỏi hàng dợi mutex :
                Socket S_WaitDownload = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                S_WaitDownload.Connect(myMe[0], System.Convert.ToInt16(myMe[1], 10));
                S_WaitDownload.Send(byWaitDownload);
                S_WaitDownload.Close();
                Thread.CurrentThread.Abort();
            }
            catch (Exception)
            {
            }
        }

        //Kiểm tra tên Mutex có tồn tại hay chưa ?
        private bool check(string s)
        {
            foreach (string str in listNameSemaphore)
                if (str == s)
                    return true;
            return false;
        }

        //cap nhap lai trang thia cua Client :
        private void clientUpdate()
        {
            //=================Mien Gang truy xuat du lieu ====================================
            S_getClient.WaitOne();
            List<DTOClient> listClient = BUSClient.getListClient();
            S_getClient.Release();
            //=================Mien Gang truy xuat du lieu ====================================
            string[] ipp = (S_connectClient.RemoteEndPoint.ToString()).Split(new char[] { ':' });
            foreach (DTOClient client in listClient)
                if (client.UserName.ToLower() == User.ToLower() && client.IPAdr == ipp[0])
                {
                    client.FlagSta = false;

                    //=======Mien Gang la truy xuat du lieu ===============
                    S_updateClient.WaitOne();
                    BUSClient.Update(client);
                    S_updateClient.Release();
                     //=======Mien Gang la truy xuat du lieu ===============

                    break;
                }
        }

        private void testLogin()
        {
            //Kiem tra xem co thread nao dang Wait Download ?
            //==============Mien Gang Hang doi T_downloadWait ===============================
            S_Login.WaitOne();
            for (int i = 0; i < numThread; i++)
            {
                if (T_downloadWait[i] != null)
                {
                    String myName = T_downloadWait[i].Name;
                    String[] myname = myName.ToString().Split(new char[] { '@' });
                    if (myname[0] == User)
                    {
                        T_downloadWait[i].Start();
                        T_downloadWait[i].Join();
                        Thread.Sleep(500);
                        T_downloadWait[i] = null;
                    }
                }
            }
            S_Login.Release();
            //==============Mien Gang===============================
            Thread.CurrentThread.Abort();
        }

        private void addDownload(String online, String myBool)
        {
            //=================Mien Gang truy xuat du lieu ====================================
            S_getClient.WaitOne();
            List<DTOClient> list = BUSClient.getListClient();
            S_getClient.Release();
            //=================Mien Gang truy xuat du lieu ====================================

            String[] share = new String[2];
            for (int j = 0; j < list.Count; j++)
                if (list[j].UserName == Arr[3])
                {
                    share[0] = list[j].IPAdr;
                    share[1] = list[j].NumPort.ToString();
                    break;
                }

            string[] ipp = (S_connectClient.RemoteEndPoint.ToString()).Split(new char[] { ':' });
            myServer.Q_dataDownload.Enqueue(User);
            myServer.Q_dataDownload.Enqueue(ipp[0]);
            myServer.Q_dataDownload.Enqueue(ipp[1]);
            myServer.Q_dataDownload.Enqueue(Arr[3]);
            myServer.Q_dataDownload.Enqueue(share[0]);
            myServer.Q_dataDownload.Enqueue(share[1]);
            myServer.Q_dataDownload.Enqueue(online);
            myServer.Q_dataDownload.Enqueue(Arr[1]);
            myServer.Q_dataDownload.Enqueue(Arr[2]);
            myServer.Q_dataDownload.Enqueue(myBool);
            myServer.flag = 1;
        }
    }
}