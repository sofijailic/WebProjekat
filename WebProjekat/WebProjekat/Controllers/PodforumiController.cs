using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class PodforumiController : ApiController
    {
        [HttpPost]
        [ActionName("DodajPodforum")]

        public bool DodajPodforum([FromBody]Podforum p)
        {

            var dataFile = HttpContext.Current.Server.MapPath("~/App_Data/podforumi.txt");
            FileStream stream = new FileStream(dataFile, FileMode.Open);
            StreamReader sr = new StreamReader(stream);

            string linija = "";
            while ((linija = sr.ReadLine()) != null)
            {
                string[] splitovano = linija.Split(';');
                if (splitovano[0] == p.Naziv)
                {
                    stream.Close();
                    sr.Close();
                    return false;
                }
            }
            sr.Close();
            stream.Close();

            FileStream streamWrite = new FileStream(dataFile, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(streamWrite);

            sw.WriteLine(p.Naziv + ";" + p.Opis + ";" + p.Ikonica + ";" + p.SpisakPravila + ";" + p.Moderator + ";" + p.Moderator);

            sw.Close();
            streamWrite.Close();

            return true;
        }

        [HttpGet]
        [ActionName("UzmiSvePodforume")]
        public List<Podforum> UzmiSvePodforume()
        {
            List<Podforum> lista = new List<Podforum>();
            var dataFile = HttpContext.Current.Server.MapPath("~/App_Data/podforumi.txt");
            FileStream stream = new FileStream(dataFile, FileMode.Open);
            StreamReader sr = new StreamReader(stream);

            string linija = "";
            while ((linija = sr.ReadLine()) != null)
            {
                Podforum p = new Podforum();
                string[] splitovano = linija.Split(';');
                p.Naziv = splitovano[0];
                p.Opis = splitovano[1];
                p.Ikonica = splitovano[2];
                p.SpisakPravila = splitovano[3];
                p.Moderator = splitovano[4];

                lista.Add(p);
            }
            sr.Close();
            stream.Close();
            return lista;

        }

        [HttpGet]
        [ActionName("UzmiPodforumPoNazivu")]
        public Podforum UzmiPodforumPoNazivu(string naziv)
        {
            // Prodji kroz podforumi.txt i nadji taj podforum, isparsiraj ga i vrati
            var dataFile = HttpContext.Current.Server.MapPath("~/App_Data/podforumi.txt");
            FileStream stream = new FileStream(dataFile, FileMode.Open);
            StreamReader sr = new StreamReader(stream);


            string linija = "";
            while ((linija = sr.ReadLine()) != null)
            {
                string[] splitovano = linija.Split(';');
                if (splitovano[0] == naziv)
                {
                    sr.Close();
                    stream.Close();

                    Podforum p = new Podforum();
                    // napuni ga podacima
                    p.Naziv = splitovano[0];
                    p.Opis = splitovano[1];
                    p.Ikonica = splitovano[2];
                    p.SpisakPravila = splitovano[3];
                    p.Moderator = splitovano[4];
                    p.Moderatori = new List<string>();
                    string[] splitovaniModeratori = splitovano[5].Split('|');
                    foreach (string moderator in splitovaniModeratori)
                    {
                        p.Moderatori.Add(moderator);
                    }
                    return p;
                }
            }
            sr.Close();
            stream.Close();
            return null;
        }
    }
}
