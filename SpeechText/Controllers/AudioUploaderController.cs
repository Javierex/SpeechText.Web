using Newtonsoft.Json;
using SpeechText.Models;
using SpeechText.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeechText.Controllers
{
    public class AudioUploaderController : Controller
    {
        string serverFolvderPath;
        AudioHelper audioHelper;
        string key;
        
        SpeechTextContext db = new SpeechTextContext();

        public AudioUploaderController()
        {
            serverFolvderPath = "~/Speech/audio.wav";
            audioHelper = new AudioHelper();
            
        }

        // GET: AudioUploader
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "file")] string file)
        {
            if (file != null)
            {
                
                var route = Server.MapPath(serverFolvderPath);
                
                key = "7feb697bb7714a0a9a23c7f0bad2bd9c";

                var audioFile = new SpeechFile();

                string response = audioHelper.ExtracText(file,key,route);

                audioFile.URI = file;
                audioFile.Text = response;

                db.SpeechFile.Add(audioFile);
                db.SaveChanges();
              
                return RedirectToAction("Index", "SpeechFiles");
            }
            return View();
        }
           
        
    }
}