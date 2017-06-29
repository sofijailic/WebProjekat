var forum = angular.module('forum', ['ngRoute']); // inicijalizovana angular aplikacja

forum.config(function ($routeProvider) { // konfiguracija potrebnih stvari koje ce se koristiti u aplikaciji
    $routeProvider.when('/', // $routeProvider = 
	{
	    redirectTo: '/podforumi'

	}).when('/podforumi',
    {
        controller: 'PodforumiController',
        templateUrl: 'MojaAplikacija/stranice/podforumi/podforumi.html'

    }).when('/logovanje',
    {
        controller: 'AutentifikacijaController',
        templateUrl: 'MojaAplikacija/stranice/logovanje.html'

    }).when('/registracija',
    {
        controller: 'AutentifikacijaController',
        templateUrl: 'MojaAplikacija/stranice/registracija.html'

    }).when('/dodajPodforum', {
        
        controller: 'PodforumiController',
        templateUrl: 'MojaAplikacija/stranice/podforumi/dodajPodforum.html'

    }).when('/teme/dodajTemu/:nazivPodforuma', {

        controller: 'TemeController',
        templateUrl: 'MojaAplikacija/stranice/teme/dodajTemu.html'

    }).when('/podforumi/:nazivPodforuma', {

        controller: 'UnutarPodforumaController',
        templateUrl: 'MojaAplikacija/stranice/podforumi/pregledPodforuma.html'

    })

});