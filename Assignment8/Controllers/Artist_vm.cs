using Assignment8.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class ArtistBase
    {
        public ArtistBase()
        {
            BirthOrStartDate = DateTime.Now.AddYears(-20);
        }

        public int Id { get; set; }

        [Display(Name = "If applicable,artist birth name")]
        [Required, StringLength(200)]
        public string BirthName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth date or start date")]
        public DateTime BirthOrStartDate { get; set; }

        [Display(Name = "Executive who look after this artist")]
        public string Executive { get; set; }

        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }

        [Display(Name = "Artist name or stage name")]
        [Required, StringLength(200)]
        public string Name { get; set; }

        [Display(Name = "Artist photo")]
        public string UrlArtist { get; set; }
    }


    public class ArtistWithDetails:ArtistBase
    {
        public ArtistWithDetails()
        {  
            Albums = new List<Album>();
        }

        [Display(Name = "Number of albums")]
        public IEnumerable<Album> Albums { get; set; }
    }

    public class ArtistAddForm
    {
        public ArtistAddForm()
        {
            BirthOrStartDate = DateTime.Now.AddYears(-20);
           
        }

        [Display(Name = "Birth date or start date")]
        [Required, StringLength(200)]
        public string BirthName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth date or start date")]
        public DateTime BirthOrStartDate { get; set; }

        [Display(Name = "Executive who look after this artist")]
        public string Executive { get; set; }

        [Display(Name = "Artist name or stage name")]
        [Required, StringLength(200)]
        public string Name { get; set; }

        [Display(Name = "Artist photo")]
        public string UrlArtist { get; set; }

        [Display(Name = "Artist's primary genre")]
        public SelectList GenreList { get; set; }
    }

    public class ArtistAdd
    {
        [Required, StringLength(200)]
        public string BirthName { get; set; }

      
        public DateTime BirthOrStartDate { get; set; }

        public string Executive { get; set; }

        public string Genre { get; set; }


        [Required, StringLength(200)]
        public string Name { get; set; }

        public string UrlArtist { get; set; }

    }
}