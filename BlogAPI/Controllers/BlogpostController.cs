using BlogAPI.Models;
using BlogAPI.Models.DTOs;
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

        [HttpPost]
        public object AddNewPost(AddNewBlogPostDto newblogpost) {
            var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            var sql = @"INSERT INTO `blogpost`(`Title`, `Content`, `postTime`, `updateTime`, `blogId`) VALUES (@title, @Content, @posttime, @updatetime, @bloggerid)";
            var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@title", newblogpost.Title);
            cmd.Parameters.AddWithValue("@Content", newblogpost.Content);
            cmd.Parameters.AddWithValue("@posttime", DateTime.Now);
            cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
            cmd.Parameters.AddWithValue("@bloggerid", newblogpost.blogId);

            cmd.ExecuteNonQuery();


            connection.Close();
            return new { objectum = newblogpost, message = "Siekres felvétel" };


        }

        [HttpDelete]

        public object DeleteBlogPost(int id)
        {
            var connenction = new MySqlConnection(ConnectionString);
            connenction.Open();

            string sql = @"DELETE FROM `blogpost` WHERE `id` = @id";
            var cmd = new MySqlCommand(@sql, connenction);
            cmd.Parameters.AddWithValue(@"id", id);
            cmd.ExecuteNonQuery();

            connenction.Close();
            return new { message = "sikeres törlés", resoult = "" };
        }


        [HttpPut]

        public object updateBlogger([FromQuery] int id, UpdateBlogPostDto updateblogpostdto)
        {
            var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            var sql = @"UPDATE `blogpost` SET `Title`=@title,`Content`=@content,`updateTime`=@time WHERE `id`=@id";

            var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@title", updateblogpostdto.Title);
            cmd.Parameters.AddWithValue("@Content", updateblogpostdto.Content);
            cmd.Parameters.AddWithValue("@time", DateTime.Now);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            connection.Close();

            return new { message = "sikeres frissites", resoult = updateblogpostdto };
        }

    }


}
