using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Graphics
{
    public partial class Form1 : Form
    {
        private int x1, y1, x2, y2, r;
        private int x1_min, y1_min, x2_min, y2_min, r2;
        private int x1_hour, y1_hour, x2_hour, y2_hour, r3;
        private double a, a_min, a_hour;
        private int r_c = 8; //
        private int r_clock;

        private Pen pen = new Pen(Color.DarkRed, 2);
        private Pen pen_min = new Pen(Color.Purple, 4);
        private Pen pen_hour = new Pen(Color.Black, 6);
        private Pen pen_1min = new Pen(Color.Black, 3);
        private System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16);
        System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.DarkRed);

        



        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
            timer1.Start();
        }

        private void InitializeTimer()
        {
            timer1.Interval = 1000;
            timer1.Enabled = true;
            // Hook up timer's tick event handler.
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        }
        
        private void Clock_Draw(System.Drawing.Graphics g)
        {
            r_clock = r + 15;
            g.DrawEllipse(System.Drawing.Pens.Black, x1 - r_clock, y1 - r_clock, 2 * r_clock, 2 * r_clock);
            g.FillEllipse(brush, x1 - r_c, y1 - r_c, 2 * r_c, 2 * r_c);

            for (int i = 1; i <= 60; i++) {
                int x0, y0, x3, y3, x_dig, y_dig;
                double a0 = -Math.PI / 2 + (i * Math.PI / 30);
                x3 = x1 + (int)(r_clock * Math.Cos(a0));
                y3 = y1 + (int)(r_clock * Math.Sin(a0));
                if (i % 5 == 0)
                {
                    x0 = x1 + (int)((r_clock - 10) * Math.Cos(a0));
                    y0 = y1 + (int)((r_clock - 10) * Math.Sin(a0));
                    x_dig = x1 + (int)(r_clock * Math.Cos(a0));
                    y_dig = y1 - 20 + (int)(r_clock * Math.Sin(a0));

                    g.DrawLine(pen_hour, x0, y0, x3, y3);
                    g.DrawString("" + (i / 5), drawFont, drawBrush, x_dig, y_dig);
                }
                else 
                {
                    x0 = x1 + (int)((r_clock - 5) * Math.Cos(a0));
                    y0 = y1 + (int)((r_clock - 5) * Math.Sin(a0));
                    g.DrawLine(pen_1min, x0, y0, x3, y3);
                }               
 
            }

        }

        private void ClockHands_Draw(System.Drawing.Graphics g)
        {
            g.DrawLine(pen, x1, y1, x2, y2);
            g.DrawLine(pen_min, x1_min, y1_min, x2_min, y2_min);
            g.DrawLine(pen_hour, x1_hour, y1_hour, x2_hour, y2_hour);

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;           
            ClockHands_Draw(g);
            Clock_Draw(g);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime now_time = DateTime.Now;
            x1 = ClientSize.Width / 2;
            y1 = ClientSize.Height / 2;
            x1_min = x1; y1_min = y1;
            x1_hour = x1; y1_hour = y1;

            //задаем радиусы
            r = 150; 
            r2 = r;
            r3 = 100;

            //задаем начальное значение угла поворота в зависимости от текущего времени
            a = Math.PI / 2 - (now_time.Second * Math.PI / 30);    
            a_min = Math.PI / 2 - (now_time.Minute * Math.PI / 30) - (now_time.Second * Math.PI / (60 * 30));
            a_hour = Math.PI / 2 - (now_time.Hour * Math.PI / 6) - (now_time.Minute * Math.PI / (60 * 6));
            
            //определяем конец секундной стрелки с учетом центра экрана
            x2 = x1 + (int)(r * Math.Cos(a));
            y2 = y1 - (int)(r * Math.Sin(a));

            //определяем конец минутной стрелки с учетом центра экрана
            x2_min = x1_min + (int)(r2 * Math.Cos(a_min));
            y2_min = y1_min - (int)(r2 * Math.Sin(a_min));

            //определяем конец часовой стрелки с учетом центра экрана
            x2_hour = x1_hour + (int)(r3 * Math.Cos(a_hour));
            y2_hour = y1_hour - (int)(r3 * Math.Sin(a_hour));


        }

        private string toTimeString(int num)
        {
            return num > 9 ? num.ToString() : "0" + num;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now_time1 = DateTime.Now;
            string seconds = toTimeString(now_time1.Second);
            string minutes = toTimeString(now_time1.Minute);
            string hours = toTimeString(now_time1.Hour);
            string time = hours + ":" + minutes + ":" + seconds;
            label1.Text = time;

            a -= Math.PI / 60;//уменьшаем угол на 1 
            a_min -= Math.PI / (60 * 60);
            a_hour -= Math.PI / Math.Pow(60, 3);

            //int hour = (int)(a_hour * 6.0 / Math.PI);
            //label1.Text = hour + ":" + now_time1.Minute.ToString() + ":" + now_time1.Second.ToString();


            //определяем конец часовой стрелки с учетом центра экрана
            x2 = x1 + (int)(r * Math.Cos(a));
            y2 = y1 - (int)(r * Math.Sin(a));

            x2_min = x1_min + (int)(r2 * Math.Cos(a_min));
            y2_min = y1_min - (int)(r2 * Math.Sin(a_min));

            x2_hour = x1_hour + (int)(r3 * Math.Cos(a_hour));
            y2_hour = y1_hour - (int)(r3 * Math.Sin(a_hour));
            Invalidate(); //вынудительный вызов перерисовки (Paint)
        }

        /*private void label1_Click(object sender, EventArgs e)
        {
            DateTime now_time1 = DateTime.Now;
            label1.Text = now_time1.Hour.ToString() + ":" + now_time1.Minute.ToString() + ":" + now_time1.Minute.ToString();
            now_time1.Ticks
        }*/
    }
}

