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
            $('#tblCliente').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                Cliente.cargarCliente(id);
            });

            $('#btnEditarCliente').on('click', function () {
                Cliente.editarCliente();
            });

            $('#btnAgregarTelefono').on('click', function () {
                Cliente.agregarTelefono();
            });

            $(document).on('click', '.btn-eliminar-telefono', function () {
                const index = $(this).data('index');
                Cliente.eliminarTelefono(index);
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
                    Cliente.renderizarTelefonos();

                    $('#modalEditarCliente').modal('show');
                }
            });
        },
        agregarTelefono() {
            const numero = $('#nuevoTelefono').val().trim();
            if (!numero) {
                Swal.fire({
                    title: 'Atención',
                    text: 'Debe ingresar un número de teléfono',
                    icon: 'warning'
                });
                return;
            }

            Cliente.telefonos.push({
                id: 0,
                numero: numero,
                fkcliente: parseInt($('#Id').val())
            });

            $('#nuevoTelefono').val('');
            Cliente.renderizarTelefonos();
        },
        eliminarTelefono(index) {
            Cliente.telefonos.splice(index, 1);
            Cliente.renderizarTelefonos();
        },
        renderizarTelefonos() {
            const lista = $('#listaTelefonos');
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
                url: form.attr('action'),
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
                },
                error: function () {
                    Swal.fire({
                        title: 'Error',
                        text: 'Ocurrió un error al intentar actualizar el cliente',
                        icon: 'error'
                    });
                }
            });
        }
    };

    $(document).ready(() => Cliente.init());

})();