forum.controller('AutentifikacijaController', function ($scope, autentifikacijaFabrika,$window,$rootScope) {
    
    if ($rootScope.ulogovan == true) { // ako je korisnik ulogovan, nemoj mu dozvoliti da ide na stranicu za registrovanje ili logovanje
        $window.location.href = "#!/podforumi"; //ako je ulogovan daj mu stranicu podforumi
    }

    $scope.registruj = function (korisnik) {   //funkcija registruj, uzima korisnika  (parametri bez tipa se pisu)

        if (korisnik.lozinka == korisnik.potvrdaLozinke) {
            autentifikacijaFabrika.Registracija(korisnik).then(function (odgovor) { //kad dobijem odgovor

                if (odgovor.data == true) {

                    $window.location.href="#!logovanje"; //ukoliko je uspesna registracija predji na stranicu logovanje
                } else {
                    alert('Korisnik sa ovim korisnickim imenom vec postoji');
                }
                
            });
        } else {
            alert('Molim vas potvrdite istu lozinku');
        }
    }

    $scope.uloguj = function (korisnik) {
        autentifikacijaFabrika.Uloguj(korisnik).then(function (odgovor) {
            if (odgovor.data == null) {
                alert('Ovaj korisnik nije registrovan');
            }
            else {
                document.cookie = "korisnik=" + JSON.stringify({ korisnickoIme: odgovor.data.KorisnickoIme, uloga: odgovor.data.Uloga }) + ";expires=Thu, 01 Jan 2019 00:00:01 GMT;";
                sessionStorage.setItem("korisnickoIme", odgovor.data.KorisnickoIme);
                sessionStorage.setItem("uloga", odgovor.data.Uloga);
                $rootScope.ulogovan = true;
                $rootScope.korisnik = {
                    korisnickoIme: odgovor.data.KorisnickoIme,
                    uloga: odgovor.data.Uloga
                };
                console.log(odgovor.data);
                $window.location.href = "#!/podforumi";
            }
        });
    }

});