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
    public partial class server : System.Windows.Forms.Form
    {
        private Socket S_sockListen;
        private Socket S_sockWork;
        private Thread S_threadListen;

        //Quan ly so luong cac Thread cua Client Connect den :
        private List<Thread> L_clientConnect = new List<Thread>();
        private int i_index;

        //Quan ly du lieu cac file downlaod offline :
        private bool b_threadListen;
        public String[][] DataWait = new String[10][];
        public int flag = 0;

        //Du lieu de add vao giao dien DataGridView Download :
        public Queue<String> Q_dataDownload = new Queue<String>();

        public server()
        {
            InitializeComponent();
            label1.Enabled = false;
            b_threadListen = true;
            for (int i = 0; i < 10; i++)
            {
                DataWait[i] = new String[4];
            }
        }

        private void server_Load(object sender, EventArgs e)
        {
            getListClient();
            getListData();
            BoxPort.Text = "8080";
            checkBox1.Checked = true;
            Butstop.Enabled = false;
        }

        private void getListClient()
        {
            dtgv.Rows.Clear();

            List<DTOClient> listClient = BUSClient.getListClient();
            for (int i = 0; i < listClient.Count; i++)
            {
                dtgv.Rows.Add(listClient[i].UserName, listClient[i].PassWord, listClient[i].IPAdr, listClient[i].NumPort, listClient[i].FlagSta);
                dtgv.Rows[dtgv.RowCount - 1].Tag = listClient[i];
            }
        }

        private void getListData()
        {
            dtgvFile.Rows.Clear();

            List<DTOData> listData = BUSData.getListData();
            List<DTOClient> listClient = BUSClient.getListClient();
            foreach (DTOData data in listData)
            {
                foreach (DTOClient cli in listClient)
                    if (cli.IDClient == data.IDClient)
                        if (cli.FlagSta == true)
                            dtgvFile.Rows.Add(data.FileName, data.Size, cli.UserName, cli.IPAdr, cli.NumPort, "Yes", data.FlagSta);
                        else
                            dtgvFile.Rows.Add(data.FileName, data.Size, cli.UserName, cli.IPAdr, cli.NumPort, "No", data.FlagSta);
            }
        }

        private void notifyIcon1_DoubleClick(object Sender, EventArgs e)
        {
            // Set the WindowState to normal if the form is minimized.
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;

            // Activate the form.
            this.Activate();
        }

        private void menuItem1_Click(object Sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ButListen.Enabled = false;
            ButQuit.Enabled = false;
            Butstop.Enabled = true;
            BoxPort.Enabled = false;
            if (BoxPort.Text == "" && checkBox1.Checked == false)
            {
                MessageBox.Show("Please input NumPort !", "MessageBox Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                ButListen.Enabled = true;
                return;
            }

            timer1.Enabled = true;
            String Port = BoxPort.Text;
            if (Port == "" && checkBox1.Checked == false)
            {
                MessageBox.Show("Please input NumPort !", "MessageBox Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                ButListen.Enabled = true;
                return;
            }

            //Kiem tra xem threa lang nghe co bi Pause hay ko ?
            if (S_threadListen != null)
            {
                if (S_threadListen.ThreadState == ThreadState.SuspendRequested || S_threadListen.ThreadState == ThreadState.Suspended)
                {
                    S_threadListen.Resume();
                    return;
                }
            }
            else
            {
                List<DTOClient> list = BUSClient.getListClient();
                foreach (DTOClient client in list)
                    if (client.FlagSta == true)
                    {
                        client.FlagSta = false;
                        BUSClient.Update(client);
                    }
            }
            //Tao Thread lang nghe de lang nghe cac ke noi den :
            S_threadListen = new Thread(new ThreadStart(serverListen));
            S_threadListen.Start();
        }

        private void serverListen()
        {
            try
            {
                String Port = BoxPort.Text;
                if (checkBox1.Checked == true)
                {
                    Port = "8080";
                }
                int C_Port = System.Convert.ToInt32(Port);
                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, C_Port);
                S_sockListen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                S_sockListen.Bind(ipep);
                S_sockListen.Listen(10);
                i_index = 0;
                while (b_threadListen)
                {
                    //Listening connection ...
                    S_sockWork = S_sockListen.Accept();

                    //Create a Thread to process :
                    Thread S_threadWork = new Thread(new ThreadStart(acceptClient));
                    S_threadWork.Start();

                    //Add Thread vao List de quan ly :
                    if (i_index == 0)
                    {
                        S_threadWork.Name = "Client" + i_index.ToString();
                        L_clientConnect.Add(S_threadWork);
                    }
                    else
                    {

                        for (int i = 0; i < L_clientConnect.Count; i++)
                        {
                            if (L_clientConnect[i + 1] == null)
                            {
                                S_threadWork.Name = "Client" + i.ToString();
                                L_clientConnect.Add(S_threadWork);
                                i_index++;
                            }
                        }
                    }
                }
                Thread.CurrentThread.Abort();
            }
            catch (SocketException)
            {
                Thread.CurrentThread.Abort();
            }
        }

        private void acceptClient()
        {
            try
            {
                String nameThread = "Client" + i_index.ToString();

                new ThreadClient(S_sockWork, this);
                for (int i = 0; i < L_clientConnect.Count; i++)
                {
                    if (L_clientConnect[i] != null)
                    {
                        if (L_clientConnect[i].Name == nameThread)
                            L_clientConnect[i] = null;
                    }
                }
                Thread.CurrentThread.Abort();
            }
            catch (AbandonedMutexException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //cap nhat lai danh sach cac client offline :
                timer1.Enabled = false;
                //xem co client dang ket noi ko ?
                for (int i = 0; i < L_clientConnect.Count; i++)
                {
                    if (L_clientConnect[i] != null)
                    {
                        if (L_clientConnect[i].IsAlive == true)
                        {
                            MessageBox.Show("You can't exit Server Because Server had some Client connect ! \n Please waiting ...");
                            return;
                        }
                    }
                }

                if (S_threadListen != null && S_threadListen.IsAlive)
                {
                    if (S_threadListen.ThreadState == ThreadState.SuspendRequested || S_threadListen.ThreadState == ThreadState.Suspended)
                        S_threadListen.Resume();

                    b_threadListen = false;
                    S_sockListen.Close();
                    b_threadListen = false;
                    while (S_threadListen.IsAlive)
                    {
                        S_threadListen.Abort();
                        S_threadListen.Join(100);
                    }
                }
                Close();
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void server_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timer1.Enabled != false)
            {
                MessageBox.Show("You had quit Server when Server listening connection ! Do you really want to disconnect Client ? \n \t\t\t\t\t @_@ ", "Quit Server !");
                timer1.Enabled = false;
                for (int i = 0; i < L_clientConnect.Count; i++)
                {
                    if (L_clientConnect[i] != null)
                    {
                        if (L_clientConnect[i].IsAlive == true)
                        {
                            L_clientConnect[i].Abort();
                            L_clientConnect[i].Interrupt();
                            MessageBox.Show(" Please Waiting ưhile Client send data to disconnect !");
                            while (L_clientConnect[i].IsAlive) ;
                        }
                    }
                }
                if (S_threadListen != null && S_threadListen.IsAlive)
                {
                    if (S_threadListen.ThreadState == ThreadState.SuspendRequested || S_threadListen.ThreadState == ThreadState.Suspended)
                        S_threadListen.Resume();

                    b_threadListen = false;
                    S_sockListen.Close();
                    while (S_threadListen.IsAlive)
                    {
                        S_threadListen.Abort();
                        S_threadListen.Join(100);
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            getListClient();
            getListData();
            if (flag == 1)
                addDataDownload();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (BoxPort.Text != "")
                checkBox1.Checked = false;
            else
                checkBox1.Checked = true;
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void endToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (BoxPort.Text == "8080")
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;
        }
        public void addDataDownload()
        {
            try
            {
                if (Q_dataDownload.Count > 0)
                {
                    String[] Mydata = new String[10];
                    for (int i = 0; i < 10; i++)
                    {
                        Mydata[i] = Q_dataDownload.Dequeue();
                    }
                    dataDownload.Rows.Add(Mydata[0], Mydata[1], Mydata[2], Mydata[3], Mydata[4], Mydata[5], Mydata[6], Mydata[7], Mydata[8], Convert.ToBoolean(Mydata[9]));
                }

                flag = 0;
            }
            catch (DataException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ButClear_Click(object sender, EventArgs e)
        {
            dataDownload.Rows.Clear();
        }

        private void Butstop_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to stop Server ? \nClient still trade data with Server when Server stop listenning.", "Stop Server", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Butstop.Enabled = false;
                BoxPort.Enabled = true;
                ButListen.Enabled = true;
                if (S_threadListen.IsAlive == true)
                    S_threadListen.Suspend();
                ButQuit.Enabled = true;
            }
        }

        private void server_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                notifyIcon1.Visible = true;
        }

    }
}
