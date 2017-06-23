forum.factory('autentifikacijaFabrika', function ($http) {


    var factory = {};

    factory.Registracija = function (korisnik) {
        return $http.post('/api/Autentifikacija/RegistrujKorisnika', {
            KorisnickoIme: korisnik.korisnickoIme,
            Lozinka: korisnik.lozinka,
            Ime: korisnik.ime,
            Prezime: korisnik.prezime,
            BrTelefona: korisnik.brojTelefona,
            Email: korisnik.email
        });
    }


    return factory;
});