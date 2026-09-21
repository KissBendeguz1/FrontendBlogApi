namespace BlogAPI.Models.DTOs
{
    public class AddNewBlogPostDto
    {
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int blogId { get; set; }
    }
}
