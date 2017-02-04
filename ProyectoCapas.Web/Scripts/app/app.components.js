angular.module('app')
    .component('listarArticulos', {
        templateUrl: '/Scripts/app/views/listarArticulos.html',
        controller: 'ListarArticulosController',
        controllerAs: 'vm'
    })
    .component('formularioArticulo', {
        templateUrl: '/Scripts/app/views/formularioArticulo.html',
        controller: 'FormularioArticuloController',
        controllerAs: 'vm',
        bindings: {
            resolve: '<',
            close: '&',
            dissmiss: '&'
        }
    })
    .component('listarClientes', {
        templateUrl: '/Scripts/app/views/listarClientes.html',
        controller: 'ListarClientesController',
        controllerAs: 'vm'
    })
    .component('formularioCliente', {
        templateUrl: '/Scripts/app/views/formularioCliente.html',
        controller: 'FormularioClienteController',
        controllerAs: 'vm',
        bindings: {
            resolve: '<',
            close: '&',
            dissmiss: '&'
        }
    })
    .component('listarFacturas', {
        templateUrl: '/Scripts/app/views/listarFacturas.html',
        controller: 'ListarFacturasController',
        controllerAs: 'vm'
    })
    .component('formularioFactura', {
        templateUrl: '/Scripts/app/views/formularioFactura.html',
        controller: 'FormularioFacturaController',
        controllerAs: 'vm',
        bindings: {
            resolve: '<',
            close: '&',
            dissmiss: '&'
        }
    });