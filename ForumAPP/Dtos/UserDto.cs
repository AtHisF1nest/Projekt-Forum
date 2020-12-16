namespace ForumAPP.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public int PostCount { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}