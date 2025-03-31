using Bingo.Models;

namespace Bingo.Helper
{
    public class GenerateCard
    {
        public static List<CardDto> GenerateNumbers(int card, int num, string _title)
        {
            var cards = new List<CardDto>();

            for (int i = 0; i < card; i++)
            {
                var c = new CardDto();
                c.IdCard = i + 1;
                c.Bingo = "BINGO";
                c.City = _title;
                c.Developer = "Hecho por Between Byte Software";
                c.Phone = "0960806054";
                c.Data = GenerateData(num);
                cards.Add(c);
            }

            return cards;
        }

        private static List<string> GenerateData(int num)
        {
            List<string> data = new List<string>();
            HashSet<int> usedNumbers = new HashSet<int>();
            var random = new Random();

            for (int i = 0; i < num - 1; i++)
            {
                int number;
                do
                {
                    number = random.Next(1 + (i % 5) * 15, 16 + (i % 5) * 15);
                } while (usedNumbers.Contains(number));
                usedNumbers.Add(number);
                data.Add(number.ToString());
            }

            data.Insert(12, "X");

            return data;
        }
    }
}