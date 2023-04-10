using System;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;

namespace GC_5
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Random rnd = new Random();
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.LightSalmon, 2);

            int x, y;
            int n = rnd.Next(30, 50);
            Point[] points = new Point[n];

            for (int i = 0; i < n; i++)
            {
                x = rnd.Next(50, this.ClientSize.Width - 50);
                y = rnd.Next(50, this.ClientSize.Height - 50);
                points[i] = new Point(x, y);
                g.DrawEllipse(pen, x, y, 5, 5);
            }
            List<Point> hull = new List<Point>();
            int l = 0;
            for (int i = 1; i < n; i++)
                if (points[i].X < points[l].X)
                    l = i;

            int p = l, q;
            do
            {
                hull.Add(points[p]);

                q = (p + 1) % n;

                for (int i = 0; i < n; i++)
                {
                    if (orientation(points[p], points[i], points[q]) == 2)
                        q = i;
                }

                p = q;

            } while (p != l);
            Pen hull_pen = new Pen(Color.Green, 2);
            for (int i = 0; i < hull.Count - 1; i++)
            {
                g.DrawLine(hull_pen, hull[i], hull[i + 1]);
            }

            g.DrawLine(hull_pen, hull[0], hull[hull.Count - 1]);
        }

        public static int orientation(Point p, Point q, Point r)
        {
            int val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0;
            return (val > 0) ? 1 : 2; 
        }
    }
}