(() => {

    const Cliente = {
        tabla: null,
        telefonos: [],
        init() {
            this.inicializarTabla();
            this.registrarEventos();
        },
        inicializarTabla() {
            this.tabla = $('#tblCliente').DataTable({
                ajax: {
                    url: '/Cliente/GetClientes',
                    type: 'GET',
                    dataSrc: 'dato'
                },
                columns: [
                    { data: 'id' },
                    { data: 'identificacion' },
                    { data: 'nombre' },
                    { data: 'apellido1' },
                    { data: 'apellido2', defaultContent: '-' },
                    { data: 'correo', defaultContent: '-' },
                    {
                        data: 'telefonos',
                        orderable: false,
                        render: (telefonos) => {
                            if (!telefonos || telefonos.length === 0) return '-';
                            return telefonos.map(t => t.numero).join(', ');
                        }
                    },
                    {
                        data: null,
                        orderable: false,
                        render: (data, type, row) => {
                            return `
                                <button class="btn btn-sm btn-primary btn-editar" data-id="${row.id}">Editar</button>
                                <a href="/Cliente/Detalle/${row.id}" class="btn btn-sm btn-info btn-detalle">Ver</a>
                            `;
                        }
                    }
                ],
                language: {
                    url: 'https://cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
                }
            });
        },
        registrarEventos() {

            $(document).on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                Cliente.cargarCliente(id);
            });

            $(document).on('click', '#btnEditarCliente', function () {
                Cliente.editarCliente();
            });

            $(document).on('click', '#btnGuardarCliente', function () {
                Cliente.guardarCliente();
            });

            $(document).on('click', '#btnAgregarTelefonoCrear', function () {
                const html = `
                    <div class="input-group mb-2 div-telefono-row">
                        <input type="text" class="form-control input-telefono" placeholder="Número de teléfono" />
                        <button class="btn btn-outline-danger btn-remover-telefono" type="button">Remover</button>
                    </div>
                `;
                $('#listaTelefonosCrear').append(html);
            });

            $(document).on('click', '.btn-remover-telefono', function () {
                $(this).closest('.div-telefono-row').remove();
            });
        },
        cargarCliente(id) {
            $.get(`/Cliente/GetClienteById?id=${id}`, function (resultado) {
                if (resultado.esCorrecto) {
                    let data = resultado.dato;

                    $('#Id').val(data.id);
                    $('#Identificacion').val(data.identificacion);
                    $('#Nombre').val(data.nombre);
                    $('#Apellido1').val(data.apellido1);
                    $('#Apellido2').val(data.apellido2);
                    $('#Correo').val(data.correo);

                    Cliente.telefonos = data.telefonos || [];
                    Cliente.renderizarTelefonosEnEditar();

                    $('#modalEditarCliente').modal('show');
                }
            });
        },
        renderizarTelefonosEnEditar() {
            const lista = $('#listaTelefonosEditar') || $('#listaTelefonos');
            lista.empty();

            if (Cliente.telefonos.length === 0) {
                lista.append('<div class="alert alert-info">No hay teléfonos registrados</div>');
                return;
            }

            Cliente.telefonos.forEach((telefono, index) => {
                lista.append(`
                    <div class="list-group-item d-flex justify-content-between align-items-center">
                        <span>${telefono.numero}</span>
                        <button type="button" class="btn btn-sm btn-danger btn-eliminar-telefono" data-index="${index}">
                            Eliminar
                        </button>
                    </div>
                `);
            });
        },
        editarCliente() {
            let form = $('#formEditarCliente');

            if ($.validator && $.validator.unobtrusive) {
                $.validator.unobtrusive.parse(form);
            }

            if (!form.valid()) {
                return;
            }

            const clienteData = {
                id: parseInt($('#Id').val()),
                identificacion: $('#Identificacion').val(),
                nombre: $('#Nombre').val(),
                apellido1: $('#Apellido1').val(),
                apellido2: $('#Apellido2').val(),
                correo: $('#Correo').val(),
                telefonos: Cliente.telefonos
            };

            $.ajax({
                url: '/Cliente/Editar',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(clienteData),
                success: function (respuesta) {
                    if (respuesta.esCorrecto) {
                        $('#modalEditarCliente').modal('hide');
                        form[0].reset();
                        Cliente.telefonos = [];
                        Cliente.tabla.ajax.reload();

                        Swal.fire({
                            title: 'Correcto',
                            text: respuesta.mensaje || 'Cliente actualizado correctamente',
                            icon: 'success'
                        });
                    } else {
                        Swal.fire({
                            title: 'Incorrecto',
                            text: respuesta.mensaje,
                            icon: 'error'
                        });
                    }
                }
            });
        },
        guardarCliente() {
            let form = $('#formCrearCliente');
            if ($.validator && $.validator.unobtrusive) {
                $.validator.unobtrusive.parse(form);
            }

            if (!form.valid()) {
                return;
            }

            const clienteDto = {
                Identificacion: form.find('#Identificacion').val(),
                Nombre: form.find('#Nombre').val(),
                Apellido1: form.find('#Apellido1').val(),
                Apellido2: form.find('#Apellido2').val(),
                Correo: form.find('#Correo').val(),
                Telefonos: []
            };

            form.find('.input-telefono').each(function () {
                const numeroTel = $(this).val();
                if (numeroTel.trim() !== "") {
                    clienteDto.Telefonos.push({ Numero: numeroTel });
                }
            });

            $.ajax({
                url: '/Cliente/Crear',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(clienteDto),
                success: function (respuesta) {
                    if (respuesta.esCorrecto) {
                        $('#modalCrearCliente').modal('hide');
                        form[0].reset();
                        form.find('.div-telefono-row').remove();
                        Cliente.tabla.ajax.reload();
                    }
                    else {
                        Swal.fire({
                            title: 'Incorrecto',
                            text: respuesta.mensaje,
                            icon: 'error'
                        });
                    }
                }
            });
        }
    };

    $(document).ready(() => Cliente.init());

})();