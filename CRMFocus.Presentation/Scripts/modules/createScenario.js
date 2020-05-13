$(document).ready(function () {
    
    $("#act, #Profesion, #Gender, #Religion , #Spending, #DestinationType").kendoDropDownList({});

    //$("#SmsTokenList > li").click(function () {
    //    console.log($(this).attr("data-value").val("result.data-value"));
    //});
    
    //$("#SMSScript").on("blur", function () {
    //    console.log($(this).prop("selectionStart"));
    //});


    //Periode Program--------------------------------------

    var start = $("#datepicker").kendoDatePicker({
        change: startChange,
        format: "dd MMMM yyyy"
    }).data("kendoDatePicker");

    var end = $("#datepicker2").kendoDatePicker({
        change: endChange,
        format: "dd MMMM yyyy"
    }).data("kendoDatePicker");

    start.max(end.value());
    end.min(start.value());

    function startChange() {
        var startDate = start.value(),
        endDate = end.value();

        if (startDate) {
            startDate = new Date(startDate);
            startDate.setDate(startDate.getDate());
            end.min(startDate);
        } else if (endDate) {
            start.max(new Date(endDate));
        } else {
            endDate = new Date();
            start.max(endDate);
            end.min(endDate);
        }
    }

    function endChange() {
        var endDate = end.value(),
        startDate = start.value();

        if (endDate) {
            endDate = new Date(endDate);
            endDate.setDate(endDate.getDate());
            start.max(endDate);
        } else if (startDate) {
            end.min(new Date(startDate));
        } else {
            endDate = new Date();
            start.max(endDate);
            end.min(endDate);
        }
    }

    //End Periode Program--------------------------------------


    //Periode Distribusi SMS--------------------------------------

    var startDistributionSMSDate = $("#datepicker3").kendoDatePicker({
        change: startChangeDistributionSMSDate,
        format: "dd MMMM yyyy"
    }).data("kendoDatePicker");

    var endDistributionSMSDate = $("#datepicker4").kendoDatePicker({
        change: endChangeDistributionSMSDate,
        format: "dd MMMM yyyy"
    }).data("kendoDatePicker");

    startDistributionSMSDate.max(endDistributionSMSDate.value());
    endDistributionSMSDate.min(startDistributionSMSDate.value());

    function startChangeDistributionSMSDate() {
        var startDate1 = startDistributionSMSDate.value(),
        endDate1 = endDistributionSMSDate.value();

        if (startDate1) {
            startDate1 = new Date(startDate1);
            startDate1.setDate(startDate1.getDate());
            endDistributionSMSDate.min(startDate1);
        } else if (endDate1) {
            startDistributionSMSDate.max(new Date(endDate1));
        } else {
            endDate1 = new Date();
            startDistributionSMSDate.max(endDate1);
            endDistributionSMSDate.min(endDate1);
        }
    }

    function endChangeDistributionSMSDate() {
        var endDate = endDistributionSMSDate.value(),
        startDate = startDistributionSMSDate.value();

        if (endDate) {
            endDate = new Date(endDate);
            endDate.setDate(endDate.getDate());
            startDistributionSMSDate.max(endDate);
        } else if (startDate) {
            endDistributionSMSDate.min(new Date(startDate));
        } else {
            endDate = new Date();
            startDistributionSMSDate.max(endDate);
            endDistributionSMSDate.min(endDate);
        }
    }

    //End Periode Distribusi SMS----------------------------------------



    //Tanggal Lahir--------------------------------------

    var startBod = $("#datepickerStartBod").kendoDatePicker({
        change: startChangeBod,
        format: "dd MMMM"
    }).data("kendoDatePicker");

    var endBod = $("#datepickerEndBod").kendoDatePicker({
        change: endChangeBod,
        format: "dd MMMM"
    }).data("kendoDatePicker");

    startBod.max(endBod.value());
    endBod.min(startBod.value());

    function startChangeBod() {
        var startDate2 = startBod.value(),
        endDate2 = endBod.value();

        if (startDate2) {
            startDate2 = new Date(startDate2);
            startDate2.setDate(startDate2.getDate());
            endBod.min(startDate2);
        } else if (endDate2) {
            startBod.max(new Date(endDate2));
        } else {
            endDate2 = new Date();
            startBod.max(endDate2);
            endBod.min(endDate2);
        }
    }

    function endChangeBod() {
        var endDate2 = endBod.value(),
        startDate2 = startBod.value();

        if (endDate2) {
            endDate2 = new Date(endDate2);
            endDate2.setDate(endDate2.getDate());
            startBod.max(endDate2);
        } else if (startDate2) {
            endBod.min(new Date(startDate2));
        } else {
            endDate2 = new Date();
            startBod.max(endDate2);
            endBod.min(endDate2);
        }
    }

    //End Tanggal Lahir-------------------------------------


    //Year--------------------------------------

    var startYear = $("#datepickerStartYear").kendoDatePicker({
        change: startChangeYear,
        start: "decade",
        depth: "decade",
        format: "yyyy"
    }).data("kendoDatePicker");

    var endYear = $("#datepickerEndYear").kendoDatePicker({
        change: endChangeYear,
        start: "decade",
        depth: "decade",
        format: "yyyy"
    }).data("kendoDatePicker");

    startYear.max(endYear.value());
    endYear.min(startYear.value());

    function startChangeYear() {
        var startDate3 = startYear.value(),
        endDate3 = endYear.value();

        if (startDate3) {
            startDate3 = new Date(startDate3);
            startDate3.setDate(startDate3.getDate());
            endYear.min(startDate3);
        } else if (endDate3) {
            startYear.max(new Date(endDate3));
        } else {
            endDate3 = new Date();
            startYear.max(endDate3);
            endYear.min(endDate3);
        }
    }

    function endChangeYear() {
        var endDate3 = endYear.value(),
        startDate3 = startYear.value();

        if (endDate3) {
            endDate3 = new Date(endDate3);
            endDate3.setDate(endDate3.getDate());
            startYear.max(endDate3);
        } else if (startDate3) {
            endYear.min(new Date(startDate3));
        } else {
            endDate3 = new Date();
            startYear.max(endDate3);
            endYear.min(endDate3);
        }
    }

    //End Year--------------------------------------



    //Customer Dari "NONE"--------------------------------------

    var startNone = $("#datepickerStartDate").kendoDatePicker({
        change: startChangeNone,
        format: "dd MMMM yyyy"
    }).data("kendoDatePicker");

    var endNone = $("#datepickerEndDate").kendoDatePicker({
        change: endChangeNone,
        format: "dd MMMM yyyy"
    }).data("kendoDatePicker");

    startNone.max(endNone.value());
    endNone.min(startNone.value());

    function startChangeNone() {
        var startDate4 = startNone.value(),
        endDate4 = endNone.value();

        if (startDate4) {
            startDate4 = new Date(startDate4);
            startDate4.setDate(startDate4.getDate());
            endNone.min(startDate4);
        } else if (endDate4) {
            startNone.max(new Date(endDate4));
        } else {
            endDate4 = new Date();
            startNone.max(endDate4);
            endNone.min(endDate4);
        }
    }

    function endChangeNone() {
        var endDate4 = endNone.value(),
        startDate4 = startNone.value();

        if (endDate4) {
            endDate4 = new Date(endDate4);
            endDate4.setDate(endDate4.getDate());
            startNone.max(endDate4);
        } else if (startDate4) {
            endNone.min(new Date(startDate4));
        } else {
            endDate4 = new Date();
            startNone.max(endDate4);
            endNone.min(endDate4);
        }
    }

    //End Customer Dari "NONE"--------------------------------------


    $("#CallTokenList > li").click(function () {
        var txt = $.trim($(this).text());
        var box = $("#s-textarea");
        box.val(box.val() + txt);
    });


    $("#SmsTokenList > li").click(function () {
        var txt = $.trim($(this).text());
        var box = $("#SMSScript");
        box.val(box.val() + txt);
    });

    $('#Short').on('change', function () {
        var selected = $(this).find("option:selected").val();
        if (selected == 3 || selected == 4 || selected == 5) {
            $("#showAnswer").show();
        } else {
            $("#showAnswer").hide();
        }
    });

    function onSelect(e) {
            if (e.item) {
                var dataItem = this.dataItem(e.item);
                if (dataItem.value == 0) {
                    $("#showingDate").show();
                } else {
                    $("#showingDate").hide();
                }
            } 
    };

    $("#ResourceType").kendoDropDownList({
        select: onSelect,
    });

    $("#Provinces").kendoDropDownList({
        change: function (e) {
            $('#Kabupatens').data("kendoDropDownList").dataSource.data([]);
            $('#Kecamatans').data("kendoDropDownList").dataSource.data([]);
            $('#Kelurahans').data("kendoDropDownList").dataSource.data([]);
            $.ajax({
                url: '/Scenario/ReadKabupaten',
                type: "POST",
                dataType: 'json',
                data: { provinceCode: this.dataItem(e.item).value },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        $('#Kabupatens').data("kendoDropDownList").dataSource.add({ "text": result[i].Text, "value": result[i].Value });
                    }                   
                },
                error: function (err) {
                    alert(err)
                }
            });
        }
    });

    $("#Kabupatens").kendoDropDownList({
        change: function (e) {
            $('#Kecamatans').data("kendoDropDownList").dataSource.data([]);
            $('#Kelurahans').data("kendoDropDownList").dataSource.data([]);
            $.ajax({
                url: '/Scenario/ReadKecamatan',
                type: "POST",
                dataType: 'json',
                data: { kabupatenCode: this.dataItem(e.item).value },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        $('#Kecamatans').data("kendoDropDownList").dataSource.add({ "text": result[i].Text, "value": result[i].Value });
                    }
                },
                error: function (err) {
                    alert(err)
                }
            });
        }
    });

    $("#Kecamatans").kendoDropDownList({
        change: function (e) {
            $('#Kelurahans').data("kendoDropDownList").dataSource.data([]);
            $.ajax({
                url: '/Scenario/ReadKelurahan',
                type: "POST",
                dataType: 'json',
                data: { kecamatanCode: this.dataItem(e.item).value },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        $('#Kelurahans').data("kendoDropDownList").dataSource.add({ "text": result[i].Text, "value": result[i].Value });
                    }
                },
                error: function (err) {
                    alert(err)
                }
            });
        }
    });


    var panelBar = $("#panelbar").kendoPanelBar().data("kendoPanelBar");
    var panelBar = $("#panelbar2").kendoPanelBar().data("kendoPanelBar");

    $("#datepickerStartDateFilterLain").kendoDatePicker({
        format: "dd MMMM yyyy"
    });

    $("#Multiple, #Kelurahans, #JumlahRO, #UnitTypeSegment, #UnitTypeSeries, #UnitMarketName, #Section, #Operator, #DayMonth, #Logic").kendoDropDownList();

    $("#jawaban, #jawaban2").kendoMaskedTextBox();


    $(document).ready(function () {
        $("#tambahcustomer").click(function () {
            $(".to-hide").css("display", "none");
            $(".to-show").css("display", "block");
        });
    });


    $("input[name='target']").click(function () {
        $('#showme').css('display', ($(this).val() === 'a') ? 'block' : 'none');
        $('#showme2').css('display', ($(this).val() === 'b') ? 'block' : 'none');
        $('#showme3').css('display', ($(this).val() === 'c') ? 'block' : 'none');
        $('#showme4').css('display', ($(this).val() === 'd') ? 'block' : 'none');
    });

    $(".removeBtn, .hrMediaView").hide();

    $('#tambahFilter').click(function () {
        var children = $("#filter > li:first").clone().find("input").val("").end().appendTo('#filter').find('.removeBtn').show();
        $('.removeBtn').click(function () {
            $(this).closest('li').remove();
        });
        $("input[data-role='datepicker']").kendoDatePicker();
        $("select").kendoDropDownList();
    });

    $('#tambahAnswer').click(function () {
        var children = $("#answer > li:first").clone().find("input:text").val("").end().appendTo('#answer').find('.removeBtn').show();
        $('.removeBtn').click(function () {
            $(this).closest('li').remove();
        });
    });

    $('#tambahPertanyaan').click(function () {
        var children = $("#QuestionType > li:first").clone().find("textarea").val("").end().appendTo('#QuestionType').find('.removeBtn, .hrMediaView').show();
        $('.removeBtn').click(function () {
            $(this).closest('li').remove();
        });
        $('.hrMediaView').click(function () {
            $(this).show();
        });
        $('#Short').on('change', function () {
            var selected = $(this).find("option:selected").val();
            if (selected == 3 || selected == 4 || selected == 5) {
                $("#showAnswer").show();
            } else {
                $("#showAnswer").hide();
            }
        });

    });
    
    $('form').submit(function (e) {
        e.preventDefault();
        var data = $(this).serializeJSON();
        console.log(data);
        $.ajax({
            url: '/Scenario/Create',
            type: "POST",
            dataType: 'json',
            data: { model: data },
            success: function (result) {
                console.log(result);
                if (result.status == "succeed") {
                    showsuccessmessage(result.message);
                    window.location.href = '/Scenario';
                } else {
                    showfailmessage(result.message);
                }
            },
            error: function (err) {
                showFailMessage(err.statusText);
            }
        });
        //return false;
    });

    $('#checkLeadBtn').click(function(){
        $.ajax({
            url: '/Scenario/GetLeadDWAI',
            type: "POST",
            dataType: 'json',
            success: function (result) {
                $('#totalLead').val(result);
            },
            error: function (err) {
                showFailMessage(err.statusText);
            }
        });
    });
});