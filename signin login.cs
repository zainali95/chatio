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
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            panel1.Visible = false;
            this.Size = new Size(355, 510);
        
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
   

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Singin_Click(object sender, EventArgs e)
        {
            Globle g = new Globle();
            if (textBox1.Text != "" && textBox2.Text != "")
            {    bool x=g.Login(textBox1.Text, textBox2.Text);
                if(x){
              
                    this.Hide();
                    var form2 = new Form2();
                    form2.Closed += (s, args) => this.Close();
                    form2.Show();
                }
                else {
                    MessageBox.Show("incorrect password or username");
                }

            }
            else {
                MessageBox.Show("Please enter credentials");
            
            }
            
            

           

        }

        private void Register_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            int newWidth = 562;
            int newHight = 532;
            panel1.MaximumSize = new Size(newWidth, newHight);
            this.Size = new Size( newWidth,newHight);
        }

     

        private void button2_Click(object sender, EventArgs e)
        {
            //back 
            panel1.Visible = false;
            this.Size = new Size(355, 510);
        }

       

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Register
            char gender=' ';
            if (radioButton1.Checked == true) { gender = 'm'; } else if(radioButton2.Checked == true) { gender = 'f'; }
            if (textBox3.Text != "" && textBox4.Text != "" && textBox6.Text != "" && textBox7.Text != "" && gender != ' ')
            {
                string query = "INSERT INTO `users` (`Uid`, `full_name`, `username`, `pswd`, `gender`, `email`) VALUES" +
                    "(NULL, '" + textBox4.Text + "','" + textBox6.Text + "', '" + textBox7.Text + "', '" + gender + "', '" + textBox3.Text + "');";
                Globle g = new Globle();
                 g.insert(query);
                 //textBox3.Text = null; textBox4.Text = null; textBox6.Text = null; textBox7.Text = null; radioButton1.Checked = false;
                
                 MessageBox.Show("please login to our app");
                 panel1.Visible = false;
                 this.Size = new Size(355, 510);
                 

            }
            else {

                MessageBox.Show("please enter all the values in the given field");
            }
        }

       
    }
}
