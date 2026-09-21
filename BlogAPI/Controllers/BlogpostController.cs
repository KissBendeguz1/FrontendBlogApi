using BlogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogpostController : ControllerBase
    {
        public string ConnectionString = "server=localhost;uid=root;password=;database=blogger;";
        [HttpGet]
        public List<Blogpost> GetBlogPosts() {
            List<Blogpost> Blogposts = new List<Blogpost>();
            var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT * FROM `blogpost`";
            var cmd = new MySqlCommand(sql, connection);
            var data = cmd.ExecuteReader();
            while (data.Read()) {
                var blogpost = new Blogpost
                {
                    Id = data.GetInt32("id"),
                    Title = data.GetString("title"),
                    Content = data.GetString("content"),
                    postTime = data.GetDateTime("posttime"),
                    updateTime = data.GetDateTime("updatetime"),


                };
                Blogposts.Add(blogpost);
            }
            connection.Close();

            return Blogposts;
        }
    }
}
