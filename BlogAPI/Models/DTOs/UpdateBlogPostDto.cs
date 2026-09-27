namespace BlogAPI.Models.DTOs
{
    public class UpdateBlogPostDto
    {
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
    }
}
