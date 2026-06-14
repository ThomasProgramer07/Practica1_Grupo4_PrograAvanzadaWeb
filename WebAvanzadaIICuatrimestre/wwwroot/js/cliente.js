(() => {

    const Cliente = {
        tabla: null,
        init() {
            this.inicializarTabla();
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
                            return `<a href="/Cliente/Detalle/${row.id}" class="btn btn-sm btn-primary btn-detalle">Ver</a>`;
                        }
                    }
                ],
                language: {
                    url: 'https://cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
                }
            });
        }
    };

    $(document).ready(() => Cliente.init());

})();