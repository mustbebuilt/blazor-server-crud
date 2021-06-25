using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;


namespace blazorServerOnlyVersionv1.Data
{
    public class FilmService
    { 
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly IWebHostEnvironment env;

        public FilmService(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, IWebHostEnvironment env)
        {
            _context = context;
            _webHostEnvironment = hostEnvironment;
            this.env = env;
        }

        public async Task<List<Film>> AllMovies()
        {
            //List<Film> model = _context.Films.ToList();
            //return View(model);
            return await _context.Films.ToListAsync();
    }

        public async Task<Film> GetFilmAsync(int Id)
        {
            Film film = await _context.Films.FirstOrDefaultAsync(c => c.FilmID.Equals(Id));
            return film;
        }

        public async Task<bool> InsertFilmAsync(FilmUpdateForm model)
        {
            Film newFilm = new Film
            {
                FilmTitle = model.FilmTitle,
                FilmCertificate = model.FilmCertificate,
                FilmDescription = model.FilmDescription,
                FilmImage = "none",
                FilmPrice = model.FilmPrice,
                FilmRating = model.FilmRating,
                FilmReleaseDate = model.FilmReleaseDate,
            };
            await _context.Films.AddAsync(newFilm);
                await _context.SaveChangesAsync();
                return true;

        }

        public async Task<bool> InsertFilmWithFileAsync(FilmUpdateForm model)
        {
            var tuple = UploadedFile(model);
            string uniqueFileName = tuple.Item1;
            string fileExt = tuple.Item2;
            long fileSize = tuple.Item3;
            string[] permittedExtensions = { ".gif", ".jpg", ".jpeg", ".png" };

            // 5 MB
            // 512000
            if (fileSize > 512000)
            {
                //return Ok("Bad Image Size");
                return false;
            }
            if (!permittedExtensions.Contains(fileExt))
            {
                //return Ok("Bad Image type of  " + fileExt);
                return false;
            }
            Film newFilm = new Film
            {
                FilmTitle = model.FilmTitle,
                FilmCertificate = model.FilmCertificate,
                FilmDescription = model.FilmDescription,
                FilmImage = uniqueFileName,
                FilmPrice = model.FilmPrice,
                FilmRating = model.FilmRating,
                FilmReleaseDate = model.FilmReleaseDate,
            };
            await _context.Films.AddAsync(newFilm);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateFilmAsync(Film filmToEdit)
        {
            _context.Films.Update(filmToEdit);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Film>> DeleteFilmAsync(int Id)
        {
            Film model = _context.Films.Find(Id);
            _context.Films.Remove(model);
            await _context.SaveChangesAsync();
            return await _context.Films.ToListAsync();
            //return true;

        }

        private Tuple<string, string, long> UploadedFile(FilmUpdateForm uploadedFile)
        {
            string uniqueFileName = null;
            string fileExtension = null;
            long fileSize = 0;

            if (uploadedFile.FileContent != null)
            {
                fileExtension = Path.GetExtension(uploadedFile.FileName);
                fileExtension = fileExtension.ToLowerInvariant();
                uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var path = $"{env.WebRootPath}\\{uniqueFileName}";
                var fs = System.IO.File.Create(path);
                fs.Write(uploadedFile.FileContent, 0,
        uploadedFile.FileContent.Length);
                fs.Close();
                string targetPath = $"{env.WebRootPath}//images";
                string destFile = System.IO.Path.Combine(targetPath, uniqueFileName);
                System.IO.File.Copy(path, destFile, true);

            }
            return new Tuple<string, string, long>(uniqueFileName, fileExtension, fileSize);
        }



    }
}
