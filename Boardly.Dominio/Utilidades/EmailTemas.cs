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
	<title>Confirma tu cuenta - Boardly</title>
	<link href=""https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700;800;900&display=swap""
		rel=""stylesheet"">
	<style>
		/* Reset */
		* {{
			margin: 0;
			padding: 0;
			box-sizing: border-box;
		}}

		body {{
			font-family: 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
			line-height: 1.6;
			color: #ffffff;
			background: linear-gradient(135deg, #0A0A0A 0%, #171717 50%, #0A0A0A 100%);
			margin: 0;
			padding: 0;
			-webkit-font-smoothing: antialiased;
			-moz-osx-font-smoothing: grayscale;
		}}

		.email-container {{
			background: linear-gradient(135deg, #0A0A0A 0%, #171717 50%, #0A0A0A 100%);
			min-height: 100vh;
			padding: 40px 20px;
		}}

		.container {{
			max-width: 600px;
			margin: 0 auto;
			background: rgba(38, 38, 38, 0.6);
			backdrop-filter: blur(20px);
			border: 1px solid rgba(82, 82, 82, 0.5);
			border-radius: 24px;
			overflow: hidden;
			box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.25);
		}}

		.header {{
			background: linear-gradient(135deg, #6366F1 0%, #06B6D4 50%, #10B981 100%);
			padding: 48px 32px;
			text-align: center;
			position: relative;
			overflow: hidden;
		}}

		.header::before {{
			content: '';
			position: absolute;
			top: 0;
			left: 0;
			right: 0;
			bottom: 0;
			background: rgba(255, 255, 255, 0.1);
			backdrop-filter: blur(10px);
			z-index: 1;
		}}

		.header-content {{
			position: relative;
			z-index: 2;
		}}

		.logo {{
			width: 80px;
			height: 80px;
			background: rgba(255, 255, 255, 0.2);
			border-radius: 20px;
			margin: 0 auto 24px;
			display: flex;
			align-items: center;
			justify-content: center;
			box-shadow: 0 8px 32px rgba(0, 0, 0, 0.2);
			border: 1px solid rgba(255, 255, 255, 0.1);
		}}

		.logo-text {{
			font-size: 32px;
			font-weight: 800;
			color: white;
			text-shadow: 0 2px 4px rgba(0, 0, 0, 0.3);
		}}

		.brand-name {{
			font-size: 36px;
			font-weight: 800;
			margin-bottom: 8px;
			background: linear-gradient(135deg, #ffffff 0%, #f5f5f5 100%);
			-webkit-background-clip: text;
			-webkit-text-fill-color: transparent;
			background-clip: text;
			text-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
		}}

		.brand-tagline {{
			font-size: 16px;
			opacity: 0.9;
			font-weight: 500;
			color: rgba(255, 255, 255, 0.9);
		}}

		.content {{
			padding: 48px 32px;
			background: rgba(23, 23, 23, 0.6);
			backdrop-filter: blur(20px);
		}}

		.welcome-section {{
			text-align: center;
			margin-bottom: 40px;
		}}

		.welcome-title {{
			font-size: 28px;
			font-weight: 700;
			margin-bottom: 16px;
			background: linear-gradient(135deg, #ffffff 0%, #d4d4d4 100%);
			-webkit-background-clip: text;
			-webkit-text-fill-color: transparent;
			background-clip: text;
		}}

		.welcome-text {{
			color: #a3a3a3;
			font-size: 16px;
			line-height: 1.6;
			max-width: 480px;
			margin: 0 auto;
		}}

		.verification-container {{
			background: rgba(38, 38, 38, 0.8);
			border: 1px solid rgba(82, 82, 82, 0.5);
			border-radius: 24px;
			padding: 40px 32px;
			text-align: center;
			margin: 40px 0;
			position: relative;
			overflow: hidden;
		}}

		.verification-container::before {{
			content: '';
			position: absolute;
			top: 0;
			left: 0;
			right: 0;
			height: 2px;
			background: linear-gradient(90deg, transparent, rgba(99, 102, 241, 0.8), rgba(6, 182, 212, 0.8), rgba(16, 185, 129, 0.8), transparent);
		}}

		.verification-label {{
			font-size: 14px;
			color: #a3a3a3;
			font-weight: 600;
			margin-bottom: 20px;
			text-transform: uppercase;
			letter-spacing: 2px;
		}}

		.verification-code {{
			font-size: 56px;
			font-weight: 800;
			letter-spacing: 12px;
			margin: 24px 0 32px;
			background: linear-gradient(135deg, #6366F1 0%, #06B6D4 50%, #10B981 100%);
			-webkit-background-clip: text;
			-webkit-text-fill-color: transparent;
			background-clip: text;
			font-family: 'Inter', monospace;
			text-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
		}}

		.verification-note {{
			font-size: 14px;
			color: #737373;
			background: rgba(99, 102, 241, 0.1);
			border: 1px solid rgba(99, 102, 241, 0.2);
			border-radius: 16px;
			padding: 20px;
			margin-top: 20px;
			line-height: 1.5;
		}}

		.instructions {{
			background: rgba(38, 38, 38, 0.6);
			border: 1px solid rgba(82, 82, 82, 0.3);
			border-radius: 20px;
			padding: 32px;
			margin: 40px 0;
		}}

		.instructions-title {{
			font-size: 20px;
			font-weight: 700;
			margin-bottom: 20px;
			color: #ffffff;
		}}

		.instructions ol {{
			color: #d4d4d4;
			font-size: 15px;
			line-height: 1.8;
			padding-left: 24px;
		}}

		.instructions li {{
			margin-bottom: 12px;
			font-weight: 500;
		}}

		.security-notice {{
			background: linear-gradient(135deg, rgba(239, 68, 68, 0.1) 0%, rgba(239, 68, 68, 0.05) 100%);
			border: 1px solid rgba(239, 68, 68, 0.2);
			border-radius: 20px;
			padding: 32px;
			margin: 40px 0;
			text-align: center;
		}}

		.security-icon {{
			width: 56px;
			height: 56px;
			background: rgba(239, 68, 68, 0.2);
			border-radius: 16px;
			margin: 0 auto 20px;
			display: flex;
			align-items: center;
			justify-content: center;
		}}

		.security-title {{
			font-size: 18px;
			font-weight: 700;
			color: #EF4444;
			margin-bottom: 12px;
		}}

		.security-text {{
			font-size: 14px;
			color: #a3a3a3;
			line-height: 1.6;
		}}

		.footer {{
			background: rgba(10, 10, 10, 0.8);
			padding: 40px 32px;
			text-align: center;
			border-top: 1px solid rgba(82, 82, 82, 0.3);
		}}

		.footer-content {{
			margin-bottom: 32px;
		}}

		.footer-title {{
			font-size: 18px;
			font-weight: 700;
			margin-bottom: 12px;
			color: #ffffff;
		}}

		.footer-text {{
			font-size: 14px;
			color: #737373;
			line-height: 1.6;
			max-width: 400px;
			margin: 0 auto;
		}}

		.footer-links {{
			display: flex;
			justify-content: center;
			gap: 32px;
			margin: 32px 0;
			flex-wrap: wrap;
		}}

		.footer-links a {{
			color: #6366F1;
			text-decoration: none;
			font-size: 14px;
			font-weight: 600;
			transition: color 0.3s ease;
		}}

		.footer-links a:hover {{
			color: #10B981;
		}}

		.footer-bottom {{
			border-top: 1px solid rgba(82, 82, 82, 0.3);
			padding-top: 32px;
			font-size: 12px;
			color: #525252;
			line-height: 1.5;
		}}

		.footer-bottom p {{
			margin-bottom: 8px;
		}}

		/* Responsive */
		@media (max-width: 640px) {{
			.email-container {{
				padding: 20px 16px;
			}}

			.container {{
				border-radius: 16px;
			}}

			.header {{
				padding: 40px 24px;
			}}

			.content {{
				padding: 40px 24px;
			}}

			.verification-code {{
				font-size: 42px;
				letter-spacing: 8px;
			}}

			.brand-name {{
				font-size: 28px;
			}}

			.welcome-title {{
				font-size: 24px;
			}}

			.footer {{
				padding: 32px 24px;
			}}

			.footer-links {{
				gap: 20px;
				flex-direction: column;
				align-items: center;
			}}
		}}
	</style>
</head>

<body>
	<div class=""email-container"">
		<div class=""container"">
			<!-- Header -->
			<div class=""header"">
				<div class=""header-content"">
					<div class=""logo"">
						<div class=""logo-text"">B</div>
					</div>
					<h1 class=""brand-name"">Boardly</h1>
					<p class=""brand-tagline"">Organiza tus proyectos fácilmente</p>
				</div>
			</div>

			<!-- Content -->
			<div class=""content"">
				<div class=""welcome-section"">
					<h2 class=""welcome-title"">¡Bienvenido a Boardly!</h2>
					<p class=""welcome-text"">
						Estás a un paso de comenzar a organizar tus proyectos de manera simple y efectiva.
						Usa el código de verificación a continuación para confirmar tu dirección de email y
						acceder a todas las funcionalidades de la plataforma.
					</p>
				</div>

				<!-- Verification Code Container -->
				<div class=""verification-container"">
					<div class=""verification-label"">Código de Verificación</div>
					<div class=""verification-code"">{codigo}</div>
					<div class=""verification-note"">
						<strong>Este código expira en 15 minutos</strong><br>
						Por tu seguridad, no compartas este código con nadie más.
					</div>
				</div>

				<!-- Instructions -->
				<div class=""instructions"">
					<h3 class=""instructions-title"">Cómo confirmar tu cuenta:</h3>
					<ol>
						<li>Regresa a la aplicación Boardly en tu navegador</li>
						<li>Ingresa el código de 6 dígitos mostrado arriba en el campo correspondiente</li>
						<li>Haz clic en ""Verificar Código"" para completar el proceso</li>
						<li>¡Listo! Tu cuenta estará confirmada y podrás comenzar a crear proyectos</li>
					</ol>
				</div>

				<!-- Security Notice -->
				<div class=""security-notice"">
					<div class=""security-icon"">
						<svg width=""28"" height=""28"" viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""2""
							stroke-linecap=""round"" stroke-linejoin=""round"" style=""color: #EF4444;"">
							<path d=""M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z"" />
							<path d=""M9 12l2 2 4-4"" />
						</svg>
					</div>
					<h4 class=""security-title"">Importante: Seguridad de tu cuenta</h4>
					<p class=""security-text"">
						Si no solicitaste este código de verificación, puedes ignorar este email de forma segura.
						Tu cuenta permanecerá protegida. Este código es válido únicamente por 15 minutos.
					</p>
				</div>
			</div>

			<!-- Footer -->
			<div class=""footer"">
				<div class=""footer-content"">
					<h4 class=""footer-title"">¿Necesitas ayuda?</h4>
					<p class=""footer-text"">
						Si tienes problemas para verificar tu cuenta o necesitas asistencia,
						nuestro equipo de soporte está disponible para ayudarte.
					</p>
				</div>

				<div class=""footer-links"">
					<a href=""#"">Centro de Ayuda</a>
					<a href=""#"">Contactar Soporte</a>
					<a href=""#"">Tutoriales</a>
					<a href=""#"">Estado del Servicio</a>
				</div>

				<div class=""footer-bottom"">
					<p>© 2025 Boardly. Todos los derechos reservados.</p>
					<p>Gestión de proyectos simplificada.</p>
					<p>Este es un email automático, por favor no respondas a esta dirección.</p>
				</div>
			</div>
		</div>
	</div>
</body>

</html>";
    }

    public static string OlvidarContrasena(string codigo, Guid usuarioId)
    {
                return $@"<!DOCTYPE html>
<html lang=""es"">

<head>
	<meta charset=""UTF-8"">
	<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
	<title>Confirma tu cuenta - Boardly</title>
	<link href=""https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700;800;900&display=swap""
		rel=""stylesheet"">
	<style>
		/* Reset */
		* {{
			margin: 0;
			padding: 0;
			box-sizing: border-box;
		}}

		body {{
			font-family: 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
			line-height: 1.6;
			color: #ffffff;
			background: linear-gradient(135deg, #0A0A0A 0%, #171717 50%, #0A0A0A 100%);
			margin: 0;
			padding: 0;
			-webkit-font-smoothing: antialiased;
			-moz-osx-font-smoothing: grayscale;
		}}

		.email-container {{
			background: linear-gradient(135deg, #0A0A0A 0%, #171717 50%, #0A0A0A 100%);
			min-height: 100vh;
			padding: 40px 20px;
		}}

		.container {{
			max-width: 600px;
			margin: 0 auto;
			background: rgba(38, 38, 38, 0.6);
			backdrop-filter: blur(20px);
			border: 1px solid rgba(82, 82, 82, 0.5);
			border-radius: 24px;
			overflow: hidden;
			box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.25);
		}}

		.header {{
			background: linear-gradient(135deg, #6366F1 0%, #06B6D4 50%, #10B981 100%);
			padding: 48px 32px;
			text-align: center;
			position: relative;
			overflow: hidden;
		}}

		.header::before {{
			content: '';
			position: absolute;
			top: 0;
			left: 0;
			right: 0;
			bottom: 0;
			background: rgba(255, 255, 255, 0.1);
			backdrop-filter: blur(10px);
			z-index: 1;
		}}

		.header-content {{
			position: relative;
			z-index: 2;
		}}

		.logo {{
			width: 80px;
			height: 80px;
			background: rgba(255, 255, 255, 0.2);
			border-radius: 20px;
			margin: 0 auto 24px;
			display: flex;
			align-items: center;
			justify-content: center;
			box-shadow: 0 8px 32px rgba(0, 0, 0, 0.2);
			border: 1px solid rgba(255, 255, 255, 0.1);
		}}

		.logo-text {{
			font-size: 32px;
			font-weight: 800;
			color: white;
			text-shadow: 0 2px 4px rgba(0, 0, 0, 0.3);
		}}

		.brand-name {{
			font-size: 36px;
			font-weight: 800;
			margin-bottom: 8px;
			background: linear-gradient(135deg, #ffffff 0%, #f5f5f5 100%);
			-webkit-background-clip: text;
			-webkit-text-fill-color: transparent;
			background-clip: text;
			text-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
		}}

		.brand-tagline {{
			font-size: 16px;
			opacity: 0.9;
			font-weight: 500;
			color: rgba(255, 255, 255, 0.9);
		}}

		.content {{
			padding: 48px 32px;
			background: rgba(23, 23, 23, 0.6);
			backdrop-filter: blur(20px);
		}}

		.welcome-section {{
			text-align: center;
			margin-bottom: 40px;
		}}

		.welcome-title {{
			font-size: 28px;
			font-weight: 700;
			margin-bottom: 16px;
			background: linear-gradient(135deg, #ffffff 0%, #d4d4d4 100%);
			-webkit-background-clip: text;
			-webkit-text-fill-color: transparent;
			background-clip: text;
		}}

		.welcome-text {{
			color: #a3a3a3;
			font-size: 16px;
			line-height: 1.6;
			max-width: 480px;
			margin: 0 auto;
		}}

		.verification-container {{
			background: rgba(38, 38, 38, 0.8);
			border: 1px solid rgba(82, 82, 82, 0.5);
			border-radius: 24px;
			padding: 40px 32px;
			text-align: center;
			margin: 40px 0;
			position: relative;
			overflow: hidden;
		}}

		.verification-container::before {{
			content: '';
			position: absolute;
			top: 0;
			left: 0;
			right: 0;
			height: 2px;
			background: linear-gradient(90deg, transparent, rgba(99, 102, 241, 0.8), rgba(6, 182, 212, 0.8), rgba(16, 185, 129, 0.8), transparent);
		}}

		.verification-label {{
			font-size: 14px;
			color: #a3a3a3;
			font-weight: 600;
			margin-bottom: 20px;
			text-transform: uppercase;
			letter-spacing: 2px;
		}}

		.verification-code {{
			font-size: 56px;
			font-weight: 800;
			letter-spacing: 12px;
			margin: 24px 0 32px;
			background: linear-gradient(135deg, #6366F1 0%, #06B6D4 50%, #10B981 100%);
			-webkit-background-clip: text;
			-webkit-text-fill-color: transparent;
			background-clip: text;
			font-family: 'Inter', monospace;
			text-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
		}}

		.verification-note {{
			font-size: 14px;
			color: #737373;
			background: rgba(99, 102, 241, 0.1);
			border: 1px solid rgba(99, 102, 241, 0.2);
			border-radius: 16px;
			padding: 20px;
			margin-top: 20px;
			line-height: 1.5;
		}}

		.instructions {{
			background: rgba(38, 38, 38, 0.6);
			border: 1px solid rgba(82, 82, 82, 0.3);
			border-radius: 20px;
			padding: 32px;
			margin: 40px 0;
		}}

		.instructions-title {{
			font-size: 20px;
			font-weight: 700;
			margin-bottom: 20px;
			color: #ffffff;
		}}

		.instructions ol {{
			color: #d4d4d4;
			font-size: 15px;
			line-height: 1.8;
			padding-left: 24px;
		}}

		.instructions li {{
			margin-bottom: 12px;
			font-weight: 500;
		}}

		.security-notice {{
			background: linear-gradient(135deg, rgba(239, 68, 68, 0.1) 0%, rgba(239, 68, 68, 0.05) 100%);
			border: 1px solid rgba(239, 68, 68, 0.2);
			border-radius: 20px;
			padding: 32px;
			margin: 40px 0;
			text-align: center;
		}}

		.security-icon {{
			width: 56px;
			height: 56px;
			background: rgba(239, 68, 68, 0.2);
			border-radius: 16px;
			margin: 0 auto 20px;
			display: flex;
			align-items: center;
			justify-content: center;
		}}

		.security-title {{
			font-size: 18px;
			font-weight: 700;
			color: #EF4444;
			margin-bottom: 12px;
		}}

		.security-text {{
			font-size: 14px;
			color: #a3a3a3;
			line-height: 1.6;
		}}

		.footer {{
			background: rgba(10, 10, 10, 0.8);
			padding: 40px 32px;
			text-align: center;
			border-top: 1px solid rgba(82, 82, 82, 0.3);
		}}

		.footer-content {{
			margin-bottom: 32px;
		}}

		.footer-title {{
			font-size: 18px;
			font-weight: 700;
			margin-bottom: 12px;
			color: #ffffff;
		}}

		.footer-text {{
			font-size: 14px;
			color: #737373;
			line-height: 1.6;
			max-width: 400px;
			margin: 0 auto;
		}}

		.footer-links {{
			display: flex;
			justify-content: center;
			gap: 32px;
			margin: 32px 0;
			flex-wrap: wrap;
		}}

		.footer-links a {{
			color: #6366F1;
			text-decoration: none;
			font-size: 14px;
			font-weight: 600;
			transition: color 0.3s ease;
		}}

		.footer-links a:hover {{
			color: #10B981;
		}}

		.footer-bottom {{
			border-top: 1px solid rgba(82, 82, 82, 0.3);
			padding-top: 32px;
			font-size: 12px;
			color: #525252;
			line-height: 1.5;
		}}

		.footer-bottom p {{
			margin-bottom: 8px;
		}}

		/* Responsive */
		@media (max-width: 640px) {{
			.email-container {{
				padding: 20px 16px;
			}}

			.container {{
				border-radius: 16px;
			}}

			.header {{
				padding: 40px 24px;
			}}

			.content {{
				padding: 40px 24px;
			}}

			.verification-code {{
				font-size: 42px;
				letter-spacing: 8px;
			}}

			.brand-name {{
				font-size: 28px;
			}}

			.welcome-title {{
				font-size: 24px;
			}}

			.footer {{
				padding: 32px 24px;
			}}

			.footer-links {{
				gap: 20px;
				flex-direction: column;
				align-items: center;
			}}
		}}
	</style>
</head>

<body>
	<div class=""email-container"">
		<div class=""container"">
			<!-- Header -->
			<div class=""header"">
				<div class=""header-content"">
					<div class=""logo"">
						<div class=""logo-text"">B</div>
					</div>
					<h1 class=""brand-name"">Boardly</h1>
					<p class=""brand-tagline"">Organiza tus proyectos fácilmente</p>
				</div>
			</div>

			<!-- Content -->
			<div class=""content"">
				<div class=""welcome-section"">
					<h2 class=""welcome-title"">¡Bienvenido a Boardly!</h2>
					<p class=""welcome-text"">
						Estás a un paso de comenzar a organizar tus proyectos de manera simple y efectiva.
						Usa el código de verificación a continuación para confirmar tu dirección de email y
						acceder a todas las funcionalidades de la plataforma.
					</p>
				</div>

				<!-- Verification Code Container -->
				<div class=""verification-container"">
					<div class=""verification-label"">Código de Verificación</div>
					<div class=""verification-code"">{codigo}</div>
					<div class=""verification-note"">
						<strong>Este código expira en 15 minutos</strong><br>
						Por tu seguridad, no compartas este código con nadie más.
					</div>
				</div>

				<!-- Instructions -->
				<div class=""instructions"">
					<h3 class=""instructions-title"">Cómo confirmar tu cuenta:</h3>
					<ol>
						<li>Regresa a la aplicación Boardly en tu navegador</li>
						<li>Ingresa el código de 6 dígitos mostrado arriba en el campo correspondiente</li>
						<li>Haz clic en ""Verificar Código"" para completar el proceso</li>
						<li>¡Listo! Tu cuenta estará confirmada y podrás comenzar a crear proyectos</li>
					</ol>
				</div>

				<!-- Security Notice -->
				<div class=""security-notice"">
					<div class=""security-icon"">
						<svg width=""28"" height=""28"" viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""2""
							stroke-linecap=""round"" stroke-linejoin=""round"" style=""color: #EF4444;"">
							<path d=""M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z"" />
							<path d=""M9 12l2 2 4-4"" />
						</svg>
					</div>
					<h4 class=""security-title"">Importante: Seguridad de tu cuenta</h4>
					<p class=""security-text"">
						Si no solicitaste este código de verificación, puedes ignorar este email de forma segura.
						Tu cuenta permanecerá protegida. Este código es válido únicamente por 15 minutos.
					</p>
				</div>
			</div>

			<!-- Footer -->
			<div class=""footer"">
				<div class=""footer-content"">
					<h4 class=""footer-title"">¿Necesitas ayuda?</h4>
					<p class=""footer-text"">
						Si tienes problemas para verificar tu cuenta o necesitas asistencia,
						nuestro equipo de soporte está disponible para ayudarte.
					</p>
				</div>

				<div class=""footer-links"">
					<a href=""#"">Centro de Ayuda</a>
					<a href=""#"">Contactar Soporte</a>
					<a href=""#"">Tutoriales</a>
					<a href=""#"">Estado del Servicio</a>
				</div>

				<div class=""footer-bottom"">
					<p>© 2025 Boardly. Todos los derechos reservados.</p>
					<p>Gestión de proyectos simplificada.</p>
					<p>Este es un email automático, por favor no respondas a esta dirección.</p>
				</div>
			</div>
		</div>
	</div>
</body>

</html>";
    }
    
}