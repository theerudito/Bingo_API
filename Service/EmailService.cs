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
                if (Validations.ValidateTitle(title) == false)
                {
                    return new Reponse { Messages = "El título es requerido", Codigo = 409 };
                }

                if (Validations.ValidateQuantity(quantity) == false)
                {
                    return new Reponse { Messages = "Debe ser Mayor a cero", Codigo = 409 };
                }

                if (Validations.ValidateEmail(email) == false)
                {
                    return new Reponse { Messages = "El email es incorrecto", Codigo = 409 };
                }

                // cards: lista de tarjetas generadas
                var cards = GenerateCard.GenerateNumbers(quantity, 25, title);

                var message = new MimeMessage();

                // DE: tu cuenta (dominio propio)
                message.From.Add(new MailboxAddress("Between Bytes Software", _emailConfig.Account));

                // PARA: el usuario que recibe el correo
                message.To.Add(new MailboxAddress("", email));

                // Agrega un Reply-To (recomendado) soporte@between-bytes.tech
                //message.ReplyTo.Add(new MailboxAddress("Soporte", "erudito.dev@"));

                // Asunto
                message.Subject = "Tus Tarjetas de Bingo";

                // Cuerpo del mensaje
                var body = new TextPart("plain")
                {
                    Text = @"Hola,

                    Gracias por usar nuestro generador de tarjetas de Bingo.

                    Adjuntamos tus tarjetas en PDF. Si no lo solicitaste tú, puedes ignorar este mensaje.

                    ¡Que lo disfrutes!

                    -- 
                    El equipo de Between Bytes Software"
                };

                // Archivo adjunto (PDF)
                var attachment = new MimePart("application", "pdf")
                {
                    Content = new MimeContent(new MemoryStream(ManagerPDF.GenerartePDF(cards))),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = "Tarjetas-Bingo.pdf"
                };

                // Combinar mensaje y adjunto
                var multipart = new Multipart("mixed");
                multipart.Add(body);
                multipart.Add(attachment);
                message.Body = multipart;

                // Enviar usando SMTP de Gmail
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailConfig.Host, _emailConfig.Port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_emailConfig.Account, _emailConfig.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return new Reponse { Messages = $"Email enviado correctamente a {email}", Codigo = 200 };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());

                return new Reponse { Messages = "Ocurrio un error al enviar el email", Codigo = 500 };
            }
        }
    }
}


