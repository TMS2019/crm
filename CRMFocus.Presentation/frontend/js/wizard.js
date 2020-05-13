$(document).ready(function () {
    //Initialize tooltips
    $('.nav-tabs > li a[title]').tooltip();

    //Wizard
    $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {  
        var $target = $(e.target);

        if ($target.parent().hasClass('disabled')) {
            return false;
        }
    });
    $(".next-step-scenario").click(function (e) {
        var ScenarioName = $("#nama-scenario").val();
        if (ScenarioName.length <= 0) {
            swal({
                icon: "error",
                text: "Nama Skenario harus di isi",
                timer: 120000,
                showconfirmbutton: true,
            });
            return false;
        } else if ($('input#periodProgram').is(':checked')) {
            var $active = $('.wizard .nav-tabs li.active');
            $active.next().removeClass('disabled');
            nextTab($active);
        } else {
            swal({
                icon: "error",
                text: "Mohon ceklis Periode Program",
                timer: 120000,
                showconfirmbutton: true,
            });
            return false;
        }
    });
    $(".next-step-targeting").click(function () {
        var TargetCustumerName = $("#input-name").val();
        if (TargetCustumerName.length <= 0) {
            swal({
                icon: "error",
                text: "Nama Target Customer Baru harus di isi",
                timer: 120000,
                showconfirmbutton: true,
            });
            return false;
        } else {
        var $active = $('.wizard .nav-tabs li.active');
        $active.next().removeClass('disabled');
        nextTab($active);
        }
    });
    $(".next-step").click(function (e) {
        var $active = $('.wizard .nav-tabs li.active');
        $active.next().removeClass('disabled');
        nextTab($active);
    });
    $(".prev-step").click(function (e) {

        var $active = $('.wizard .nav-tabs li.active');
        prevTab($active);

    });
});

function nextTab(elem) {
    $(elem).next().find('a[data-toggle="tab"]').click();
}
function prevTab(elem) {
    $(elem).prev().find('a[data-toggle="tab"]').click();
}