using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bing.Speech;
using System.Net;
using System.IO;

namespace SpeechText.Util
{
    public class AudioHelper
    {
            

        public AudioHelper()
        {
        }

        public string ExtracText (string URL, string key, string route)
        {
            //Se encarga de descargar el archivo de la url
            string patch = route;
            using (var client = new WebClient())
            {
                client.DownloadFile(URL, patch);
            }
            
            //Se hace la peticion a la API
            string requestUri = "https://speech.platform.bing.com/speech/recognition/dictation/cognitiveservices/v1?language=es-MX&format=simple";
            string responseString;

            HttpWebRequest request = null;
            request = (HttpWebRequest)HttpWebRequest.Create(requestUri);
            request.SendChunked = true;
            request.Accept = @"application/json;text/xml";
            request.Method = "POST";
            request.ProtocolVersion = HttpVersion.Version11;
            request.ContentType = @"audio/wav; codec=audio/pcm; samplerate=16000";
            request.Headers["Ocp-Apim-Subscription-Key"] = key;

            // Send an audio file by 1024 byte chunks
            using (var fs = new FileStream(patch, FileMode.Open, FileAccess.Read))
            {

                /*
                * Open a request stream and write 1024 byte chunks in the stream one at a time.
                */
                byte[] buffer = null;
                int bytesRead = 0;
                using (Stream requestStream = request.GetRequestStream())
                {
                    /*
                    * Read 1024 raw bytes from the input audio file.
                    */
                    buffer = new Byte[checked((uint)Math.Min(1024, (int)fs.Length))];
                    while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }

                    // Flush
                    requestStream.Flush();
                }
            }
            /*
           * Get the response from the service.
           */
            Console.WriteLine("Response:");
            using (WebResponse response = request.GetResponse())
            {
                Console.WriteLine(((HttpWebResponse)response).StatusCode);

                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    responseString = sr.ReadToEnd();
                }

                Console.WriteLine(responseString);
               
            }

            return responseString;
        }
    }
}