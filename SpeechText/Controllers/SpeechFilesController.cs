using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpeechText.Models;
using SpeechText.Util;
using Newtonsoft.Json;

namespace SpeechText.Controllers
{
    public class SpeechFilesController : Controller
    {
        private SpeechTextContext db = new SpeechTextContext();

        // GET: SpeechFiles
        public ActionResult Index()
        {          
            return View(db.SpeechFile.ToList());
        }

        // GET: SpeechFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpeechFile speechFile = db.SpeechFile.Find(id);
            if (speechFile == null)
            {
                return HttpNotFound();
            }
            var udiotext = speechFile.Text;
            var list = JsonConvert.DeserializeObject<AudioJson>(udiotext);
            var texto = list.DisplayText;
            ViewBag.textoNuevo = texto;

            return View(speechFile);
        }

        // GET: SpeechFiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpeechFiles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,URI,Text")] SpeechFile speechFile)
        {
            if (ModelState.IsValid)
            {
                db.SpeechFile.Add(speechFile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(speechFile);
        }

        // GET: SpeechFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpeechFile speechFile = db.SpeechFile.Find(id);
            if (speechFile == null)
            {
                return HttpNotFound();
            }
            return View(speechFile);
        }

        // POST: SpeechFiles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,URI,Text")] SpeechFile speechFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(speechFile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(speechFile);
        }

        // GET: SpeechFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpeechFile speechFile = db.SpeechFile.Find(id);
            if (speechFile == null)
            {
                return HttpNotFound();
            }
            return View(speechFile);
        }

        // POST: SpeechFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SpeechFile speechFile = db.SpeechFile.Find(id);
            db.SpeechFile.Remove(speechFile);
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
