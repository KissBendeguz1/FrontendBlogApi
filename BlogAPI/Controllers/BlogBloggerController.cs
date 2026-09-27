using BlogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogBloggerController : ControllerBase
    {
        public string ConnectionString = "server=localhost;uid=root;password=;database=blogger;";
        [HttpGet("{id}")]
        public object GetBloggerNameEmail(int id)
        {
            var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            var sql = @"SELECT `name`,`email` FROM `blogger` WHERE `id`=@id";
            var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            var datareader = cmd.ExecuteReader();

            object data = null;
            if (datareader.Read() == true)
            {
                var bloggerdata = new BloggerNameEmail()
                {

                    Name = datareader.GetString("name"),
                    Email = datareader.GetString("email"),

                };

                return new { message = "Sikeres", res = bloggerdata };
            }
            else
            {
                data = new { message = "Sikertelen lekérdezés", resoult = "Nincs ilyen felhasználó" };
            }




            connection.Close();
            return data;

        }


        [HttpGet("posts/{id}")]
        public object GetBloggerNameTitleContent(int id)
        {
            var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            var sql = @"SELECT `name`, `title`, `content` FROM `blogger` INNER JOIN `blogpost` ON `blogger`.`id` = `blogpost`.`blogId` WHERE `blogger`.`id`=@id";
            var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            var datareader = cmd.ExecuteReader();

            object data = null;
            if (datareader.Read() == true)
            {
                var bloggerposts = new BloggerNamePosts()
                {
                    Name = datareader.GetString("name"),
                    TitlesContent = new List<object>(),
                };


                bloggerposts.TitlesContent.Add(new { Title = datareader.GetString("title"), Content = datareader.GetString("content") });


                while (datareader.Read())
                {
                    bloggerposts.TitlesContent.Add(new { Title = datareader.GetString("title"), Content = datareader.GetString("content") });
                }

                connection.Close();
                return new { message = "Sikeres", res = bloggerposts };
            }
            else
            {
                data = new { message = "Sikertelen lekérdezés", resoult = "Nincs ilyen felhasználó, vagy nincsenek posztjai" };
            }

            connection.Close();
            return data;

        }


        [HttpGet("posts")]
        public object GetPostCount()
        {
            var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            var sql = @"SELECT COUNT(*) AS count FROM `blogpost`";
            var cmd = new MySqlCommand(sql, connection);
            var data = cmd.ExecuteReader();
            data.Read();
            int count = data.GetInt32("count");
            connection.Close();
            return "postok száma: " + count ;

            


        }

        [HttpGet("postCount/{id}")]
        public object GetBloggerPostCount(int id)
        {
            var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            var sql = @"SELECT COUNT(*) AS count FROM `blogpost` WHERE `blogId`=@id";
            var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            var data = cmd.ExecuteReader();
            data.Read();
            int count = data.GetInt32("count");
            connection.Close();
            return "postok száma: " + count;
            
        }
    }
}
