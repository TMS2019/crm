$(document).ready(function () {
    $.ajax({
        url: '/FollowUpBySms/ReadDetails',
        type: "POST",
        dataType: 'json',
        data: { scenarioCode: window.location.href.split('scenarioCode=')[1] },
        success: function (result) {
            createGrid(result)
        },
        error: function (err) {
            showFailMessage(err.statusText);
        }
    });   
});

function createGrid(datagrid) {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            data: datagrid,
            pageSize: 10,
            schema: {
                model: {
                    id: "CRMCustomerNum"
                }
            }
        },
        persistSelection: true,
        pageSize: 10,
        scrollable: true,
        columnMenu: {
            sortable: false,
            filterable: false
        },
        filterable: {
            mode: "row",
            extra: false,
            operators: {
                string: {
                    startswith: "Starts with",
                    eq: "Is equal to",
                    neq: "Is not equal to"
                }
            }
        },
        sortable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        columns: [
            { selectable: false, width: "50px" },
            { field: "Nama", title: "Nama" },
            { field: "Telepon", title: "Telepon" },
            { field: "Unit", title: "Unit" },
            //{ field: "NoPlat", title: "No Plat" },
            {
                field: "TglUpload", title: "Tgl Upload",
                template: "#= kendo.toString(kendo.parseDate(TglUpload, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            {
                field: "TglTerkirim", title: "Tgl Terkirim",
                template: "#= kendo.toString(kendo.parseDate(TglTerkirim, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            { field: "Scenario", title: "Scenario" },
            { field: "Status", title: "Status" }
        ]
    });
}