namespace SpotifyDashboard.Server.Models
{
    public class User
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }

        public User()
        {
            DisplayName = string.Empty;
            Email = string.Empty;
        }

        public User(string displayName, string email)
        {
            DisplayName = displayName;
            Email = email;
        }
    }
}
