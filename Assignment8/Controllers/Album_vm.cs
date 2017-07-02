using Assignment8.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class AlbumBase
    {
        public AlbumBase()
        {
            ReleaseDate = DateTime.Now;
            
        }


        public int Id { get; set; }

        [Display(Name = "Coordinator who looks after this album")]
        public string Coordinator { get; set; }

        [Display(Name = "Album's Primary genre")]
        public string Genre { get; set; }

        [Display(Name = "Album name")]
        [Required, StringLength(200)]
        public string Name { get; set; }

        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Album image(cover art)")]
        public string UrlAlbum { get; set; }
    }


    public class AlbumWithDetails:AlbumBase
    {
       

        public AlbumWithDetails()
        {
            
            Tracks = new List<Track>();
            Artists = new List<Artist>();
        }

        [Display(Name = "Number of tracks on this ablum")]
        public IEnumerable<Track> Tracks { get; set; }

        [Display(Name = "Number of artists on this ablum")]
        public IEnumerable<Artist> Artists { get; set; }
        
    }

    public class AlbumAddForm
    {
        public AlbumAddForm()
        {
            ReleaseDate = DateTime.Now;          

        }

        [Display(Name = "Coordinator who looks after this album")]
        public string Coordinator { get; set; }

        [Display(Name = "Album's Primary genre")]
        public SelectList GenreList { get; set; }

        [Display(Name = "Album name")]
        [Required, StringLength(200)]
        public string Name { get; set; }


        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "URL to album image(cover art)")]
        public string UrlAlbum { get; set; }

        
        public MultiSelectList TrackList { get; set; }
        public MultiSelectList ArtistList { get; set; }
        

    }

    public class AlbumAdd
    {
        public AlbumAdd()
        {
            ReleaseDate = DateTime.Now;
            TrackIds = new List<int>();
            ArtistIds = new List<int>();
        }

        public string Coordinator { get; set; }

        public string Genre { get; set; }


        [Required, StringLength(200)]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public string UrlAlbum { get; set; }
        public IEnumerable<int> TrackIds { get; set; }
        public IEnumerable<int> ArtistIds { get; set; }

    }
}