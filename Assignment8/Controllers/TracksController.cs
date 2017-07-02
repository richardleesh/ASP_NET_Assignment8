using Assignment8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    
    public class TracksController : Controller
    {
        private Manager m = new Manager();

        // GET: Tracks
        public ActionResult Index()
        {
            return View(m.TarckGetAll());
        }

        // GET: Tracks/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.TrackGetByIdWithDetails(id.GetValueOrDefault());
            if (o == null) return HttpNotFound();
            else return View(o);
        }

        [Authorize(Roles = "Admin,Executive,Coordinator,Clerk,Staff")]
        // GET: Tracks/Create
        public ActionResult Create()
        {
            // Create a view model object
            var accountDetails = new AccountDetails();

            // Identity object "name" (i.e. not the claim)
            accountDetails.UserName =HttpContext.User.Identity.Name;

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

            // Get the email, if present
           // var email = identity.FindFirst(ClaimTypes.Email);
            //accountDetails.ClaimsEmail = email == null ? "(none)" : email.Value;



            TrackAddForm form = new TrackAddForm();
            form.Clerk = givenName.Value + " " + surname.Value;
            form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
            form.AlbumList = new MultiSelectList(m.AlbumGetAll(), dataValueField: "Id", dataTextField: "Name");
            return View(form);
        }

        // POST: Tracks/Create

        [Authorize(Roles = "Admin,Executive,Coordinator,Clerk,Staff")]
        [HttpPost]
        public ActionResult Create(TrackAdd track)
        {
            if (!ModelState.IsValid)
            {
                return View(track);
            }
            var addedTrack = m.TrackAdd(track);

            if(addedTrack != null)
            { 
                return RedirectToAction("Details",new { id = addedTrack.Id});
            }
            else
            {
                return View();
            }
        }

        // GET: Tracks/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tracks/Edit/5
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

        // GET: Tracks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tracks/Delete/5
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
