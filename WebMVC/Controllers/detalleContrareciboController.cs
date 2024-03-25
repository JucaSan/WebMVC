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
    public class detalleContrareciboController : Controller
    {

        BLDetalleContrarecibo bLdetalleContrarecibo = new BLDetalleContrarecibo(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        // Método para la tabla con los datos (Home)
        public ActionResult Mostrar()
        {
            List<detalleContrarecibo> recibo = null;
            string msj = "";
            recibo = bLdetalleContrarecibo.ObtenerDetaleContrarecibo(ref msj);
            return View(recibo);
        }


        public ActionResult Create()
        {
            // Mensaje de referencia
            string msj = "";

            // Lista de datos para el DropDownList
            BLNota bLnotas = new BLNota(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            List<nota> notas = null;
            notas = bLnotas.Obtenernota(ref msj);

            BLContrarecibo bLcontra = new BLContrarecibo(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            List<Contrarecibo> contrarecibos = null;
            contrarecibos = bLcontra.ObtenerContrarecibo(ref msj);

            // Lista de Items con los datos anteriores
            List<SelectListItem> items = new List<SelectListItem>();
            List<SelectListItem> items2 = new List<SelectListItem>();

            // Llenado de la lista SelectedItem
            foreach (nota c in notas)
            {
                items.Add(new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{"Numero de nota: " + c.Numeronota} - {" Fecha de nota: " + c.fecha}"
                });
            }
            foreach (Contrarecibo c in contrarecibos)
            {
                items2.Add(new SelectListItem
                {
                    Value = c.id.ToString(),
                    Text = $"{"Fecha de contrarecibo: " + c.fecha} - {"Obra: " + c.obra}",
                });
            }
            ViewBag.listaDrop = items;
            ViewBag.listaDrop2 = items2;
            return View();
        }

        public ActionResult Agregar()
        {
            string msj = "";

            detalleContrarecibo recibo = new detalleContrarecibo()
            {
                contrarecibo = Request.Form["txtcontrarecibo"],
                nota = Request.Form["txtnota"],
                total = Request.Form["txttotal"],
                pagada = Request.Form["txtpagada"],
            };


            bLdetalleContrarecibo.InsertarDetalleContrarecibo(recibo, ref msj);
            // Recargar los datos antes de redirigir a la vista Mostrar
            List<detalleContrarecibo> recibos = null;
            recibos = bLdetalleContrarecibo.ObtenerDetaleContrarecibo(ref msj);

            // Devolver la vista Mostrar con los datos actualizados
            return View("Mostrar", recibos);
        }

        public ActionResult Edit(string id)
        {
            // Mensaje de referencia
            string msj = "";

            // Lista de datos para el DropDownList
            BLNota bLnotas = new BLNota(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            List<nota> notas = null;
            notas = bLnotas.Obtenernota(ref msj);

            BLContrarecibo bLcontra = new BLContrarecibo(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            List<Contrarecibo> contrarecibos = null;
            contrarecibos = bLcontra.ObtenerContrarecibo(ref msj);

            // Lista de Items con los datos anteriores
            List<SelectListItem> items = new List<SelectListItem>();
            List<SelectListItem> items2 = new List<SelectListItem>();

            // Llenado de la lista SelectedItem
            foreach (nota c in notas)
            {
                items.Add(new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{"Numero de nota: "+c.Numeronota} - {" Fecha de nota: "+c.fecha}"
                }) ;
            }
            foreach (Contrarecibo c in contrarecibos)
            {
                items2.Add(new SelectListItem
                {
                    Value = c.id.ToString(),
                    Text = $"{"Fecha de contrarecibo: "+c.fecha} - {"Obra: "+c.obra}",
                });
            }
            ViewBag.listaDrop = items;
            ViewBag.listaDrop2 = items2;
            ViewBag.id = id;

            detalleContrarecibo contra = new detalleContrarecibo();
            contra = bLdetalleContrarecibo.detallereciboPorId(int.Parse(id), ref msj);
            return View(contra);
        }

        public ActionResult Editar()
        {
            string msj = "";

            detalleContrarecibo recibo = new detalleContrarecibo()
            {
                id = int.Parse(Request.Form["ID"]),
                contrarecibo = Request.Form["txtcontrarecibo"],
                nota = Request.Form["txtnota"],
                total = Request.Form["txttotal"],
                pagada = Request.Form["txtpagada"],
            };


            bLdetalleContrarecibo.EditarDetalleContrarecibo(recibo, ref msj);
            // Recargar los datos antes de redirigir a la vista Mostrar
            List<detalleContrarecibo> recibos = null;
            recibos = bLdetalleContrarecibo.ObtenerDetaleContrarecibo(ref msj);

            // Devolver la vista Mostrar con los datos actualizados
            return View("Mostrar", recibos);

        }

        public ActionResult Eliminar(int id)
        {
            string msj = "";
            detalleContrarecibo recibo = new detalleContrarecibo()
            {
                id = id
            };

            bLdetalleContrarecibo.EliminarDetalleContrarecibo(recibo, ref msj);
            List<detalleContrarecibo> recibos = null;
            recibos = bLdetalleContrarecibo.ObtenerDetaleContrarecibo(ref msj);
            return View("Mostrar", recibos);
        }

    }
}