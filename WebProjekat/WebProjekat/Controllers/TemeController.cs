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

        [HttpGet]
        [ActionName("UzmiSveTemeZaPodforum")]
        public List<Tema> UzmiSveTemeZaPodforum(string podforum)
        {
            List<Tema> listaTema = new List<Tema>();
            var dataFile = HttpContext.Current.Server.MapPath("~/App_Data/teme.txt");
            FileStream stream = new FileStream(dataFile, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                if (line != "")
                {

                    string[] splitter = line.Split(';');
                    List<string> listaKomentara = new List<string>();

                    if (splitter[0] == podforum)
                    {
                        string[] commentSplitter = splitter[8].Split('|');
                        foreach (string komentar in commentSplitter)
                        {
                            if (komentar != "nePostoje")
                            {
                                listaKomentara.Add(komentar);
                            }

                        }

                        listaTema.Add(new Tema(splitter[0], splitter[1], splitter[2], splitter[3], splitter[4], DateTime.Parse(splitter[5]), Int32.Parse(splitter[6]), Int32.Parse(splitter[7]), listaKomentara));
                    }
                }

            }
            sr.Close();
            stream.Close();
            return listaTema;
        }

        [HttpGet]
        [ActionName("UzmiTemuPoNaslovu")]
        public Tema UzmiTemuPoNaslovu(string podforum, string tema)
        {
            var dataFile = HttpContext.Current.Server.MapPath("~/App_Data/teme.txt");
            FileStream stream = new FileStream(dataFile, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";

            List<string> listaKomentara = new List<string>();

            while ((line = sr.ReadLine()) != null)
            {
                string[] splitter = line.Split(';');
                if (splitter[0] == podforum && splitter[1] == tema)
                {
                    string[] komentarSplitter = splitter[8].Split('|');
                    foreach (string komentar in komentarSplitter)
                    {
                        if (komentar != "nePostoje")
                        {
                            listaKomentara.Add(komentar);
                        }
                    }
                    sr.Close();
                    stream.Close();
                    return new Tema(splitter[0], splitter[1], splitter[2], splitter[3], splitter[4], DateTime.Parse(splitter[5]), Int32.Parse(splitter[6]), Int32.Parse(splitter[7]), listaKomentara);
                }
            }

            sr.Close();
            stream.Close();
            return null;
        }
    }
}
