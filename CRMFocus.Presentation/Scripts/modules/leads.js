function onChange(e) {
    customerCodes = this.selectedKeyNames();
}
$(document).ready(function () {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: "/Leads/Read"
            },
            pageSize: 10,
            schema: {
                model: {
                    id: "Id"
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
            //{ selectable: true, width: "50px" },
            //{ field: "Id", title: "Id", hidden: true },
            { field: "Name", title: "Name", width: "200px" },
            { field: "CellNo", title: "Telepon", width: "150px" },
            { field: "CustomerCode", title: "Customer Code", width: "170px" },
            { field: "Email", title: "Email", width: "150px" },
            {
                field: "BirthDate", title: "Birth Date", width: "130px",
                template: "#= (BirthDate == null) ? ' ' : kendo.toString(kendo.parseDate(BirthDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            { field: "Religion", title: "Religion", width: "100px" },
            { field: "Gender", title: "Gender", width: "130px" },
            { field: "Education", title: "Education", width: "150px" },
            { field: "Profesion", title: "Profesion", width: "180px" },
            { field: "Address", title: "Address", width: "250px" },
            { field: "Spending", title: "Spending", width: "150px" },
            { field: "SourceData", title: "Sumber Data", width: "150px" },
            { field: "EngineCode", title: "Engine code", width: "130px" },
            { field: "EngineNo", title: "Engine No", width: "130px" },
            { field: "UnitTypeSegment", title: "Unit Type Segment", width: "180px" },
            { field: "UnitTypeSeries", title: "Unit Type Series", width: "180px" },
            { field: "UnitMarketName", title: "Unit Market Name", width: "200px" },
            {
                field: "TglBeli", title: "Tgl Beli", width: "130px",
                template: "#= (TglBeli == null) ? ' ' : kendo.toString(kendo.parseDate(TglBeli, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            { field: "PaymentType", title: "Payment Type", width: "130px" },
            //{ field: "ServiceType", title: "Service Type", width: "130px" },
            //{
            //    field: "ServiceDate", title: "Service Date", width: "130px",
            //    template: "#= kendo.toString(kendo.parseDate(ServiceDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            //},
            { field: "Provinsi", title: "Provinsi", width: "150px" },
            { field: "Kabupaten", title: "Kabupaten", width: "150px" },
            { field: "Kecamatan", title: "Kecamatan", width: "150px" },
            { field: "Kelurahan", title: "Kelurahan", width: "150px" }
        ]
    });

    var grid = $("#grid").data("kendoGrid");
});