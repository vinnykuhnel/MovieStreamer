using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using movieapp.Model;

namespace movieapp.Pages
{
    public class CatalogModel : PageModel
    {
        private readonly MovieDbContext _db;

        public CatalogModel(MovieDbContext db)
        {
            _db = db;          
        }

        public IEnumerable<Movie> movies { get; set; }

        public async Task OnGet()
        {
            movies = await _db.Movies.ToListAsync();
        }
    }
}
