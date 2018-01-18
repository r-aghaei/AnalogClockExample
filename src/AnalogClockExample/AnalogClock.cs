using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalogClockExample
{
    public class AnalogClock:Control
    {
        Timer timer;
        public AnalogClock()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
                this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            var r1 = this.ClientRectangle;
            r1.Inflate(-3, -3);
            g.DrawEllipse(Pens.Black, r1);
            var r2 = r1;
            r2.Inflate(-5, -5);
            TextRenderer.DrawText(g, DateTime.Now.ToString("HH:mm:ss"), this.Font,
                new Rectangle(r1.Left, r1.Top + 2 * r1.Height / 3, r1.Width, r1.Height / 3),
                Color.Black);
            e.Graphics.TranslateTransform(r2.Left + r2.Width / 2, r2.Top + r2.Height / 2);
            var c = g.BeginContainer();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.RotateTransform(DateTime.Now.Hour * 30f + DateTime.Now.Minute / 2f);
            g.FillEllipse(Brushes.Black, -5, -5, 10, 10);
            using (var p = new Pen(Color.Black, 4))
                g.DrawLine(p, 0, 0, 0, -r2.Height / 2 + 30);
            g.EndContainer(c);
            c = g.BeginContainer();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.RotateTransform(DateTime.Now.Minute * 6);
            using (var p = new Pen(Color.Black, 2))
                g.DrawLine(p, 0, 0, 0, -r2.Height / 2 + 10);
            g.EndContainer(c);
            c = g.BeginContainer();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.RotateTransform(DateTime.Now.Second * 6);
            using (var p = new Pen(Color.Red, 2))
                g.DrawLine(p, 0, 10, 0, -r2.Height / 2 + 15);
            g.EndContainer(c);
        }

        protected override void Dispose(bool disposing)
        {
            this.timer.Dispose();
            base.Dispose(disposing);
        }
    }
}
