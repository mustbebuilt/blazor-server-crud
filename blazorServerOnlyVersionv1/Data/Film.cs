using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace blazorServerOnlyVersionv1.Data
{
    public class Film

    {
        [Key]
        public int FilmID { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string FilmTitle { get; set; }

        [Required]
        [Column(TypeName = "varchar(5)")]
        public string FilmCertificate { get; set; }

        [Column(TypeName = "text")]
        public string FilmDescription { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string FilmImage { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal FilmPrice { get; set; }

        public int FilmRating { get; set; }

        [DataType(DataType.Date)]
        public DateTime FilmReleaseDate { get; set; }

    }

}
