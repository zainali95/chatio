using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Chatio
{
    public partial class Form2 : Form
    {
        Globle globle = new Globle();
        List<User> users;
        List<Group> groups;
      
        public Form2()
        {


            users=globle.contactlist();
            groups = globle.grouplist();
           
            InitializeComponent();
        }
       
       
       

        
       


     
   
        //chat button
        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            panel3contacts(panel3);
            panel3.Show();
        }

       

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
          
            panel3groups(panel3);

        }

      

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            contextMenuStrip2.Show(pictureBox2, new Point( 0,pictureBox2.Height));
        }

      

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = Globle.name;
            label2.Text = "@"+Globle.uname;
            
            panel3contacts(panel3);
            panel3.AutoScroll = true;
           
        }

        private void panel3contacts(Panel p)
        {
           // List<Panel> ls = new List<Panel>();
            int i = 1;
            foreach(User user in this.users )
            {
              
                Label dynamicLabel = new Label();
                dynamicLabel.Text = user.FullName;
                dynamicLabel.Margin = new Padding(10);
                dynamicLabel.Location = new Point(60, i * 65);
                dynamicLabel.Height = 20;
                dynamicLabel.Width = 100;
                dynamicLabel.Click += new EventHandler(contact_Click);
                dynamicLabel.Show();
                p.Controls.Add(dynamicLabel);
                i++;
                
            }

            
          
        }

        private void panel3groups(Panel p)
        {
            List<Panel> ls = new List<Panel>();
            int i = 1;
            foreach (Group group in this.groups)
            {
                Label dynamicLabel = new Label();
                dynamicLabel.Text = group.groupname;
                dynamicLabel.Name = group.id.ToString();
                dynamicLabel.Margin = new Padding(10);
                dynamicLabel.Location = new Point(60, i * 65);
                dynamicLabel.Height = 20;
                dynamicLabel.Width = 100;
                dynamicLabel.Click += new EventHandler(group_Click);
                dynamicLabel.Show();
                p.Controls.Add(dynamicLabel);
                i++;
            }



        }
        private void button_Click(object sender, EventArgs e)
        {
           
        }

        private Timer timer1;
        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 2000; // in miliseconds
            timer1.Start();
            panel3groups(panel3);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            messagebox();
            gmessagebox();
        }
        public void messagebox()
        {
            var messages = this.globle.messagelist();
            textBox3.Clear();
            
            foreach (message m in messages)
            {
                textBox3.Text += "\r\n" + m.from.ToString() + " : " + m.text.ToString() + "\r\n" + m.created_at.ToString() + "\r\n\r\n";
            }
        }
        public void gmessagebox()
        {
            var messages = this.globle.gmessagelist();
            textBox4.Clear();

            foreach (message m in messages)
            {
                textBox4.Text += "\r\n" + m.from.ToString() + " : " + m.text.ToString() + "\r\n" + m.created_at.ToString() + "\r\n\r\n";
            }
        }
        void contact_Click(object sender, EventArgs e)
        {
            panel5.Hide();
            string fullname=(sender as Label).Text;
            int index = users.FindIndex(item => item.FullName==fullname);
            string username = users[index].username;
            int to=users[index].id;
            panel4.Hide();
            Globle.to = to;
            label4.Text = fullname;
            label3.Text = "@"+username;
            messagebox();
            

           
           
        }
        void group_Click(object sender, EventArgs e)
        {
            panel4.Hide();
            panel5.Show();
            string gname = (sender as Label).Text;
            int index = groups.FindIndex(item => item.groupname == gname);
            bool isadmin = groups[index].isadmin;
            int to = groups[index].id;
            if (isadmin == false) { button1.Hide(); } else { button1.Show();}
            Globle.to = to;
            this.label5.Text = gname;

            gmessagebox();






        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {string query="INSERT INTO `message` (`mid`, `from_user`, `to_user`, `message`, `created_at`) VALUES (NULL, "+Globle.id+", "+Globle.to+", '"+textBox2.Text+"', CURRENT_TIMESTAMP);";
            if(textBox2.Text!=" ")
            globle.insert(query);
            messagebox();
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            string query = "INSERT INTO `gchat` (`gcid`, `Users_Uid`, `groups_gid`, `message`, `created_at`)  VALUES (NULL, " + Globle.id + ", " + Globle.to + ", '" + textBox5.Text + "', CURRENT_TIMESTAMP);";
            if (textBox2.Text != " ")
                globle.insert(query);
            gmessagebox();
        }


        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            
            foreach(Control c in this.panel3.Controls){

                     if(c is Label && c.Text.Contains(textBox1.Text)){
                         c.Show();
                         }
                     else{
                         c.Hide();
                         }
                    }
        }
       

      

       
        
    }
}
