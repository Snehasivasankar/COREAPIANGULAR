using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
namespace CoreApiAngular.Models
{
    public class UserDBCls
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-O4EHA8II\SQLEXPRESS;database=DBAngularAPI;Integrated security=true");
    public string InsertDB(UserCls objcls)
        {
            SqlCommand cmd = new SqlCommand("sp_UserInsert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@na", objcls.name);//get
            cmd.Parameters.AddWithValue("@ag", objcls.age);
            cmd.Parameters.AddWithValue("@addr", objcls.addr);
            cmd.Parameters.AddWithValue("@email", objcls.email);
            cmd.Parameters.AddWithValue("@photo", objcls.photo);
            cmd.Parameters.AddWithValue("@una", objcls.uname);
            cmd.Parameters.AddWithValue("@pw", objcls.password);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return ("Inserted successfully");

        }
        public string LoginDB(UserCls objcls)
        {
            try
            {
                string cid = "";
                SqlCommand cmd = new SqlCommand("sp_login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@una", objcls.uname);
                cmd.Parameters.AddWithValue("@pw", objcls.password);//get
                con.Open();
                cid = cmd.ExecuteScalar().ToString();
                con.Close();
                return cid;
            }
            catch(Exception ex)
            {
                if(con.State==ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }
        public string GetUserID(UserCls objcls)
        {
            try
            {
                string cid = "";
                SqlCommand cmd = new SqlCommand("sp_GetId",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@una", objcls.uname);
                cmd.Parameters.AddWithValue("@pw", objcls.password);//get
                con.Open();
                cid = cmd.ExecuteScalar().ToString();
                con.Close();
                return cid;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }
        public UserCls SelectProfileDB(int id)
        {
            var getdata = new UserCls();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_selectProfile",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId",id);
               
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while(sdr.Read())
                {
                    getdata = new UserCls
                    {
                        uid = Convert.ToInt32(sdr["UserId"]),//set
                        name = sdr["Name"].ToString(),
                        age = Convert.ToInt32(sdr["Age"]),
                        addr = sdr["Address"].ToString(),
                        email = sdr["Email"].ToString(),
                        photo = sdr["Photo"].ToString(),
                    };
                }
                con.Close();
                return getdata;

            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
    }
}
