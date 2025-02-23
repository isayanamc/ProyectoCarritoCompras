using System;
using System.Net;
using System.Net.Mail;
using DTO;

namespace CoreApp
{
    public class NotificationManager
    {
        public static void SendEmail(DTO.User user, string subject, string message)

        {
            try
            {
                // Configuración del servidor SMTP
                string smtpSender = "smtp.gmail.com";
                int smtpPort = 587;
                string senderEmail = "miEmail@gmail.com"; 
                string senderPassword = "tu_contraseña";  

                // Crear el mensaje
                var mail = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true  // Permite enviar correos con HTML
                };
                mail.To.Add(new MailAddress(user.Email, user.Name));

                // Configurar el cliente SMTP
                using (var smtpClient = new SmtpClient(smtpSender, smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    smtpClient.EnableSsl = true;

                    smtpClient.Send(mail);
                }

                Console.WriteLine($"📩 Correo enviado a {user.Email} exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🚫 Error al enviar correo: {ex.Message}");
                throw; // Se mantiene el stack trace original de la excepción
            }
        }
    }
}
