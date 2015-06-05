using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestMvcApplication2.Models;

namespace TestMvcApplication2.Controllers
{
    public class AbmController : Controller
    {
        private ConexionBD modelo = new ConexionBD();
        
        //
        // GET: /Abm/

        public ActionResult Index()
        {
            return View();
        }

        #region Metodos definidos por mi
        public ActionResult AgregarUsuario(FormCollection collection)
        {
            modelo.Conectar();
            Usuario usuarioForm = new Usuario();

            usuarioForm.Dni = Convert.ToInt32(collection["dni"]);
            usuarioForm.Nombre = collection["nombre"];
            usuarioForm.Apellido = collection["apellido"];
            usuarioForm.FechaNacimiento = Convert.ToDateTime(collection["fecha_nacimiento"]);
            usuarioForm.Domicilio = collection["domicilio"];
            usuarioForm.Localidad = collection["localidad"];
            for (int i = 0; i < 3; i++)
            {
                usuarioForm.AgregarTelefono(new Telefono(collection["telefono" + (i + 1)], collection["tipoTel" + (i + 1)]));
                usuarioForm.AgregarMail(new Mail(collection["mail" + (i + 1)], collection["tipoMail" + (i + 1)]));
            }
            modelo.EjecutarUpdate(usuarioForm);
            modelo.Desconectar();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult CargarInfoUsuario(int id)
        {
            modelo.Conectar();
            ViewBag.Usuario = modelo.EjecutarInfoUsuario(id);
            modelo.Desconectar();
            return View();
        }
        
        public ActionResult ModificarUsuario(int id)
        {
            // TODO código para modificar el usuario
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EliminarUsuario(int id)
        {
            modelo.Conectar();
            modelo.EjecutarDelete(id);
            modelo.Desconectar();
            return RedirectToAction("Index", "Home");
        } 
        #endregion
        
        #region Metodos ya definidos
        //
        // GET: /Abm/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Abm/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Abm/Create

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
        // GET: /Abm/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Abm/Edit/5

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
        // GET: /Abm/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Abm/Delete/5

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
