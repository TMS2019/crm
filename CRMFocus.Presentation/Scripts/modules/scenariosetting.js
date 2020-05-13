$(document).ready(function () {
    var crudServiceBaseUrlApiH1 = window.location.origin,
        dataSourceScenarioSettingh1 = new kendo.data.DataSource({
            transport: {
                read: {
                    url: crudServiceBaseUrlApiH1 + "/api/scenariosetting/GetAllScenarioSettingH1",
                    dataType: "json"
                },
                update: {
                    url: crudServiceBaseUrlApiH1 + "/api/scenariosetting/update",
                    type: "put",
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
            pageSize: 10,
            schema: {
                model: {
                    id: "ScenarioSettingViewId",
                    fields: {
                        ScenarioSettingViewId: { editable: false, nullable: true },
                        ScenarioName: { editable: false, validation: { required: true } },
                        isAutomatic: { validation: { required: true } },
                        SMSContent: { validation: { required: true } },
                        SmsMax: { validation: { required: true } },
                        DataSortByDirection: { validation: { required: true } },
                        StartDistributionSmsDate: { validation: { required: true } },
                        EndDistributionSmsDate: { validation: { required: true } },
                        isActive: { type:"boolean", validation: { required: false } }
                    }
                }
            }
        });

    $("#gridScenarioSettingh1").kendoGrid({
        dataSource: dataSourceScenarioSettingh1,
        pageable: true,
        groupable: false,
        sortable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        columns: [
            { field: "ScenarioName", title: "Nama Scenario" },
            {
                field: "isAutomatic", title: "Otomatis",
                template: '<input disabled="disabled" type="checkbox" #= isAutomatic ? \'checked="checked"\' : "" # "/>',
                attributes: { "style": "text-align: center;" },
                editor: customBoolEditorisActive1
            },
            { field: "SMSContent", title: "Konten SMS" },
            { field: "SmsMax", title: "SMS Maks" },
            { field: "DataSortByDirection", title: "Data Sort By" },
            {
                field: "StartDistributionSmsDate", title: "Distribute",
                template: "#= kendo.toString(kendo.parseDate(StartDistributionSmsDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                format: "{0:dd MMM yyyy HH:mm}", editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);

                    input.appendTo(container);

                    input.kendoDatePicker({});
                }
            },
            {
                field: "EndDistributionSmsDate", title: "Range",
                template: "#= kendo.toString(kendo.parseDate(EndDistributionSmsDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                format: "{0:dd MMM yyyy HH:mm}", editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);

                    input.appendTo(container);

                    input.kendoDatePicker({});
                }
            },
            {
                field: "isActive", title: "Active",
                template: '<input disabled="disabled" type="checkbox" #= isActive ? \'checked="checked"\' : "" # "/>',
                editor: customBoolEditorisActive2,
                attributes: { "style": "text-align: center;" },
                width: "75px"
            },
            {
                template: "<span class='k-icon k-i-info k-i-information'></span>",
                attributes: { "style": "text-align: center;" },
                width: "50px"
            },
            { command: ["edit"], title: "Actions" }
        ],
        editable: "inline"
    }).data("kendoGrid");
    function customBoolEditorisActive1(container, options) {
        $('<input class="k-checkbox" type="checkbox" name="isAutomatic" data-type="boolean" data-bind="checked:isAutomatic">').appendTo(container);
        $('<label class="k-checkbox-label">&#8203;</label>').appendTo(container);
    }
    function customBoolEditorisActive2(container, options) {
        $('<input class="k-checkbox" type="checkbox" name="isActive" data-type="boolean" data-bind="checked:isActive">').appendTo(container);
        $('<label class="k-checkbox-label">&#8203;</label>').appendTo(container);
    }


    var crudServiceBaseUrlApiH2 = window.location.origin,
    dataSourceScenarioSettingh2 = new kendo.data.DataSource({
        transport: {
            read: {
                url: crudServiceBaseUrlApiH2 + "/api/scenariosetting/GetAllScenarioSettingH2",
                dataType: "json"
            },
            update: {
                url: crudServiceBaseUrlApiH2 + "/api/scenariosetting/update",
                type: "put",
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
        pageSize: 10,
        schema: {
            model: {
                id: "ScenarioSettingViewId",
                fields: {
                    ScenarioSettingViewId: { editable: false, nullable: true },
                    ScenarioName: { editable: false, validation: { required: true } },
                    isAutomatic: { validation: { required: true } },
                    SMSContent: { validation: { required: true } },
                    SmsMax: { validation: { required: true } },
                    DataSortByDirection: { validation: { required: true } },
                    StartDistributionSmsDate: { validation: { required: true } },
                    EndDistributionSmsDate: { validation: { required: true } },
                    Set: { validation: { required: false } }

                }
            }
        }
    });

    $("#gridScenarioSettingh2").kendoGrid({
        dataSource: dataSourceScenarioSettingh2,
        pageable: true,
        groupable: false,
        sortable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        columns: [
            { field: "ScenarioName", title: "Nama Scenario" },
            { field: "isAutomatic", title: "Otomatis" },
            { field: "SMSContent", title: "Konten SMS" },
            { field: "SmsMax", title: "SMS Maks" },
            { field: "DataSortByDirection", title: "Data Sort By" },
            {
                field: "StartDistributionSmsDate", title: "Distribute",
                template: "#= kendo.toString(kendo.parseDate(StartDistributionSmsDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                format: "{0:dd MMM yyyy HH:mm}", editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);

                    input.appendTo(container);

                    input.kendoDatePicker({});
                }
            },
            {
                field: "EndDistributionSmsDate", title: "Range",
                template: "#= kendo.toString(kendo.parseDate(EndDistributionSmsDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                format: "{0:dd MMM yyyy HH:mm}", editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);

                    input.appendTo(container);

                    input.kendoDatePicker({});
                }
            },
            {
                field: "isActive", title: "Active",
                template: '<input disabled="disabled" type="checkbox" #= isActive ? \'checked="checked"\' : "" # "/>',
                attributes: { "style": "text-align: center;" },
                width: "75px"
            },
            {
                template: "<span class='k-icon k-i-info k-i-information'></span>",
                attributes: { "style": "text-align: center;" },
                width: "50px"
            },
            { command: ["edit"], title: "Actions" }
        ],
        editable: "inline"
    }).data("kendoGrid");





    var crudServiceBaseUrlApiH3 = window.location.origin,
    dataSourceScenarioSettingh3 = new kendo.data.DataSource({
        transport: {
            read: {
                url: crudServiceBaseUrlApiH3 + "/api/scenariosetting/GetAllScenarioSettingH3",
                dataType: "json"
            },
            update: {
                url: crudServiceBaseUrlApiH3 + "/api/scenariosetting/update",
                type: "put",
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
        pageSize: 10,
        schema: {
            model: {
                id: "ScenarioSettingViewId",
                fields: {
                    ScenarioSettingViewId: { editable: false, nullable: true },
                    ScenarioName: { editable: false, validation: { required: true } },
                    isAutomatic: { validation: { required: true } },
                    SMSContent: { validation: { required: true } },
                    SmsMax: { validation: { required: true } },
                    DataSortByDirection: { validation: { required: true } },
                    StartDistributionSmsDate: { validation: { required: true } },
                    EndDistributionSmsDate: { validation: { required: true } },
                    Set: { validation: { required: false } }

                }
            }
        }
    });

    $("#gridScenarioSettingh3").kendoGrid({
        dataSource: dataSourceScenarioSettingh3,
        pageable: true,
        groupable: false,
        sortable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        columns: [
            { field: "ScenarioName", title: "Nama Scenario" },
            { field: "isAutomatic", title: "Otomatis" },
            { field: "SMSContent", title: "Konten SMS" },
            { field: "SmsMax", title: "SMS Maks" },
            { field: "DataSortByDirection", title: "Data Sort By" },
            {
                field: "StartDistributionSmsDate", title: "Distribute",
                template: "#= kendo.toString(kendo.parseDate(StartDistributionSmsDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                format: "{0:dd MMM yyyy HH:mm}", editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);

                    input.appendTo(container);

                    input.kendoDatePicker({});
                }
            },
            {
                field: "EndDistributionSmsDate", title: "Range",
                template: "#= kendo.toString(kendo.parseDate(EndDistributionSmsDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                format: "{0:dd MMM yyyy HH:mm}", editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);

                    input.appendTo(container);

                    input.kendoDatePicker({});
                }
            },
            {
                field: "isActive", title: "Active",
                template: '<input disabled="disabled" type="checkbox" #= isActive ? \'checked="checked"\' : "" # "/>',
                attributes: { "style": "text-align: center;" },
                width: "75px"
            },
            {
                template: "<span class='k-icon k-i-info k-i-information'></span>",
                attributes: { "style": "text-align: center;" },
                width: "50px"
            },
            { command: ["edit"], title: "Actions" }
        ],
        editable: "inline"
    }).data("kendoGrid");



    var crudCustomScenario = window.location.origin,
    dataSourceCustomScenario = new kendo.data.DataSource({
        transport: {
            read: {
                url: crudCustomScenario + "/api/scenariosetting/GetAllCustomScenario",
                dataType: "json"
            },
            parameterMap: function (model, operation) {
                if (operation !== "read" && model) {
                    return kendo.stringify(model);
                }
            }
        },
        batch: false,
        pageSize: 10,
        schema: {
            model: {
                id: "ScenarioSettingViewId",
                fields: {
                    ScenarioSettingViewId: { editable: false, nullable: true },
                    ScenarioName: { editable: false, validation: { required: true } },
                    isAutomatic: { editable: false, validation: { required: true } },
                    SMSContent: { editable: false, validation: { required: true } },
                    SmsMax: { editable: false, validation: { required: true } },
                    DataSortByDirection: { editable: false, validation: { required: true } },
                    StartDistributionSmsDate: { editable: false, validation: { required: true } },
                    EndDistributionSmsDate: { editable: false, validation: { required: true } },
                    Set: { editable: false, validation: { required: false } }

                }
            }
        }
    });

    $("#gridCustomScenario").kendoGrid({
        dataSource: dataSourceCustomScenario,
        pageable: true,
        height: 550,
        groupable: false,
        sortable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        columns: [
            { field: "ScenarioName", title: "Nama Scenario" },
            { field: "SmsMax", title: "SMS Maks" },
            { field: "DataSortByDirection", title: "Data Sort By" },
            {
                field: "StartDistributionSmsDate", title: "Distribute",
                template: "#= kendo.toString(kendo.parseDate(StartDistributionSmsDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                format: "{0:dd MMM yyyy HH:mm}", editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);

                    input.appendTo(container);

                    input.kendoDatePicker({});
                }
            },
            {
                field: "EndDistributionSmsDate", title: "Range",
                template: "#= kendo.toString(kendo.parseDate(EndDistributionSmsDate, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                format: "{0:dd MMM yyyy HH:mm}", editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);

                    input.appendTo(container);

                    input.kendoDatePicker({});
                }
            },
            {
                field: "isActive", title: "Active",
                template: '<input disabled="disabled" type="checkbox" #= isActive ? \'checked="checked"\' : "" # "/>',
                attributes: { "style": "text-align: center;" },
                width: "75px"
            }
        ]
    }).data("kendoGrid");


});
