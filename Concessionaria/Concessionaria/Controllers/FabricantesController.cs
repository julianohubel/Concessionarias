using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Concessionaria.Models;
using PagedList;
using PagedList.Mvc;

namespace Concessionaria.Controllers
{
    public class FabricantesController : Controller
    {
        private ConcessionariaContext db = new ConcessionariaContext();

        // GET: Fabricantes
        public ActionResult Index()
        {
            return View(db.Fabricante.OrderBy(f=> f.Nome).ToPagedList(1,10));
        }

        public  PartialViewResult PartialIndex(int? page)
        {
            IPagedList<Fabricante> fabricante = db.Fabricante.OrderBy(f => f.Nome).ToPagedList(page ?? 1, 10);

            return PartialView(fabricante);
        }

        // GET: Fabricantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = db.Fabricante.Find(id);
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }
        // GET: Fabricantes/Details/5
        public ActionResult DetailsName(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = db.Fabricante.Where(f => f.Nome == nome).SingleOrDefault();         
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View("Details",fabricante);
        }

        // GET: Fabricantes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fabricantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FabricanteID,Nome,PaisOrigem")] Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                db.Fabricante.Add(fabricante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fabricante);
        }

        // GET: Fabricantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = db.Fabricante.Find(id);
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }

        // POST: Fabricantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FabricanteID,Nome,PaisOrigem")] Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fabricante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fabricante);
        }

        // GET: Fabricantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = db.Fabricante.Find(id);
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }

        // POST: Fabricantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fabricante fabricante = db.Fabricante.Include(f=> f.Carros).Where(f=>f.FabricanteID ==  id).SingleOrDefault();
            if(fabricante.Carros.Count > 0  )
            {
                // return JavaScript(aler  alert("Há carros vinculados, exclusão não permitida"));
                //return Content("<script language='javascript' type='text/javascript'>alert('Há carros vinculados, exclusão não permitida!');</script>");
                TempData["Msg"] = "Há carros vinculados, exclusão não permitida";
                return View(fabricante);
            }
            db.Fabricante.Remove(fabricante);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
