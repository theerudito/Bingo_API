namespace Bingo.Models
{
    public class CardDto
    {
        public int IdCard { get; set; }
        public string Bingo { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public List<string> Data { get; set; } = null!;
        public string Developer { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}