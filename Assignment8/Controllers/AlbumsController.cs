using Assignment8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class AlbumsController : Controller
    {
        private Manager m = new Manager();


        // GET: Albums
        public ActionResult Index()
        {

            return View(m.AlbumGetAll());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.AlbumGetByIdWithDetails(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {

                return View(o);
            }
           
        }

        // GET: Albums/Create
        [Authorize(Roles = "Admin,Executive,Coordinator,Clerk,Staff")]
        public ActionResult Create()
        {
            // Create a view model object
            var accountDetails = new AccountDetails();

            // Identity object "name" (i.e. not the claim)
            accountDetails.UserName = HttpContext.User.Identity.Name;

            // Work with the current User in claims-compatible mode
            var identity = User.Identity as ClaimsIdentity;

            // Now, go through the claims...

            // Get the name, if present
            //var name = identity.FindFirst(ClaimTypes.Name);
            //accountDetails.ClaimsName = name == null ? "(none)" : name.Value;

            // Get the given name, if present
            var givenName = identity.FindFirst(ClaimTypes.GivenName);
            accountDetails.ClaimsGivenName = givenName == null ? "(none)" : givenName.Value;

            // Get the surname, if present
            var surname = identity.FindFirst(ClaimTypes.Surname);
            accountDetails.ClaimsSurname = surname == null ? "(none)" : surname.Value;

            AlbumAddForm form = new AlbumAddForm();
            form.Coordinator = givenName.Value + " " + surname.Value;
            form.GenreList = new SelectList(m.GenreGetAll(), "Name","Name");
            form.ArtistList = new MultiSelectList(m.ArtistGetAll(), dataValueField: "Id", dataTextField: "Name");
            form.TrackList = new MultiSelectList(items:m.TarckGetAll(), dataValueField: "Id", dataTextField: "Name");
            return View(form);
        }

        // POST: Albums/Create
        [Authorize(Roles = "Admin,Executive,Coordinator,Clerk,Staff")]
        [HttpPost]
        public ActionResult Create(AlbumAdd album)
        {
            if(!ModelState.IsValid)
            {
                return View(album);
            }
          
            var addedAlbum = m.AlbumAdd(album);

            if(addedAlbum != null)
            {
                return RedirectToAction("Details", new { id = addedAlbum.Id });
            }
             else
            {
                return View();
            }
        }

        // GET: Albums/Edit/5
        [Authorize(Roles = "Admin,Executive,Coordinator,Clerk,Staff")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Albums/Edit/5
        [Authorize(Roles = "Admin,Executive,Coordinator,Clerk,Staff")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Albums/Delete/5
        [Authorize(Roles = "Admin,Executive,Coordinator,Clerk,Staff")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Albums/Delete/5
        [Authorize(Roles = "Admin,Executive,Coordinator,Clerk,Staff")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
