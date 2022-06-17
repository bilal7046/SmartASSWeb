// JavaScript Document
$(document).ready(function(e) {
	$(".device-nav").click(function(){
				$(this).find('span').toggleClass('fa-times').toggleClass('fa-bars');
				$("nav").toggleClass("reveal");
			});
			$(window).scroll(function() {    
				var scroll = $(window).scrollTop();
			
				if (scroll > 100) {
					$("header").addClass("change");
				} else {
					$("header").removeClass("change");
				}
			});
			
	$('.crypto-currency-table table tr').click(function(e) {
        $(this).toggleClass('active');
	});
    $('[data-toggle="tooltip"]').tooltip();
});
