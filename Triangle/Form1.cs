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

namespace Triangle
{
    public partial class Form1 : Form
    {
        public bool rightmouse;
        public int numpoint;
        public int dst;

        public int goctoado_x, goctoado_y;
        
        public int x, y;
        public int mx, my;
        public int x0, y0;
        public int h, w;

        public struct poi
        {
            public int A, B;
        }

        public poi[] a = new poi[4];

        public Form1()
        {
            InitializeComponent();
            this.MouseWheel += pictureBox1_MouseWheel;
            x = 0;
            x0 = -1;
            y = 0;
            y0 = -1;
            goctoado_x = 0;
            goctoado_y = 0;
            dst = 20;
            numpoint = 0;
            rightmouse = false;
            pictureBox1.Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 1);
            Pen trucxypen = new Pen(Color.Black, 5);

            //ve khung
           
            e.Graphics.DrawLine(pen, 0, 0, w, 0);
            e.Graphics.DrawLine(pen, w, 0, w, h);
            e.Graphics.DrawLine(pen, w, h, 0, h);
            e.Graphics.DrawLine(pen, 0, h, 0, 0);

            //ve cot
            for (int i = 0; i < w; i = i + 1)
            {
                if ((i - goctoado_x) % dst == 0) e.Graphics.DrawLine(pen, i, 0, i, h);
                if ((i - goctoado_x) == 0) e.Graphics.DrawLine(trucxypen, i, 0, i, h);
            }

            //ve dong
            for (int i = 0; i < h; i = i + 1)
            {
                if ((i - goctoado_y) % dst == 0) e.Graphics.DrawLine(pen, 0, i, w, i);
                if ((i - goctoado_y) == 0) e.Graphics.DrawLine(trucxypen, 0, i, w, i);
            }



            Pen rpen = new Pen(Color.Red, 3);
            SolidBrush rbrush = new SolidBrush(Color.Red);

            SolidBrush abrush = new SolidBrush(Color.DarkOrange);
            SolidBrush bbrush = new SolidBrush(Color.DodgerBlue);
            SolidBrush cbrush = new SolidBrush(Color.DarkViolet);

            //Locate Mouse
            e.Graphics.FillEllipse(rbrush, goctoado_x - mx * dst - 5, goctoado_y - my * dst - 5, 10, 10);
            label10.Text = (-mx).ToString();
            label11.Text = my.ToString();

            //ve cac diem cua tam giac

            int xa=0, ya=0, xb=0, yb=0, xc=0, yc=0;

            for (int i = 1; i <= numpoint; i++)
            {
                Font k = new Font("Arial", 20);
                int dx = goctoado_x - a[i].A * dst;
                int dy = goctoado_y - a[i].B * dst;
                
                if (i > 1) e.Graphics.DrawLine(rpen, dx, dy, goctoado_x - a[i-1].A*dst, goctoado_y - a[i-1].B*dst);

                //dx và dy phai -5 de point ko bi lech

                if (i==1)
                {
                    xa = dx-5; ya = dy-5;
                    e.Graphics.FillEllipse(abrush, dx -5 , dy-5 , 10, 10);
                    e.Graphics.DrawString("A", k, abrush, dx, dy);
                }

                if (i == 2)
                {
                    xb = dx-5; yb = dy-5;
                    e.Graphics.FillEllipse(bbrush, dx - 5, dy - 5, 10, 10);
                    e.Graphics.DrawString("B", k, bbrush, dx, dy);
                }
                if (i == 3)
                {
                    xc = dx-5; yc = dy-5;
                    e.Graphics.FillEllipse(cbrush, dx - 5, dy - 5, 10, 10);
                    e.Graphics.DrawString("C", k, cbrush, dx, dy);
                }

                /*//ve diem A
                bool traitren = false,
                     traiduoi = false,
                     phaitren = false,
                     phaiduoi = false;
                int diema=0, diemb=0, diemc=0;

                if (xb > xa)
                {
                    if (yb > ya) phaitren = true;
                    else if (yb <= ya) phaiduoi = true;
                }
                else if (xb <= xa)
                {
                    if (yb > ya) traitren = true;
                    else if (yb <= ya) traiduoi = true;
                }

                if (xc > xa)
                {
                    if (yc > ya) phaitren = true;
                    else if (yc <= ya) phaiduoi = true;
                }
                else if (xc <= xa)
                {
                    if (yc > ya) traitren = true;
                    else if (yc <= ya) traiduoi = true;
                }

                if (traiduoi == true && phaiduoi == true) diema = 1; 
                else if (traitren == true && phaitren == true) diema = 2; 
                else if (phaitren == true && phaiduoi == true) diema = 3; 
                else if (traitren == true && traiduoi == true) diema = 4; 
                else if (phaitren == true && traiduoi == true) diema = 5; 
                else if (phaiduoi == true && traitren == true) diema = 6;
                else if (phaiduoi==true)

                //ve diem B

                ìf(xa > xb)
                {
                    if (ya > yb) phaitren = true;
                    else if (ya <= yb) phaiduoi = true;
                }
                else if (xa <= xb)
                {
                    if (ya > yb) traitren = true;
                    else if (ya <= yb) traiduoi = true;
                }

                if (xc > xb)
                {
                    if (yc > yb) phaitren = true;
                    else if (yc <= yb) phaiduoi = true;
                }
                else if (xc <= xb)
                {
                    if (yc > yb) traitren = true;
                    else if (yc <= yb) traiduoi = true;
                }

                if (traiduoi == true && phaiduoi == true) diemb = 1;
                else if (traitren == true && phaitren == true) diemb = 2;
                else if (phaitren == true && phaiduoi == true) diemb = 3;
                else if (traitren == true && traiduoi == true) diemb = 4;
                else if (phaitren == true && traiduoi == true) diemb = 5;
                else if (phaiduoi == true && traitren == true) diemb = 6;





                //ve
                if (numpoint ==3)
                {
                    if (diema == 1)
                    {
                        e.Graphics.DrawString("A", k, abrush, xa, ya + 10);
                    }
                    else if (diema == 2)
                    {
                        e.Graphics.DrawString("A", k, abrush, xa, ya - 10);
                    }
                    else if (diema == 3)
                    {
                        e.Graphics.DrawString("A", k, abrush, xa - 10, ya);
                    }
                    else if (diema == 4)
                    {
                        e.Graphics.DrawString("A", k, abrush, xa + 10, ya);
                    }
                    else if (diema == 5)
                    {
                        e.Graphics.DrawString("A", k, abrush, xa + 10, ya - 10);
                    }
                    else if (diema == 6)
                    {
                        e.Graphics.DrawString("A", k, abrush, xa - 10, ya + 10);
                    }

                }*/
                

            }

