using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Concessionaria.Models;
using Concessionaria.ViewsModel;
using PagedList;
using PagedList.Mvc;

namespace Concessionaria.Controllers
{
    public class ProprietariosController : Controller
    {
        private ConcessionariaContext db = new ConcessionariaContext();

        // GET: Proprietarios
        public ActionResult Index(string Pesquisa, int? page)
        {
            var proprietarios = db.Proprietarios
                .Where(p => p.Nome.Contains(string.IsNullOrEmpty(Pesquisa) ? p.Nome : Pesquisa))
                .OrderBy(p => p.Nome)
                .ToPagedList(page ?? 1, 10);

            if (Request.IsAjaxRequest())
            {
                return PartialView("PartialIndex", proprietarios);
            }
            return View(proprietarios);
        }

        public PartialViewResult PartialIndex(string Pesquisa, int? page)
        {
            var proprietarios = db.Proprietarios
            .Where(p => p.Nome.Contains(string.IsNullOrEmpty(Pesquisa) ? p.Nome : Pesquisa))
            .OrderBy(p => p.Nome)
            .ToPagedList(page ?? 1, 10);
            return PartialView("PartialIndex", proprietarios);
        }
        public ActionResult AutoComplete(string term)
        {
            var model = db.Proprietarios.Where(p => p.Nome.StartsWith(term)).Select(r =>
               new
               {
                   label = r.Nome
               }).Take(10);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: Proprietarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proprietario proprietario = db.Proprietarios.Find(id);
            if (proprietario == null)
            {
                return HttpNotFound();
            }
            return View(proprietario);
        }

        // GET: Proprietarios/Create
        public ActionResult Create()
        {
            var Proprietario = new Proprietario();
            Proprietario.Carros = new List<Carro>();
            PreencheProprietarioCarros(Proprietario);
            return View();
        }

        // POST: Proprietarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProprietarioID,Nome,Email,CPF,DataNascimento")] Proprietario proprietario, string[] carrosSelecionados)
        {


            proprietario.Carros = new List<Carro>();
            foreach (var item in carrosSelecionados)
            {
                proprietario.Carros.Add(db.Carro.Find(int.Parse(item)));
            }
            if (ModelState.IsValid)
            {
                db.Proprietarios.Add(proprietario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PreencheProprietarioCarros(proprietario);
            return View(proprietario);
        }

        protected void PreencheProprietarioCarros(Proprietario proprietario)
        {

            var allCarros = db.Carro;
            var propCarros = new HashSet<int>(proprietario.Carros.Select(c => c.CarroID));
            List<ProprietarioCarros> proprietarioCarros = new List<ProprietarioCarros>();

            foreach (var carro in allCarros)
            {
                proprietarioCarros.Add(new ProprietarioCarros() { CarroID = carro.CarroID, Nome = carro.Nome, Atribuido = propCarros.Contains(carro.CarroID) });
            }

            ViewBag.Carros = proprietarioCarros;

        }


        // GET: Proprietarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proprietario proprietario = db.Proprietarios.Include(p => p.Carros).Where(p => p.ProprietarioID == id).SingleOrDefault();
            PreencheProprietarioCarros(proprietario);


            if (proprietario == null)
            {
                return HttpNotFound();
            }
            return View(proprietario);
        }

        // POST: Proprietarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Proprietario proprietario, string[] carrosSelecionados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proprietario).State = EntityState.Modified;
                proprietario.Carros = new List<Carro>();
                proprietario.Carros = db.Proprietarios.Include(p => p.Carros).Where(p => p.ProprietarioID == id).SingleOrDefault().Carros;
                UpdateCarros(proprietario, carrosSelecionados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proprietario);
        }

        protected void UpdateCarros(Proprietario proprietario, string[] carrosSelecionados)
        {
            if (carrosSelecionados == null)
            {
                proprietario.Carros = new List<Carro>();
                return;
            }


            var allCarros = db.Carro;
            var proprietarioCarros = new HashSet<int>(proprietario.Carros.Select(c => c.CarroID));
            var selecionados = new HashSet<string>(carrosSelecionados);

            foreach (var item in allCarros)
            {
                if (selecionados.Contains(item.CarroID.ToString()))
                {
                    if (!proprietarioCarros.Contains(item.CarroID))
                        proprietario.Carros.Add(item);
                }
                else
                {
                    if (proprietarioCarros.Contains(item.CarroID))
                        proprietario.Carros.Remove(item);
                }
            }

        }



        // GET: Proprietarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proprietario proprietario = db.Proprietarios.Find(id);
            if (proprietario == null)
            {
                return HttpNotFound();
            }
            return View(proprietario);
        }

        // POST: Proprietarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {


            Proprietario proprietario = db.Proprietarios.Find(id);
            db.Proprietarios.Remove(proprietario);
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
