using Assignment8.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class TrackBase
    {
        [Display(Name = "Clerk who helps with album tasks")]
        public string Clerk { get; set; }

        [Display(Name = "Composer name(s)")]
        public string Composers { get; set; }

        [Display(Name = "Track genre")]
        public string Genre { get; set; }

        public int Id { get; set; }

        [Display(Name = "Track name")]
        [Required, StringLength(200)]
        public string Name { get; set; }
    }

    public class TrackWithDetails : TrackBase 
    {
        public TrackWithDetails()
        {
            Albums = new List<Album>();
        }

        [Display(Name ="Number of albums with this track")]
        public IEnumerable<Album> Albums { get; set; }
    }

    public class TrackAddForm
    {
        [Display(Name = "Clerk who helps with album tasks")]
        public string Clerk { get; set; }


        [Display(Name = "Composer name(s)")]
        public string Composers { get; set; }

        [Display(Name = "Track genre")]
        public SelectList GenreList { get; set; }

        [Display(Name = "Track name")]
        [Required, StringLength(200)]
        public string Name { get; set; }


        [Display(Name = "Number of albums with this track")]
        public MultiSelectList AlbumList { get; set; }

    }

    public class TrackAdd
    {

        public string Clerk { get; set; }

        public string Composers { get; set; }

        public string Genre { get; set; }        

        [Required, StringLength(200)]
        public string Name { get; set; }

        public IEnumerable<int> AlbumIds { get; set; }
    }


}