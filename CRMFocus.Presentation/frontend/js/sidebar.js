$(document).ready(function(){ 
    /*Menu-toggle*/
    $("#menu-toggle").click(function(e) {
        e.preventDefault();
        $("#wrapper").toggleClass("active");
    });

    $('body').scrollspy({ target: '#navwrap', offset:80});

})