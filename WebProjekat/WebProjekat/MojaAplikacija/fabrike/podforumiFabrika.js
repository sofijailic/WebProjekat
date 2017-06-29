forum.factory('podforumiFabrika', function ($http) {


    var factory = {};

    factory.DodajPodforum = function (podforum) {
        return $http.post('/api/Podforumi/DodajPodforum', {
            Naziv: podforum.naziv, //ova imena isto kao i kod  klase Korisnik! vodi racuna
            Opis: podforum.opis,
            Ikonica: podforum.ikonica,
            SpisakPravila: podforum.pravila,
            Moderator: podforum.moderator
        });
    }

    factory.UzmiSvePodforume = function () {
        return $http.get('/api/Podforumi/UzmiSvePodforume');
    }

    factory.uzmiPodforumPoNazivu = function (nazivPodforuma) {
        return $http.get('/api/Podforumi/UzmiPodforumPoNazivu?naziv=' + nazivPodforuma);
    }



    return factory;
});