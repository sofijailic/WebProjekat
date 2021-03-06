﻿using System;
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
    
    public class AutentifikacijaController : ApiController
    {
        [HttpPost]
        [ActionName("RegistrujKorisnika")]
        public bool RegistrujKorisnika([FromBody]Korisnik k)
        {
            var dataFile = HttpContext.Current.Server.MapPath("~/App_Data/korisnici.txt");
            FileStream stream = new FileStream(dataFile, FileMode.Open);
            StreamReader sr = new StreamReader(stream);

            string linija = "";
            while ( (linija = sr.ReadLine()) != null )
            {
                string[] splitovano = linija.Split(';');
                if (splitovano[0] == k.KorisnickoIme)
                {
                    stream.Close();
                    sr.Close();
                    return false;
                }
            }
            sr.Close();
            stream.Close();

            k.Uloga = "korisnik";
            k.DatumRegistracije = DateTime.Now;

            FileStream stream2 = new FileStream(dataFile, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(stream2);

            sw.WriteLine(k.KorisnickoIme + ";" + k.Lozinka + ";" + k.Uloga + ";" + k.Ime + ";" + k.Prezime + ";" + k.BrTelefona + ";" + k.Email + ";" + k.DatumRegistracije.ToShortDateString() );
            sw.Close();
            stream2.Close();
            return true;
        }

        [HttpPost]
        [ActionName("UlogujKorisnika")]
        public Korisnik UlogujKorisnika([FromBody]Korisnik k)
        {
            var dataFile = HttpContext.Current.Server.MapPath("~/App_Data/korisnici.txt");
            FileStream stream = new FileStream(dataFile, FileMode.Open);
            StreamReader sr = new StreamReader(stream);

            string linija = "";
            while ((linija = sr.ReadLine()) != null)
            {
                string[] splitovano = linija.Split(';');
                if (splitovano[0] == k.KorisnickoIme)
                {
                    stream.Close();
                    sr.Close();
                    return new Korisnik(splitovano[0],null,splitovano[2],splitovano[3],splitovano[4],splitovano[5],splitovano[6],DateTime.Parse(splitovano[7]));
                }
            }
            sr.Close();
            stream.Close();
            return null;
        }
    }
}
