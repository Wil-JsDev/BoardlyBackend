namespace Boardly.Dominio.Puertos.Cloudinary;

public interface ICloudinaryServicio
{
    Task<string> SubirImagenAsync(Stream archivo, string nombreImagen, CancellationToken cancellationToken);
}