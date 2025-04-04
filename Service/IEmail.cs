using Bingo.Helper;
using Bingo.Models;

namespace Bingo.Service
{
    public interface IEmail
    {
        Task<Reponse> SendCard(int quantity, string title, string email);
        Task<Reponse> SendCards(List<CardDto> cardList);
    }
}