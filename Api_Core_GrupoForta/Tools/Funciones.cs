using Api_Core_GrupoForta.Models.Share;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Api_Core_GrupoForta.Tools
{
    public class Funciones
    {
        public static string GetSHA256(string str)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] stream = null;
                StringBuilder sb = new StringBuilder();
                stream = sha256.ComputeHash(encoding.GetBytes(str));
                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                return sb.ToString();
            }
        }


        public string Base64Decode(string data)
        {

            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }

        private MimeMessage CreateMimeMessageFromEmailMessage(EmailMessage message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(message.Sender);
            mimeMessage.To.Add(message.Reciever);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            { Text = message.Content };
            return mimeMessage;
        }

        public async Task<bool> SendEmail(string asunto, string contenido, NotificationMetadata notificationMetadata)
        {
            try{

                MailMessage mm = new MailMessage(notificationMetadata.Sender, notificationMetadata.Reciever, asunto, contenido);
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(notificationMetadata.Sender, notificationMetadata.Password)
                };
                await client.SendMailAsync(mm);


                //EmailMessage message = new EmailMessage();
                //message.Sender = new MailboxAddress(notificationMetadata.Sender);
                //message.Reciever = new MailboxAddress(notificationMetadata.Reciever);
                //message.Subject = asunto;
                //message.Content = contenido;
                //var mimeMessage = CreateMimeMessageFromEmailMessage(message);
                //using (SmtpClient smtpClient = new SmtpClient())
                //{
                //    await smtpClient.ConnectAsync(notificationMetadata.SmtpServer, notificationMetadata.Port, true);
                //    await smtpClient.AuthenticateAsync(notificationMetadata.UserName, notificationMetadata.Password);
                //    await smtpClient.SendAsync(mimeMessage);
                //    await smtpClient.DisconnectAsync(true);
                //}
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
