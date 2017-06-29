using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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


            return true;
        }
    }
}
