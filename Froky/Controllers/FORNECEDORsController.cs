using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Froky.Models;

namespace Froky.Controllers
{
    public class FORNECEDORsController : Controller
    {
        private FornecedorModel db = new FornecedorModel();

        // GET: FORNECEDORs
        public ActionResult Index()
        {
            return View(db.FORNECEDOR.ToList());
        }

        // GET: FORNECEDORs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FORNECEDOR fORNECEDOR = db.FORNECEDOR.Find(id);
            if (fORNECEDOR == null)
            {
                return HttpNotFound();
            }
            return View(fORNECEDOR);
        }

        // GET: FORNECEDORs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FORNECEDORs/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_FORNECEDOR,CNPJ,RAZAO_SOCIAL,TELEFONE_PRIMÁRIO,TELEFONE_SECUNDÁRIO,E_MAIL")] FORNECEDOR fORNECEDOR)
        {
            if (ModelState.IsValid)
            {
                db.FORNECEDOR.Add(fORNECEDOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fORNECEDOR);
        }

        // GET: FORNECEDORs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FORNECEDOR fORNECEDOR = db.FORNECEDOR.Find(id);
            if (fORNECEDOR == null)
            {
                return HttpNotFound();
            }
            return View(fORNECEDOR);
        }

        // POST: FORNECEDORs/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_FORNECEDOR,CNPJ,RAZAO_SOCIAL,TELEFONE_PRIMÁRIO,TELEFONE_SECUNDÁRIO,E_MAIL")] FORNECEDOR fORNECEDOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fORNECEDOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fORNECEDOR);
        }

        // GET: FORNECEDORs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FORNECEDOR fORNECEDOR = db.FORNECEDOR.Find(id);
            if (fORNECEDOR == null)
            {
                return HttpNotFound();
            }
            return View(fORNECEDOR);
        }

        // POST: FORNECEDORs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FORNECEDOR fORNECEDOR = db.FORNECEDOR.Find(id);
            db.FORNECEDOR.Remove(fORNECEDOR);
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
