using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestMvcApplication2;
using System.Collections;
using TestMvcApplication2.Models;
using System.Threading;
using System.Globalization;

namespace TestMvcApplication2.Controllers
{
    public class HomeController : Controller
    {
        private ConexionBD modelo = new ConexionBD();
     
        #region Metodos creados por mi
        //
        // GET: /Home/

        public ActionResult Index()  //se llama por defecto siempre con el index. 
        {
            return View();
        }
                
        public ActionResult NuevoUsuario()
        {
            return View();
        }

        public ActionResult MostrarDatosCompletos()
        {
            modelo.Conectar();
            ViewBag.Listado = modelo.EjecutarConsultaDetalle();
            modelo.Desconectar();
            return View();
        }

        public ActionResult DetallesUsuario(int id)
        {
            modelo.Conectar();
            Usuario usuarioDetalles = new Usuario();
            usuarioDetalles = modelo.EjecutarConsultaDetalleUsuario(id);
            ViewBag.Telefonos = usuarioDetalles.ListaTelefonos;
            ViewBag.Mails = usuarioDetalles.ListaMails;
            modelo.Desconectar();
            return View();
        }

        //private void Application_BeginRequest(Object source, EventArgs e)
        //{
        //    HttpApplication application = (HttpApplication)source;
        //    HttpContext context = application.Context;

        //    string culture = null;
        //    if (context.Request.UserLanguages != null && Request.UserLanguages.Length > 0)
        //    {
        //        culture = Request.UserLanguages[0];
        //        Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
        //        Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        //    }
        //}

        #endregion

        #region Metodos ya definidos

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Home/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Home/Edit/5

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

        //
        // GET: /Home/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Home/Delete/5

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

        #endregion

    }
}
