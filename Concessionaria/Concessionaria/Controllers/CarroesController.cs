using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Concessionaria.Models;

namespace Concessionaria.Controllers
{
    public class CarroesController : Controller
    {
        private ConcessionariaContext db = new ConcessionariaContext();

        // GET: Carroes
        public ActionResult Index()
        {
            var carro = db.Carro.Include(c => c.Fabricante);
            return View(carro.ToList());
        }

        // GET: Carroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = db.Carro.Find(id);
            if (carro == null)
            {
                return HttpNotFound();
            }
            return View(carro);
        }

        // GET: Carroes/Create
        public ActionResult Create()
        {
            ViewBag.FabricanteID = new SelectList(db.Fabricante, "FabricanteID", "Nome");
            var carro = new Carro();
            carro.Proprietarios = new List<Proprietario>();
            var viewModel = new List<Proprietario>();            
            ViewBag.Proprietarios = db.Proprietarios.ToList();
            return View();
        }

        private void PopulateProprietarios()
        {
            var todosProprietarios = db.Proprietarios;
        }

        // POST: Carroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarroID,FabricanteID,Nome,Ano,Combustivel")] Carro carro, string[] proprietariosSelecionados)
        {
            if(proprietariosSelecionados != null)
            {
                carro.Proprietarios = new List<Proprietario>();
                foreach(string proprietario in proprietariosSelecionados)
                {
                    carro.Proprietarios.Add(db.Proprietarios.Find(int.Parse(proprietario)));
                }
            }


            if (ModelState.IsValid)
            {
                db.Carro.Add(carro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FabricanteID = new SelectList(db.Fabricante, "FabricanteID", "Nome", carro.FabricanteID);
            return View(carro);
        }

        // GET: Carroes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = db.Carro.Find(id);
            if (carro == null)
            {
                return HttpNotFound();
            }
            ViewBag.FabricanteID = new SelectList(db.Fabricante, "FabricanteID", "Nome", carro.FabricanteID);
            return View(carro);
        }

        // POST: Carroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarroID,FabricanteID,Nome,Ano,Combustivel")] Carro carro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FabricanteID = new SelectList(db.Fabricante, "FabricanteID", "Nome", carro.FabricanteID);
            return View(carro);
        }

        // GET: Carroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = db.Carro.Find(id);
            if (carro == null)
            {
                return HttpNotFound();
            }
            return View(carro);
        }

        // POST: Carroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carro carro = db.Carro.Find(id);
            db.Carro.Remove(carro);
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
