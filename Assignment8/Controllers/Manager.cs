using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment8.Models;
using System.Security.Claims;
using System.Collections;

namespace Assignment8.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // Get AutoMapper instance
        public IMapper mapper = AutoMapperConfig.RegisterMappings();



        // Declare a property to hold the user account for the current request
        // Can use this property here in the Manager class to control logic and flow
        // Can also use this property in a controller 
        // Can also use this property in a view; for best results, 
        // near the top of the view, add this statement:
        // var userAccount = new ConditionalMenu.Controllers.UserAccount(User as System.Security.Claims.ClaimsPrincipal);
        // Then, you can use "userAccount" anywhere in the view to render content
        public UserAccount UserAccount { get; private set; }

       
        public Manager()
        {
            // If necessary, add constructor code here

            // Initialize the UserAccount property
            UserAccount = new UserAccount(HttpContext.Current.User as ClaimsPrincipal);

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }



        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()




        //Artist methods
        public IEnumerable<ArtistBase> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBase>>(ds.Artists.OrderBy(o => o.Name));
        }

       

        public ArtistWithDetails ArtistGetByIdWithDetails(int id)
        {
            var o = ds.Artists.Include("Albums").SingleOrDefault(ar=>ar.Id==id);
            return mapper.Map<Artist, ArtistWithDetails>(o);
        }

        public ArtistWithDetails ArtistAdd(ArtistAdd artist)
        {
            var addedArtist = ds.Artists.Add(mapper.Map<ArtistAdd, Artist>(artist));
            ds.SaveChanges();
            return addedArtist == null ? null:mapper.Map<Artist, ArtistWithDetails>(addedArtist);
        }


        public IEnumerable<GenreBase> GenreGetAll()
        {
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBase>>(ds.Genres.OrderBy(o=>o.Name));
        }


        //Album methods
        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBase>>(ds.Albums.OrderBy(o => o.Name));
        }

        public AlbumWithDetails AlbumGetByIdWithDetails(int id)
        {
            var o = ds.Albums.Include("Artists").Include("Tracks").SingleOrDefault(al => al.Id == id);
            return mapper.Map<Album, AlbumWithDetails>(o);
        }

        public AlbumWithDetails AlbumAdd(AlbumAdd album)
        {
            var addedAlbum = ds.Albums.Add(mapper.Map<AlbumAdd, Album>(album));

            
            foreach (var item in album.ArtistIds)
            {
                var a = ds.Artists.Find(item);
                addedAlbum.Artists.Add(a);
            }

            foreach (var item in album.TrackIds)
            {
                var a = ds.Tracks.Find(item);
                addedAlbum.Tracks.Add(a);
            }
            
            ds.SaveChanges();

            return addedAlbum == null ? null : mapper.Map<Album, AlbumWithDetails>(addedAlbum);
        }


        //Track methods
        public IEnumerable<TrackWithDetails> TarckGetAll()
        {
           var t= mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetails>>(ds.Tracks.Include("Albums").OrderBy(o => o.Name));
            return t;
        }

         public TrackWithDetails TrackGetByIdWithDetails(int id)
        {
            var o = ds.Tracks.Include("Albums").SingleOrDefault(t => t.Id == id);
            return mapper.Map<Track, TrackWithDetails>(o);
        }

        public TrackWithDetails TrackAdd(TrackAdd track)
        {
            var addedTrack = ds.Tracks.Add(mapper.Map<TrackAdd, Track>(track));


            foreach (var item in track.AlbumIds)
            {
                var a = ds.Albums.Find(item);
                addedTrack.Albums.Add(a);
            }

            ds.SaveChanges();

            return addedTrack == null ? null : mapper.Map<Track, TrackWithDetails>(addedTrack);
        }


        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Genre

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims



                LoadDataAlbum();
                LoadDataArtist();
                LoadDataGenre();
                LoadDataTrack();
                //ds.SaveChanges();
                done = true;
            }

            return done;
        }



        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool LoadDataGenre()
        {
            if (ds.Genres.Count() > 0) { return false; }
            ds.Genres.Add(new Genre() {Name = "Country" });
            ds.Genres.Add(new Genre() { Name = "Blues" });
            ds.Genres.Add(new Genre() { Name = "Popular" });
            ds.Genres.Add(new Genre() { Name = "Hip Hop" });
            ds.Genres.Add(new Genre() { Name = "Jazz" });
            ds.Genres.Add(new Genre() { Name = "Pop" });
            ds.Genres.Add(new Genre() { Name = "Reggae" });
            ds.Genres.Add(new Genre() { Name = "R&B" });
            ds.Genres.Add(new Genre() { Name = "Metal" });
            ds.Genres.Add(new Genre() { Name = "Punk" });

            //save
            ds.SaveChanges();

            return true;
        }
        public bool LoadDataArtist()
        {
            if (ds.Artists.Count() > 0) { return false; }
            
            ds.Artists.Add(new Artist()
            {
                BirthName = "Adele Adkins",
                Name = "Adele",
                BirthOrStartDate = new DateTime(1988, 5, 5),
                Genre = "Pop",
                UrlArtist = "http://queenonline.com/",
                Executive = "Trum Executive"
            });

            ds.Artists.Add(new Artist()
            {
                BirthName = "",
                Name = "Bryan Adams",
                BirthOrStartDate = new DateTime(1959, 11, 5),
                Genre = "Rock",
                UrlArtist = "http://queenonline.com/",
                Executive = "Trum Executive"
            });

            ds.Artists.Add(new Artist()
            {
                BirthName = "",
                Name = "The Beatles",
                BirthOrStartDate = new DateTime(1962, 8, 15),
                Genre = "pop",
                UrlArtist = "http://queenonline.com/",
                Executive = "Trum Executive"
            });

            //save
            ds.SaveChanges();

            return true;
        }

        public bool LoadDataAlbum()
        {
            if (ds.Albums.Count() > 0) return false;

            ds.Albums.Add(new Album() {
                Coordinator = "Hilary Coordinator",
                Genre ="soft rock",
                Name = "Joanne",
                ReleaseDate = DateTime.Parse("October 21, 2016"),
                UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/thumb/f/fd/Lady_Gaga_-_Joanne_%28Official_Album_Cover%29.png/220px-Lady_Gaga_-_Joanne_%28Official_Album_Cover%29.png"

            });

            ds.SaveChanges();

            return true;
        }

        public bool LoadDataTrack()
        {
            if (ds.Tracks.Count() > 0) return false;
            ds.Tracks.Add(new Track()
            {
                Clerk = "Noname Staff",
                Composers = "Stefani Germanotta",
                Genre = "Soft rock",
                Name = "Diamond Heart"
            });
            ds.Tracks.Add(new Track()
            {
                Clerk = "Noname Staff",
                Composers = "Germanotta Ronson ",
                Genre = "Soft rock",
                Name = "Joanne"
            });

            ds.SaveChanges();
            return true;
        }

    }

    // New "UserAccount" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it
    public class UserAccount
    {
        // Constructor, pass in the security principal
        public UserAccount(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        // Add other role-checking properties here as needed
        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}