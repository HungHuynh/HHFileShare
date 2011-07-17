using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Client
{
    public partial class Login : Form
    {
        private Socket S_Client;
        private IPAddress IP;

        public delegate void delPassData(TextBox text);
        public Login()
        {
            InitializeComponent();
        }
        
        private void Login_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            try
            {
                foreach (IPAddress ip in Dns.GetHostByName(Dns.GetHostName()).AddressList)
                {
                    BoxCard.Items.Add(ip.ToString());
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (IP == null)
                {
                    IP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0];
                }
                if (textBox1.Text == "" || textBox2.Text == "")
                {

                    MessageBox.Show("Username or Password incorrect !", "MessageBox Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

                    return;
                }
                if (textBox5.Text == "" && checkBox2.Checked == false)
                {

                    MessageBox.Show("NumPort download incorrect !", "MessageBox Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

                    return;
                }
                if ((textBox3.Text == "" || textBox4.Text == "") && checkBox1.Checked == false)
                {

                    MessageBox.Show("IP Address incorrect !", "MessageBox Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

                    return;
                }
                //Input data IP and Port :
                String szPortDownload = textBox5.Text;
                if (checkBox2.Checked == true)
                {
                    szPortDownload = "9090";
                }
                else
                {
                    if (System.Convert.ToInt32(textBox5.Text, 10) < 65000)
                        szPortDownload = textBox5.Text;
                    else
                    {
                        MessageBox.Show("Port of Client between 0 and 65430");
                        return;
                    }
                }
                String szIPSelected = textBox3.Text;
                String szPort = textBox4.Text;
                if (checkBox1.Checked == true)
                {
                    szIPSelected = IP.ToString() ;
                    szPort = "8080";
                }
                int alPort = System.Convert.ToInt16(szPort, 10);

                //Create Socket :
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(szIPSelected), Convert.ToInt32(szPort));
                S_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //Connect :
                S_Client.Connect(ipep);
                
                //Send information from Client :
                string MyLogin = "Login@" + textBox1.Text.Trim() + "@" + textBox2.Text + "@" + szPortDownload+"@"+IP.ToString()+"@null";
                byte[] byMyLogin = System.Text.Encoding.ASCII.GetBytes(MyLogin);
                this.S_Client.Send(byMyLogin);

                //doi ket qua tu server :
                S_Client.ReceiveTimeout = 5000;
                byte[] login = new byte[64];
                int i_login = this.S_Client.Receive(login);
                char[] chars_login = new char[i_login];
                System.Text.Decoder d_login = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d_login.GetChars(login, 0, i_login, chars_login, 0);
                System.String S_login = new System.String(chars_login);

                if (S_login == "Login")
                {
                    button1.Enabled = false;
                    this.Hide();

                    //Close va deface giao dien tai day :
                    Download myDownload = new Download(S_Client, textBox1.Text);
                    delPassData del = new delPassData(myDownload.funData);
                    del(this.textBox5);
                    myDownload.Text = textBox1.Text;
                    myDownload.IP = IP;
                    myDownload.ShowDialog();

                    this.Close();
                }
                else
                    MessageBox.Show("Error Login! Please try again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                button1.Enabled = false;
            else button1.Enabled = true;
        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                checkBox2.Checked = false;
            }
            else
            {
                checkBox2.Checked = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                checkBox1.Checked = false;
            }
            else
            {
                checkBox1.Checked = true;
            }
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration myRegister = new Registration(S_Client);
            myRegister.ShowDialog();
            textBox1.Text = myRegister.user;
            textBox2.Text = myRegister.pass;
        }

        private void linkLabel1_TabIndexChanged(object sender, EventArgs e)
        {
            Registration myRegister = new Registration(S_Client);
            myRegister.ShowDialog();
        }

        private void BoxCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                IP= IPAddress.Parse(BoxCard.SelectedItem.ToString());
            }
            catch (InvalidCastException)
            {
            }
        }

   }
}
