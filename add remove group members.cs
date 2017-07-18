using System;
using System.Collections;
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
    public partial class Form4 : Form
    {
        Globle g = new Globle();
       
        List<User> isgmember ;
        List<User> isnotgmember;
        public Form4()
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

     


       private void Form4_Load(object sender, EventArgs e)
        {
            isgmember = g.isgmember(Globle.id, true);
            isnotgmember = g.isgmember(Globle.id, false);
            List<Group>groups = g.grouplist();
            Form2 f2 = new Form2();
            int index = groups.FindIndex(item => item.id ==Globle.to );
            string groupname = groups[index].groupname; 
            label1.Text = groupname;
            panel1.AutoScroll = true;
            foreach (var user in isnotgmember)
            {
               comboBox1.Items.Add(user.FullName);
            }
            panel1members(panel1);
        }

       private void panel1members(Panel p)
       {
           isgmember = g.isgmember(Globle.id, true);

           p.Controls.Clear();
           int repetition = 0;
           foreach (User user in isgmember )
           {
               Button dynamicbutton = new Button();
              
               dynamicbutton.Click += new System.EventHandler(delete_Click);
               dynamicbutton.Name = user.FullName;
               dynamicbutton.Text = "remove"; 
              
               dynamicbutton.Location = new Point(300, repetition * 50);
               dynamicbutton.Height = 30;
               dynamicbutton.Width = 50;
               
               dynamicbutton.Show();


               Label dynamicLabel = new Label();
               dynamicLabel.Text = user.FullName;
               dynamicLabel.Location = new Point(0, repetition * 53);
               dynamicLabel.Height = 20;
               dynamicLabel.Width = 100;
               dynamicLabel.Show();

               
               p.Controls.Add(dynamicLabel);
               p.Controls.Add(dynamicbutton);
               repetition++;
              
           }



       }
       void delete_Click(object sender, EventArgs e)
       {
           
           string gname = (sender as Button).Name;
           int index = isgmember.FindIndex(item => item.FullName == gname);

           int to = isgmember[index].id;
           
           DialogResult result = MessageBox.Show("do u really want to remove this person", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
           if (result == DialogResult.Yes)
           {
              
               g.delete("DELETE FROM `gmember` WHERE `groups_gid`='"+Globle.id+"' and `Users_Uid`= '"+to.ToString()+"';");
               
           }

           panel1members(panel1);




       }
        

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uname = comboBox1.Text;
            int index = isnotgmember.FindIndex(item => item.FullName == uname);
            int uid = isnotgmember[index].id;
            
            g.insert("INSERT INTO `gmember` (`gmid`, `groups_gid`, `Users_Uid`) VALUES (NULL, '" + Globle.to.ToString() + "', '" + uid.ToString() + "');");
            comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
            panel1members(panel1);
        }

       

        
    }
}
