using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalWeb2.Models;
using System.Data.SqlClient;

namespace FinalWeb2.Controllers
{
    public class SociosController : Controller
    {
        private FinalWebEntities2 db = new FinalWebEntities2();
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        void connectionString()
        {
            con.ConnectionString = "Data Source=SQL5041.site4now.net;Initial Catalog=DB_A50AF3_finalweb;User Id=DB_A50AF3_finalweb_admin;Password=rubier123;";

        }
        [HttpPost]
        public ActionResult Verify(Account acc)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select Correo,Clave from Usuarios where Correo ='" + acc.Correo + "' and Clave ='" + acc.Clave + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult Register(Account acc)
        {

            return RedirectToAction("Login");
        }

        // GET: Socios
        public ActionResult Index()
        {
            var socios = db.Socios.Include(s => s.Afiliados1).Include(s => s.Estados).Include(s => s.MemberShip);
            return View(socios.ToList());
        }

        // GET: Socios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Socios socios = db.Socios.Find(id);
            if (socios == null)
            {
                return HttpNotFound();
            }
            return View(socios);
        }

        // GET: Socios/Create
        public ActionResult Create()
        {
            ViewBag.Afiliados = new SelectList(db.Afiliados, "IdAfiliado", "Afiliados1");
            ViewBag.EstadoMembrecia = new SelectList(db.Estados, "IdEstados", "Estado");
            ViewBag.Tipo_Membresia = new SelectList(db.MemberShip, "IdShup", "TipoMembrecia");
            return View();
        }

        // POST: Socios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSocio,Nombre,Apellidos,Cedula,Foto,Direccion,Telefono,Sexo,Edad,FechaNacimiento,Afiliados,Tipo_Membresia,LugarTrabajo,DireccionOficina,TelOficiona,EstadoMembrecia,FechaIngreso,FechaSalida")] Socios socios)
        {
            if (ModelState.IsValid)
            {
                db.Socios.Add(socios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Afiliados = new SelectList(db.Afiliados, "IdAfiliado", "Afiliados1", socios.Afiliados);
            ViewBag.EstadoMembrecia = new SelectList(db.Estados, "IdEstados", "Estado", socios.EstadoMembrecia);
            ViewBag.Tipo_Membresia = new SelectList(db.MemberShip, "IdShup", "TipoMembrecia", socios.Tipo_Membresia);
            return View(socios);
        }

        // GET: Socios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Socios socios = db.Socios.Find(id);
            if (socios == null)
            {
                return HttpNotFound();
            }
            ViewBag.Afiliados = new SelectList(db.Afiliados, "IdAfiliado", "Afiliados1", socios.Afiliados);
            ViewBag.EstadoMembrecia = new SelectList(db.Estados, "IdEstados", "Estado", socios.EstadoMembrecia);
            ViewBag.Tipo_Membresia = new SelectList(db.MemberShip, "IdShup", "TipoMembrecia", socios.Tipo_Membresia);
            return View(socios);
        }

        // POST: Socios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSocio,Nombre,Apellidos,Cedula,Foto,Direccion,Telefono,Sexo,Edad,FechaNacimiento,Afiliados,Tipo_Membresia,LugarTrabajo,DireccionOficina,TelOficiona,EstadoMembrecia,FechaIngreso,FechaSalida")] Socios socios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Afiliados = new SelectList(db.Afiliados, "IdAfiliado", "Afiliados1", socios.Afiliados);
            ViewBag.EstadoMembrecia = new SelectList(db.Estados, "IdEstados", "Estado", socios.EstadoMembrecia);
            ViewBag.Tipo_Membresia = new SelectList(db.MemberShip, "IdShup", "TipoMembrecia", socios.Tipo_Membresia);
            return View(socios);
        }

        // GET: Socios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Socios socios = db.Socios.Find(id);
            if (socios == null)
            {
                return HttpNotFound();
            }
            return View(socios);
        }

        // POST: Socios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Socios socios = db.Socios.Find(id);
            db.Socios.Remove(socios);
            db.SaveChanges();
            return RedirectToAction("Index");
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
