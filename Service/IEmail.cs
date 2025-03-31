using Bingo.Helper;
using Bingo.Models;

namespace Bingo.Service
{
    public interface IEmail
    {
        Task<Reponse> SendEmail(int quantity, string title, string email);
    }
}