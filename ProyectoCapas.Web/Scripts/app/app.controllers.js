angular.module('app')

    .controller('ListarArticulosController', function (ArticulosService, bsLoadingOverlayService, $uibModal, EventListener) {

        var vm = this;
        vm.flags = { server_error: false }
        vm.pagination = {
            page: 1,
            pageSize: 5,
            total: 0,
            items: []
        };

        vm.$onInit = function () {
            GetPage();
            EventListener.listen('actualizar-pagina-articulos', function () {
                GetPage();
            });
        };

        vm.changePage = function () {
            GetPage();
        };

        vm.VerDetalleArticulo = function (cod_art) {
            var success = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-articulos' });

                $uibModal.open({
                    component: 'formularioArticulo',
                    animation: true,
                    backdrop: 'static',
                    size: 'sm',
                    resolve: {
                        config: function () {
                            return {
                                articulo: Response.articulo,
                                tipoFormulario: 3
                            }
                        }
                    }
                });
            };

            var error = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-articulos' });
                swal('Hubo un error', null, 'error');
            };
            bsLoadingOverlayService.start({ referenceId: 'ovl-listar-articulos' });
            ArticulosService.ListarArticulo(cod_art).then(success).catch(error);
        };

        vm.BorrarArticulo = function (cod_art) {
            var success = function (Response) {
                GetPage();
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-articulos' });
                swal('Artículo eliminado', null, 'success');
            };

            var error = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-articulos' });
                swal('Hubo un error', null, 'error');
            };

            var config = {
                title: '¿El artículo se eliminará, Deseea continuar?',
                allowEscapeKey: false,
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Sí",
                cancelButtonText: "No",
                closeOnConfirm: false,
                showLoaderOnConfirm: true
            };
            swal(config, function () {
                bsLoadingOverlayService.start({ referenceId: 'ovl-listar-articulos' });
                ArticulosService.EliminarArticulo(cod_art).then(success).catch(error);
            });
        };

        vm.NuevoArticulo = function () {
            $uibModal.open({
                component: 'formularioArticulo',
                animation: true,
                backdrop: 'static',
                size: 'sm',
                resolve: {
                    config: function () {
                        return {
                            articulo: {},
                            tipoFormulario: 1
                        }
                    }
                }
            });
        };

        vm.ActualizarArticulo = function (articulo) {
            var success = function (Response) {
                delete articulo.$old;
                articulo.$update = false;
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-articulos' });
            };
            var error = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-articulos' });
                swal('Hubo un error', null, 'error');
            };
            var errors = ArticulosService.ValidarArticulo(articulo);
            if (errors.length > 0) {
                var msg = '';
                for (var i in errors) {
                    msg += '- ' + errors[i] + '\n';
                }
                swal('Resuelva las siguientes inconsistencias', msg, 'error');
                return false;
            }
            bsLoadingOverlayService.start({ referenceId: 'ovl-listar-articulos' });
            ArticulosService.ActualizarArticulo(articulo.cod_art, articulo).then(success).catch(error);
        };

        vm.HabilitarEdicion = function (articulo) {
            var old = angular.copy(articulo);
            articulo.$old = old;
            articulo.$update = true;
        };

        vm.CancelarEdicion = function (articulo) {
            articulo.descrip = articulo.$old.descrip;
            articulo.prec_unic = articulo.$old.prec_unic;
            articulo.stock = articulo.$old.stock;
            delete articulo.$old;
            articulo.$update = false;
        };

        var GetPage = function () {
            var success = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-articulos' });
                vm.pagination = Response.pagination;
            };

            var error = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-articulos' });
                swal('Hubo un error', null, 'error');
            };
            bsLoadingOverlayService.start({ referenceId: 'ovl-listar-articulos' });
            ArticulosService.ListarArticulos(vm.pagination.page, vm.pagination.pageSize).then(success).catch(error);
        }

    })

    .controller('FormularioArticuloController', function (ArticulosService, bsLoadingOverlayService, EventListener) {

        var vm = this;

        vm.$onInit = function () {
            vm.tipoFormulario = vm.resolve.config.tipoFormulario;
            vm.articulo = vm.resolve.config.articulo;
            setTitle();
        };

        vm.guardar = function () {
            var success = function (Response) {
                EventListener.emit('actualizar-pagina-articulos');
                bsLoadingOverlayService.stop({ referenceId: 'ovl-formulario-articulo' });
                if (vm.tipoFormulario == 1) {
                    swal('Artículo registrado', null, 'success');
                    vm.close();
                } else {
                    swal('Artículo actualizado', null, 'success');
                    vm.tipoFormulario = 3;
                }
            };
            var error = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-formulario-articulo' });
                swal('Hubo un error', null, 'error');
            };

            var errors = ArticulosService.ValidarArticulo(vm.articulo);
            if (errors.length > 0) {
                var msg = '';
                for (var i in errors) {
                    msg += '- ' + errors[i] + '\n';
                }
                swal('Resuelva las siguientes inconsistencias', msg, 'error');
                return false;
            }

            var method = null;
            if (vm.tipoFormulario == 1)
                method = ArticulosService.GuardarArticulo(vm.articulo);
            if (vm.tipoFormulario == 2)
                method = ArticulosService.ActualizarArticulo(vm.articulo.cod_art, vm.articulo);
            bsLoadingOverlayService.start({ referenceId: 'ovl-formulario-articulo' });
            method.then(success).catch(error);
        }


        var setTitle = function () {
            vm.title = "";
            switch (vm.tipoFormulario) {
                case 1:
                    vm.title = "Nuevo Artículo";
                    break;
                case 2:
                    vm.title = "Artículo Cod. " + vm.articulo.cod_art;
                    break;
                case 3:
                    vm.title = "Detalle de Artículo";
                    break;
            };
        };

    })

    .controller('ListarClientesController', function (ClientesService, bsLoadingOverlayService, $uibModal, EventListener) {

        var vm = this;
        vm.flags = { server_error: false }
        vm.pagination = {
            page: 1,
            pageSize: 5,
            total: 0,
            items: []
        };

        vm.$onInit = function () {
            GetPage();
            EventListener.listen('actualizar-pagina-clientes', function () {
                GetPage();
            });
        };

        vm.changePage = function () {
            GetPage();
        };

        vm.VerDetalleCliente = function (cod_art) {
            var success = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-clientes' });

                $uibModal.open({
                    component: 'formularioCliente',
                    animation: true,
                    backdrop: 'static',
                    size: 'sm',
                    resolve: {
                        config: function () {
                            return {
                                cliente: Response.cliente,
                                tipoFormulario: 3
                            }
                        }
                    }
                });
            };

            var error = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-clientes' });
                swal('Hubo un error', null, 'error');
            };
            bsLoadingOverlayService.start({ referenceId: 'ovl-listar-clientes' });
            ClientesService.ListarCliente(cod_art).then(success).catch(error);
        };

        vm.BorrarCliente = function (cod_clie) {
            var success = function (Response) {
                GetPage();
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-clientes' });
                swal('Cliente eliminado', null, 'success');
            };

            var error = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-clientes' });
                swal('Hubo un error', null, 'error');
            };

            var config = {
                title: '¿El cliente se eliminará, Deseea continuar?',
                allowEscapeKey: false,
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Sí",
                cancelButtonText: "No",
                closeOnConfirm: false,
                showLoaderOnConfirm: true
            };
            swal(config, function () {
                bsLoadingOverlayService.start({ referenceId: 'ovl-listar-clientes' });
                ClientesService.EliminarCliente(cod_clie).then(success).catch(error);
            });
        };

        vm.NuevoCliente = function () {
            $uibModal.open({
                component: 'formularioCliente',
                animation: true,
                backdrop: 'static',
                size: 'sm',
                resolve: {
                    config: function () {
                        return {
                            cliente: {},
                            tipoFormulario: 1
                        }
                    }
                }
            });
        };

        vm.ActualizarCliente = function (cliente) {
            var success = function (Response) {
                delete cliente.$old;
                cliente.$update = false;
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-clientes' });
            };
            var error = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-clientes' });
                swal('Hubo un error', null, 'error');
            };
            var errors = ClientesService.ValidarCliente(cliente);
            if (errors.length > 0) {
                var msg = '';
                for (var i in errors) {
                    msg += '- ' + errors[i] + '\n';
                }
                swal('Resuelva las siguientes inconsistencias', msg, 'error');
                return false;
            }
            bsLoadingOverlayService.start({ referenceId: 'ovl-listar-clientes' });
            ClientesService.ActualizarCliente(cliente.cod_clie, cliente).then(success).catch(error);
        };

        vm.HabilitarEdicion = function (cliente) {
            var old = angular.copy(cliente);
            cliente.$old = old;
            cliente.$update = true;
        };

        vm.CancelarEdicion = function (cliente) {
            cliente.mon_ape = cliente.$old.mon_ape;
            cliente.telef = cliente.$old.telef;
            cliente.dni = cliente.$old.dni;
            cliente.dir = cliente.$old.dir;
            delete cliente.$old;
            cliente.$update = false;
        };

        var GetPage = function () {
            var success = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-clientes' });
                vm.pagination = Response.pagination;
            };

            var error = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-clientes' });
                swal('Hubo un error', null, 'error');
            };
            bsLoadingOverlayService.start({ referenceId: 'ovl-listar-clientes' });
            ClientesService.ListarClientes(vm.pagination.page, vm.pagination.pageSize).then(success).catch(error);
        }

    })

    .controller('FormularioClienteController', function (ClientesService, bsLoadingOverlayService, EventListener) {

        var vm = this;

        vm.$onInit = function () {
            vm.tipoFormulario = vm.resolve.config.tipoFormulario;
            vm.cliente = vm.resolve.config.cliente;
            setTitle();
        };

        vm.guardar = function () {
            var success = function (Response) {
                EventListener.emit('actualizar-pagina-clientes');
                bsLoadingOverlayService.stop({ referenceId: 'ovl-formulario-cliente' });
                if (vm.tipoFormulario == 1) {
                    swal('Cliente registrado', null, 'success');
                    vm.close();
                } else {
                    swal('Cliente actualizado', null, 'success');
                    vm.tipoFormulario = 3;
                }
            };
            var error = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-formulario-cliente' });
                swal('Hubo un error', null, 'error');
            };

            var errors = ClientesService.ValidarCliente(vm.cliente);
            if (errors.length > 0) {
                var msg = '';
                for (var i in errors) {
                    msg += '- ' + errors[i] + '\n';
                }
                swal('Resuelva las siguientes inconsistencias', msg, 'error');
                return false;
            }

            var method = null;
            if (vm.tipoFormulario == 1)
                method = ClientesService.GuardarCliente(vm.cliente);
            if (vm.tipoFormulario == 2)
                method = ClientesService.ActualizarCliente(vm.cliente.cod_clie, vm.cliente);
            bsLoadingOverlayService.start({ referenceId: 'ovl-formulario-cliente' });
            method.then(success).catch(error);
        }


        var setTitle = function () {
            vm.title = "";
            switch (vm.tipoFormulario) {
                case 1:
                    vm.title = "Nuevo Cliente";
                    break;
                case 2:
                    vm.title = "Cliente Cod. " + vm.cliente.cod_clie;
                    break;
                case 3:
                    vm.title = "Detalle de Cliente";
                    break;
            };
        };

    })

    .controller('ListarFacturasController', function (FacturasService, bsLoadingOverlayService, $uibModal, EventListener) {

        var vm = this;
        vm.flags = { server_error: false }
        vm.pagination = {
            page: 1,
            pageSize: 5,
            total: 0,
            items: []
        };

        vm.$onInit = function () {
            GetPage();
            EventListener.listen('actualizar-pagina-facturas', function () {
                GetPage();
            });
        };

        vm.changePage = function () {
            GetPage();
        };

        vm.VerDetalleFactura = function (num_fact) {
            $uibModal.open({
                component: 'formularioFactura',
                animation: true,
                backdrop: 'static',
                size: 'sm',
                resolve: {
                    config: function () {
                        return {
                            numeroFactura: num_fact,
                            tipoFormulario: 3
                        }
                    }
                }
            });
        };

        vm.NuevaFactura = function () {
            $uibModal.open({
                component: 'formularioFactura',
                animation: true,
                backdrop: 'static',
                size: 'sm',
                resolve: {
                    config: function () {
                        return {
                            tipoFormulario: 1
                        }
                    }
                }
            });
        };

        var GetPage = function () {
            var success = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-facturas' });
                vm.pagination = Response.pagination;
            };

            var error = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-listar-facturas' });
                swal('Hubo un error', null, 'error');
            };
            bsLoadingOverlayService.start({ referenceId: 'ovl-listar-facturas' });
            FacturasService.ListarFacturas(vm.pagination.page, vm.pagination.pageSize).then(success).catch(error);
        }

    })

    .controller('FormularioFacturaController', function (FacturasService, bsLoadingOverlayService, EventListener) {

        var vm = this;

        vm.url_buscar_clientes = '/api/cliente/listar?filtro=';
        vm.url_buscar_articulos = '/api/articulo/listar?filtro=';

        vm.$onInit = function () {
            vm.tipoFormulario = vm.resolve.config.tipoFormulario;
            vm.factura = {
                detalle: []
            };
            if (vm.tipoFormulario == 3)
                ListarFactura(vm.resolve.config.numeroFactura);

            setTitle();
        };

        vm.guardar = function () {
            var success = function (Response) {
                EventListener.emit('actualizar-pagina-facturas');
                bsLoadingOverlayService.stop({ referenceId: 'ovl-formulario-factura' });
                vm.close();
                swal('Se generó la factura ' + Response.factura, null, 'success');
            };

            var error = function (Response) {
                bsLoadingOverlayService.stop({ referenceId: 'ovl-formulario-factura' });
                swal('Hubo un error', null, 'error');
            };

            var config = {
                title: '¿Se generará una nueva factura, Deseea continuar?',
                allowEscapeKey: false,
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Sí",
                cancelButtonText: "No",
                closeOnConfirm: false,
                showLoaderOnConfirm: true
            };

            var errors = FacturasService.ValidarFactura(vm.factura);
            if (errors.length > 0) {
                var msg = '';
                for (var i in errors) {
                    msg += '- ' + errors[i] + '\n';
                }
                swal('Resuelva las siguientes inconsistencias', msg, 'error');
                return false;
            }

            swal(config, function () {
                bsLoadingOverlayService.start({ referenceId: 'ovl-formulario-factura' });
                FacturasService.GuardarFactura(vm.factura).then(success).catch(error);
            });

        }

        vm.selectCliente = function (item) {
            if (angular.isUndefined(item)) {
                vm.factura.mon_ape = null;
                vm.factura.cod_clie = null;
                return;
            }
            if (item.originalObject == null || item.originalObject == undefined) {
                vm.factura.mon_ape = null;
                vm.factura.cod_clie = null;
                return;
            }
            if (angular.isString(item.originalObject) && item.originalObject == "") {
                vm.factura.mon_ape = null;
                vm.factura.cod_clie = null;
                return;
            } else {
                var cliente = item.originalObject;
                vm.factura.mon_ape = cliente.mon_ape;
                vm.factura.cod_clie = cliente.cod_clie;
            }
        };

        vm.selectArticulo = function (item) {
            if (angular.isUndefined(item)) {
                return;
            }
            if (item.originalObject == null || item.originalObject == undefined) {
                return;
            }
            if (angular.isString(item.originalObject) && item.originalObject == "") {
                return;
            } else {
                var articulo = item.originalObject;
                if (articulo.stock == 0) {
                    swal('No hay stock!', 'No se puede agregar este producto', 'error');
                    return;
                }
                if (articuloEnDetalle(articulo.cod_art)) {
                    swal('El artículo ya se ha agregado', '', 'warning');
                    return;
                }
                vm.factura.detalle.push({
                    descrip: articulo.descrip,
                    cod_art: articulo.cod_art,
                    cant: 1,
                    stock: articulo.stock,
                    prec_unic: articulo.prec_unic
                });
            }
        };

        vm.QuitarVenta = function ($item) {
            vm.factura.detalle.splice(vm.factura.detalle.indexOf($item), 1);
        };

        vm.montoTotal = function () {
            var monto = 0;
            if (vm.factura.detalle.length == 0) {
                return monto;
            }
            for (var i in vm.factura.detalle) {
                monto += vm.factura.detalle[i].cant * vm.factura.detalle[i].prec_unic;
            }

            return monto;

        }

        var articuloEnDetalle = function (cod_art) {
            var existe = false;
            for (var i in vm.factura.detalle) {
                if (vm.factura.detalle[i].cod_art == cod_art) {
                    existe = true;
                    break;
                }
            }
            return existe;
        };

        var ListarFactura = function (numeroFactura) {
            var success = function (Response) {
                vm.factura = Response.factura;
            };

            var error = function (Response) {
                swal('Hubo un error', null, 'error');
            };
            FacturasService.ListarFactura(numeroFactura).then(success).catch(error);
        };


        var setTitle = function () {
            vm.title = "";
            switch (vm.tipoFormulario) {
                case 1:
                    vm.title = "Nueva Factura";
                    break;
                case 3:
                    vm.title = "Detalle de factura";
                    break;
            };
        };

    })
