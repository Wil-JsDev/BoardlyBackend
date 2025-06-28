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
        MimeMessage email = new ();

        email.Sender = MailboxAddress.Parse(_correoConfiguraciones.EmailFrom);
        email.To.Add(MailboxAddress.Parse(dto.To)); //This send email
        email.Subject = dto.Subject;
            
        BodyBuilder bodyBuilder = new ();
        bodyBuilder.HtmlBody = dto.Body;
        email.Body = bodyBuilder.ToMessageBody();

        //SMTP configuration
        using MailKit.Net.Smtp.SmtpClient smtp = new();
        smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
        smtp.Connect(_correoConfiguraciones.SmtpHost, _correoConfiguraciones.SmtpPort,SecureSocketOptions.StartTls);
        smtp.Authenticate(_correoConfiguraciones.SmtpUser,_correoConfiguraciones.SmtpPass);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
}
