using Bingo.Models;

namespace Bingo.Service
{
    public interface IEmail
    {
        Task<bool> SendEmail(int quantity, string title, string email);
    }
}