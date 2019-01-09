jQuery(document).ready(function($) {
	

next = jQuery('.next');
prev = jQuery('.prev');
pagination = jQuery('#pagination li a');

pagination.click(function(e) {
	e.preventDefault();
	mythis = jQuery(this);
	thisid = jQuery(this).attr('data-id');
	pagination.removeClass();
	mythis.addClass('pag_active');
	 jQuery('.active').removeClass();
	 jQuery('#spinners li[data-id="'+thisid+'"]').addClass('active');
});

next.click(function(e) {
	e.preventDefault();
	thisid = jQuery('.active').attr('data-id');


	 if (thisid == 6) {

               jQuery('.active').removeClass();
                jQuery('#spinners').find('li:first-child').addClass('active');
                	  pagination.removeClass('pag_active');
                jQuery('#pagination li a[data-id="1"]').addClass('pag_active');
            } else {
               jQuery('.active').removeClass().next().addClass('active');
                jQuery('.pag_active').removeClass('pag_active').parent().next().find('a').addClass('pag_active');
            }





});

prev.click(function(e) {
	e.preventDefault();
	thisid = jQuery('.active').attr('data-id');
	console.log(thisid);

    
		 if (thisid == 1) {
               jQuery('.active').removeClass();
                jQuery('#spinners').find('li:last-child').addClass('active');
                                pagination.removeClass('pag_active');
                jQuery('#pagination li a[data-id="6"]').addClass('pag_active');

            } else {
               jQuery('.active').removeClass().prev().addClass('active');
                jQuery('.pag_active').removeClass('pag_active').parent().prev().find('a').addClass('pag_active');
            }

	

});



});