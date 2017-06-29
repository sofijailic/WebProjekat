﻿forum.factory('temeFabrika', function ($http) {


    var factory = {};

    factory.dodajTemu = function (tema) {

        return $http.post('api/Teme/DodajTemu', {
            PodforumKomePripada: tema.podforum,
            Naslov: tema.naslov,
            Tip: tema.tip,
            Autor: tema.autor,
            Sadrzaj: tema.sadrzajTeme,
            PozitivniGlasovi: 0,
            NegativniGlasovi: 0
        });
    }
    return factory;
});