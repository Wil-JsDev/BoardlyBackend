namespace Boardly.Dominio.Utilidades;

public static class EmailTemas
{
public static string ConfirmarCuenta(string codigo, Guid usuarioId)
{
    return $@"<!DOCTYPE html>
<html lang=""es"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>✨ Confirma tu cuenta - Boardly</title>
    <link href=""https://fonts.googleapis.com/css2?family=Inter:wght@400;500;700&family=Space+Grotesk:wght@600&display=swap"" rel=""stylesheet"">
    <style>
        @keyframes fadeIn {{
            0% {{ opacity: 0; transform: translateY(20px) scale(0.95); }}
            100% {{ opacity: 1; transform: translateY(0) scale(1); }}
        }}
        @keyframes float {{
            0%, 100% {{ transform: translateY(0); }}
            50% {{ transform: translateY(-10px); }}
        }}
        @keyframes pulse {{
            0%, 100% {{ box-shadow: 0 0 0 0 rgba(99, 102, 241, 0.4); }}
            70% {{ box-shadow: 0 0 0 15px rgba(99, 102, 241, 0); }}
        }}
        @keyframes gradientFlow {{
            0% {{ background-position: 0% 50%; }}
            50% {{ background-position: 100% 50%; }}
            100% {{ background-position: 0% 50%; }}
        }}
        .email-container {{
            animation: fadeIn 0.8s cubic-bezier(0.23, 1, 0.32, 1) forwards;
        }}
        .verification-card {{
            animation: float 6s ease-in-out infinite;
        }}
        .verification-code {{
            animation: pulse 2s infinite;
        }}
        .gradient-header {{
            background: linear-gradient(90deg, #6366F1, #06B6D4, #10B981);
            background-size: 200% 200%;
            animation: gradientFlow 8s ease infinite;
            height: 8px;
        }}
        .gradient-border {{
            position: relative;
            z-index: 1;
        }}
        .gradient-border::before {{
            content: '';
            position: absolute;
            z-index: -1;
            inset: 0;
            padding: 2px;
            border-radius: 24px;
            background: linear-gradient(135deg, #6366F1, #06B6D4, #10B981, #6366F1);
            background-size: 300% 300%;
            animation: gradientFlow 8s ease infinite;
            -webkit-mask: 
                linear-gradient(#fff 0 0) content-box, 
                linear-gradient(#fff 0 0);
            -webkit-mask-composite: xor;
            mask-composite: exclude;
        }}
    </style>
</head>
<body style=""margin:0;padding:20px;background:#050505;font-family:'Inter',-apple-system,BlinkMacSystemFont,sans-serif;"">
    <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"">
        <tr>
            <td align=""center"">
                <!-- Contenedor principal con efecto neón -->
                <div class=""email-container gradient-border"" style=""max-width:520px;margin:0 auto;background:#0A0A0A;border-radius:24px;overflow:hidden;box-shadow:0 25px 50px -12px rgba(0,0,0,0.5);"">
                    
                    <!-- Marco superior con degradado animado -->
                    <div class=""gradient-header"" style=""width:100%;""></div>
                    
                    <!-- Encabezado futurista -->
                    <div style=""padding:50px 30px 40px;text-align:center;background:#111111;position:relative;overflow:hidden;"">
                        <div style=""position:absolute;top:0;left:0;right:0;bottom:0;background:radial-gradient(circle at 70% 30%,rgba(99,102,241,0.1) 0%,transparent 70%);""></div>
                        <h1 style=""font-family:'Space Grotesk',sans-serif;font-size:32px;font-weight:600;color:transparent;background:linear-gradient(90deg,#FFFFFF,#6366F1);-webkit-background-clip:text;background-clip:text;margin-bottom:12px;"">CONFIRMACIÓN REQUERIDA</h1>
                        <p style=""font-size:14px;color:#A1A1AA;letter-spacing:1px;margin:0;"">Tu acceso a Boardly está a un paso</p>
                    </div>
                    
                    <!-- Contenido principal -->
                    <div class=""content"" style=""padding:40px 30px;text-align:center;background:#0A0A0A;"">
                        
                        <!-- Tarjeta de verificación flotante -->
                        <div class=""verification-card"" style=""margin-bottom:40px;"">
                            <div style=""font-size:13px;color:#A1A1AA;margin-bottom:20px;letter-spacing:2px;text-transform:uppercase;"">CÓDIGO DE ACCESO</div>
                            
                            <!-- Código con efecto de pulso -->
                            <div class=""verification-code"" style=""font-family:'Space Grotesk',sans-serif;font-size:48px;font-weight:700;color:#FFFFFF;letter-spacing:0.25em;padding:30px;margin:0 auto 25px;background:#111111;border-radius:16px;display:inline-block;position:relative;border:1px solid #252525;"">
                                <div style=""position:absolute;top:0;left:0;right:0;height:2px;background:linear-gradient(90deg,#6366F1,#06B6D4);""></div>
                                {codigo}
                                <div style=""position:absolute;bottom:0;left:0;right:0;height:2px;background:linear-gradient(90deg,#6366F1,#06B6D4);""></div>
                            </div>
                            
                            <!-- Tiempo de expiración -->
                            <div style=""display:inline-flex;align-items:center;gap:8px;background:rgba(245,158,11,0.1);color:#F59E0B;padding:10px 24px;border-radius:50px;font-size:14px;font-weight:600;border:1px solid rgba(245,158,11,0.2);"">
                                <svg width=""16"" height=""16"" viewBox=""0 0 24 24"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                                    <path d=""M12 8V12L15 15M21 12C21 16.9706 16.9706 21 12 21C7.02944 21 3 16.9706 3 12C3 7.02944 7.02944 3 12 3C16.9706 3 21 7.02944 21 12Z"" stroke=""#F59E0B"" stroke-width=""1.5"" stroke-linecap=""round"" stroke-linejoin=""round""/>
                                </svg>
                                <span>EXPIRA EN 15 MINUTOS</span>
                            </div>
                        </div>
                        
                        <!-- Instrucciones con efecto de aparición -->
                        <div style=""max-width:400px;margin:0 auto 40px;animation:fadeIn 0.8s 0.2s both;"">
                            <p style=""font-size:15px;color:#D4D4D4;line-height:1.7;margin-bottom:0;"">
                                Ingresa este código único en la aplicación Boardly para activar tu cuenta y comenzar a organizar tus proyectos como un profesional.
                            </p>
                        </div>
                        
                        <!-- Separador dinámico -->
                        <div style=""height:1px;background:linear-gradient(90deg,transparent,#6366F1,#06B6D4,transparent);margin:40px 0;opacity:0.3;""></div>
                        
                        <!-- Aviso de seguridad destacado -->
                        <div style=""background:rgba(239,68,68,0.05);border-radius:16px;padding:24px;text-align:left;border:1px solid rgba(239,68,68,0.2);position:relative;overflow:hidden;"">
                            <div style=""position:absolute;top:0;left:0;right:0;height:2px;background:linear-gradient(90deg,#EF4444,#DC2626);""></div>
                            <div style=""display:flex;align-items:flex-start;gap:16px;"">
                                <div style=""flex-shrink:0;width:24px;height:24px;background:#EF4444;border-radius:6px;display:flex;align-items:center;justify-content:center;"">
                                    <svg width=""16"" height=""16"" viewBox=""0 0 24 24"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                                        <path d=""M12 9V11M12 15H12.01M10 4H14C16.2091 4 18 5.79086 18 8V18C18 20.2091 16.2091 22 14 22H10C7.79086 22 6 20.2091 6 18V8C6 5.79086 7.79086 4 10 4Z"" stroke=""white"" stroke-width=""1.5"" stroke-linecap=""round"" stroke-linejoin=""round""/>
                                    </svg>
                                </div>
                                <div>
                                    <h3 style=""font-size:16px;font-weight:700;color:#EF4444;margin-bottom:8px;"">ADVERTENCIA DE SEGURIDAD</h3>
                                    <p style=""font-size:14px;color:#A1A1AA;line-height:1.6;margin:0;"">
                                        Este código es personal e intransferible. Por seguridad, nunca lo compartas con nadie. El equipo de Boardly nunca te lo pedirá por otros medios.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <!-- Pie de página minimalista -->
                    <div style=""padding:30px;text-align:center;border-top:1px solid #1F1F1F;background:#0A0A0A;"">
                        <p style=""font-size:12px;color:#52525B;margin:0;"">© {DateTime.Now.Year} Boardly Technologies · Todos los derechos reservados</p>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</body>
</html>
";
}

public static string OlvidarContrasena(string codigo, Guid usuarioId)
{
    return $@"<!DOCTYPE html>
<html lang=""es"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>🔑 Restablece tu contraseña - Boardly</title>
    <link href=""https://fonts.googleapis.com/css2?family=Inter:wght@400;500;700&family=Space+Grotesk:wght@600&display=swap"" rel=""stylesheet"">
    <style>
        @keyframes fadeIn {{
            0% {{ opacity: 0; transform: translateY(20px) scale(0.95); }}
            100% {{ opacity: 1; transform: translateY(0) scale(1); }}
        }}
        @keyframes float {{
            0%, 100% {{ transform: translateY(0); }}
            50% {{ transform: translateY(-10px); }}
        }}
        @keyframes pulse {{
            0%, 100% {{ box-shadow: 0 0 0 0 rgba(99, 102, 241, 0.4); }}
            70% {{ box-shadow: 0 0 0 15px rgba(99, 102, 241, 0); }}
        }}
        @keyframes gradientFlow {{
            0% {{ background-position: 0% 50%; }}
            50% {{ background-position: 100% 50%; }}
            100% {{ background-position: 0% 50%; }}
        }}
        .email-container {{
            animation: fadeIn 0.8s cubic-bezier(0.23, 1, 0.32, 1) forwards;
        }}
        .verification-card {{
            animation: float 6s ease-in-out infinite;
        }}
        .verification-code {{
            animation: pulse 2s infinite;
        }}
        .gradient-header {{
            background: linear-gradient(90deg, #6366F1, #06B6D4, #10B981);
            background-size: 200% 200%;
            animation: gradientFlow 8s ease infinite;
            height: 8px;
        }}
        .gradient-border {{
            position: relative;
            z-index: 1;
        }}
        .gradient-border::before {{
            content: '';
            position: absolute;
            z-index: -1;
            inset: 0;
            padding: 2px;
            border-radius: 24px;
            background: linear-gradient(135deg, #6366F1, #06B6D4, #10B981, #6366F1);
            background-size: 300% 300%;
            animation: gradientFlow 8s ease infinite;
            -webkit-mask: 
                linear-gradient(#fff 0 0) content-box, 
                linear-gradient(#fff 0 0);
            -webkit-mask-composite: xor;
            mask-composite: exclude;
        }}
        .btn-primary {{
            background: linear-gradient(135deg, #6366F1, #06B6D4);
            color: white;
            padding: 16px 32px;
            border-radius: 12px;
            font-weight: 600;
            text-decoration: none;
            display: inline-block;
            margin: 20px 0;
            transition: all 0.3s ease;
            box-shadow: 0 4px 20px rgba(99, 102, 241, 0.3);
        }}
        .btn-primary:hover {{
            transform: translateY(-2px);
            box-shadow: 0 6px 25px rgba(99, 102, 241, 0.4);
        }}
    </style>
</head>
<body style=""margin:0;padding:20px;background:#050505;font-family:'Inter',-apple-system,BlinkMacSystemFont,sans-serif;"">
    <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"">
        <tr>
            <td align=""center"">
                <!-- Contenedor principal con efecto neón -->
                <div class=""email-container gradient-border"" style=""max-width:520px;margin:0 auto;background:#0A0A0A;border-radius:24px;overflow:hidden;box-shadow:0 25px 50px -12px rgba(0,0,0,0.5);"">
                    
                    <!-- Marco superior con degradado animado -->
                    <div class=""gradient-header"" style=""width:100%;""></div>
                    
                    <!-- Encabezado futurista -->
                    <div style=""padding:50px 30px 40px;text-align:center;background:#111111;position:relative;overflow:hidden;"">
                        <div style=""position:absolute;top:0;left:0;right:0;bottom:0;background:radial-gradient(circle at 70% 30%,rgba(99,102,241,0.1) 0%,transparent 70%);""></div>
                        <h1 style=""font-family:'Space Grotesk',sans-serif;font-size:32px;font-weight:600;color:transparent;background:linear-gradient(90deg,#FFFFFF,#6366F1);-webkit-background-clip:text;background-clip:text;margin-bottom:12px;"">RESTABLECER CONTRASEÑA</h1>
                        <p style=""font-size:14px;color:#A1A1AA;letter-spacing:1px;margin:0;"">Hemos recibido una solicitud para restablecer tu contraseña</p>
                    </div>
                    
                    <!-- Contenido principal -->
                    <div class=""content"" style=""padding:40px 30px;text-align:center;background:#0A0A0A;"">
                        
                        <!-- Mensaje introductorio -->
                        <div style=""margin-bottom:30px;"">
                            <p style=""font-size:15px;color:#D4D4D4;line-height:1.7;"">
                                Usa el siguiente código para verificar tu identidad y crear una nueva contraseña segura.
                            </p>
                        </div>
                        
                        <!-- Tarjeta de verificación flotante -->
                        <div class=""verification-card"" style=""margin-bottom:40px;"">
                            <div style=""font-size:13px;color:#A1A1AA;margin-bottom:20px;letter-spacing:2px;text-transform:uppercase;"">CÓDIGO DE VERIFICACIÓN</div>
                            
                            <!-- Código con efecto de pulso -->
                            <div class=""verification-code"" style=""font-family:'Space Grotesk',sans-serif;font-size:48px;font-weight:700;color:#FFFFFF;letter-spacing:0.25em;padding:30px;margin:0 auto 25px;background:#111111;border-radius:16px;display:inline-block;position:relative;border:1px solid #252525;"">
                                <div style=""position:absolute;top:0;left:0;right:0;height:2px;background:linear-gradient(90deg,#6366F1,#06B6D4);""></div>
                                {codigo}
                                <div style=""position:absolute;bottom:0;left:0;right:0;height:2px;background:linear-gradient(90deg,#6366F1,#06B6D4);""></div>
                            </div>
                            
                            <!-- Tiempo de expiración -->
                            <div style=""display:inline-flex;align-items:center;gap:8px;background:rgba(245,158,11,0.1);color:#F59E0B;padding:10px 24px;border-radius:50px;font-size:14px;font-weight:600;border:1px solid rgba(245,158,11,0.2);"">
                                <svg width=""16"" height=""16"" viewBox=""0 0 24 24"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                                    <path d=""M12 8V12L15 15M21 12C21 16.9706 16.9706 21 12 21C7.02944 21 3 16.9706 3 12C3 7.02944 7.02944 3 12 3C16.9706 3 21 7.02944 21 12Z"" stroke=""#F59E0B"" stroke-width=""1.5"" stroke-linecap=""round"" stroke-linejoin=""round""/>
                                </svg>
                                <span>EXPIRA EN 15 MINUTOS</span>
                            </div>
                        </div>
                        
                        <!-- Botón de acción -->
                        <a href=""https://boardly.com/reset-password?code={codigo}&userId={usuarioId}"" class=""btn-primary"" style=""color:white;text-decoration:none;"">
                            RESTABLECER CONTRASEÑA
                        </a>
                        
                        <!-- Instrucciones -->
                        <div style=""max-width:400px;margin:0 auto 30px;animation:fadeIn 0.8s 0.2s both;"">
                            <p style=""font-size:14px;color:#A1A1AA;line-height:1.7;"">
                                O copia y pega este enlace en tu navegador:<br>
                                <span style=""font-family:'Courier New',monospace;font-size:13px;color:#6366F1;word-break:break-all;"">
                                    https://boardly.com/reset-password?code={codigo}&userId={usuarioId}
                                </span>
                            </p>
                        </div>
                        
                        <!-- Separador dinámico -->
                        <div style=""height:1px;background:linear-gradient(90deg,transparent,#6366F1,#06B6D4,transparent);margin:40px 0;opacity:0.3;""></div>
                        
                        <!-- Aviso de seguridad destacado -->
                        <div style=""background:rgba(239,68,68,0.05);border-radius:16px;padding:24px;text-align:left;border:1px solid rgba(239,68,68,0.2);position:relative;overflow:hidden;"">
                            <div style=""position:absolute;top:0;left:0;right:0;height:2px;background:linear-gradient(90deg,#EF4444,#DC2626);""></div>
                            <div style=""display:flex;align-items:flex-start;gap:16px;"">
                                <div style=""flex-shrink:0;width:24px;height:24px;background:#EF4444;border-radius:6px;display:flex;align-items:center;justify-content:center;"">
                                    <svg width=""16"" height=""16"" viewBox=""0 0 24 24"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                                        <path d=""M12 9V11M12 15H12.01M10 4H14C16.2091 4 18 5.79086 18 8V18C18 20.2091 16.2091 22 14 22H10C7.79086 22 6 20.2091 6 18V8C6 5.79086 7.79086 4 10 4Z"" stroke=""white"" stroke-width=""1.5"" stroke-linecap=""round"" stroke-linejoin=""round""/>
                                    </svg>
                                </div>
                                <div>
                                    <h3 style=""font-size:16px;font-weight:700;color:#EF4444;margin-bottom:8px;"">IMPORTANTE</h3>
                                    <p style=""font-size:14px;color:#A1A1AA;line-height:1.6;margin:0;"">
                                        Si no solicitaste restablecer tu contraseña, ignora este email o contacta a nuestro equipo de soporte inmediatamente.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <!-- Pie de página minimalista -->
                    <div style=""padding:30px;text-align:center;border-top:1px solid #1F1F1F;background:#0A0A0A;"">
                        <p style=""font-size:12px;color:#52525B;margin:0;"">© {DateTime.Now.Year} Boardly Technologies · Todos los derechos reservados</p>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</body>
</html>
";
}
    
}