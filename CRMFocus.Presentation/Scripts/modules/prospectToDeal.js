'use strict'
var prospectIds = $('#input-txt-prospect-ids');
jQuery(function ($) {

    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: "/ProspectToDeal/Read"
            },
            pageSize: 10,
            schema: {
                model: {
                    id: "ProspectId"
                }
            }
        },
        persistSelection: true,
        scrollable: true,
        sortable: true,
        change: onChange,
        columnMenu: {
            sortable: false,
            filterable: false
        },
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
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
        columns: [
            { selectable: true, width: "50px" },
            { title: "Title", field: "Title" },
            { title: "Nama", field: "Name" },
            { title: "Telepon", field: "Phone" },
            { title: "Pembelian Terakhir", field: "LastPurchaseUnit" },
            { title: "Sales Person", field: "SalesName" },
            { title: "Cabang", field: "Dealer" },
            { title: "Cabang Terkini", field: "CurrentDealer" },
            { title: "Follow Up", field: "FollowUp", encoded: false },
            { title: "Status", field: "Status" },
        ]
    });

    function onChange(arg) {
        prospectIds.val(this.selectedKeyNames());
        //alert("The selected product ids are: [" + this.selectedKeyNames().join(", ") + "]");
    }

    $("#input-btn-distribute").click(function (event) {
        if (prospectIds.val().length > 0) {
            $("#form-prospecttodeal").submit();
        } else {
            alert("There are no data selected");
        }; 
    });

    $("#act").kendoDropDownList();
    var size = $("#act").data("kendoDropDownList");
});