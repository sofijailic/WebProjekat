forum.controller('GlavniController', function ($scope,$rootScope) {

    if (document.cookie !== "") { // proverimo ako cookie nije prazan
        console.log(document.cookie);
        var cookieInfo = document.cookie.substring(9, document.cookie.length); // cookieInfo je sada sve ono sto se nalazi u cookiju znaci {korisnickoIme: "ime",uloga:"korisnik"}
        var parsed = JSON.parse(cookieInfo); // parsiramo string u JSON objekat
        sessionStorage.setItem("korisnickoIme", parsed.korisnickoIme); // stavljamo na sesiju korisnickoIme
        sessionStorage.setItem("uloga", parsed.uloga); // i ulogu
        $rootScope.ulogovan = true; // kazemo da je ulogovan
        $rootScope.korisnik = { // stavljamo korisnika na rootScope
            korisnickoIme: sessionStorage.getItem("korisnickoIme"),
            uloga: sessionStorage.getItem("uloga"),
        };
    } else { // ako je cookie prazan stavljamo da korisnik nije ulogovan
        $rootScope.ulogovan = false;
    }

    $scope.IzlogujSe = function () {
        document.cookie = 'korisnik=;expires=Thu, 01 Jan 1970 00:00:01 GMT;';
        sessionStorage.clear();
        $rootScope.ulogovan = false;
        $rootScope.korisnik = {};
        document.location.href = "#!/logovanje";
    }

});