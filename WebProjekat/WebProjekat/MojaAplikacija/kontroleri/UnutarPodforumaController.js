forum.controller('UnutarPodforumaController', function ($scope, $rootScope, $routeParams, podforumiFabrika) {

    $scope.nazivPodforuma = $routeParams.nazivPodforuma;
    

    function inicijalizacija() {
        console.log('Usao u podforum');

        podforumiFabrika.uzmiPodforumPoNazivu($scope.nazivPodforuma).then(function (odgovor) {
            console.log(odgovor.data);
            $scope.podforum = odgovor.data;
        });

    }

    inicijalizacija();

});