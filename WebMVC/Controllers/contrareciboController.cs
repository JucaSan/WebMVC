using ProjectModel.Entities;
using ProjectModel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class contrareciboController : Controller
    {

        BLContrarecibo bLContrarecibo = new BLContrarecibo(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        // Método para la tabla con los datos (Home)
        public ActionResult Mostrar()
        {
            List<Contrarecibo> recibo = null;
            string msj = "";
            recibo = bLContrarecibo.ObtenerContrarecibo(ref msj);
            return View(recibo);
        }


        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Agrega()
        {
            string msj = "";

            Contrarecibo recibo = new Contrarecibo()
            {
                fecha = Request.Form["txtfecha"],
                obra = Request.Form["txtobra"],
                extra = Request.Form["txtExt"],
            };


            bLContrarecibo.InsertarContrarecibo(recibo, ref msj);
            // Recargar los datos antes de redirigir a la vista Mostrar
            List<Contrarecibo> recibos = null;
            recibos = bLContrarecibo.ObtenerContrarecibo(ref msj);

            // Devolver la vista Mostrar con los datos actualizados
            return View("Mostrar", recibos);
        }

        public ActionResult Edit(string id)
        {
            string msj = "";
            ViewBag.id = id;

            Contrarecibo contra = new Contrarecibo();
            contra = bLContrarecibo.ContrareciboPorId(int.Parse(id), ref msj);
            return View(contra);
        }

        public ActionResult Editar()
        {
            string msj = "";

            Contrarecibo recibo = new Contrarecibo()
            {
                id = int.Parse(Request.Form["ID"]),
                fecha = Request.Form["txtfecha"],
                obra = Request.Form["txtobra"],
                extra = Request.Form["txtExt"],

            };


            bLContrarecibo.EditarContrarecibo(recibo, ref msj);
            // Recargar los datos antes de redirigir a la vista Mostrar
            List<Contrarecibo> recibos = null;
            recibos = bLContrarecibo.ObtenerContrarecibo(ref msj);

            // Devolver la vista Mostrar con los datos actualizados
            return View("Mostrar", recibos);

        }

        public ActionResult Eliminar(int id)
        {
            string msj = "";
            Contrarecibo recibo = new Contrarecibo()
            {
                id = id
            };

            bLContrarecibo.EliminarContrarecibo(recibo, ref msj);
            List<Contrarecibo> recibos = null;
            recibos = bLContrarecibo.ObtenerContrarecibo(ref msj);
            return View("Mostrar", recibos);
        }


    }
}