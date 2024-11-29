using HiddenVilla.Service.IService;
using Microsoft.AspNetCore.Components.Forms;

namespace HiddenVilla.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public FileUpload(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }
        public bool DeleteFile(string filename)
        {
            //bool Status = false;
            try
            {
                //also you can use
                //var path = Path.Combine(webHostEnvironment.WebRootPath, "RoomImages", FileName);
                var path = $"{webHostEnvironment.WebRootPath}\\RoomImages\\{filename}";
                if (File.Exists(path))
                {
                    File.Delete(path);
                    //Status = true ;
                    return true ;
                }
                return false ;
            }
            catch(Exception ex)  
            {
                throw ex;
            }

        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(file.Name); //for create,delete,open,move,copy file
                var FileName = Guid.NewGuid().ToString() + fileInfo.Extension; // giving another name ro file
                var FolderDirectory = $"{webHostEnvironment.WebRootPath}\\RoomImages";
                var path = Path.Combine(webHostEnvironment.WebRootPath, FolderDirectory, FileName);
                //var path = Path.Combine(webHostEnvironment.WebRootPath, "RoomImages", FileName);

                var memoryStream = new MemoryStream(); // to store file using memorystream in folder 
                await file.OpenReadStream().CopyToAsync(memoryStream);

                if (!Directory.Exists(FolderDirectory))
                {
                    Directory.CreateDirectory(FolderDirectory);
                }
                // to copy file or move file from one place to other place
                await using (var fs = new FileStream(path,FileMode.Create, FileAccess.Write)) // to store file in folder
                {
                    memoryStream.WriteTo(fs);
                }
                var Url = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host.Value}/";
                var fullPath = $"{Url}RoomImages/{FileName}";
                return fullPath;

            }
            catch(Exception ex) 
            {
                throw ex; 
            }
        }
    }
}