/*
    private void InitializeTimer()
        {
// Run this procedure in an appropriate event.
          counter = 0;
          timer1.Interval = 1600;
          timer1.Enabled = true;
// Hook up timer's tick event handler.
          this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
         }
    private void timer1_Tick(object sender, System.EventArgs e)
    {
        if (counter >= 10)
        {
            // Exit loop code.
            timer1.Enabled = false;
            counter = 0;
        }
        else
        {
            // Run your procedure here.
            // Increment counter.
            counter = counter + 1;
            label1.Text = "Procedures Run: " + counter.ToString();
        }
    }
*/

/* private void button1_Click(object sender, EventArgs e)
 {
     System.Drawing.Pen myPen;

     myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
     System.Drawing.Graphics formGraphics = this.CreateGraphics();
     formGraphics.DrawLine(myPen, 0, 0, 200, 200);
     myPen.Dispose();
     formGraphics.Dispose();
 }

 private void button2_Click(object sender, EventArgs e)
 {
     System.Drawing.Graphics formGraphics = this.CreateGraphics();
     string drawString = "Визуальное программирование";
     System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16);
     System.Drawing.SolidBrush drawBrush = new
     System.Drawing.SolidBrush(System.Drawing.Color.DarkRed);
     float x = 50.0f;
     float y = 50.0f;
     formGraphics.DrawString(drawString, drawFont, drawBrush, x, y);
     drawFont.Dispose();
     drawBrush.Dispose();
     formGraphics.Dispose();   

 }

 private void button3_Click(object sender, EventArgs e)
 {
     //Эллипс и прямоугольник
     System.Drawing.Graphics graphics = this.CreateGraphics();
     System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(
         10, 10, 110, 150);
     graphics.DrawEllipse(System.Drawing.Pens.Black, rectangle);
     graphics.DrawRectangle(System.Drawing.Pens.Red, rectangle);            
     graphics.Dispose();

 }*/
