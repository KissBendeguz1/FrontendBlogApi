namespace BlogAPI.Models
{
    public class BloggerNamePosts
    {
        public string Name { get; set; } = string.Empty;
        public List<object> TitlesContent { get; set; } = new List<object>();
    }
}
