'use strict'
var temp = $('#suspecIdtemp');
$(document).ready(function () {
    $("#input-btn-distribute").click(function (event) {
        if (prospectIds.val().length > 0) {
            $("#form-prospecttodeal").submit();
        } else {
            alert("There are no data selected");
        };
    });

    $("#action").kendoDropDownList({
        change: function (e) {
            if (this.dataItem(e.item).value !== "0" && this.dataItem(e.item).value.toLowerCase() === "Inactive".toLowerCase()) {
                Save(temp.val(), '/SuspectToProspect/DeactivateSuspect')
            } else if (this.dataItem(e.item).value !== "0" && this.dataItem(e.item).value.toLowerCase() === "distribute") {
                Distribute(temp.val());
            }
        }
    });

    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: "/SuspectToProspect/Read"
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
            { field: "Title", title: "Title" },
            { field: "Name", title: "Nama" },
            { field: "Phone", title: "Telepon" },
            { field: "LastPurchaseUnit", title: "Pembelian Terakhir" }, // Belum di join
            { field: "Dealer", title: "Cabang" },
            { field: "LastSalesName", title: "Sales Terakhir" },
            { field: "CurrentSalesName", title: "Sales Sekarang" },
            { field: "ScenarioName", title: "Scenario" },
            { field: "CallLog", title: "Call Log", encoded: false, filterable:false },
            { field: "Note", title: "Catatan", filterable:false },
            { field: "Status", title: "Status" }
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

function Distribute(suspectIds) {
    var $form = $('<form>', {
        action: "/DistributeSuspect/List",
        method: 'post'
    });

    $('<input>').attr({
        type: "hidden",
        name: "suspectIds",
        value: suspectIds
    }).appendTo($form);

    $form.appendTo('body').submit();
}

function Save(suspectIds, url) {
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

$(function () {
    $('.input-daterange').datepicker({
        startDate: "0d",
        autoclose: true,
        orientation: "top left",
        todayHighlight: true,
        format: "yyyy-mm-dd"
    });
});
$(document).on('click', '.callLogButton', function () {
    var susid = $(this).data('susid');
    var scenarioCode = $(this).data('scode');
    var scenarioName = $(this).data('sname');
    $("#scenarioName").text(scenarioName);
    kendo.ui.progress($(".loaderscript"), true);

    $.ajax({ // Get Data Pelanggan
        url: "/SuspectToProspect/GetDataCustomer",
        type: "POST",
        dataType: 'json',
        data: { suspectID: susid },
        success: function (result) {
            var viewModel = kendo.observable({
                customer: result
            });
            kendo.bind($("#detailcustomer"), viewModel);
        },
        error: function (err) {
            console.log(err);
        }
    });

    $.ajax({ // Get Scripts
        url: "/SuspectToProspect/GetCallScripts",
        type: "POST",
        dataType: 'json',
        data : {scenarioCode:scenarioCode},
        success: function (result) {
            var arr = new kendo.data.ObservableArray(result);
            var viewModel = kendo.observable({
                arr: arr
            });
            kendo.bind($("#scriptForm"), viewModel);
            $(".k-dropdown").kendoDropDownList();
            $(".k-date").kendoDatePicker();
        },
        complete: function(){
            kendo.ui.progress($(".loaderscript"), false);
        },
        error: function (err) {
            console.log(err);
        }
    });

    $('form').submit(function (e) {
        var callStatus = parseInt($('#selectUpdate').val());
        var nextFollowup = $('.k-date').val();
        //var followUp = $('#followUpDatePicker').val();
        var note = $('#note').val();

        $.ajax({
            url: "/SuspectToProspect/UpdateStatus",
            type: "POST",
            dataType: 'json',
            data: { suspectID: susid, callStatus: callStatus, nextFollowup: nextFollowup, note: note },
            success: function (result) {
                temp.val("");
                location.reload();
                setDefaultDropDownList();

            },
            error: function (err) {
                temp.val("");
            }
        });
        return false;
    });

});