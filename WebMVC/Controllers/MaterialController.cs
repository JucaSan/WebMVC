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
    public class MaterialController : Controller
    {
        BLMaterial bLMaterial = new BLMaterial(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        
        // Método para la tabla con los datos (Home)
        public ActionResult Mostrar()
        {
            List<Material> materials = null;
            string msj = "";
            materials = bLMaterial.ObtenerMateriales(ref msj);
            return View(materials);
        }




        // Método para traer la vista crear
        public ActionResult Create()
        {
            // Mensaje de referencia
            string msj = "";

            // Lista de datos para el DropDownList
            BLCategoriaMaterial bLCategoriaMaterial = new BLCategoriaMaterial(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            List<CategoriaMaterial> categories = null;
            categories = bLCategoriaMaterial.ObtenerCategorias(ref msj);

            // Lista de Items con los datos anteriores
            List<SelectListItem> items =  new List<SelectListItem>();

            // Llenado de la lista SelectedItem
            foreach(CategoriaMaterial c in  categories)
            {
                items.Add(new SelectListItem
                {
                    Value = c.idCategoria.ToString(),
                    Text = c.nomCategoria,
                });
            }
            ViewBag.listaDrop = items;

            return View();
        }

        public ActionResult Edit(string id)
        {
            // Mensaje de referencia
            string msj = "";

            // Lista de datos para el DropDownList
            BLCategoriaMaterial bLCategoriaMaterial = new BLCategoriaMaterial(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            List<CategoriaMaterial> categories = null;
            categories = bLCategoriaMaterial.ObtenerCategorias(ref msj);

            // Lista de Items con los datos anteriores
            List<SelectListItem> items = new List<SelectListItem>();

            // Llenado de la lista SelectedItem
            foreach (CategoriaMaterial c in categories)
            {
                items.Add(new SelectListItem
                {
                    Value = c.idCategoria.ToString(),
                    Text = c.nomCategoria,
                });
            }
            ViewBag.listaDrop = items;
            ViewBag.id = id;

            Material material = new Material();
            material = bLMaterial.MaterialPorId(int.Parse(id), ref msj);


            return View(material);
        }

        public ActionResult Editar()
        {
            string msj = "";

            Material material = new Material()
            {
                Idmaterial = int.Parse(Request.Form["ID"]),
                NombreMat = Request.Form["txtNom"],
                Marca = Request.Form["txtMar"],
                Categoria = Request.Form["txtCat"],
                UnidadMedida = Request.Form["txtUni"]
            };


            bLMaterial.EditarMaterial(material, ref msj);
            // Recargar los datos antes de redirigir a la vista Mostrar
            List<Material> materials = null;
            materials = bLMaterial.ObtenerMateriales(ref msj);

            // Devolver la vista Mostrar con los datos actualizados
            return View("Mostrar", materials);
        }

        public ActionResult Agregar()
        {
            string msj = "";

            Material material = new Material()
            {
                NombreMat = Request.Form["txtNom"],
                Marca = Request.Form["txtMar"],
                Categoria = Request.Form["txtCat"],
                UnidadMedida = Request.Form["txtUni"]
            };


            bLMaterial.InsertarMaterial(material, ref msj);
            // Recargar los datos antes de redirigir a la vista Mostrar
            List<Material> materials = null;
            materials = bLMaterial.ObtenerMateriales(ref msj);

            // Devolver la vista Mostrar con los datos actualizados
            return View("Mostrar", materials);
        }


        public ActionResult Eliminar(int id)
        {
            string msj = "";
            Material material = new Material()
            {
                Idmaterial = id
            };

            bLMaterial.EliminarMaterial(material, ref msj);
            List<Material> materials  = null;
            materials = bLMaterial.ObtenerMateriales(ref msj);
            return View("Mostrar", materials);
        }


    }
}