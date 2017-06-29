forum.controller('UnutarPodforumaController', function ($scope, $rootScope, $routeParams, podforumiFabrika, $window, temeFabrika) {

    $scope.nazivPodforuma = $routeParams.nazivPodforuma;
    

    function inicijalizacija() {
        console.log('Usao u podforum');

        podforumiFabrika.uzmiPodforumPoNazivu($scope.nazivPodforuma).then(function (odgovor) {
            console.log(odgovor.data);
            $scope.podforum = odgovor.data;

            temeFabrika.uzmiSveTemeZaPodforum($scope.podforum.Naziv).then(function (odgovor) {
                console.log(odgovor.data);

                $scope.temePodforuma = odgovor.data;
            });

        });

    }

    inicijalizacija();

    $scope.otvoriDodavanjeNoveTeme = function () {
        $window.location.href = '#!/teme/dodajTemu/'+$scope.nazivPodforuma;
    }



});