$(document).ready(function () {
    var crudServiceBaseUrlApi = window.location.origin,
        dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: crudServiceBaseUrlApi + "/api/unitpricesetting",
                    dataType: "json"
                },
                create: {
                    url: crudServiceBaseUrlApi + "/api/unitpricesetting/create",
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                },
                update: {
                    url: crudServiceBaseUrlApi + "/api/unitpricesetting/update",
                    type: "put",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"

                },
                destroy: {
                    url: crudServiceBaseUrlApi + "/api/unitpricesetting/delete",
                    type: "delete",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                },

                parameterMap: function (model, operation) {
                    if (operation !== "read" && model) {
                        return kendo.stringify(model);
                    }
                }
            },
            batch: false,
            pageSize: 20,
            schema: {
                model: {
                    id: "Id",
                    fields: {
                        Id: { editable: false, nullable: true },
                        Merk: { editable: false, validation: { required: true } },
                        Varian: { editable: false, validation: { required: true } },
                        StartPrice: { validation: { required: true } },
                        EndPrice: { validation: { required: true } },
                    }
                }
            }
        });

    $("#gridUnit").kendoGrid({
        dataSource: dataSource,
        pageable: true,
        resizable:true,
        groupable: false,
        sortable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        //toolbar: ["create"],
        columns: [
            { field: "Merk", title: "Merk" },
            { field: "Varian", title: "Varian"},
            { field: "StartPrice", title: "Start Price" },
            { field: "EndPrice", title: "End Price" },
            {
                template: "<span class='k-icon k-i-info k-i-information'></span>",
                attributes: { "style": "text-align: center;" },
                width:"50px"
            },
            { command: ["edit", "destroy"], title: "Actions" }
        ],
        editable: "inline"
    }).data("kendoGrid");
});