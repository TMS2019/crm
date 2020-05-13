$(document).ready(function(){
	var $fitHeight    = $('.fit-height');
	var $windowHeight; 

	$windowHeight = $(window).height();
	$fitHeight.height($windowHeight)

	$(window).on('resize', function() {
	    $windowHeight = $(window).height();
	    $fitHeight.height($windowHeight)
	})
});