$(document).ready(function () {
    function showSuccessMessage(text) {
        swal({
            icon: "success",
            text: text,
            timer: 120000,
            showConfirmButton: true
        }).then(function () {
            window.location.href = '/leads';
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
    $("#nextstep, #panelbar > li:first > .removeBtn").hide();
    //$("#GetTitle, #Religion, #Gender, #Education, #CustomerCode, #Profesion, #Spending, #UnitTypeSegment, #UnitTypeSeries, #UnitMarketName, #PaymentType, #Provinsi, #Kabupaten, #Kecamatan, #Kelurahan, #ServiceType").niceSelect();

    $("select").kendoDropDownList();
    $("#Birthdays, #Services, #TanggalBeli").kendoDatePicker({});

    $("#Provinsi").kendoDropDownList({
        change: function (e) {
            $('#Kabupaten').data("kendoDropDownList").dataSource.data([]);
            $('#Kecamatan').data("kendoDropDownList").dataSource.data([]);
            $('#Kelurahan').data("kendoDropDownList").dataSource.data([]);
            $.ajax({
                url: '/Scenario/ReadKabupaten',
                type: "POST",
                dataType: 'json',
                data: { provinceCode: this.dataItem(e.item).value },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        $('#Kabupaten').data("kendoDropDownList").dataSource.add({ "text": result[i].Text, "value": result[i].Value });
                    }
                },
                error: function (err) {
                    alert(err)
                }
            });
        }
    });

    $("#Kabupaten").kendoDropDownList({
        change: function (e) {
            $('#Kecamatan').data("kendoDropDownList").dataSource.data([]);
            $('#Kelurahan').data("kendoDropDownList").dataSource.data([]);
            $.ajax({
                url: '/Scenario/ReadKecamatan',
                type: "POST",
                dataType: 'json',
                data: { kabupatenCode: this.dataItem(e.item).value },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        $('#Kecamatan').data("kendoDropDownList").dataSource.add({ "text": result[i].Text, "value": result[i].Value });
                    }
                },
                error: function (err) {
                    alert(err)
                }
            });
        }
    });

    $("#Kecamatan").kendoDropDownList({
        change: function (e) {
            $('#Kelurahan').data("kendoDropDownList").dataSource.data([]);
            $.ajax({
                url: '/Scenario/ReadKelurahan',
                type: "POST",
                dataType: 'json',
                data: { kecamatanCode: this.dataItem(e.item).value },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        $('#Kelurahan').data("kendoDropDownList").dataSource.add({ "text": result[i].Text, "value": result[i].Value });
                    }
                },
                error: function (err) {
                    alert(err)
                }
            });
        }
    });

    $("#Kelurahan").kendoDropDownList({});

    var panelBar = $("#panelbar").kendoPanelBar().data("kendoPanelBar");

    $('#tambahUnit').click(function() {
        var children = $("#panelbar > li:first").clone().find("input:text").val("").end().appendTo('#panelbar').find('.removeBtn').show();
        $('.removeBtn').click(function () {
            $(this).closest('li').remove();
        });
        //$("select").kendoDropDownList();
        $("select").kendoDropDownList();
        $("#Birthdays, #Services, #TanggalBeli").kendoDatePicker();
    });
    $('input#isunit').on('click', function () {
        if ($(this).is(':checked')) {
            $("#save").hide();
            $("#nextstep").show();
        }
        else {
            $("#save").show();
            $("#nextstep").hide();
        }
    });

    (function ($) {
        $.fn.serializeField = function () {
            var result = {
                LeadsUnitTransaction: []
            };
            this.each(function () {

                $(this).find("fieldset").each(function () {
                    var $this = $(this);
                    var name = $this.attr("name");
                    if (name) {
                        var fields = {};
                        $.each($this.serializeArray(), function () {
                            fields[this.name] = this.value;
                        });
                        result.LeadsUnitTransaction.push(fields);
                    }
                    else {
                        $.each($this.serializeArray(), function () {
                            result[this.name] = this.value;
                        });
                    }
                });

            });

            return result;
        };
    })(jQuery);
    $("#agree").on("change", function () {
        this.value = this.checked ? 1 : 0;
    }).change();

    $('form').submit(function (e) {
        var Name = $("#input-name").val();
        if (Name.length <= 0) {
            swal({
                icon: "error",
                text: "Nama harus di isi",
                timer: 120000,
                showconfirmbutton: true,
            });
            return false;
        }
        var CellNo = $("#input-phone").val();
        if (CellNo.length <= 0) {
            swal({
                icon: "error",
                text: "Telepon harus di isi",
                timer: 120000,
                showconfirmbutton: true,
            });
            return false;
        }
        if ($('input#isunit').is(':checked')) {
            var EngineCode = $("#input-kode").val();
            if (EngineCode.length <= 0) {
                swal({
                    icon: "error",
                    text: "Kode mesin harus di isi",
                    timer: 120000,
                    showconfirmbutton: true,
                });
                return false;
            }
            var EngineNo = $("#input-nomor").val();
            if (EngineNo.length <= 0) {
                swal({
                    icon: "error",
                    text: "Nomor mesin harus di isi",
                    timer: 120000,
                    showconfirmbutton: true,
                });
                return false;
            }
            var SourceData = $("#sumberdata").val();
            if (SourceData.length <= 0) {
                swal({
                    icon: "error",
                    text: "Sumber data harus di isi",
                    timer: 120000,
                    showconfirmbutton: true,
                });
                return false;
            }
        } else {
            $("#step2 input[type='text'], #step2 select").prop("disabled", true);
        }
        e.preventDefault();
        var data = $(this).serializeField();
        $.ajax({
            url: '/Leads/Create',
            type: "POST",
            dataType: 'json',
            data: {model:data },
            success: function (result) {
                if (result.Status == "Succeed") {
                    showSuccessMessage(result.Message);
                    //window.location.href='/leads';
                } else {
                    showFailMessage(result.Message);
                }
            },
            error: function (err) {
                showFailMessage(err.statusText);
            }
        });
        //return false;
    });

});