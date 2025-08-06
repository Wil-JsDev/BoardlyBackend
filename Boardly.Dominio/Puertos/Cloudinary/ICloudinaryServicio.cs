namespace Boardly.Dominio.Puertos.Cloudinary;

public interface ICloudinaryServicio
{
    Task<string> SubirImagenAsync(Stream archivo, string nombreImagen, CancellationToken cancellationToken);
    Task<string> SubirArchivoAsync(Stream archivo, string nombreArchivo, CancellationToken cancellationToken);

}