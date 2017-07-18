using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Dynamic;
using System.Diagnostics;

namespace Chatio
{

  
    class Globle
    {
        static public int id = 1;
        static public int to = 1;
        static public string name = "zain";
        static public string uname = "zz";
         
       
   
        public static string Server = "Server=localhost; Database = chatio; Uid = root; Pwd ='';";
        public MySqlConnection conn = new MySqlConnection(Server);
        public int dml(string query) {

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            int rowaffected = cmd.ExecuteNonQuery();
            conn.Close();
            return rowaffected;
        }
        public int insert(string query) {
            int rowaffected;
            return  rowaffected=dml(query);
        }
        public int delete(string query) {
            int rowaffected;
            return rowaffected = dml(query);
        }
        //addgroup in database and make login user admin
        public int addgroup(string gname)
        {

           
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("groupadd", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("UserID", Globle.id));
            cmd.Parameters.Add(new MySqlParameter("gname", gname));
            int rowaffected = cmd.ExecuteNonQuery();
            conn.Close();
            return rowaffected;
        }
        public List<User> isgmember(int gid,bool x){
        conn.Open();

        List<User> values = new List<User>();
        string sql;
        if (x == false) {
            sql = "SELECT `users`.`Uid`,`users`.`full_name`,`users`.`username` FROM `users` WHERE `Uid` not IN (select gmember.Users_Uid FROM gmember WHERE gmember.groups_gid =" + gid + "  )";
        }
        else {
            sql = "SELECT `users`.`Uid`,`users`.`full_name`,`users`.`username` FROM `users` WHERE `Uid` IN (select gmember.Users_Uid FROM gmember WHERE gmember.groups_gid =" + gid + " and gmember.Users_Uid !=" + Globle.id + ")";
        }
        
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            int uid = Convert.ToInt32(reader["Uid"]);
            string username = reader["username"].ToString();
            string name = reader["full_name"].ToString();
            values.Add(new User(uid, name, username));
        }
        conn.Close();
        return values;
    }
       //return group in which member of 
        public List<Group> grouplist()
        {
            conn.Open();
            List<Group> values = new List<Group>();

            string sql = "SELECT gid,gname,admin from groups,gmember where gid=groups_gid and users_Uid="+Globle.id+";";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int admin = Convert.ToInt32(reader["admin"]);
                bool isadmin;
                if (admin == Globle.id)
                {
                    isadmin = true;
                }
                else {
                    isadmin = false;
                }
                string groupname = reader["gname"].ToString();
                int gid = Convert.ToInt32(reader["admin"]);
                values.Add(new Group(gid,groupname,isadmin));
            }
            conn.Close();
            return values;
        }
       //return users 
        public List<User> contactlist()
        {
            conn.Open();
            List<User> values = new List<User>();

            string sql = "SELECT * FROM users where Uid !="+id+";";
            
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int uid = Convert.ToInt32(reader["Uid"]);
                string username = reader["username"].ToString();
                string name = reader["full_name"].ToString();
                values.Add(new User(uid,name,username));
            }
            conn.Close();
            return values;
        }
       //login return true if valid user
        public bool Login(string user,string pass) { 
           conn.Open();
           bool result;
        
        string query ="SELECT * FROM `users` WHERE `username`='" + user + "' AND `pswd`='" + pass + "';";
           MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            User defaultuser = new User();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int uid = Convert.ToInt32(reader["Uid"]);

                    string username = reader["username"].ToString();
                    string name = reader["full_name"].ToString();
                    
                    Globle.uname = username;
                    Globle.name = name;
                    Globle.id = uid;

                }
                result = true;
            }
            else
            {
                result = false;
            }
            
            conn.Close();
            return result;
        }
       //return single user messages
        public List<message> messagelist() {
       
                List<message> MessageItemlist = new List<message>();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("convo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("UserID",Globle.id));
                cmd.Parameters.Add(new MySqlParameter("UserID1",Globle.to));
           
                 using (MySqlDataReader dr = cmd.ExecuteReader()) {
            
                     while (dr.Read())
                    {

                       string created_at = Convert.ToString(dr["created_at"]);
                       string Message = Convert.ToString(dr["message"]);
                       string from = Convert.ToString(dr["from"]);
                       MessageItemlist.Add(new message(created_at,from,Message));
                    }
        
                 }

             conn.Close();
             return MessageItemlist;
        }
       //return group messages
        public List<message> gmessagelist()
        {


            List<message> GroupMessageItemlist = new List<message>();

            conn.Open();
            string query = "SELECT gchat.message,created_at,A.full_name as 'from' FROM gchat LEFT JOIN users A ON A.Uid= gchat.Users_Uid WHERE gchat.groups_gid=" + Globle.to + ";";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            using (MySqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {

                    string created_at = Convert.ToString(dr["created_at"]);
                    string Message = Convert.ToString(dr["message"]);
                    string from = Convert.ToString(dr["from"]);

                    GroupMessageItemlist.Add(new message(created_at, from, Message));
                }
            }

            conn.Close();
            return GroupMessageItemlist;
        }
    }
}
