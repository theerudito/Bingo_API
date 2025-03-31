namespace Bingo.Models
{
    public class Email_ConfigDto
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Account { get; set; } = string.Empty;
        public string Alias { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}