using Boardly.Dominio.Configuraciones;
using Boardly.Dominio.Puertos.Cloudinary;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace Boardly.Infraestructura.Compartido.Adaptadores;

public class CloudinaryServicio : ICloudinaryServicio
{
    private CloudinaryConfiguraciones _cloudinary { get; }

    public CloudinaryServicio(IOptions<CloudinaryConfiguraciones> cloudinary)
    {
        _cloudinary = cloudinary.Value;
    }
    
    public async Task<string> SubirImagenAsync(Stream archivo, string nombreImagen, CancellationToken cancellationToken)
    {
        Cloudinary cloudinary = new (_cloudinary.CloudinaryUrl);
        ImageUploadParams image = new()
        {
            File = new FileDescription(nombreImagen, archivo),
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = true
        };
            
        var subirResultado = await cloudinary.UploadAsync(image,cancellationToken);
        
        return subirResultado.SecureUrl.ToString();
    }
}