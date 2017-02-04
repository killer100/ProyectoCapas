
angular.module('app')

    .service('EventListener', function ($rootScope) {
        return {
            broadcast: function (action, data) {
                $rootScope.$broadcast(action, data);
            },
            emit: function (action, data) {
                $rootScope.$emit(action, data);
            },
            listen: function (action, func) {
                $rootScope.$on(action, function (event, data) {
                    func(data);
                });
            }
        }
    })

    .service('ArticulosService', function ($http, $q) {

        var svc = this;

        svc.ListarArticulos = function (page, pageSize) {
            var defered = $q.defer();
            var params = {
                page: page,
                pageSize: pageSize
            };
            $http.get('/api/articulo', { params: params }).
                then(function (Response) { defered.resolve(Response.data); }, function (Response) { defered.reject(Response); });
            return defered.promise;
        };

        svc.ListarArticulo = function (cod_art) {
            var defered = $q.defer();
            $http.get('/api/articulo/' + cod_art).
                then(function (Response) { defered.resolve(Response.data); }, function (Response) { defered.reject(Response); });
            return defered.promise;
        };

        svc.ActualizarArticulo = function (id, articulo) {
            var defered = $q.defer();
            $http.put('/api/articulo/' + id, articulo).
                then(function (Response) { defered.resolve(Response.data); }, function (Response) { defered.reject(Response); });
            return defered.promise;
        };

        svc.EliminarArticulo = function (id) {
            var defered = $q.defer();
            $http.delete('/api/articulo/' + id).
                then(function (Response) { defered.resolve(Response.data); }, function (Response) { defered.reject(Response); });
            return defered.promise;
        };

        svc.GuardarArticulo = function (articulo) {
            var defered = $q.defer();
            $http.post('/api/articulo', articulo).
                then(function (Response) { defered.resolve(Response.data); }, function (Response) { defered.reject(Response); });
            return defered.promise;
        };

        svc.ValidarArticulo = function (articulo) {
            var errors = [];

            if (articulo.cod_art == null || articulo.cod_art.length == 0)
                errors.push("Ingrese código");
            else if (articulo.cod_art.length > 4)
                errors.push("El código debe tener como máximo 04 caracteres");


            if (articulo.descrip == null || articulo.descrip.length == 0)
                errors.push("Ingrese descripción");
            else if (articulo.descrip.length > 20)
                errors.push("La descripción debe tener como máximo 20 caracteres");


            if (articulo.prec_unic == null)
                errors.push("Ingrese precio");
            else if (!isFinite(articulo.prec_unic))
                errors.push("El precio ingresado no es válido");
            else if (articulo.prec_unic <= 0)
                errors.push("El precio debe ser mayor que 0");


            if (articulo.stock == null)
                errors.push("Ingrese stock");
            else if (!isFinite(articulo.stock))
                errors.push("El stock ingresado no es válido");
            else if (articulo.stock <= 0)
                errors.push("El stock debe ser mayor que 0");


            return errors;
        };

        return {
            ListarArticulos: svc.ListarArticulos,
            ListarArticulo: svc.ListarArticulo,
            ActualizarArticulo: svc.ActualizarArticulo,
            EliminarArticulo: svc.EliminarArticulo,
            GuardarArticulo: svc.GuardarArticulo,
            ValidarArticulo: svc.ValidarArticulo
        };
    })

    .service('ClientesService', function ($http, $q) {

        var svc = this;

        svc.ListarClientes = function (page, pageSize) {
            var defered = $q.defer();
            var params = {
                page: page,
                pageSize: pageSize
            };
            $http.get('/api/cliente', { params: params }).
                then(function (Response) { defered.resolve(Response.data); }, function (Response) { defered.reject(Response); });
            return defered.promise;
        };

        svc.ListarCliente = function (cod_clie) {
            var defered = $q.defer();
            $http.get('/api/cliente/' + cod_clie).
                then(function (Response) { defered.resolve(Response.data); }, function (Response) { defered.reject(Response); });
            return defered.promise;
        };

        svc.ActualizarCliente = function (cod_clie, cliente) {
            var defered = $q.defer();
            $http.put('/api/cliente/' + cod_clie, cliente).
                then(function (Response) { defered.resolve(Response.data); }, function (Response) { defered.reject(Response); });
            return defered.promise;
        };

        svc.EliminarCliente = function (cod_clie) {
            var defered = $q.defer();
            $http.delete('/api/cliente/' + cod_clie).
                then(function (Response) { defered.resolve(Response.data); }, function (Response) { defered.reject(Response); });
            return defered.promise;
        };

        svc.GuardarCliente = function (cliente) {
            var defered = $q.defer();
            $http.post('/api/cliente', cliente).
                then(function (Response) { defered.resolve(Response.data); }, function (Response) { defered.reject(Response); });
            return defered.promise;
        };

        svc.ValidarCliente = function (cliente) {
            var errors = [];

            if (cliente.cod_clie == null || cliente.cod_clie.length == 0)
                errors.push("Ingrese código");
            else if (cliente.cod_clie.length > 4)
                errors.push("El código debe tener como máximo 04 caracteres");


            if (cliente.mon_ape == null || cliente.mon_ape.length == 0)
                errors.push("Ingrese nombres y apellidos");
            else if (cliente.mon_ape.length > 25)
                errors.push("Los nombres y apellidos debe tener como máximo 25 caracteres");


            if (cliente.telef == null || cliente.telef.length == 0)
                errors.push("Ingrese telefono");
            else if (cliente.telef.length > 9)
                errors.push("El telfono debe tener como máximo 09 caracteres");


            if (cliente.dni == null || cliente.dni.length == 0)
                errors.push("Ingrese dni");
            else if (cliente.dni.length > 8)
                errors.push("El dni debe tener como máximo 08 caracteres");


            if (cliente.dir == null || cliente.dir.length == 0)
                errors.push("Ingrese dirección");
            else if (cliente.dir.length > 30)
                errors.push("La dirección debe tener como máximo 30 caracteres");

            return errors;
        };

        return {
            ListarClientes: svc.ListarClientes,
            ListarCliente: svc.ListarCliente,
            ActualizarCliente: svc.ActualizarCliente,
            EliminarCliente: svc.EliminarCliente,
            GuardarCliente: svc.GuardarCliente,
            ValidarCliente: svc.ValidarCliente
        };
    })

    .service('FacturasService', function ($http, $q) {

        var svc = this;

        svc.ListarFacturas = function (page, pageSize) {
            var defered = $q.defer();
            var params = {
                page: page,
                pageSize: pageSize
            };
            $http.get('/api/factura', { params: params }).
                then(function (Response) { defered.resolve(Response.data); }, function (Response) { defered.reject(Response); });
            return defered.promise;
        };

        svc.GuardarFactura = function (factura) {
            var defered = $q.defer();
            $http.post('/api/factura', factura).
                then(function (Response) { defered.resolve(Response.data); }, function (Response) { defered.reject(Response); });
            return defered.promise;
        };

        svc.ValidarFactura = function (factura) {
            var errors = [];

            if (factura.cod_clie == null || factura.cod_clie.length == 0)
                errors.push("Seleccione cliente");


            if (factura.detalle.length == 0)
                errors.push("Ingrese al menos un producto");

            return errors;
        };

        svc.ListarFactura = function (num_fact) {
            var defered = $q.defer();
            $http.get('/api/factura/' + num_fact).
                then(function (Response) { defered.resolve(Response.data); }, function (Response) { defered.reject(Response); });
            return defered.promise;
        };

        return {
            ListarFacturas: svc.ListarFacturas,
            GuardarFactura: svc.GuardarFactura,
            ValidarFactura: svc.ValidarFactura,
            ListarFactura: svc.ListarFactura
        };
    })