using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SB_4952.Models;
using System.Data;
using System.Xml.Linq;
using System.Diagnostics;

namespace SB_4952.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Login
        [HttpGet]
        public string Get()
        {
            string query = @"select login_id, staff_code, login, password, name, email, contact_no, device_token, role_id, remark, login_status, doc_suffix, proj_no from sprint_eesa.cms_login";

            DataTable table = new DataTable();
            string sqlDataSource = getConnString();
            Debug.WriteLine(sqlDataSource);
            MySqlDataReader myReader;

            using(MySqlConnection conn = new MySqlConnection(sqlDataSource))
            {
                conn.Open();

                using (MySqlCommand myCommand = new MySqlCommand(query, conn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    conn.Close();
                } 
            }

            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(table);
            return JsonString;
        }

        // POST api/Login
        [HttpPost]
        public void Post(Login login)
        {
            string query = @"insert into sprint_eesa.cms_login (staff_code, login, password, name, email, contact_no, device_token, role_id, remark, login_status, doc_suffix, proj_no) 
                            values (@StaffCode, @LoginUsername, @Password, @Name, @Email, @ContactNo, @DeviceToken, @RoleId, @Remark, @LoginStatus, @DocSuffix, @ProjNo)";

            DataTable table = new DataTable();
            string sqlDataSource = getConnString();
            MySqlDataReader myReader;

            using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
            {
                conn.Open();

                using (MySqlCommand myCommand = new MySqlCommand(query, conn))
                {
                    myCommand.Parameters.AddWithValue("@StaffCode", login.StaffCode);
                    myCommand.Parameters.AddWithValue("@LoginUsername", login.LoginUsername);
                    myCommand.Parameters.AddWithValue("@Password", login.Password);
                    myCommand.Parameters.AddWithValue("@Name", login.Name);
                    myCommand.Parameters.AddWithValue("@Email", login.Email);
                    myCommand.Parameters.AddWithValue("@ContactNo", login.ContactNo);
                    myCommand.Parameters.AddWithValue("@DeviceToken", login.DeviceToken);
                    myCommand.Parameters.AddWithValue("@RoleId", login.RoleId);
                    myCommand.Parameters.AddWithValue("@Remark", login.Remark);
                    myCommand.Parameters.AddWithValue("@LoginStatus", login.LoginStatus);
                    myCommand.Parameters.AddWithValue("@DocSuffix", login.DocSuffix);
                    myCommand.Parameters.AddWithValue("@ProjNo", login.ProjNo);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    conn.Close();
                }
            }
        }

        // PUT api/Login/5
        [HttpPut]
        public void Put(Login login)
        {
            string query = @"update sprint_eesa.cms_login 
                            set staff_code = @StaffCode, login = @LoginUsername, password = @Password, name = @Name, email = @Email, contact_no = @ContactNo, device_token = @DeviceToken, role_id = @RoleId, remark = @Remark, login_status = @LoginStatus, doc_suffix = @DocSuffix, proj_no = @ProjNo
                            where login_id=@LoginId";

            DataTable table = new DataTable();
            string sqlDataSource = getConnString();
            MySqlDataReader myReader;

            using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
            {
                conn.Open();

                using (MySqlCommand myCommand = new MySqlCommand(query, conn))
                {
                    myCommand.Parameters.AddWithValue("@LoginId", login.LoginId);
                    myCommand.Parameters.AddWithValue("@StaffCode", login.StaffCode);
                    myCommand.Parameters.AddWithValue("@LoginUsername", login.LoginUsername);
                    myCommand.Parameters.AddWithValue("@Password", login.Password);
                    myCommand.Parameters.AddWithValue("@Name", login.Name);
                    myCommand.Parameters.AddWithValue("@Email", login.Email);
                    myCommand.Parameters.AddWithValue("@ContactNo", login.ContactNo);
                    myCommand.Parameters.AddWithValue("@DeviceToken", login.DeviceToken);
                    myCommand.Parameters.AddWithValue("@RoleId", login.RoleId);
                    myCommand.Parameters.AddWithValue("@Remark", login.Remark);
                    myCommand.Parameters.AddWithValue("@LoginStatus", login.LoginStatus);
                    myCommand.Parameters.AddWithValue("@DocSuffix", login.DocSuffix);
                    myCommand.Parameters.AddWithValue("@ProjNo", login.ProjNo);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    conn.Close();
                }
            }
        }

        // DELETE api/Login/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string query = @"delete from sprint_eesa.cms_login
                            where login_id=@LoginId";

            DataTable table = new DataTable();
            string sqlDataSource = getConnString();
            MySqlDataReader myReader;

            using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
            {
                conn.Open();

                using (MySqlCommand myCommand = new MySqlCommand(query, conn))
                {
                    myCommand.Parameters.AddWithValue("@LoginId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    conn.Close();
                }
            }
        }

        private string getConnString()
        {
            string FilePath = @".\Connection.txt";

            using (StreamReader sr = new StreamReader(FilePath))
            {
                string line;
                string return_string = "";

                while ((line = sr.ReadLine()) != null)
                {
                    return_string += line;
                }
                return return_string;
            }

        }
    }
}
