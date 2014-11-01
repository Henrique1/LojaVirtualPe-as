using LojaVirtual.Aplicacao;
using LojaVirtual.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaVirtual.UI.Web.Areas.Painel.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioAplicacao app;
        public UsuarioController()
        {
            app = Construtor.UsuarioAplicacao();
        }
        public ActionResult Index()
        {
            return View(app.ListarTodos());
        }

        public ActionResult Cadastrar()
        {
            return View(new Usuario());
        }

        [HttpPost]
        public ActionResult Cadastrar(Usuario entidade)
        {
            if (ModelState.IsValid)
            {
                app.Salvar(entidade);
                return RedirectToAction("Index");
            }
            return View(entidade);
        }
    }
}