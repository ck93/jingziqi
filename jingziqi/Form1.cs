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
            comboBox1.SelectedIndex = 0;
        }
        int[,] array = new int[3,3]{{0,0,0},{0,0,0},{0,0,0}};
        private enum mycolor{black, white }
        mycolor chess;
        bool first = true;
        int step = 1;
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
        private int victory()
        {
            for (int i = 0; i < 3; i++)
            {
                if (array[i, 0] + array[i, 1] + array[i, 2] == 3)
                    return 1;
                else if (array[i, 0] + array[i, 1] + array[i, 2] == -3)
                    return -1;
                if (array[0, i] + array[1, i] + array[2, i] == 3)
                    return 1;
                else if (array[0, i] + array[1, i] + array[2, i] == -3)
                    return -1;
            }
            if (array[0, 0] + array[1, 1] + array[2, 2] == 3)
                return 1;
            else if (array[0, 0] + array[1, 1] + array[2, 2] == -3)
                return -1;
            if (array[0, 2] + array[1, 1] + array[2, 0] == 3)
                return 1;
            else if (array[0, 2] + array[1, 1] + array[2, 0] == -3)
                return -1;
            return 0;
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            Graphics g = pictureBox1.CreateGraphics();
            Bitmap black = new Bitmap(@"D:\Documents\GitHub\jingziqi\jingziqi\黑子.png");
            Bitmap white = new Bitmap(@"D:\Documents\GitHub\jingziqi\jingziqi\白子.png");
            if (chess == mycolor.black)
            {
                if (first && step % 2 == 1 || !first && step % 2 == 0)
                {
                    g.DrawImage(black, x / 80 * 80 + 10, y / 80 * 80 + 10, 60, 60);
                    array[x / 80, y / 80] = 1;
                }
                else
                {
                    g.DrawImage(white, x / 80 * 80 + 10, y / 80 * 80 + 10, 60, 60);
                    array[x / 80, y / 80] = -1;
                }
                step++;
            }
            else
            {
                if (first && step % 2 == 1 || !first && step % 2 == 0)
                {
                    g.DrawImage(white, x / 80 * 80 + 10, y / 80 * 80 + 10, 60, 60);
                    array[x / 80, y / 80] = -1;
                }
                else
                {
                    g.DrawImage(black, x / 80 * 80 + 10, y / 80 * 80 + 10, 60, 60);
                    array[x / 80, y / 80] = 1;
                }
                step++;
            }
            if (victory() == 1)
                MessageBox.Show("黑子赢！");
            if (victory() == -1)
                MessageBox.Show("白子赢！");
            if (step == 10)
                MessageBox.Show("和棋！");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            chess = mycolor.black;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            chess = mycolor.white;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    array[i,j] = 0;
            pictureBox1.Image = null;
            step = 0;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            first = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            first = false;
        }
    }
}
