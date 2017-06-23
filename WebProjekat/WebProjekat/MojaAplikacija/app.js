var forum = angular.module('forum', ['ngRoute']); // inicijalizovana anglar aplikacja

forum.config(function ($routeProvider) { // konfiguracija potrebnih stvari koje ce se koristiti u aplikaciji
    $routeProvider.when('/', // kada se ucita 
	{
	    redirectTo: '/podforumi'

	}).when('/podforumi',
    {
        templateUrl: 'MojaAplikacija/stranice/podforumi.html'
    }).when('/logovanje',
    {
        templateUrl: 'MojaAplikacija/stranice/logovanje.html'
    })

});