'use strict'
var temp = $('#suspecIdtemp');
$(document).ready(function () {
    $("#action").kendoDropDownList({
        change: function (e) {
            if (this.dataItem(e.item).value !== "0" && this.dataItem(e.item).value.toLowerCase() == "Reactivate".toLowerCase()) {
                Save(temp.val(), '/InactiveLeads/ReactivateSuspect')
            }

            if (this.dataItem(e.item).value !== "0" && this.dataItem(e.item).value.toLowerCase() == "Delete".toLowerCase()) {
                Save(temp.val(), '/InactiveLeads/DeleteSuspect')
            }
        }
    });

    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: "/InactiveLeads/Read"
            },
            pageSize: 10,
            schema: {
                model: {
                    id: "SuspectId"
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
            { field: "Name", title: "Name" },
            { field: "Telepon", title: "Telepon" },
            { field: "Email", title: "Email" },
            { field: "Kota", title: "Kota" },
            { field: "Sumber", title: "Sumber" },
            {
                field: "TglMasuk", title: "Tgl Masuk",
                template: "#= kendo.toString(kendo.parseDate(TglMasuk, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            { field: "Cabang", title: "Cabang" },
            { field: "Scenario", title: "Scenario" },
            //{ field: "Stage", title: "Stage" },
            { field: "Note", title: "Note" }
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

function Save(suspectIds, url)
{
    if (suspectIds.length > 0) {
        $.ajax({
            url: url,
            type: "POST",
            dataType: 'json',
            data: { suspectIds: suspectIds },
            success: function (result) {
                temp.val("");
                
                //$("#grid").data("kendoGrid").dataSource.data([]);
                //$("#grid").data("kendoGrid").dataSource.read();
                location.reload();
                setDefaultDropDownList();
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
    var dropdownlist = $("#action").data("kendoDropDownList");
    dropdownlist.select(function (dataItem) {
        return dataItem.value === "0";
    });
}
