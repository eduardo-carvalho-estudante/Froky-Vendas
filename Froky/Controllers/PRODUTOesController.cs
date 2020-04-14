using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Froky.Models;

namespace Froky.Controllers
{
    public class PRODUTOesController : Controller
    {
        private DBModel db = new DBModel();

        // GET: PRODUTOes
        public ActionResult Index()
        {
            var pRODUTO = db.PRODUTO.Include(p => p.CATEGORIA).Include(p => p.FORNECEDOR);
            return View(pRODUTO.ToList());
        }

        // GET: PRODUTOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUTO pRODUTO = db.PRODUTO.Find(id);
            if (pRODUTO == null)
            {
                return HttpNotFound();
            }
            return View(pRODUTO);
        }

        // GET: PRODUTOes/Create
        public ActionResult Create()
        {
            ViewBag.FK_CATEGORIA = new SelectList(db.CATEGORIA, "ID_CATEGORIA", "CATEGORIA1");
            ViewBag.FK_FORNECEDOR = new SelectList(db.FORNECEDOR, "ID_FORNECEDOR", "CNPJ");
            return View();
        }

        // POST: PRODUTOes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase ImageFile, [Bind(Include = "ID_PRODUTO,NOME,DESCRICAO,QUANTIDADE,PRECO,IMAGEM,FK_CATEGORIA,FK_FORNECEDOR,ImageFile")] PRODUTO pRODUTO)
        {



            pRODUTO.ImageFile = ImageFile;
            string filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string extension = Path.GetExtension(ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("ddmmss") + extension;
            pRODUTO.IMAGEM = "~/Imagens/" + filename;
            filename = Path.Combine(Server.MapPath("~/Imagens/"), filename);
            pRODUTO.ImageFile.SaveAs(filename);

            if (ModelState.IsValid)
            {
                db.PRODUTO.Add(pRODUTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.Clear();

            ViewBag.FK_CATEGORIA = new SelectList(db.CATEGORIA, "ID_CATEGORIA", "CATEGORIA1", pRODUTO.FK_CATEGORIA);
            ViewBag.FK_FORNECEDOR = new SelectList(db.FORNECEDOR, "ID_FORNECEDOR", "CNPJ", pRODUTO.FK_FORNECEDOR);
            return View(pRODUTO);
        }

        // GET: PRODUTOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUTO pRODUTO = db.PRODUTO.Find(id);
            if (pRODUTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_CATEGORIA = new SelectList(db.CATEGORIA, "ID_CATEGORIA", "CATEGORIA1", pRODUTO.FK_CATEGORIA);
            ViewBag.FK_FORNECEDOR = new SelectList(db.FORNECEDOR, "ID_FORNECEDOR", "CNPJ", pRODUTO.FK_FORNECEDOR);
            return View(pRODUTO);
        }

        // POST: PRODUTOes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( HttpPostedFileBase ImageFile , [Bind(Include = "NOME,DESCRICAO,QUANTIDADE,PRECO,IMAGEM,FK_CATEGORIA,FK_FORNECEDOR")] PRODUTO pRODUTO)
        {
            pRODUTO.ImageFile = ImageFile;
            string filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string extension = Path.GetExtension(ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("ddmmss") + extension;
            pRODUTO.IMAGEM = "~/Imagens/" + filename;
            filename = Path.Combine(Server.MapPath("~/Imagens/"), filename);
            pRODUTO.ImageFile.SaveAs(filename);

            if (ModelState.IsValid)
            {
                db.Entry(pRODUTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_CATEGORIA = new SelectList(db.CATEGORIA, "ID_CATEGORIA", "CATEGORIA1", pRODUTO.FK_CATEGORIA);
            ViewBag.FK_FORNECEDOR = new SelectList(db.FORNECEDOR, "ID_FORNECEDOR", "CNPJ", pRODUTO.FK_FORNECEDOR);
            return View(pRODUTO);
        }

        // GET: PRODUTOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUTO pRODUTO = db.PRODUTO.Find(id);
            if (pRODUTO == null)
            {
                return HttpNotFound();
            }
            return View(pRODUTO);
        }

        // POST: PRODUTOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUTO pRODUTO = db.PRODUTO.Find(id);
            db.PRODUTO.Remove(pRODUTO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult CreateCategoria()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategoria([Bind(Include = "ID_CATEGORIA,CATEGORIA1")] CATEGORIA cATEGORIA)
        {

            if (ModelState.IsValid)
            {
                db.CATEGORIA.Add(cATEGORIA);
                db.SaveChanges();
                
            }
            ModelState.Clear();
            return RedirectToAction("Create");
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
