using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Froky.Models;


namespace Froky.Controllers
{
    public class LoginController : Controller
    {


        public ActionResult LogIn()
        {
            return View();
        }


        // GET: Login

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(USUARIO u)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DBModel()) // Nome do entity localizado no Usuario.Context
                {
                    //var login = from a in db.usuarios select a;
                    var v = db.USUARIO.Where(a => a.E_MAIL.Equals(u.E_MAIL) && a.SENHA.Equals(u.SENHA)).FirstOrDefault();
                    if (v != null)
                    {
                        if (v.TIPO_DE_USUARIO.Equals("Administrador"))
                        {
                            Session["nomeUsuarioLogado"] = v.NOME.ToString();
                            return RedirectToAction("PaginaInicial", "PaginaInicial");
                        }
                       
                            
                        
                    }
                }
            }
            return View(u);
        }
    }
}