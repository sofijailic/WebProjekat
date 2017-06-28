forum.controller('PodforumiController', function ($scope, $window, podforumiFabrika) {
    
    function inicijalizacija() {
        console.log('Inicijalizovan podforumi controller');

        podforumiFabrika.UzmiSvePodforume().then(function (odgovor) {
            console.log(odgovor.data);
            $scope.podforumi = odgovor.data;
        });
    }

    inicijalizacija();

    $scope.otvoriStranicuDodavanjePodforuma = function () {

        $window.location.href = "#!/dodajPodforum";
    }

    $scope.dodajPodforum = function (podforum) {

        podforum.moderator = sessionStorage.getItem("korisnickoIme");
        podforum.ikonica = "slika.jpg";

        podforumiFabrika.DodajPodforum(podforum).then(function (odgovor) {
            console.log(odgovor.data);
        });

    }
});