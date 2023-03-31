"use strict";

const analyst = prompt('Informe seu nome de analista');
$('#btn-analyst').text(analyst);

const connection = new signalR.HubConnectionBuilder()
    .withUrl('https://localhost:7289/case-alerts', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
    }).build();

connection.start()
    .then(() => console.log('Connection started!'))
    .catch(err => console.log('Error while connect with server'));

connection.on('SendCaseAlertToUser', (result) => {
    $('#table-case-alert').bootstrapTable('load', result);
});

$('#table-case-alert').bootstrapTable({
    url: '/get-case-alerts',
    pagination: true,
    rowStyle: rowStyle,
    classe: 'table table-bordered',
    columns: [
        {
            field: 'operate', title: 'Acao', align: 'center',
            formatter: function (value, row, index) {

                let btnicon = !row.analyst ? 'fa-clock-rotate-left' : 'fa-user-clock';
                let btnclass = !row.analyst ? 'btn-warning' : 'btn-success';
                let btnfunction = !row.analyst ? `onclick="StartInvestigation('${row.id}');` : '';
                let btndisabled = !row.analyst ? '' : 'disabled="true"';

                return [
                    `<button class="btn ${btnclass} assing-analyst" id="${row.id}" ${btnfunction} ${btndisabled}">
                        <i class="fas ${btnicon}" >
                        </i>
                    </button>`
                ].join('');
            }
        },
        { field: 'name', title: 'Name' },
        { field: 'description', title: 'Description' },
        { field: 'isActive', title: 'IsActive', align: 'center', },
        {
            field: 'createAt', title: 'CreateAt', align: 'center',
            formatter: function valueFormatter(value, row, index) {
                return (!value ? '-' : moment(row.createAt).format("DD/MM/YYYY HH:mm:ss"))
            }
        },
        {
            field: 'updateAt', title: 'UpdateAt', align: 'center',
            formatter: function valueFormatter(value, row, index) {
                return (!value ? '-' : moment(row.updateAt).format("DD/MM/YYYY HH:mm:ss"))
            }
        },
        { field: 'analyst', title: 'Analyst', align: 'center', },
    ]
})


function StartInvestigation(id) {

    console.log(id);

    let command = {
        analyst: analyst
    }

    SendRequest('PATCH', `/update-analyst-case-alert/${id}`, 'JSON', command, (response) => {
        console.log('Alerta atribuido');
    }, (error) => {
        alert(`Não foi possivel atribuir o alerta devido: ${error}`);
    })
}

function rowStyle(row, index) {
    if (!!row.updateAt) {
        return { classes: 'bg-opacity-10 bg-success'/*, css: { color: 'white' }*/ }
    }
    return {
        css: { color: '' }
    }
}