            //Draw Triangle Segment
            if (numpoint == 1)
            {
                e.Graphics.DrawLine(rpen, goctoado_x - a[numpoint].A*dst, goctoado_y - a[numpoint].B*dst, goctoado_x-mx*dst, goctoado_y-my*dst);
            }
            if (numpoint == 2)
            {
                e.Graphics.DrawLine(rpen, goctoado_x - a[numpoint].A * dst, goctoado_y - a[numpoint].B * dst, goctoado_x - mx * dst, goctoado_y - my * dst);
                e.Graphics.DrawLine(rpen, goctoado_x - a[numpoint-1].A * dst, goctoado_y - a[numpoint-1].B * dst, goctoado_x - mx * dst, goctoado_y - my * dst);
            }
            if (numpoint == 3)
            {
                e.Graphics.DrawLine(rpen, goctoado_x - a[numpoint].A*dst, goctoado_y - a[numpoint].B*dst, goctoado_x - a[1].A*dst, goctoado_y - a[1].B*dst);
            }
        }

        double d1, d2, d3;

        public void tinhthongso()
        {
            //chu vi
            d1 = Math.Sqrt((a[1].A - a[2].A) * (a[1].A - a[2].A) + (a[1].B - a[2].B) * (a[1].B - a[2].B));
            d2 = Math.Sqrt((a[1].A - a[3].A) * (a[1].A - a[3].A) + (a[1].B - a[3].B) * (a[1].B - a[3].B));
            d3 = Math.Sqrt((a[3].A - a[2].A) * (a[3].A - a[2].A) + (a[3].B - a[2].B) * (a[3].B - a[2].B));
            double p = d1 + d2 + d3;
            label9.Text = Math.Round(p, 3).ToString();

            //dien tich
            p /= 2;
            double s = Math.Sqrt(p * (p - d1) * (p - d2) * (p - d3));
            label7.Text = Math.Round(s, 3).ToString();

        }

        public void phanloaitamgiac()
        {
            double c1 = d2 * d2 + d3 * d3 - d1 * d1;
            c1 = c1 / (2 * d2 * d3);
            c1 = Math.Round(c1, 10);

            double c2 = d1 * d1 + d3 * d3 - d2 * d2;
            c2 = c2 / (2 * d1 * d3);
            c2 = Math.Round(c2, 10);

            double c3 = d1 * d1 + d2 * d2 - d3 * d3;
            c3 = c3 / (2 * d1 * d2);
            c3 = Math.Round(c3, 10);

            double k = Math.Min(c1, Math.Min(c2, c3));
            double z1 = c1 - c2;
            double z2 = c1 - c3;
            double z3 = c2 - c3;

            if (c1 == -1 || c2 == -1 || c3 == -1 || d1 == 0 || d2 == 0 || d3 == 0)
            {
                label8.Text = "3 điểm thẳng hàng";
            }
            else
            {
                if (k == 0)
                {
                    label8.Text = "Tam giác vuông";
                    if (z1 == 0 || z2 == 0 || z3 == 0) label8.Text = label8.Text + " cân";
                }
                else
                {
                    if (k < 0)
                    {
                        label8.Text = "Tam giác tù";
                        if (z1 == 0 || z2 == 0 || z3 == 0) label8.Text = label8.Text + " cân";
                    }
                    else
                    {
                        if (z1 == 0 || z2 == 0 || z3 == 0) label8.Text = "Tam giác cân";
                        else
                            label8.Text = "Tam giác nhọn";
                    }
                }
            }
        }

