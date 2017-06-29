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
    public class TemeController : ApiController
    {
        [HttpPost]
        [ActionName("DodajTemu")]
        public bool DodajTemu([FromBody]Tema t)
        {
            // Prodji kroz sve teme vidi da li vec postoji ova sa ovim nazivom podforuma i naslovom, ako postoji nemoj dodati ,ako ne postoji dodaj

            var dataFile = HttpContext.Current.Server.MapPath("~/App_Data/teme.txt");
            FileStream stream = new FileStream(dataFile, FileMode.Open);
            StreamReader sr = new StreamReader(stream);

            string linija = "";
            while ((linija = sr.ReadLine()) != null)
            {
                string[] splitovano = linija.Split(';');
                if (splitovano[0] == t.Naslov)
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

            t.DatumKreiranja = DateTime.Now;

            sw.WriteLine(t.PodforumKomePripada + ";" + t.Naslov + ";" + t.Tip + ";" + t.Autor + ";" + t.Sadrzaj + ";" +t.DatumKreiranja.ToShortDateString() + ";" + t.PozitivniGlasovi.ToString() + ";" + t.NegativniGlasovi.ToString()+";nemaKomentara");

            sw.Close();
            streamWrite.Close();

            return true;
           
        }
    }
}
