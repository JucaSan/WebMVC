using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectModel.Entities;
using ProjectModel.Models;

namespace WebMVC.Controllers
{
    public class CategoriaMaterialController : Controller
    {
        BLCategoriaMaterial bLCategoriaMaterial = new BLCategoriaMaterial(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        // Método para la tabla con los datos (Home)
        public ActionResult Mostrar()
        {
            List<CategoriaMaterial> categories = null;
            string msj = "";
            categories = bLCategoriaMaterial.ObtenerCategorias(ref msj);
            return View(categories);
        }


        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Agregar()
        {
            string msj = "";

            CategoriaMaterial categoria = new CategoriaMaterial()
            {
                nomCategoria = Request.Form["txtNom"],
                extra = Request.Form["txtExt"],
            };


            bLCategoriaMaterial.InsertarCategoria(categoria, ref msj);
            // Recargar los datos antes de redirigir a la vista Mostrar
            List<CategoriaMaterial> categories = null;
            categories = bLCategoriaMaterial.ObtenerCategorias(ref msj);

            // Devolver la vista Mostrar con los datos actualizados
            return View("Mostrar", categories);
        }

        public ActionResult Edit(string id)
        {
            string msj = "";
            ViewBag.id = id;

            CategoriaMaterial categoria = new CategoriaMaterial();
            categoria = bLCategoriaMaterial.CategoriaMaterialPorId(int.Parse(id), ref msj);
            return View(categoria);
        }

        public ActionResult Editar()
        {
            string msj = "";

            CategoriaMaterial categoria = new CategoriaMaterial()
            {
                idCategoria = int.Parse(Request.Form["ID"]),
                nomCategoria = Request.Form["txtNom"],
                extra = Request.Form["txtExt"],
            };


            bLCategoriaMaterial.EditarCategoria(categoria, ref msj);
            // Recargar los datos antes de redirigir a la vista Mostrar
            List<CategoriaMaterial> categories = null;
            categories = bLCategoriaMaterial.ObtenerCategorias(ref msj);

            // Devolver la vista Mostrar con los datos actualizados
            return View("Mostrar", categories);

        }


        public ActionResult Eliminar(int id)
        {
            string msj = "";
            CategoriaMaterial categoria = new CategoriaMaterial()
            {
                idCategoria = id
            };

            bLCategoriaMaterial.EliminarCategoria(categoria, ref msj);
            List<CategoriaMaterial> categories = null;
            categories = bLCategoriaMaterial.ObtenerCategorias(ref msj);
            return View("Mostrar", categories);
        }


    }
}