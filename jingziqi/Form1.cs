using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace jingziqi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        private enum mycolor{black, white }
        mycolor chess;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; //创建画板,这里的画板是由Form提供的.
            Pen blackpen = new Pen(Color.Black, 2);//定义了一个黑色,宽度为的画笔
            g.DrawRectangle(blackpen, 1, 1, 240, 240);//在画板上画矩形
            g.DrawLine(blackpen, 1, 80, 240, 80);
            g.DrawLine(blackpen, 1, 160, 240, 160);
            g.DrawLine(blackpen, 80, 1, 80, 240);
            g.DrawLine(blackpen, 160, 1, 160, 240);
        }
        private void placechess(mycolor chess, Bitmap bitmap, int x, int y, int size)
        {
            switch (chess)
            {
                case mycolor.black:
                    break;
                case mycolor.white:
                    break;
            }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            Graphics g = pictureBox1.CreateGraphics();
            Bitmap black = new Bitmap(@"D:\Documents\GitHub\jingziqi\jingziqi\黑子.png");
            Bitmap white = new Bitmap(@"D:\Documents\GitHub\jingziqi\jingziqi\白子.png");

            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            chess = mycolor.black;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            chess = mycolor.black;
        }
        /*private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; //创建画板,这里的画板是由Form提供的.
            Pen blackpen = new Pen(Color.Black, 2);//定义了一个黑色,宽度为的画笔
            g.DrawRectangle(blackpen, 59, 29, 246, 246);//在画板上画矩形,起始坐标为(10,10),宽为,高为
            g.DrawLine(blackpen, 59, 111, 305, 111);//在画板上画直线,起始坐标为(10,10),终点坐标为(100,100)
            g.DrawLine(blackpen, 59, 193, 305, 193);
            g.DrawLine(blackpen, 141, 29, 141, 275);
            g.DrawLine(blackpen, 223, 29, 223, 275);
        }*/

       
    


    }
}
