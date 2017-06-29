forum.factory('temeFabrika', function ($http) {


    var factory = {};

    factory.dodajTemu = function (tema) {

        return $http.post('api/Teme/DodajTemu', {
            PodforumKomePripada: tema.podforum,
            Naslov: tema.naslov,
            Tip: tema.tip,
            Autor: tema.autor,
            Sadrzaj: tema.sadrzaj,
            PozitivniGlasovi: 0,
            NegativniGlasovi: 0
        });
    }

    factory.uzmiSveTemeZaPodforum = function (nazivPodforuma) {
        return $http.get('/api/Teme/UzmiSveTemeZaPodforum?podforum=' + nazivPodforuma);
    }

    factory.uzmiTemuPoNazivu = function (nazivPodforuma, naslovTeme) {
        return $http.get('/api/Teme/UzmiTemuPoNaslovu?podforum=' + nazivPodforuma + "&tema=" + naslovTeme);
    }

    return factory;
});