'use strict'
var temp = $('#SMSFollowupIDtemp');
var hostPath = "";
$(document).ready(function () {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: "/FollowUpBySms/Read"
            },
            pageSize: 10,
            schema: {
                model: {
                    id: "ScenarioCode"
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
        change: onChange,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        columns: [
            { selectable: true, width: "50px" },
            { field: "NamaSkenario", title: "Nama Skenario" },
            { field: "TipeSkenario", title: "Tipe Skenario" },
            {
                field: "TanggalMulai", title: "Tanggal Mulai" ,
                template: "#= kendo.toString(kendo.parseDate(TanggalMulai, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            { 
                field: "TanggalSelesai", title: "Tanggal Selesai",
                template: "#= kendo.toString(kendo.parseDate(TanggalSelesai, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            { field: "Total", title: "Total" },
            { field: "Terkirim", title: "Terkirim" },
            { field: "Gagal", title: "Gagal" }
        ]
    });

    var grid = $("#grid").data("kendoGrid");

    grid.thead.on("click", ".k-checkbox", onClick);
});

function onChange(e) {
    temp.val("");
    temp.val(this.selectedKeyNames())
    var path = $('.detailBtn')[0].href.split('scenarioCode=')[0] + "scenarioCode=" + temp.val();
    $('.detailBtn')[0].href = path;
}

function onClick(e) {
    var grid = $("#grid").data("kendoGrid");

    oldPageSize = grid.dataSource.pageSize();
    grid.dataSource.pageSize(grid.dataSource.data().length);

    if (grid.dataSource.data().length === grid.select().length) {
        grid.clearSelection();
    } else {
        grid.select("tr");
    };

    grid.dataSource.pageSize(oldPageSize);
}