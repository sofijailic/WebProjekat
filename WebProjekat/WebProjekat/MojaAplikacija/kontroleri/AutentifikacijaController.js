forum.controller('AutentifikacijaController', function ($scope, autentifikacijaFabrika,$window) {
    
    $scope.registruj = function (korisnik) {

        if (korisnik.lozinka == korisnik.potvrdaLozinke) {
            autentifikacijaFabrika.Registracija(korisnik).then(function (odgovor) {

                if (odgovor.data == true) {

                    $window.location.href="#!logovanje";
                } else {
                    alert('Korisnik sa ovim korisnickim imenom vec postoji');
                }
                
            });
        } else {
            alert('Molim vas potvrdite istu lozinku');
        }

        
    }

});