forum.controller('UnutarPodforumaController', function ($scope, $rootScope, $routeParams, podforumiFabrika, $window) {

    $scope.nazivPodforuma = $routeParams.nazivPodforuma;
    

    function inicijalizacija() {
        console.log('Usao u podforum');

        podforumiFabrika.uzmiPodforumPoNazivu($scope.nazivPodforuma).then(function (odgovor) {
            console.log(odgovor.data);
            $scope.podforum = odgovor.data;
        });

    }

    inicijalizacija();

    $scope.otvoriDodavanjeNoveTeme = function () {
        $window.location.href = '#!/teme/dodajTemu/'+$scope.nazivPodforuma;
    }



});