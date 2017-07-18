using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatio
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        //moveable window
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
        //moveable window end 

        private void button1_Click(object sender, EventArgs e)
        {
            
            Globle g = new Globle();
            g.addgroup(textBox1.Text);
            Form2 f2 = new Form2();
            f2.Refresh();
            this.Close();
            
        }
        
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
