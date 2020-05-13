$(document).ready(function () {
    var crudServiceBaseUrlApi = window.location.origin,
        dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: crudServiceBaseUrlApi + "/api/dummy",
                    dataType: "json"
                },
                create: {
                    url: crudServiceBaseUrlApi + "/api/dummy/create",
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                },
                update: {
                    url: crudServiceBaseUrlApi + "/api/dummy/update",
                    type: "put",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"

                },
                destroy: {
                    url: crudServiceBaseUrlApi + "/api/dummy/delete",
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
                        DummyName: { validation: { required: true } }
                    }
                }
            }
        });

    $("#gridAPI").kendoGrid({
        dataSource: dataSource,
        pageable: true,
        height: 550,
        groupable: false,
        sortable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        toolbar: ["create"],
        columns: [
            { field: "DummyName", title: "Dummy" },
            { command: ["edit", "destroy"], title: "Actions" }
        ],
        editable: "inline"
    }).data("kendoGrid");
});