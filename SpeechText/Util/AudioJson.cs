using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeechText.Util
{
    //Clase encargada de mapear el el Json que llega
    public class AudioJson
    {
        
        public string RecognitionStatus { get; set; }
        public string DisplayText { get; set; }
        public int Offset { get; set; }
        public int Duration { get; set; }
    }
}