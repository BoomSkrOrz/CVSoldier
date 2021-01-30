using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CVSoldier
{
    public partial class FormMsg : Form
    {
        public FormMsg(string msg)
        {
            InitializeComponent();
            richTextBox1.Text = msg;
            timer1.Interval = Timeout ;
            timer1.Start();
        }
        private int currentX;//横坐标 
        private int currentY;//纵坐标 
        private int screenHeight;//屏幕高度 
        private int screenWidth;//屏幕宽度
        private static int Timeout = 1500;
        private void FormMsg_Load(object sender, EventArgs e)
        {
            Rectangle rect = Screen.PrimaryScreen.WorkingArea;
            screenHeight = rect.Height;
            screenWidth = rect.Width;
            currentX = screenWidth - this.Width;
            currentY = screenHeight - this.Height;
            this.Location = new System.Drawing.Point(currentX, currentY);
            PromptInfo.AnimateWindow(this.Handle, 100, PromptInfo.AW_SLIDE | PromptInfo.AW_VER_NEGATIVE);
        }

        private void FormMsg_FormClosing(object sender, FormClosingEventArgs e)
        {
            //PromptInfo.AnimateWindow(this.Handle, Timeout - 100, PromptInfo.AW_SLIDE | PromptInfo.AW_VER_POSITIVE);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        public static void ShowMsg(string msg,int timeout=1500)
        {
            FormMsg.Timeout = timeout;
            FormMsg form = new FormMsg(msg);
            form.Show();
        }
    }
}
