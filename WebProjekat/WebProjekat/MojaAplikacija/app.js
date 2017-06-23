var forum = angular.module('forum', ['ngRoute']); // inicijalizovana angular aplikacja

forum.config(function ($routeProvider) { // konfiguracija potrebnih stvari koje ce se koristiti u aplikaciji
    $routeProvider.when('/', // $routeProvider = 
	{
	    redirectTo: '/podforumi'

	}).when('/podforumi',
    {
        controller: 'PodforumiController',
        templateUrl: 'MojaAplikacija/stranice/podforumi.html'

    }).when('/logovanje',
    {
        templateUrl: 'MojaAplikacija/stranice/logovanje.html'
    }).when('/registracija',
    {
        controller: 'AutentifikacijaController',
        templateUrl: 'MojaAplikacija/stranice/registracija.html'
    })

});