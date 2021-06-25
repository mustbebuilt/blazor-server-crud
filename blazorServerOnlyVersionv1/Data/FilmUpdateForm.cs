using System;
using System.ComponentModel.DataAnnotations;

namespace blazorServerOnlyVersionv1.Data
{
    public class FilmUpdateForm

    {
        public int FilmID { get; set; }

        [Required(ErrorMessage = "Please provide a film name")]
        public string FilmTitle { get; set; }

        [Required(ErrorMessage = "Please provide a film certificate")]
        public string FilmCertificate { get; set; }

        [Required(ErrorMessage = "Please provide a film description")]
        public string FilmDescription { get; set; }

        public string FileName { get; set; }

        public byte[] FileContent { get; set; }

        public decimal FilmPrice { get; set; }

        public int FilmRating { get; set; }

        public DateTime FilmReleaseDate { get; set; }

    }

}
