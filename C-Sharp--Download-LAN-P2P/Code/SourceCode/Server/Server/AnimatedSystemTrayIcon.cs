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

namespace Server
{
    public class AnimatedSystemTrayIcon : System.Windows.Forms.Form
    {
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.ComponentModel.IContainer components;

        public AnimatedSystemTrayIcon()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();

            // Initialize contextMenu1 :
            this.contextMenu1.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] { this.menuItem1 });

            // Initialize menuItem1
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "Exit";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);

            // Set up how the form should be displayed.
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Text = "My Server :";

            // Create the NotifyIcon.
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);

            // The Icon property sets the icon that will appear
            // in the systray for this application.
            notifyIcon1.Icon = new Icon("server.ico");

            // The ContextMenu property sets the menu that will
            // appear when the systray icon is right clicked.
            notifyIcon1.ContextMenu = this.contextMenu1;

            // The Text property sets the text that will be displayed,
            // in a tooltip, when the mouse hovers over the systray icon.
            notifyIcon1.Text = "Server";
            notifyIcon1.Visible = true;

            // Handle the DoubleClick event to activate the form.
            notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);

        }

        protected override void Dispose(bool disposing)
        {
            // Clean up any components being used.
            if (disposing)
                if (components != null)
                    components.Dispose();

            base.Dispose(disposing);
        }

        private void notifyIcon1_DoubleClick(object Sender, EventArgs e)
        {
            // Show the form when the user double clicks on the notify icon.
                        
            // Set the WindowState to normal if the form is minimized.
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;

            // Activate the form.
            this.Activate();
        }

        private void menuItem1_Click(object Sender, EventArgs e)
        {
            // Close the form, which closes the application.
            this.Close();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AnimatedSystemTrayIcon
            // 
            this.ClientSize = new System.Drawing.Size(292, 269);
            this.Name = "AnimatedSystemTrayIcon";
//            this.Load += new System.EventHandler(this.AnimatedSystemTrayIcon_Load);
            this.ResumeLayout(false);

        }
    }
}
