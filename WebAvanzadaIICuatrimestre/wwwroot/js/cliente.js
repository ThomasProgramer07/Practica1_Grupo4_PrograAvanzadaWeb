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
                    url: 'Cliente/GetClientes',
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
                        title: 'Acciones',
                        orderable: false,
                        render: (data, type, row) => {
                            return `
                                <button class="btn btn-sm btn-primary btn-editar" data-id="${row.id}">Editar</button>
                                <a href="/Cliente/Detalle/${row.id}" class="btn btn-sm btn-info btn-detalle">Ver</a>
                                <button class="btn btn-sm btn-danger btn-eliminar" data-id="${row.id}">Eliminar</button>
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
            $('#tblCliente').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                Cliente.cargarCliente(id);
            });

            $('#tblCliente').on('click', '.btn-eliminar', function () {
                const id = $(this).data('id');
                Cliente.eliminarCliente(id);
            });

            $('#btnGuardarCliente').on('click', function () {
                Cliente.guardarCliente();
            });

            $('#btnEditarCliente').on('click', function () {
                Cliente.editarCliente();
            });

            $('#btnAgregarTelefonoCrear').on('click', function () {
                const index = $('#listaTelefonosCrear .telefono-item').length;
                const html = `
                    <div class="input-group mb-2 telefono-item">
                        <input type="text" name="Telefonos[${index}].Numero" class="form-control" placeholder="Número de teléfono" />
                        <button type="button" class="btn btn-outline-danger btn-remover-telefono">Remover</button>
                    </div>
                `;
                $('#listaTelefonosCrear').append(html);
            });

            $('#btnAgregarTelefono').on('click', function () {
                const numero = $('#nuevoTelefono').val().trim();
                if (numero === '') {
                    Swal.fire({
                        title: 'Incorrecto',
                        text: 'Ingrese un número de teléfono',
                        icon: 'error'
                    });
                    return;
                }

                Cliente.telefonos.push({
                    id: 0,
                    numero: numero,
                    fkcliente: parseInt($('#IdEditar').val()) || 0
                });

                Cliente.renderizarTelefonosEnEditar();
                $('#nuevoTelefono').val('');
            });

            $(document).on('click', '.btn-remover-telefono', function () {
                $(this).closest('.telefono-item').remove();
                Cliente.reindexarTelefonosCrear();
            });

            $(document).on('click', '.btn-eliminar-telefono', function () {
                const index = $(this).data('index');
                Cliente.telefonos.splice(index, 1);
                Cliente.renderizarTelefonosEnEditar();
            });
        },
        reindexarTelefonosCrear() {
            $('#listaTelefonosCrear .telefono-item').each(function (index) {
                $(this).find('input[name^="Telefonos"]').attr('name', `Telefonos[${index}].Numero`);
            });
        },
        guardarCliente() {
            let form = $('#formCrearCliente');

            if (!form.valid()) {
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {
                    if (respuesta.esCorrecto) {
                        $('#modalCrearCliente').modal('hide');
                        form[0].reset();
                        $('#listaTelefonosCrear').empty();
                        Cliente.tabla.ajax.reload();

                        Swal.fire({
                            title: 'Correcto',
                            text: respuesta.mensaje,
                            icon: 'success'
                        });
                    } else {
                        Swal.fire({
                            title: 'Incorrecto',
                            text: respuesta.mensaje,
                            icon: 'error'
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        title: 'Error',
                        text: 'Ocurrió un error al intentar guardar el cliente',
                        icon: 'error'
                    });
                }
            });
        },
        cargarCliente(id) {
            $.get(`/Cliente/GetClienteById?id=${id}`, function (resultado) {
                if (resultado.esCorrecto) {
                    let data = resultado.dato;

                    $('#IdEditar').val(data.id);
                    $('#IdentificacionEditar').val(data.identificacion);
                    $('#NombreEditar').val(data.nombre);
                    $('#Apellido1Editar').val(data.apellido1);
                    $('#Apellido2Editar').val(data.apellido2 || '');
                    $('#CorreoEditar').val(data.correo || '');

                    Cliente.telefonos = data.telefonos || [];
                    Cliente.renderizarTelefonosEnEditar();

                    $('#modalEditarCliente').modal('show');
                }
            });
        },
        renderizarTelefonosEnEditar() {
            const lista = $('#listaTelefonosEditar');
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
                        <input type="hidden" name="Telefonos[${index}].Id" value="${telefono.id || 0}" />
                        <input type="hidden" name="Telefonos[${index}].Numero" value="${telefono.numero}" />
                        <input type="hidden" name="Telefonos[${index}].Fkcliente" value="${parseInt($('#IdEditar').val()) || 0}" />
                    </div>
                `);
            });
        },
        editarCliente() {
            let form = $('#formEditarCliente');

            if (!form.valid()) {
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {
                    if (respuesta.esCorrecto) {
                        $('#modalEditarCliente').modal('hide');
                        form[0].reset();
                        Cliente.telefonos = [];
                        Cliente.tabla.ajax.reload();

                        Swal.fire({
                            title: 'Correcto',
                            text: respuesta.mensaje,
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
        eliminarCliente(id) {
            Swal.fire({
                title: "Estas seguro?",
                text: "No podras revertir esta operacion!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Si, eliminar",
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: `/Cliente/DeleteCliente?id=${id}`,
                        type: 'DELETE',
                        success: function (respuesta) {
                            if (respuesta.esCorrecto) {
                                Cliente.tabla.ajax.reload();
                                Swal.fire({
                                    title: 'Correcto',
                                    text: respuesta.mensaje || 'Cliente eliminado correctamente',
                                    icon: 'success'
                                });
                            } else {
                                Swal.fire({
                                    title: 'Incorrecto',
                                    text: respuesta.mensaje || 'No se pudo eliminar el cliente',
                                    icon: 'error'
                                });
                            }
                        },
                        error: function () {
                            Swal.fire({
                                title: 'Error',
                                text: 'Ocurrió un error al intentar eliminar el cliente',
                                icon: 'error'
                            });
                        }
                    });
                }
            });
        }
    };

    $(document).ready(() => Cliente.init());

})();