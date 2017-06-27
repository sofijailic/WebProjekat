forum.factory('autentifikacijaFabrika', function ($http) {


    var factory = {};

    factory.Registracija = function (korisnik) {
        return $http.post('/api/Autentifikacija/RegistrujKorisnika', {
            KorisnickoIme: korisnik.korisnickoIme, //ova imena isto kao i kod  klase Korisnik! vodi racuna
            Lozinka: korisnik.lozinka,
            Ime: korisnik.ime,
            Prezime: korisnik.prezime,
            BrTelefona: korisnik.brojTelefona,
            Email: korisnik.email
        });
    }

    factory.Uloguj = function (korisnik) {
        return $http.post('/api/Autentifikacija/UlogujKorisnika', {
            KorisnickoIme: korisnik.korisnickoIme,
            Lozinka: korisnik.loznika
        });
    }


    return factory;
});