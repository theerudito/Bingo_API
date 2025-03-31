using Bingo.Helper;
using Bingo.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Bingo.Service
{
    public class EmailService(IOptions<Email_ConfigDto> options) : IEmail
    {
        private readonly Email_ConfigDto _emailConfig = options.Value;

        public async Task<Reponse> SendEmail(int quantity, string title, string email)
        {
            try
            {
                if (Validations.ValidateField(title, quantity) == false)
                {
                    return new Reponse { Messages = "El título es requerido", Codigo = 409 };
                }

                if (Validations.ValidateField(title, quantity) == false)
                {
                    return new Reponse { Messages = "Debe ser Mayor a cero", Codigo = 409 };
                }

                if (Validations.ValidateEmail(email) == false)
                {
                    return new Reponse { Messages = "El email es incorrecto", Codigo = 409 };
                }

                var cards = GenerateCard.GenerateNumbers(quantity, 25, title);

                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_emailConfig.Alias, _emailConfig.Account));

                message.To.Add(new MailboxAddress("", email));

                message.Subject = "Tarjetas Bingo";

                var body = new TextPart("plain")
                {
                    Text = "Aqui Puedes Descargas tus Tarjetas de Bingo"
                };

                var attachment = new MimePart("application", "pdf")
                {
                    Content = new MimeContent(new MemoryStream(ManagerPDF.GenerartePDF(cards))),

                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),

                    ContentTransferEncoding = ContentEncoding.Base64,

                    FileName = "Bingo"
                };

                var multipart = new Multipart("mixed");

                multipart.Add(body);

                multipart.Add(attachment);
                message.Body = multipart;

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailConfig.Host, _emailConfig.Port, SecureSocketOptions.StartTls);

                    await client.AuthenticateAsync(_emailConfig.Account, _emailConfig.Password);

                    await client.SendAsync(message);

                    await client.DisconnectAsync(true);
                }

                return new Reponse { Messages = $"Email enviado correctamente {email}", Codigo = 200 };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());

                return new Reponse { Messages = "Ocurrio un error al enviar el email", Codigo = 500 };
            }
        }
    }
}