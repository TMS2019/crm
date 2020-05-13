'use strict'
var dataTemporary = [];
var scenarioLeadMappingViewIds = "";
var scenarioNameVal = "";
var scenarioNameText = "";
var scenarioTypeVal = "";
var scenarioTypeText = "";

function showSuccessMessage(text) {
    swal({
        icon: "success",
        title: "Success",
        text: text,
        timer: 120000,
        showConfirmButton: true
    });
}

function showFailMessage(text) {
    swal({
        icon: "error",
        //title: "Errors",
        text: text,
        timer: 120000,
        showCancelButton: false,
        showConfirmButton: false
    });
}

$(document).ready(function () {

    $("#files").kendoUpload({
        multiple: false,
    });

    $("#scenarioType").kendoDropDownList({
        change: function (e) {
            if (this.dataItem(e.item).value !== "0") {
                scenarioTypeVal = this.dataItem(e.item).value;
                scenarioTypeText = this.dataItem(e.item).text;
            }
        }
    });

    $("#scenarioName").kendoDropDownList({
        change: function (e) {
            if (this.dataItem(e.item).value !== "0") {
                scenarioNameVal = this.dataItem(e.item).value;
                scenarioNameText = this.dataItem(e.item).text;
            }
        }
    });


    $('#previewBtn').click(function () {
        // Checking whether FormData is available in browser  
        if (window.FormData !== undefined) {
            scenarioLeadMappingViewIds = "";
            var fileUpload = $("#file").get(0);
            var files = fileUpload.files;

            // Create FormData object  
            var fileData = new FormData();
            fileData.append(files[0].name, files[0]);

            $('#messageList li').remove();

            $.ajax({
                url: '/ManageSendSms/PreviewExcell',
                type: "POST",
                data: fileData,
                processData: false,
                contentType: false,
                success: function (result) {                 
                    dataTemporary = result;
                    createGrid();
                    for (var i = 0; i < result.length; i++) {
                        if (result[i].Status === "Error") {
                            //$('#messageList').append('<li style="color:red;">' + result[i].Message + '</li>');
                            showFailMessage(result[i].Message);
                        }
                    }
                },
                error: function (err) {
                    //$('#messageList').append('<li style="color:red;">Error : *' + err.statusText + '</li>');
                    showFailMessage(err.statusText);
                }
            });
        }
        else {
            alert("FormData is not supported.");
        }
    });

    $('#saveBtn').click(function () {
        $('#messageList li').remove();
        $.ajax({
            url: '/ManageSendSms/Save',
            type: "POST",
            dataType: 'json',
            data: { scenarioLeadMappingViewIds: scenarioLeadMappingViewIds, scenarioCode: scenarioNameVal, scenarioType: scenarioTypeVal },
            success: function (result) {
                if (result.Status === "Error") {
                    //$('#messageList').append('<li style="color:red;">' + result[i].Message + '</li>');
                    showFailMessage(result.Message);
                }
                else {
                    showSuccessMessage(result.Message);
                    //$('#messageList').append('<li style="color:red;">' + result.Message + '</li>');    
                }
            },
            error: function (err) {
                showFailMessage(err.statusText);
                //$('#messageList').append('<li style="color:red;">Error : *' + err.statusText + '</li>');
            }
        });
    });


    $('#resetBtn').click(function () {
        $('#messageList li').remove();
        scenarioLeadMappingViewIds = "";
        $('#file').val("");
        $("#grid").data("kendoGrid").dataSource.data([]);
    });

});

function onChange(e) {
    scenarioLeadMappingViewIds = this.selectedKeyNames();
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

function createGrid()
{
    $("#grid").kendoGrid({
        dataSource: {
            data: dataTemporary,
            pageSize: 5,
            schema: {
                model: {
                    id: "ScenarioLeadMappingViewId"
                }
            }
        },
        pageable: true,
        scrollable: false,
        persistSelection: true,
        sortable: true,
        columnMenu: {
            sortable: false,
            filterable: false
        },
        change: onChange,
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
            { field: "Nama", title: "Nama" },
            { field: "Telepon", title: "Telepon" },
            { field: "Unit", title: "Unit" },
            { field: "Email", title: "Email" },
            { field: "Varian", title: "Varian" },
            { field: "Alamat", title: "Alamat" },
            {
                field: "TanggalDiUnggah", title: "Tanggal di Unggah",
                template: "#= (TanggalDiUnggah === null) ? '' : kendo.toString(kendo.parseDate(TanggalDiUnggah, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            { field: "CabangDescription", title: "Cabang" },
            { field: "EngineCode", title: "Engine Code" },
            { field: "EngineNumber", title: "Engine Number" }
        ]
    });

    var grid = $("#grid").data("kendoGrid");
    grid.thead.on("click", ".k-checkbox", onClick);
}