using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace mousaDragOperation
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void DoLeftClickDown()
        {
            //Call the imported function with the cursor's current position
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
        }

        public void DoLeftClickUp()
        {
            //Call the imported function with the cursor's current position
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x_start = this.Location.X + 60;
            int y_start = this.Location.Y + 10;
            Cursor.Position = new Point(x_start, y_start);

            SlowMouseDrag(x_start, y_start, x_start + 500, y_start + 500);
        }

        private void SlowMouseDrag(int x_start, int y_start, int x_end, int y_end)
        {
            int step_unit = 10;
            int x_delta = x_end - x_start;
            int y_delta = y_end - y_start;
            
            double total_distance = Math.Sqrt(Math.Pow(x_delta, 2) + Math.Pow(y_delta, 2));
            double sin_theta = y_delta / total_distance;

            for (int current_distance = 0; current_distance < total_distance; current_distance += step_unit)
            {
                double y_displacement = sin_theta * step_unit;
                double x_displacement = Math.Sqrt(Math.Pow(step_unit, 2) - Math.Pow(y_displacement, 2));

                DoMouseDrag(x_start, y_start, Convert.ToInt32(x_start + x_displacement), Convert.ToInt32(y_start + y_displacement));

                x_start = Convert.ToInt32(x_start + x_displacement);
                y_start = Convert.ToInt32(y_start + y_displacement);

                //this.Refresh();
                //Thread.Sleep(4000);
            }
            
        }

        private void DoMouseDrag(int x_start, int y_start, int x_end, int y_end)
        {
            Cursor.Position = new Point(x_start, y_start);
            //Thread.Sleep(1000);
            DoLeftClickDown();
            //Thread.Sleep(1000);
            Cursor.Position = new Point(x_end, y_end);
            //Thread.Sleep(1000);
            DoLeftClickUp();
            //Thread.Sleep(1000);
        }
    }
}
