using Microsoft.AspNetCore.Http;
using RestASPNET.Data.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RestASPNET.Business.Implementations
{
    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly List<string> allowedExtensions = new List<string>()
        {
            ".pdf",".png",".jpeg",".jpg"
        };
        private readonly string basePath;
        private readonly IHttpContextAccessor _context;

        public FileBusinessImplementation(IHttpContextAccessor context)
        {
            _context = context;
            basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }

        public byte[] GetFile(string fileName)
        {
            var filePath = basePath + fileName;
            return File.ReadAllBytes(filePath);
        }

        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            FileDetailVO fileDetail = new FileDetailVO();

            var fileType = Path.GetExtension(file.FileName);
            var baseUrl = _context.HttpContext.Request.Host;

            if (allowedExtensions.Exists(e => fileType.ToLower().Equals(e.ToLower())))
            {
                var docName = Path.GetFileName(file.FileName);
                if (file is not null && file.Length > 0)
                {
                    var destination = Path.Combine(basePath, "", docName);
                    fileDetail.DocumentName = docName;
                    fileDetail.DocType = fileType;
                    fileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1/" + fileDetail.DocumentName);

                    using var stream = new FileStream(destination, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
                else
                    return null;
            }
            else 
                return null;

            return fileDetail;
        }

        public async Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDetailVO> list = new List<FileDetailVO>();

            foreach (var file in files)
            {
                list.Add(await SaveFileToDisk(file));

            }

            return list.Count == 0 ? null : list;
        }
    }
}
