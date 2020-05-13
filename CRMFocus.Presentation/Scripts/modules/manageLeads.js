'use strict'
var temp = $('#suspecIdtemp');
var newDealer = "";
var newDealerDesciption = "";
$(document).ready(function () {
    $("#newDealer").kendoDropDownList({
        change: function (e) {
            if (this.dataItem(e.item).value !== "0") {
                newDealer = this.dataItem(e.item).value;
                newDealerDesciption = this.dataItem(e.item).text;               
            }
            
        }
    });

    $("#set").click(function () {
        var selectedRows = temp.val().split(',');
        var grid = $('#grid').data('kendoGrid');
        for (var i = 0; i < selectedRows.length; i++) {
            var dataItem = grid.dataSource.get(selectedRows[i]);
            dataItem.set("CabangBaru", newDealerDesciption);
        }
        setDefaultDropDownList();
    });

    $("#save").click(function () {
        Save(temp.val(), newDealer);
    });

    $("#reset").click(function () {
        $("#grid").data("kendoGrid").dataSource.read();
    });

    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: "/ManageLeads/Read"
            },
            pageSize: 10,
            schema: {
                model: {
                    id: "SuspectId"
                }
            }
        },
        persistSelection: true,
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
            { field: "Name", title: "Name" },
            { field: "Telepon", title: "Telepon" },
            { field: "Email", title: "Email" },
            { field: "PembelianTerakhir", title: "Pembelian Terakhir" },
            { field: "Sumber", title: "Sumber" },
            {
                field: "TglMasuk", title: "Tgl Beli",
                template: "#= kendo.toString(kendo.parseDate(TglMasuk, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            { field: "Cabang", title: "Cabang" },
            { field: "CabangBaru", title: "Cabang Baru" },
            { field: "ScenarioName", title: "Scenario Name" }
        ]
    });

    var grid = $("#grid").data("kendoGrid");

    grid.thead.on("click", ".k-checkbox", onClick);
});


function onChange(e) {
    temp.val("");
    temp.val(this.selectedKeyNames())
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

function Save(suspectIds, currentDealer) {
    if (suspectIds.length > 0) {
        $.ajax({
            url: '/ManageLeads/Save',
            type: "POST",
            dataType: 'json',
            data: { suspectIds: suspectIds, currentDealer: currentDealer},
            success: function (result) {
                temp.val("");

                //$("#grid").data("kendoGrid").dataSource.data([]);
                //$("#grid").data("kendoGrid").dataSource.read();
                location.reload();
               // setDefaultDropDownList();
                // alert(suspectIds);
                //if (result.Status) {
                //    succedMessage.text(result.Message);
                //}
                //else {
                //    errorMessage.text("Error : *" + result.Message);
                //}
            },
            error: function (err) {
                temp.val("");
                //errorMessage.text("Error : *" + err.statusText);
            }
        });
    }
}

function setDefaultDropDownList() {
    var dropdownlist = $("#newDealer").data("kendoDropDownList");
    dropdownlist.select(function (dataItem) {
        return dataItem.value === "0";
    });
}