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
            var carro = db.Carro.Include(c => c.Fabricante).Include(c => c.Proprietarios);
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
            if (proprietariosSelecionados != null)
            {
                carro.Proprietarios = new List<Proprietario>();
                foreach (string proprietario in proprietariosSelecionados)
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
            PreencheCarrosProprietarios(carro);
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
            Carro carro = db.Carro.Include(c => c.Proprietarios).Where(c => c.CarroID == id).SingleOrDefault();
            PreencheCarrosProprietarios(carro);
            if (carro == null)
            {
                return HttpNotFound();
            }
            ViewBag.FabricanteID = new SelectList(db.Fabricante, "FabricanteID", "Nome", carro.FabricanteID);
            return View(carro);
        }

        private void PreencheCarrosProprietarios(Carro carro)
        {
            var todosProprietarios = db.Proprietarios;
            var carrosProprietarios = new HashSet<int>(carro.Proprietarios.Select(p => p.ProprietarioID));
            var viewProprietarios = new List<Concessionaria.ViewsModel.CarrosProprietarios>();
            foreach (var proprietario in todosProprietarios)
            {
                viewProprietarios.Add(new ViewsModel.CarrosProprietarios
                {
                    ProprietarioID = proprietario.ProprietarioID,
                    Nome = proprietario.Nome,
                    Atribuido = carrosProprietarios.Contains(proprietario.ProprietarioID)
                });

            }
            ViewBag.Proprietarios = viewProprietarios;
        }

        // POST: Carroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Carro carro ,int?  id, string[] proprietariosSelecionados)
        {


            if (ModelState.IsValid)
            {
                db.Entry(carro).State = EntityState.Modified;
                carro.Proprietarios = new List<Proprietario>();

                carro.Proprietarios = db.Carro.Include(p => p.Proprietarios).Where(p => p.CarroID == id).SingleOrDefault().Proprietarios;            

                UpdateProprietarios(proprietariosSelecionados, carro);                   
                
                db.SaveChanges();

                //var carroToUpdate = db.Carro.Include(c => c.Proprietarios).Where(c => c.CarroID == id).SingleOrDefault();
                //UpdateProprietarios(proprietariosSelecionados, carroToUpdate);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            PreencheCarrosProprietarios(carro);
            ViewBag.FabricanteID = new SelectList(db.Fabricante, "FabricanteID", "Nome", carro.FabricanteID);
            return View(carro);
        }

        protected void UpdateProprietarios(string[] proprietariosSelecionados, Carro carro)
        {
            if (proprietariosSelecionados == null)
            {
                carro.Proprietarios = new List<Proprietario>();
                return;
            }


            var propSelecionados = new HashSet<string>(proprietariosSelecionados);
            var propCarro = new HashSet<int>(carro.Proprietarios.Select(c => c.ProprietarioID));

            foreach (var prop in db.Proprietarios)
            {
                if (propSelecionados.Contains(prop.ProprietarioID.ToString()))
                {
                    if(!propCarro.Contains(prop.ProprietarioID))
                    {
                        carro.Proprietarios.Add(prop);
                    }
                }
                else
                {
                    if(propCarro.Contains(prop.ProprietarioID))
                    {
                        carro.Proprietarios.Remove(prop);                        
                    }
                }
            }

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
