using Newtonsoft.Json;
using SpeechText.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeechText.Util
{
    //Clase encargada de crear una lista de todas las palabras en la base de datos
    //sin repetir palabra
    public class Word
    {
        SpeechTextContext db = new SpeechTextContext();

        public List<string> GetsWords()
        {
            var listFiles = db.SpeechFile.ToList();

            var listText = new List<string>();

            var listaWord = new List<string>();

            foreach (var file in listFiles)
            {
                var json = file.Text;
                var list = JsonConvert.DeserializeObject<AudioJson>(json);
                var texto = list.DisplayText;
                listText.Add(texto);
            }

            foreach (var texto in listText)
            {
                Char delimiter = (char)32;
                String[] substrings = texto.Split(delimiter);
                foreach (var substring in substrings)
                {
                    listaWord.Add(substring);
                }
            }

            return listaWord;

        }

        public List<string> Words()
        {

            var listWords = GetsWords();
            List<string> distinct = listWords.Distinct().ToList();
            return distinct;

        }
    }
}