        public void lamtron()
        {
            if (a[numpoint].A % dst < dst / 2)
            {
                a[numpoint].A = a[numpoint].A - a[numpoint].A % dst;
            }
            else
            {
                a[numpoint].A = a[numpoint].A + (dst - a[numpoint].A % dst);
            }
            if (a[numpoint].B % dst < dst / 2)
            {
                a[numpoint].B = a[numpoint].B - a[numpoint].B % dst;
            }
            else
            {
                a[numpoint].B = a[numpoint].B + (dst - a[numpoint].B % dst);
            }
            a[numpoint].A /= dst;
            a[numpoint].B /= dst;

        }
        public void xuattoado()
        {
            if (numpoint == 1)
            {
                label1.Text = (-a[numpoint].A).ToString();
                label2.Text = (a[numpoint].B).ToString();
            }
            else if (numpoint == 2)
            {
                label3.Text = (-a[numpoint].A).ToString();
                label4.Text = (a[numpoint].B).ToString();

            }
            else if (numpoint == 3)
            {
                label5.Text = (-a[numpoint].A).ToString();
                label6.Text = (a[numpoint].B).ToString();
            }

        }
        public void tinhchat()
        {
            tinhthongso();
            phanloaitamgiac();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                numpoint++;
                if (numpoint > 3)
                {
                    numpoint = 1;
                    label3.Text = "xb";
                    label4.Text = "yb";
                    label5.Text = "xc";
                    label6.Text = "yc";
                    label7.Text = "000";
                    label9.Text = "000";
                }
                a[numpoint].A =  goctoado_x - e.X;
                a[numpoint].B =  goctoado_y - e.Y;


                lamtron();
                xuattoado();
                if (numpoint == 3) tinhchat();     

            }
            if (e.Button == MouseButtons.Right)
            {
                x0 = e.X;
                y0 = e.Y;
                rightmouse = true;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            rightmouse = false;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            numpoint = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            h = pictureBox1.Height - 1;
            w = pictureBox1.Width - 1;

            goctoado_x = w / 2;
            goctoado_y = h / 2;
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                int temp = dst;
                if (dst >= 10) dst = dst * 110 / 100;
                else dst++;

                if ((dst * 100) / 20 > 500) dst = 100; //gioi han zoom

                int kx = dst * mx - temp * mx;
                int ky = dst * my - temp * my;
                goctoado_x = goctoado_x + kx;
                goctoado_y = goctoado_y + ky;

            }
            else
            {
                int temp = dst;
                if (dst > 3 ) dst = dst *100/110;

                if ((dst * 100) / 20 < 50) dst = 10; //gioi han zoom

                int kx = dst * mx - temp * mx;
                int ky = dst * my - temp * my;
                goctoado_x = goctoado_x + kx;
                goctoado_y = goctoado_y + ky;
            }

            int dozoom = (dst * 100) / 20;
            label21.Text = dozoom.ToString() + "%";
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            mx = e.X;
            my = e.Y;

            if (rightmouse == true)
            {
                goctoado_x = goctoado_x + mx - x0;
                goctoado_y = goctoado_y + my - y0;
                x0 = mx;
                y0 = my;
            }

            mx = goctoado_x - mx;
            my = goctoado_y - my;
            if (mx % dst < dst / 2)
            {
                mx = mx - mx % dst;
            }
            else
            {
                mx = mx + (dst - mx % dst);
            }
            if (my % dst < dst / 2)
            {
                my = my - my % dst;
            }
            else
            {
                my = my + (dst - my % dst);
            }
            mx /= dst;
            my /= dst;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label13.Visible = false;
                label14.Visible = false;
                label12.Visible = false;

            }
            else 
            {
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                label12.Visible = true;
            }
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == false)
            {
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                label15.Visible = false;
                label16.Visible = false;
                label17.Visible = false;

            }
            else
            {
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label15.Visible = true;
                label16.Visible = true;
                label17.Visible = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            x = 0;
            x0 = -1;
            y = 0;
            y0 = -1;
            goctoado_x = w/2;
            goctoado_y = h / 2;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Visible = true ;
        }
    }
}



