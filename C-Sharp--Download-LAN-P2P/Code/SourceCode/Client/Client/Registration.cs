using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Client
{
    public partial class Registration : Form
    {
        private Socket S_register;
        public String user;
        public String pass;

        public Registration(Socket login)
        {
            this.S_register = login;
            InitializeComponent();
            textBox4.Text = "127.0.0.1";
            textBox5.Text = "8080";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                {
                    button1.Enabled = false;
                    MessageBox.Show("Username or Password incorrect!", "MessageBox Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    goto exit;
                }
                if (string.Compare(textBox2.Text, textBox3.Text) != 0)
                {
                    button1.Enabled = false;
                    MessageBox.Show("Confirm Password incorrect!", "MessageBox Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    goto exit;
                }

                String szIPSelected = textBox4.Text;
                String szPort = textBox5.Text;
                int alPort = System.Convert.ToInt16(szPort, 10);

                //Create Socket :
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(szIPSelected), Convert.ToInt16(szPort));
                this.S_register = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                
                //Connect :
                this.S_register.Connect(ipep);

                //Send informaton for Server :
                string s_register = "Register@" + textBox1.Text + "@" + textBox2.Text + "@" + textBox3.Text;
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(s_register);
                this.S_register.Send(byData);

                //Receive information from Server :
                byte[] register = new byte[1024];
                int i_register = this.S_register.Receive(register);
                char[] chars_register = new char[i_register];
                System.Text.Decoder d_register = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d_register.GetChars(register, 0, i_register, chars_register, 0);
                System.String S_register = new System.String(chars_register);
                
                //============================================================================
                if (S_register == "Accept")
                {
                    user = textBox1.Text;
                    pass = textBox2.Text;
                    MessageBox.Show("Register succeed !", "Announcement", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(S_register, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            exit:
                button1.Enabled = true;
            }
            catch (System.Net.Sockets.SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login li = new Login();
            this.Close();
            li.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                button1.Enabled = false;
            else button1.Enabled = true;
        }

        private void linkLabel1_TabIndexChanged(object sender, EventArgs e)
        {
            Login li = new Login();
            this.Close();
            li.Show();
        }
    }
}
