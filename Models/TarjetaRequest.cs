namespace Bingo_API.Models
{
    public class TarjetaRequest
    {
        public int quantity { get; set; }
        public string title { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
    }
}