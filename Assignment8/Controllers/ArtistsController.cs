using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class ArtistsController : Controller
    {
        private Manager m = new Manager();

        // GET: Artists
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.ArtistGetByIdWithDetails(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
            
        }

        // GET: Artists/Create
        [Authorize(Roles = "Admin,Executive,Coordinator,Clerk,Staff")]
        public ActionResult Create()
        {
            ArtistAddForm form = new ArtistAddForm();
            form.GenreList = new SelectList(m.GenreGetAll(), "Id","Name");

            return View(form);
        }

        // POST: Artists/Create
        [Authorize(Roles = "Admin,Executive,Coordinator,Clerk,Staff")]
        [HttpPost]
        public ActionResult Create(ArtistAdd artist)
        {
            if (!ModelState.IsValid)
            {
                return View(artist);
            }

            var addedArtist = m.ArtistAdd(artist);

            if(addedArtist != null)            
            {
                // TODO: Add insert logic here

                return RedirectToAction("Details", new { id = addedArtist.Id});
            }
            else
            {
                return View();
            }
                
          
        }

        // GET: Artists/Edit/5
        [Authorize(Roles = "Admin,Executive,Coordinator,Clerk,Staff")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Artists/Edit/5
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

        // GET: Artists/Delete/5
        [Authorize(Roles = "Admin,Executive,Coordinator,Clerk,Staff")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Artists/Delete/5
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
