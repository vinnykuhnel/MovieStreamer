using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using movieapp.Model;
using System.Net.Http.Headers;

namespace movieapp.Pages
{
    [IgnoreAntiforgeryToken]
    public class ViewModel : PageModel
    {
        public readonly MovieDbContext _db;
        public string videoName;
        

        public ViewModel(MovieDbContext db)
        {
            _db = db;
        }

        private string movieFile { get; set; }

        public IActionResult OnGet(int ID)
        {
            Movie movie = _db.Movies.Find(ID);
            var fileStr = movie.filename;
            videoName = fileStr;
            var res = File(System.IO.File.OpenRead(fileStr), "video/mp4");
            res.EnableRangeProcessing = true;
            return res;

        }
    }
}
