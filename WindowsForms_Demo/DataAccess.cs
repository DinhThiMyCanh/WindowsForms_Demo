using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WindowsForms_Demo
{
   static class DataAccess
    {
        private static SqlConnection cn;
        private static SqlCommand cmd;
        private static SqlDataAdapter da;
        
        public static void moKetNoi()
        {
            string st = @"Data Source=CANH-DHQN\SQLEXPRESS;Initial Catalog=QLNV;Integrated Security=True";
            // cn = new SqlConnection(st);
            cn = new SqlConnection();
            cn.ConnectionString = st;
            cn.Open();
        }

        public static void dongKetNoi()
        {
            cn.Close();
        }
        public static DataTable getData(string sql)
        {
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(sql, cn);
            da.Fill(dt);
            return dt;
        }
        public static int updateData(string sql,string [] name =null, object [] value=null)
        {
            cn.Open();
            int data = 0;
            cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Clear();
            string[] listname = sql.Split(' ');
            int i = 0;
            foreach (string item in listname)
                if (item.Contains('@'))
                {
                    cmd.Parameters.AddWithValue(item, value[i]);
                    i++;
                }    
            data = cmd.ExecuteNonQuery();
            cn.Close();
            return data;
        }
        
    }
}
