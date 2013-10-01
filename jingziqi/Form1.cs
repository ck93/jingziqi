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
            comboBox1.SelectedIndex = 1;
        }
        int[,] array = new int[3,3]{{0,0,0},{0,0,0},{0,0,0}};
        private enum mycolor{black, white }
        mycolor chess;
        bool first = true;
        bool ready = false;
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
        private int search()
        {
            int best = 0;
            if (step == 9)
            {
                for (int i = 0; i < 3; i++)
                     for (int j = 0; j < 3; j++)
                         if (array[i, j] == 0)
                         {
                             best = 10 * i + j;
                             return best;
                         }
            }
            int temp = -9;
            int[,] site = new int[3, 3] { { -9, -9, -9 }, { -9, -9, -9 }, { -9, -9, -9 } };
            if (chess == mycolor.black)
            {
                 for (int i = 0; i < 3; i++)
                     for (int j = 0; j < 3; j++)
                     {
                         if (array[i, j] == 0)
                         {
                             array[i, j] = -1;
                             if (numofpath(array, 1) > 0)
                                 return (10 * i + j);
                             for (int m = 0; m < 3; m++)
                                 for (int n = 0; n < 3; n++)
                                     if (array[m, n] == 0)
                                     {
                                         array[m, n] = 1;
                                         if (danger(array, 1))
                                         {
                                             site[i, j] = -10;
                                         }
                                         if (site[i, j] == -9 || value(mycolor.black) < site[i, j])
                                             site[i, j] = value(mycolor.black);                                        
                                         array[m, n] = 0;
                                     }
                             array[i, j] = 0;
                         }                         
                     }
                for (int i = 0; i < 3; i++)
                     for (int j = 0; j < 3; j++)
                         if (site[i, j] > temp)
                         {
                             temp = site[i, j];
                             best = 10 * i + j;
                         }
                return best;
            }
            return best;
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!ready)
                return;
            int x = e.X;
            int y = e.Y;
            Graphics g = pictureBox1.CreateGraphics();
            Bitmap black = new Bitmap(@"D:\Documents\GitHub\jingziqi\jingziqi\黑子.png");
            Bitmap white = new Bitmap(@"D:\Documents\GitHub\jingziqi\jingziqi\白子.png");
            if (array[x / 80, y / 80] == 0)
            {
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
            }
            else
            {
                MessageBox.Show("此处不能放置棋子！");
                return;
            }
            if (victory() == 1)
            {
                ready = false;
                Form2 f = new Form2(this);
                f.label1.Text = "黑子赢！是否重新开局？";
                f.ShowDialog(); 
            }
            else if (victory() == -1)
            {
                ready = false;
                Form2 f = new Form2(this);
                f.label1.Text = "白子赢！是否重新开局？";
                f.ShowDialog();
            }
            else if (step == 10)
            {
                ready = false;
                Form2 f = new Form2(this);
                f.label1.Text = "和棋！是否重新开局？";
                f.ShowDialog();
            }
            else
            {
                if (comboBox1.SelectedIndex == 1)
                {
                    int result = search();
                    int xx = result / 10;
                    int yy = result % 10;
                    g.DrawImage(white, xx * 80 + 10, yy * 80 + 10, 60, 60);
                    array[xx, yy] = -1;
                    step++;
                }
                if (victory() == 1)
                {
                    ready = false;
                    Form2 f = new Form2(this);
                    f.label1.Text = "黑子赢！是否重新开局？";
                    f.ShowDialog();
                }
                else if (victory() == -1)
                {
                    ready = false;
                    Form2 f = new Form2(this);
                    f.label1.Text = "白子赢！是否重新开局？";
                    f.ShowDialog();
                }
                else if (step == 10)
                {
                    ready = false;
                    Form2 f = new Form2(this);
                    f.label1.Text = "和棋！是否重新开局？";
                    f.ShowDialog();
                }
            }           
        }
        private int numofpath(int[,] array, int dest)
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                if (array[i, 0] == dest && array[i, 1] == dest && array[i, 2] == dest)
                    count++;
                if (array[0, i] == dest && array[1, i] == dest && array[2, i] == dest)
                    count++;
            }
            if (array[0, 0] == dest && array[1, 1] == dest && array[2, 2] == dest)
                count++;
            if (array[0, 2] == dest && array[1, 1] == dest && array[2, 0] == dest)
                count++;
            return count;
        }
        private bool danger(int[,] array, int dest)
        {
            if (array[2, 0] == 0 && array[0, 2] == 0)
                if (array[0, 0] == dest && array[1, 0] == dest && array[0, 1] == dest)
                    return true;
                else if (array[2, 2] == dest && array[1, 2] == dest && array[2, 1] == dest)
                    return true;
           if (array[0, 0] == 0 && array[2, 2] == 0)
               if (array[0, 2] == dest && array[0, 1] == dest && array[1, 2] == dest)
                   return true;
               else if (array[2, 0] == dest && array[1, 0] == dest && array[2, 1] == dest)
                   return true;
           return false;
        }
        private int value(mycolor chess)
        {
            int[,] copy1 = new int[3, 3];
            Array.Copy(array, copy1, 9);
            int[,] copy2 = new int[3, 3];
            Array.Copy(array, copy2, 9);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (array[i, j] == 0)
                    {
                        copy1[i, j] = 1;
                        copy2[i, j] = -1;
                    }
            if (chess == mycolor.black)
            {
                if (numofpath(array, 1) > 0)
                    return -10;
                return (numofpath(copy2, -1) - numofpath(copy1, 1));
            }
            else
            {
                if (numofpath(array, -1) > 0)
                    return -10;
                return (numofpath(copy1, 1) - numofpath(copy2, -1));
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            chess = mycolor.black;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            chess = mycolor.white;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    array[i,j] = 0;
            pictureBox1.Image = null;
            step = 1;
            ready = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            first = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            first = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1 && !first)
            {
                Graphics g = pictureBox1.CreateGraphics();
                Bitmap black = new Bitmap(@"D:\Documents\GitHub\jingziqi\jingziqi\黑子.png");
                Bitmap white = new Bitmap(@"D:\Documents\GitHub\jingziqi\jingziqi\白子.png");
                if (chess == mycolor.black)
                {
                    g.DrawImage(white, 90, 90, 60, 60);
                    array[1, 1] = -1;
                }
                else
                {
                    g.DrawImage(black, 90, 90, 60, 60);
                    array[1, 1] = 1;
                }
                step++;
            }
            ready = true;
        }
    }
}
