namespace ForumAPP.Dtos
{
    public class SearchThreadsDto
    {
        public string SearchText { get; set; }
        public bool CanGetPopular { get; set; }
        public bool CanGetWithoutPosts { get; set; }
        public int TagId { get; set; }
    }
}