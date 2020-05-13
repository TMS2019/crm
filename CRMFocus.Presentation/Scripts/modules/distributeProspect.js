'use strict'
var prospectIdTemp = $("#prospectIdTemp");
var newCurrentDealerCode = "";
var newCurrentDealer = "";
var newCurrentSales = "";
$(document).ready(function () {
    $("#input-btn-reset").click(function () {
        $("#grid").data("kendoGrid").dataSource.read();
    });

    $.ajax({
        url: '/DistributeProspect/GetSelectedProspect',
        method: 'POST',
        dataType: 'json',
        data: { prospectIds : $("#ProspectIds").val() },
        success: function (result) {
            createGrid(result)
        },
        error: function (err) {
            alert(err);
        }
    });

    function createGrid(dataGrid) {
        $("#grid").kendoGrid({
            dataSource: {
                type: "json",
                data: dataGrid,
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
            pageable: {
                refresh: false,
                pageSizes: true,
                buttonCount: 5
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
            columns: [
                { selectable: true, width: "50px" },
                { title: "Title", field: "Title" },
                { title: "Nama", field: "Name" },
                { title: "Telepon", field: "Phone" },
                { title: "Pembelian Terakhir", field: "LastPurchaseUnit" },
                { title: "Cabang", field: "LastDealer" },
                { title: "Sales Person", field: "LastSales" },
                { hidden: true, field: "CurrentDealerCode" },
                { title: "Cabang Terkini", field: "CurrentDealer" },
                { title: "Sales Terkini", field: "CurrentSales" },
                { title: "Expire Date", field: "ExpireDate" },
            ]
        });
    }

    function onChange(arg) {
        prospectIdTemp.val("");
        prospectIdTemp.val(this.selectedKeyNames());
    }
    $("#Sales").kendoDropDownList({});
    $("#Dealers").kendoDropDownList({
        change: function (e) {
            $("#Sales").data("kendoDropDownList").dataSource.data([]);
            $.ajax({
                url: '/DistributeProspect/ReadSales',
                type: 'POST',
                dataType: 'json',
                data: { dealerCode: this.dataItem(e.item).value },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        $("#Sales").data("kendoDropDownList").dataSource.add({ "text": result[i].Text, "value": result[i].Value });
                    }
                },
                error: function (err) {
                    alert(err);
                }
            });
        }
    });

    //initial data load for sales
    $("#Dealers").data("kendoDropDownList").trigger("change");

    $("#input-btn-assign").click(function () {
        var selectedRows = prospectIdTemp.val().split(',');
        newCurrentDealerCode = $("#Dealers").data("kendoDropDownList").value();
        newCurrentDealer = $("#Dealers").data("kendoDropDownList").text();
        newCurrentSales = $("#Sales").data("kendoDropDownList").text();
        var grid = $('#grid').data('kendoGrid');
        for (var i = 0; i < selectedRows.length; i++) {
            var dataItem = grid.dataSource.get(selectedRows[i]);
            dataItem.set("CurrentDealerCode", newCurrentDealerCode);
            dataItem.set("CurrentDealer", newCurrentDealer);
            dataItem.set("CurrentSales", newCurrentSales);
        }
    });

    $("#input-btn-autoassgin").click(function () {
        var sales = $("#Sales").data("kendoDropDownList").dataSource.data();
        //filter out default text
        var filteredSales = _.filter(sales, function (s) {
            return (s.text !== "" && s.value !== "");
        });

        var selectedRows = prospectIdTemp.val().split(',');
        var currentDealer = $("#Dealers").data("kendoDropDownList");

        if (filteredSales.length > 0) {
            var grid = $('#grid').data('kendoGrid');
            for (var i = 0; i < selectedRows.length; i++) {
                var dataItem = grid.dataSource.get(selectedRows[i]);
                var expireDate = new Date();
                expireDate.setDate(expireDate.getDate() + 7);

                //if no rows is selected dataItem will be undefined
                if (typeof dataItem !== "undefined") {
                    dataItem.set("CurrentDealer", currentDealer.text());
                    dataItem.set("CurrentSales", filteredSales[Math.floor(Math.random() * filteredSales.length)].text);
                    dataItem.set("CurrentDealerCode", currentDealer.value());
                    dataItem.set("ExpireDate", expireDate.getDate() + "-" + (expireDate.getMonth() + 1) + "-" + expireDate.getFullYear());
                }
            }
        }
    });

    $("#input-btn-save").click(function () {
        var selectedRows = prospectIdTemp.val().split(',');
        var grid = $('#grid').data('kendoGrid');
        var updatedProspects = new Array();
        for (var i = 0; i < selectedRows.length; i++) {
            var dataItem = grid.dataSource.get(selectedRows[i]);
            updatedProspects.push(dataItem);
        }
        
        var jsonArray = JSON.parse(JSON.stringify(updatedProspects));
        $.ajax({
            url: '/DistributeProspect/Distribute',
            method: 'POST',
            dataType : 'json',
            data: { distributeProspectView: jsonArray, dealer: $("#Dealers").data("kendoDropDownList").value() },
            success: function (result)
            {
                console.log(result);
            },
            error: function (err) {
                console.log(err);
            }
        }).done(function (data) {
            location.reload();
        });
    });

    $("#act").kendoDropDownList();
    var size = $("#act").data("kendoDropDownList");
});