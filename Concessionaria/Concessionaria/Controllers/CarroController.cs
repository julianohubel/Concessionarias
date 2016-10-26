﻿using System;
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
    public class CarroController : Controller
    {
        private ConcessionariaContext db = new ConcessionariaContext();

        // GET: Carro
        public ActionResult Index()
        {
            var carro = db.Carro.Include(c => c.Fabricante);
            return View(carro.ToList());
        }

        // GET: Carro/Details/5
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

        // GET: Carro/Create
        public ActionResult Create()
        {
            ViewBag.FabricanteID = new SelectList(db.Fabricante, "FabricanteID", "Nome");
            return View();
        }

        // POST: Carro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarroID,FabricanteID,Nome,Ano,Combustivel")] Carro carro)
        {
            if (ModelState.IsValid)
            {
                db.Carro.Add(carro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FabricanteID = new SelectList(db.Fabricante, "FabricanteID", "Nome", carro.FabricanteID);
            return View(carro);
        }

        // GET: Carro/Edit/5
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

        // POST: Carro/Edit/5
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

        // GET: Carro/Delete/5
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

        // POST: Carro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carro carro = db.Carro.Find(id);
            db.Carro.Remove(carro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BomDia()
        {
            return Content("Bom Dia!");
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
