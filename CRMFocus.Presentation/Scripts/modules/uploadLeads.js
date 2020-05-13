'use strict'
var dataTemporary = [];
var customerCodes = "";

function showSuccessMessage(text) {
    swal({
        icon: "success",
        text: text,
        timer: 120000,
        showConfirmButton: true
    });
}

function showFailMessage(text) {
    swal({
        icon: "error",
        text: text,
        timer: 120000,
        showConfirmButton: true
    });
}

$(document).ready(function () {
    $('#file').change(function () {
        $('#subfile').val($(this).val());
    });
    var oldPageSize = 0;
    //createGrid1();

    $("#files").kendoUpload({
        multiple: false,
    });

    $('#previewBtn').click(function () {
          // Checking whether FormData is available in browser  
       if (window.FormData !== undefined) {
            customerCodes = "";
            var fileUpload = $("#file").get(0);
            var files = fileUpload.files;

            // Create FormData object  
            var fileData = new FormData();
            fileData.append(files[0].name, files[0]);

            $('#messageList li').remove();

            $.ajax({
                url: '/UploadLeads/PreviewExcell',
                type: "POST",
                data: fileData,
                processData: false,
                contentType: false,
                success: function (result) {
                    //$('#grid1').remove();
                    dataTemporary = result;
                    createGrid2()
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
            url: '/UploadLeads/Save',
            type: "POST",
            dataType: 'json',
            data: {customerCodes : customerCodes},
            success: function (result) {
                showSuccessMessage(result.Message);
                //$('#messageList').append('<li style="color:red;">' + result.Message + '</li>');               
            },
            error: function (err) {
                showFailMessage(err.statusText);
                //$('#messageList').append('<li style="color:red;">Error : *' + err.statusText + '</li>');
            }
        });
    });

    $('#resetBtn').click(function () {
        $('#messageList li').remove();
        customerCodes = "";
        $('#file').val("");
        $("#grid2").data("kendoGrid").dataSource.data([]);
    });
});

function onChange(e) {
    customerCodes = this.selectedKeyNames();
}

function onClick(e) {
    var grid = $("#grid2").data("kendoGrid");

    oldPageSize = grid.dataSource.pageSize();
    grid.dataSource.pageSize(grid.dataSource.data().length);

    if (grid.dataSource.data().length === grid.select().length) {
        grid.clearSelection();
    } else {
        grid.select("tr");
    };

    grid.dataSource.pageSize(oldPageSize);
}


function createGrid1() {
    $("#grid1").kendoGrid({
        dataSource: {
            data: [],
            pageSize: 10,
            schema: {
                model: {
                    id: "UploadLeadsViewId"
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
            refresh: false,
            pageSizes: true,
            buttonCount: 5
        },
        columns: [
            { selectable: true, width: "50px" },
            { field: "UploadLeadsViewId", title: "UploadLeadsViewId", hidden: true },
            { field: "Name", title: "Name", width: "200px" },
            { field: "CellNo", title: "Cell No", width: "150px" },
            { field: "CustomerCode", title: "Customer Code", width: "130px" },
            {
                field: "BirthDate", title: "Birth Date", width: "130px",
                template: "#= kendo.toString(kendo.parseDate(BirthDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            { field: "Address", title: "Address", width: "250px" },
            { field: "Gender", title: "Gender", width: "130px" },
            { field: "Religion", title: "Religion", width: "100px" },
            { field: "Profesion", title: "Profesion", width: "180px" },
            { field: "Spending", title: "Spending", width: "150px" },
            { field: "Education", title: "Education", width: "150px" },
            { field: "isCallable", title: "is Callable", width: "100px" },
            { field: "Email", title: "Email", width: "250px" },
            { field: "SourceData", title: "Sumber Data", width: "150px" },
            { field: "UnitMarketName", title: "Unit Market Name", width: "200px" },
            { field: "EngineCode", title: "Engine code", width: "130px" },
            { field: "EngineNo", title: "Engine No", width: "130px" },
            {
                field: "TglBeli", title: "Tgl Beli Dlr", width: "130px",
                template: "#= kendo.toString(kendo.parseDate(TglBeli, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            { field: "PaymentType", title: "Payment Type", width: "130px" },
            { field: "ServiceType", title: "Service Type", width: "130px" },
            {
                field: "ServiceDate", title: "Service Date", width: "130px",
                template: "#= kendo.toString(kendo.parseDate(ServiceDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            { field: "Kelurahan", title: "Kelurahan", width: "150px" },
            { field: "Kecamatan", title: "Kecamatan", width: "150px" },
            { field: "Kabupaten", title: "Kabupaten", width: "150px" },
            { field: "Provinsi", title: "Provinsi", width: "150px" },
            { field: "UnitTypeSegment", title: "Unit Type Segment", width: "180px" },
            { field: "UnitTypeSeries", title: "Unit Type Series", width: "180px" },
            { field: "DealerCode", title: "Dealer Code", width: "180px" },
            { field: "DealerName", title: "Dealer Name", width: "180px" },
            { field: "RegionCode", title: "Region Code", width: "180px" },
            { field: "RegionName", title: "Region Name", width: "180px" }

        ]
    });

    var grid = $("#grid1").data("kendoGrid");
    grid.thead.on("click", ".k-checkbox", onClick);

    if (userRole.toLocaleLowerCase() === 'dealer') {
        grid.hideColumn("DealerCode");
        grid.hideColumn("DealerName");
        grid.hideColumn("RegionCode");
        grid.hideColumn("RegionName");
    }

    if (userRole.toLocaleLowerCase() === 'main dealer') {
        grid.hideColumn("RegionCode");
        grid.hideColumn("RegionName");
    }
}

function createGrid2() {
    var columns = "";
    if (userRole.toLocaleLowerCase() === 'dealer') {
        var temp = [
            { selectable: true, width: "50px" },
            { field: "UploadLeadsViewId", title: "UploadLeadsViewId", hidden: true },
            { field: "Name", title: "Name", width: "200px" },
            { field: "CellNo", title: "Cell No", width: "150px" },
            { field: "CustomerCode", title: "Customer Code", width: "130px" },
            {
                field: "BirthDate", title: "Birth Date", width: "130px",
                template: "#= (BirthDate === null) ? '' : kendo.toString(kendo.parseDate(BirthDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            { field: "Address", title: "Address", width: "250px" },
            { field: "GenderDescription", title: "Gender", width: "130px" },
            { field: "ReligionDescription", title: "Religion", width: "100px" },
            { field: "ProfesionDescription", title: "Profesion", width: "180px" },
            { field: "SpendingDescription", title: "Spending", width: "150px" },
            { field: "EducationDescription", title: "Education", width: "150px" },
            { field: "isCallable", title: "is Callable", width: "100px" },
            { field: "Email", title: "Email", width: "250px" },
            { field: "SourceData", title: "Sumber Data", width: "150px" },
            { field: "UnitMarketName", title: "Unit Market Name", width: "200px" },
            { field: "EngineCode", title: "Engine code", width: "130px" },
            { field: "EngineNo", title: "Engine No", width: "130px" },
            {
                field: "TglBeli", title: "Tgl Beli Dlr", width: "130px",
                template: "#= (TglBeli === null) ? '' : kendo.toString(kendo.parseDate(TglBeli, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            { field: "PaymentTypeDescription", title: "Payment Type", width: "130px" },
            { field: "ServiceType", title: "Service Type", width: "130px" },
            {
                field: "ServiceDate", title: "Service Date", width: "130px",
                template: "#= (ServiceDate === null) ? '' : kendo.toString(kendo.parseDate(ServiceDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            { field: "Kelurahan", title: "Kelurahan", width: "150px" },
            { field: "Kecamatan", title: "Kecamatan", width: "150px" },
            { field: "Kabupaten", title: "Kabupaten", width: "150px" },
            { field: "Provinsi", title: "Provinsi", width: "150px" },
            { field: "UnitTypeSegment", title: "Unit Type Segment", width: "180px" },
            { field: "UnitTypeSeries", title: "Unit Type Series", width: "180px" }            
        ];

        columns = temp;
    } else if (userRole.toLocaleLowerCase() === 'main dealer') {
        temp = [
            { selectable: true, width: "50px" },
            { field: "UploadLeadsViewId", title: "UploadLeadsViewId", hidden: true },
            { field: "Name", title: "Name", width: "200px" },
            { field: "CellNo", title: "Cell No", width: "150px" },
            { field: "CustomerCode", title: "Customer Code", width: "130px" },
            {
                field: "BirthDate", title: "Birth Date", width: "130px",
                template: "#= (BirthDate === null) ? '' : kendo.toString(kendo.parseDate(BirthDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            { field: "Address", title: "Address", width: "250px" },
            { field: "GenderDescription", title: "Gender", width: "130px" },
            { field: "ReligionDescription", title: "Religion", width: "100px" },
            { field: "ProfesionDescription", title: "Profesion", width: "180px" },
            { field: "SpendingDescription", title: "Spending", width: "150px" },
            { field: "EducationDescription", title: "Education", width: "150px" },
            { field: "isCallable", title: "is Callable", width: "100px" },
            { field: "Email", title: "Email", width: "250px" },
            { field: "SourceData", title: "Sumber Data", width: "150px" },
            { field: "UnitMarketName", title: "Unit Market Name", width: "200px" },
            { field: "EngineCode", title: "Engine code", width: "130px" },
            { field: "EngineNo", title: "Engine No", width: "130px" },
            {
                field: "TglBeli", title: "Tgl Beli Dlr", width: "130px",
                template: "#= (TglBeli === null) ? '' : kendo.toString(kendo.parseDate(TglBeli, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            { field: "PaymentTypeDescription", title: "Payment Type", width: "130px" },
            { field: "ServiceType", title: "Service Type", width: "130px" },
             {
                 field: "ServiceDate", title: "Service Date", width: "130px",
                 template: "#= (ServiceDate === null) ? '' : kendo.toString(kendo.parseDate(ServiceDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
             },
            { field: "Kelurahan", title: "Kelurahan", width: "150px" },
            { field: "Kecamatan", title: "Kecamatan", width: "150px" },
            { field: "Kabupaten", title: "Kabupaten", width: "150px" },
            { field: "Provinsi", title: "Provinsi", width: "150px" },
            { field: "UnitTypeSegment", title: "Unit Type Segment", width: "180px" },
            { field: "UnitTypeSeries", title: "Unit Type Series", width: "180px" },
            { field: "DealerCode", title: "Dealer Code", width: "180px" },
            { field: "DealerName", title: "Dealer Name", width: "180px" }];

        columns = temp;
        } 
    else {
        temp = [
               { selectable: true, width: "50px" },
               { field: "UploadLeadsViewId", title: "UploadLeadsViewId", hidden: true },
               { field: "Name", title: "Name", width: "200px" },
               { field: "CellNo", title: "Cell No", width: "150px" },
               { field: "CustomerCode", title: "Customer Code", width: "130px" },
               {
                   field: "BirthDate", title: "Birth Date", width: "130px",
                   template: "#= (BirthDate === null) ? '' : kendo.toString(kendo.parseDate(BirthDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
               },
               { field: "Address", title: "Address", width: "250px" },
                { field: "GenderDescription", title: "Gender", width: "130px" },
                { field: "ReligionDescription", title: "Religion", width: "100px" },
                { field: "ProfesionDescription", title: "Profesion", width: "180px" },
                { field: "SpendingDescription", title: "Spending", width: "150px" },
                { field: "EducationDescription", title: "Education", width: "150px" },
               { field: "isCallable", title: "is Callable", width: "100px" },
               { field: "Email", title: "Email", width: "250px" },
               { field: "SourceData", title: "Sumber Data", width: "150px" },
               { field: "UnitMarketName", title: "Unit Market Name", width: "200px" },
               { field: "EngineCode", title: "Engine code", width: "130px" },
               { field: "EngineNo", title: "Engine No", width: "130px" },
               {
                   field: "TglBeli", title: "Tgl Beli Dlr", width: "130px",
                   template: "#= (TglBeli === null) ? '' : kendo.toString(kendo.parseDate(TglBeli, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
               },
               { field: "PaymentTypeDescription", title: "Payment Type", width: "130px" },
               { field: "ServiceType", title: "Service Type", width: "130px" },
                {
                    field: "ServiceDate", title: "Service Date", width: "130px",
                    template: "#= (ServiceDate === null) ? '' : kendo.toString(kendo.parseDate(ServiceDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
                },
               { field: "Kelurahan", title: "Kelurahan", width: "150px" },
               { field: "Kecamatan", title: "Kecamatan", width: "150px" },
               { field: "Kabupaten", title: "Kabupaten", width: "150px" },
               { field: "Provinsi", title: "Provinsi", width: "150px" },
               { field: "UnitTypeSegment", title: "Unit Type Segment", width: "180px" },
               { field: "UnitTypeSeries", title: "Unit Type Series", width: "180px" },
               { field: "DealerCode", title: "Dealer Code", width: "180px" },
               { field: "DealerName", title: "Dealer Name", width: "180px" },
            { field: "RegionCode", title: "Region Code", width: "180px" },
            { field: "RegionName", title: "Region Name", width: "180px" }];

        columns = temp;
    }


    $("#grid2").kendoGrid({
        dataSource: {
            data: dataTemporary,
            pageSize: 10,
            schema: {
                model: {
                    id: "UploadLeadsViewId"
                }
            }
        },
        persistSelection: true,
        scrollable: true,
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
            refresh: false,
            pageSizes: true,
            buttonCount: 5
        },
        columns: columns
    });

    var grid = $("#grid2").data("kendoGrid");
    grid.thead.on("click", ".k-checkbox", onClick);
}


