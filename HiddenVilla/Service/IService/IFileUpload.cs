using Microsoft.AspNetCore.Components.Forms;

namespace HiddenVilla.Service.IService
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile file);
        bool DeleteFile(string fiename);
    }
}
