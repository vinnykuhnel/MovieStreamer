using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.IO;
using movieapp.Model;

namespace movieapp.Pages
{
    [IgnoreAntiforgeryToken]
    [DisableRequestSizeLimit]
    public class IndexModel : PageModel
    {
        private MovieDbContext _db;

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        private IHostEnvironment _environment;
        public IndexModel(IHostEnvironment environment, MovieDbContext db)
        {
            _environment = environment;
            _db = db;
        }

        
        

        public void OnGet()
        {

        }

        

        public async Task OnPostAsync()
        {
            if (UploadedFile == null || UploadedFile.Length == 0)
            {
                Console.WriteLine("File Null");
                return;
            }

            

            Console.WriteLine(UploadedFile.FileName);
            var file = Path.Combine(_environment.ContentRootPath, "uploads", String.Concat(DateTime.Now.ToString("yyyy-MM-ddTHH꞉mm꞉ss"), "-", UploadedFile.FileName));
            using (var filestream = new FileStream(file, FileMode.Create))
            {
                await UploadedFile.CopyToAsync(filestream);
            }
            Movie newMovie = new Movie()
            {
                Name = UploadedFile.FileName.Split('.')[0],
                filename = file
            };
            
        
            await _db.Movies.AddAsync(newMovie);
            await _db.SaveChangesAsync();

            
        }
    }
}
