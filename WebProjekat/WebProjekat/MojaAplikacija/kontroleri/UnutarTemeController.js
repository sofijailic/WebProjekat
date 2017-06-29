forum.controller('UnutarTemeController',function($scope, $rootScope, $routeParams, temeFabrika) {

    function inicijalizacija() {
        console.log('Inicijalizovan unutar teme controller');

        temeFabrika.uzmiTemuPoNazivu($routeParams.nazivPodforuma, $routeParams.naslovTeme).then(function (odgovor) {
            console.log(odgovor.data);
            $scope.tema = odgovor.data;

        });
    }

    inicijalizacija();



});