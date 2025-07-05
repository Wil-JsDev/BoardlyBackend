using Boardly.Aplicacion.DTOs.Email;
using Boardly.Dominio.Configuraciones;
using Boardly.Dominio.Puertos.Email;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Boardly.Infraestructura.Compartido.Adaptadores;

public class EnviadorDeCorreos: ICorreoServicio<SolicitudCorreo>
{
    private CorreoConfiguraciones _correoConfiguraciones { get; }

    public EnviadorDeCorreos(IOptions<CorreoConfiguraciones> options)
    {
        _correoConfiguraciones = options.Value;
    }

    public async Task Execute(SolicitudCorreo dto)
    {

        try
        {

            MimeMessage email = new ();

            email.Sender = MailboxAddress.Parse(_correoConfiguraciones.EmailFrom);
            email.To.Add(MailboxAddress.Parse(dto.Destinatario)); //This send email
            email.Subject = dto.Asunto;
                
            BodyBuilder bodyBuilder = new ();
            bodyBuilder.HtmlBody = dto.Cuerpo;
            email.Body = bodyBuilder.ToMessageBody();

            //SMTP configuration
            using MailKit.Net.Smtp.SmtpClient smtp = new();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            await smtp.ConnectAsync(_correoConfiguraciones.SmtpHost, _correoConfiguraciones.SmtpPort,SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_correoConfiguraciones.SmtpUser,_correoConfiguraciones.SmtpPass);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception )
        {
            //Ignored
        }
    }
}
