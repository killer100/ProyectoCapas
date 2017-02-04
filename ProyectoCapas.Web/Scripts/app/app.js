
angular.module('app', ['ui.router', 'ui.bootstrap', 'bsLoadingOverlay', 'angucomplete-alt'])
    .run(function (bsLoadingOverlayService) {
        bsLoadingOverlayService.setGlobalConfig({
            delay: 0,
            activeClass: 'position-relative',
            templateUrl: '_overlay-template.html',
            templateOptions: undefined
        });
    })

    .config(function ($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise("/");
        $stateProvider
            .state('home', {
                url: '/',
                template: 'hola mundo!',
                title: 'Home'
            })
            .state('articulos-list', {
                url: '/articulos',
                template: '<listar-articulos></listar-articulos>',
                title: 'Articulos'
            })
            .state('clientes-list', {
                url: '/clientes',
                template: '<listar-clientes></listar-clientes>',
                title: 'Clientes'
            })
            .state('facturas-list', {
                url: '/facturas',
                template: '<listar-facturas></listar-facturas>',
                title: 'Facturas'
            })
    })

    .filter('range', function () {
        return function (input, min, max) {
            min = parseInt(min); //Make string input int
            max = parseInt(max);
            for (var i = min; i <= max; i++)
                input.push(i);
            return input;
        };
    });

