using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestMvcApplication2;
using System.Collections;

namespace TestMvcApplication2.Controllers
{
    public class HomeController : Controller
    {
        private ConexionBDSingleton modelo = new ConexionBDSingleton();

        #region Metodos creados por mi
        //
        // GET: /Home/

        public ActionResult Index()  //se llama por defecto siempre con el index. 
        {
            modelo.Conectar();
            string textoConsulta = "SELECT nombre, apellido FROM Usuarios";
            ViewBag.Listado = modelo.EjecutarConsultaIndex(textoConsulta);
            modelo.Desconectar();
            return View();
        }

        public ActionResult MostrarDatosCompletos()
        {
            modelo.Conectar();
            string textoConsulta = "SELECT Usuarios.dni, Usuarios.nombre, Usuarios.apellido, Usuarios.fecha_nacimiento, Usuarios.domicilio, Localidades.nombre as localidad " 
                                    + " FROM Usuarios INNER JOIN Localidades"
                                    + " ON Usuarios.localidad = Localidades.id_localidad ;";
            ViewBag.Listado = modelo.EjecutarConsultaDetalle(textoConsulta);
            modelo.Desconectar();
            return View();
        }

        public ActionResult NuevoUsuario()
        {
            return View();
        }

        public ActionResult AgregarUsuario(FormCollection collection)
        {
            modelo.Conectar();

            string dni = collection["dni"];
            string nombre = collection["nombre"];
            string apellido = collection["apellido"];
            string fecha_nacimiento = collection["fecha_nacimiento"];
            string domicilio = collection["domicilio"];
            string localidad = collection["localidad"];

            string consultaNuevoUsuario = "INSERT INTO Usuarios VALUES ("
                + dni + ",'" + nombre + "','" + apellido + "','" + fecha_nacimiento + "','" + domicilio + "'," + localidad + ",null);";

            // --------------------------------------------------------------

            modelo.EjecutarUpdate(consultaNuevoUsuario);
            modelo.Desconectar();

            return RedirectToAction("NuevoUsuario");
        }

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
