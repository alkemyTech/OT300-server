using System.IO;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IImageStorageHerlper
    {
        Task<string> UploadImageAsync(Stream imageFile,string fileName);
        Task<string> UploadImageAsync(FileStream imageFile);
    }
}