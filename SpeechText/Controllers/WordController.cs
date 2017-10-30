using SpeechText.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeechText.Controllers
{
    public class WordController : Controller
    {
        Word word = new Word();
        // GET: Word
        public ActionResult Index()
        {
            List<string> listaWords = null;
            listaWords = word.Words();
            return View(listaWords);
        }
    }
}