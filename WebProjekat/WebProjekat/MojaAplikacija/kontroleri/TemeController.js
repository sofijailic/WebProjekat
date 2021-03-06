﻿forum.controller('TemeController', function ($scope, $rootScope, temeFabrika, $routeParams, $window) {
    function inicijalizacija() {
        console.log('Teme kontroler inicijalizovan');
    }

    inicijalizacija();

    $scope.dodajTemu = function (tema) {

        // proveri da li je tema.naslov == "" || tema.naslov == null , ako jeste alert('Ne smete imati prazan naslov'); return;

        tema.autor = sessionStorage.getItem("korisnickoIme");
        tema.podforum = $routeParams.nazivPodforuma;
        temeFabrika.dodajTemu(tema).then(function (odgovor) {
            console.log(odgovor.data);
            $window.location.href = "#!/podforumi/" + $routeParams.nazivPodforuma;
        });
    }

});