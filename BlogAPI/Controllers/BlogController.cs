using BlogAPI.Models;
using BlogAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        public string ConnectionString = "server=localhost;uid=root;password=;database=blogger;";

        [HttpGet]
        public List<Blogger> GetBloggers()
        {
            List<Blogger> Bloggers = new List<Blogger>();
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = "SELECT * FROM blogger;";

            var cmd = new MySqlCommand(sql, connection);

            var data = cmd.ExecuteReader();

            while (data.Read())
            {
                var blogger = new Blogger
                {
                    Id = data.GetInt32("id"),
                    Name = data.GetString("name"),
                    Email = data.GetString("email"),
                    Age = data.GetInt32("age"),
                    Password = data.GetString("password"),
                    RegistrationTime = data.GetDateTime("registrationtime")
                };

                Bloggers.Add(blogger);
            }

            connection.Close();

            return Bloggers;

            
            
            
        }

        [HttpPost]
        public object AddNewBlogger([FromBody] AddNewBloggerDto addnewbloggerdto)
        {
            var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            string sql = @"INSERT INTO `blogger`( `Name`, `Email`, `Age`, `Password`, `RegistrationTime`) VALUES (@name,@email,@age,@password,@registrationTime)";
            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@name", addnewbloggerdto.Name);
            cmd.Parameters.AddWithValue("@email", addnewbloggerdto.Email);
            cmd.Parameters.AddWithValue("@age", addnewbloggerdto.Age);
            cmd.Parameters.AddWithValue("@password", addnewbloggerdto.Password);
            cmd.Parameters.AddWithValue("@registrationTime", DateTime.Now);

            cmd.ExecuteNonQuery();


            connection.Close();
            return new { objectum = addnewbloggerdto, message = "Siekres felvétel"};
            
        }

        [HttpDelete]

        public object DeleteBlogger(int id)
        {
            var connenction = new MySqlConnection(ConnectionString);
            connenction.Open();

            string sql = @"DELETE FROM `blogger` WHERE `id` = @id";
            var cmd = new MySqlCommand(@sql, connenction);
            cmd.Parameters.AddWithValue(@"id", id);
            cmd.ExecuteNonQuery();

            connenction.Close();
            return new { message = "sikeres törlés", resoult = "" };
        }

        [HttpPut]

        public object updateBlogger([FromQuery] int id, UpdateBloggerDto updatebloggerdto) 
        {
            var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            var sql = @"UPDATE `blogger` SET `Name`=@name,`Email`=@email,`Age`=@age,`Password`=@password WHERE `Id`=@id";

            var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@name", updatebloggerdto.Name);
            cmd.Parameters.AddWithValue("@email", updatebloggerdto.Email);
            cmd.Parameters.AddWithValue("@age", updatebloggerdto.Age);
            cmd.Parameters.AddWithValue("@password", updatebloggerdto.Password);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            connection.Close();

            return new { message = "sikeres frissites", resoult=updatebloggerdto };
        }

        [HttpGet("id")]    
        public object GetBloggerById(int id) 
        {
            var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            var sql = @"SELECT * FROM `blogger` WHERE `id`=@id";
            var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            var datareader = cmd.ExecuteReader();
            
            object data = null;
            if (datareader.Read() ==true)
            {
                var blogger = new Blogger()
                {
                    Id = datareader.GetInt32("id"),
                    Name = datareader.GetString("name"),
                    Email = datareader.GetString("email"),
                    Age = datareader.GetInt32("Age"),
                    Password = datareader.GetString("password"),
                    RegistrationTime = datareader.GetDateTime("registrationTime")
                };

                return new { message = "Sikeres", res = blogger };
            }
            else {
                data = new { message = "Sikerestelen lekérdezés", resoult = "nyikhaj" };
            }

            


            connection.Close();
            return data;

        }
    }
}
