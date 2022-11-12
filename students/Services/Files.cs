using Microsoft.Extensions.Hosting;
using students.IServices;

namespace students.Services
{
    public class Files : IFile
    {
        private readonly IWebHostEnvironment _hostEnviroment;
        public Files(IWebHostEnvironment hostEnviroment) {
            _hostEnviroment = hostEnviroment;
        }
        public string uploadfile(dynamic file)
        {
            string rootpath = _hostEnviroment.WebRootPath;

            string filename = Guid.NewGuid().ToString();
            var upload = Path.Combine(rootpath, @"images\courses");
            var extention = Path.GetExtension(file[0].FileName);
            using (var filestrem = new FileStream(Path.Combine(upload, filename + extention), FileMode.Create))
            {
                file[0].CopyTo(filestrem);
            }
            return filename + extention;
        }
    }
}
