using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NumberPicturesSaver
{
    public partial class Form1 : Form
    {
        bool canDraw;
        Bitmap bmp = new Bitmap(100, 100);
        int[] count = new int[10];
        string path = @"F:\DigitDB\PictureSaverThin\";
        Color black = Color.FromArgb(255, 0, 0, 0);
        Color white = Color.FromArgb(255, 255, 255, 255);

        public Form1()
        {
            InitializeComponent();
            clear();
            pictureBox1.Image = bmp;
            using (StreamReader sr = new StreamReader(path+"count.txt"))
            {
                for (int i = 0; i < 10; i++)
                    count[i] = Convert.ToInt32(sr.ReadLine());                    
            }
            textBox1.Text = minID().ToString();
        }

        void clear()
        {
            bmp = new Bitmap(100, 100);
            for (int i = 0; i < 100; i++)
                for (int j = 0; j < 100; j++)
                    bmp.SetPixel(i, j, white);
            pictureBox1.Image = bmp;
        }

        int minID()
        {
            int min = count.Min();
            int id = count.ToList().IndexOf(min);
            return id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            int n = Convert.ToInt32(textBox1.Text);
            bmp.Save(path+textBox1.Text + count[n].ToString() + ".bmp");
            count[n]++;
            using(StreamWriter sw = new StreamWriter(path+"count.txt"))
            {
                for (int i = 0; i < 10; i++)
                {
                    sw.WriteLine(count[i].ToString());
                    listBox1.Items.Add(count[i].ToString());
                }
            }
            clear();
            n++;
            if (n == 10)
                n = 0;
            textBox1.Text = n.ToString();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                listBox1.Items.Clear();
                int n = Convert.ToInt32(textBox1.Text);
                bmp.Save(path + textBox1.Text + count[n].ToString() + ".bmp");
                count[n]++;
                using (StreamWriter sw = new StreamWriter(path + "count.txt"))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        sw.WriteLine(count[i].ToString());
                        listBox1.Items.Add(count[i].ToString());
                    }
                }
                clear();
                n++;
                if (n == 10)
                    n = 0;
                textBox1.Text = n.ToString();
            }
            else
            canDraw = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            canDraw = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canDraw)
            {
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (e.X + i > -1 && e.X + i < 100 && e.Y + j > -1 && e.Y + j < 100)
                            bmp.SetPixel(e.X + i, e.Y + j, black);
                pictureBox1.Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
