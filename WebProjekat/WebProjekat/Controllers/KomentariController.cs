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
    public class KomentariController : ApiController
    {

        [HttpPost]
        [ActionName("DodajKomentarNaTemu")]
        public Komentar DodajKomentarNaTemu([FromBody]Komentar k)
        {
            // Treba da splituje k.TemaKojojPripada po - i pita da li je splitter[0] == splitovanaTema[0] (splitovanaTema[0] je podforum u kom se tema nalazi a splitovanaTema[1] je naslov teme)
            // i trbea da pita da li je splitter[1] == splitovanaTema[1]

            string[] splitovanaTema = k.TemaKojojPripada.Split('-');
            List<string> listaSvihTema = new List<string>();
            int brojac = 0;
            int indexZaIzmenu = -1;

            var dataFile = HttpContext.Current.Server.MapPath("~/App_Data/teme.txt");
            FileStream stream = new FileStream(dataFile, FileMode.Open);
            StreamReader sr = new StreamReader(stream);

            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                listaSvihTema.Add(line);
                brojac++;

                string[] splitter = line.Split(';');
                if (splitter[0] == splitovanaTema[0] && splitter[1] == splitovanaTema[1])
                {
                    indexZaIzmenu = brojac;
                }
            }
            sr.Close();
            stream.Close();
            // Upis u teme.txt tj dodavanje novog
            FileStream stream2 = new FileStream(dataFile, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(stream2);


            k.Id = Guid.NewGuid().ToString();
            k.DatumKomentara = DateTime.Now;
            k.Izmenjen = false;
            k.NegativniGlasovi = 0;
            k.PozitivniGlasovi = 0;
            k.Podkomentari = new List<Komentar>();
            k.RoditeljskiKomentar = "nemaRoditelja";
            k.Obrisan = false;

            // Prvo ako ova tema nema komentar tj ako joj je spliter listaSvihTema[indexZaIzmenu-1][8] == 'nePostoje', obrisi to nePostoje

            listaSvihTema[indexZaIzmenu - 1] += "|" + k.Id;

            foreach (string tema in listaSvihTema)
            {
                sw.WriteLine(tema);

            }
            sw.Close();
            stream2.Close();

            // Upis u komentari.txt
            var dataFile2 = HttpContext.Current.Server.MapPath("~/App_Data/komentari.txt");
            FileStream stream3 = new FileStream(dataFile2, FileMode.Append, FileAccess.Write);
            StreamWriter swKomentari = new StreamWriter(stream3);

            swKomentari.WriteLine(k.Id + ";" + k.TemaKojojPripada + ";" + k.Autor + ";" + k.DatumKomentara.ToShortDateString() + ";" + k.RoditeljskiKomentar + ";" + k.Tekst + ";" + k.PozitivniGlasovi.ToString() + ";" + k.NegativniGlasovi.ToString() + ";" + k.Izmenjen.ToString() + ";" + k.Obrisan.ToString() + ";" + "nemaPodkomentara");

            swKomentari.Close();
            stream3.Close();
            return k;
        }

        [HttpGet]
        [ActionName("UzmiKomentareOdTeme")]
        public List<Komentar> UzmiKomentareOdTeme(string idTeme)
        {
            var dataFile = HttpContext.Current.Server.MapPath("~/App_Data/komentari.txt");
            FileStream stream = new FileStream(dataFile, FileMode.Open);
            StreamReader sr = new StreamReader(stream);

            string line = "";

            List<Komentar> listaKomentaraZaTemu = new List<Komentar>();
            List<Komentar> listaPodkomentara = new List<Komentar>();

            while ((line = sr.ReadLine()) != null)
            {
                string[] splitter = line.Split(';');
                //ovaj splitter splituje komentare
                // Daj mi taj komentar samo ako nije obrisan
                if (splitter[1] == idTeme && splitter[9] == "False")
                {
                    listaPodkomentara = new List<Komentar>();
                    string[] ideviPodkomentara = splitter[10].Split('|');
                    foreach (string idPodkomentaraUKomentarima in ideviPodkomentara)
                    {
                        if (idPodkomentaraUKomentarima != "nemaPodkomentara")
                        {
                            var dataFile2 = HttpContext.Current.Server.MapPath("~/App_Data/podkomentari.txt");
                            FileStream stream2 = new FileStream(dataFile2, FileMode.Open);
                            StreamReader readerPodkomentara = new StreamReader(stream2);

                            string podkomentarLinija = "";
                            while ((podkomentarLinija = readerPodkomentara.ReadLine()) != null)
                            {

                                string[] podkomentarTokens = podkomentarLinija.Split(';');
                                // Vrati sve podkomentare koji nisu obrisani
                                if (podkomentarTokens[1] == idPodkomentaraUKomentarima && podkomentarTokens[8] == "False")
                                {
                                    Komentar podkomentar = new Komentar();
                                    podkomentar.Id = podkomentarTokens[1];
                                    podkomentar.RoditeljskiKomentar = podkomentarTokens[0];
                                    podkomentar.Autor = podkomentarTokens[2];
                                    podkomentar.DatumKomentara = DateTime.Parse(podkomentarTokens[3]);
                                    podkomentar.Tekst = podkomentarTokens[4];
                                    podkomentar.PozitivniGlasovi = Int32.Parse(podkomentarTokens[5]);
                                    podkomentar.NegativniGlasovi = Int32.Parse(podkomentarTokens[6]);
                                    podkomentar.Izmenjen = bool.Parse(podkomentarTokens[7]);
                                    podkomentar.Obrisan = bool.Parse(podkomentarTokens[8]);
                                    podkomentar.TemaKojojPripada = podkomentarTokens[9];

                                    listaPodkomentara.Add(podkomentar);
                                }
                            }
                            readerPodkomentara.Close();
                            stream2.Close();
                        }

                    }
                    listaKomentaraZaTemu.Add(new Komentar(splitter[0], splitter[1], splitter[2], DateTime.Parse(splitter[3]), splitter[4], listaPodkomentara, splitter[5], Int32.Parse(splitter[6]), Int32.Parse(splitter[7]), bool.Parse(splitter[8]), bool.Parse(splitter[9])));
                }
            }

            sr.Close();
            stream.Close();
            return listaKomentaraZaTemu;
        }
    }
}
