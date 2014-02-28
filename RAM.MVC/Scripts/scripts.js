/* -----------------------------------------------------------------------------

	TABLE OF CONTENTS

	1.) Global Functions
	2.) General
	3.) Header
	4.) Core
	5.) Bottom Panel
	6.) Screen Resize
	7.) Style Switcher

----------------------------------------------------------------------------- */

(function($){
    "use strict";

/* -----------------------------------------------------------------------------

	1.) GLOBAL FUNCTIONS

----------------------------------------------------------------------------- */
    


	/* -------------------------------------------------------------------------
		ACCORDION
	------------------------------------------------------------------------- */


	$.fn.stAccordion = function(){

		var self = $(this),
		items = self.find( '.accordion-item' );
		items.filter( '.active' ).find( '.accordion-item-content' ).css( 'display', 'block' );

		self.find( '.accordion-item-title ' ).click(function(){
			if ( ! $(this).parent().hasClass( 'active' ) ) {
				items.filter( '.active' ).find( '.accordion-item-content' ).slideUp(300);
				items.filter( '.active' ).removeClass( 'active' );
				$(this).parent().find( '.accordion-item-content' ).slideDown(300);
				$(this).parent().addClass( 'active' );
			}
			else {
				$(this).parent().find( '.accordion-item-content' ).slideUp(300);
				$(this).parent().removeClass( 'active' );
			}
		});

	};

	/* -------------------------------------------------------------------------
		BACKGROUND VIDEO
	------------------------------------------------------------------------- */

	$.fn.stBackgroundVideo = function(){

		var self = $(this),
		container = self.find( '.video-bg-container' ),
		container_inner = self.find( '.video-bg-container-inner' );

		// INIT VIDEO
		container.find( 'video' ).mediaelementplayer({
			loop: true,
			videoWidth: -1,
			videoHeight: -1,
			enableKeyboard: false,
			iPadUseNativeControls: false,
			pauseOtherPlayers: true,
			iPhoneUseNativeControls: false,
			AndroidUseNativeControls: false,
			enableAutosize: false
		});

		var update_video = function() {

			container.find( '.mejs-container' ).removeAttr( 'style' );

			var container_width = container.width(),
			container_height = container.height(),
			container_ratio = container_width / container_height,
			inner_width = container_inner.width(),
			inner_height = container_inner.height();




			if ( inner_width < container_width ) {
				container_inner.removeAttr( 'style' );
			}
			if ( inner_height < container_height ) {

				container_inner.css( 'width', inner_width * container_ratio );
			}

			// CENTER HORIZONTALLY
			container_inner.css( 'left', - Math.abs( container_inner.width() - container_width ) / 2 );

		};

		update_video();
		$(window).resize(function(){
			update_video();
		});

	};

	/* -------------------------------------------------------------------------
		CONTACT FORM
	------------------------------------------------------------------------- */
	

	$.fn.stContactForm = function(){

		var form = $(this).prop( 'tagName' ).toLowerCase() === 'form' ? $(this) : $(this).find( 'form' ),
		submit_btn = form.find( '.submit' );

		form.submit(function(e){
			e.preventDefault();

			if ( ! submit_btn.hasClass( 'loading' ) ) {

				// form not valid
				if ( ! form.stFormValid() ) {
					form.find( 'p.alert-message.warning.validation' ).slideDown(300);
					return false;
				}
				// form valid
				else {

					submit_btn.addClass( 'loading' ).attr( 'data-label', submit_btn.text() );
					submit_btn.text( submit_btn.data( 'loading-label' ) );

					// ajax request
					$.ajax({
						type: 'POST',
						url: form.attr( 'action' ),
						data: form.serialize(),
						success: function( data ){

							form.find( '.alert-message.validation' ).hide();
							form.prepend( data.Message );
							form.find( '.alert-message.success, .alert-message.phpvalidation' ).slideDown(300);
							submit_btn.removeClass( 'loading' );
							submit_btn.text( submit_btn.attr( 'data-label' ) );

							// reset all inputs
							if ( data.Status == 'success') {
								form.find( 'input, textarea' ).each( function() {
									$(this).val( '' );
								});
							}

						},
						error: function(){
							form.find( '.alert-message.validation' ).slideUp(300);
							form.find( '.alert-message.request' ).slideDown(300);
							submit_btn.removeClass( 'loading' );
							submit_btn.text( submit_btn.attr( 'data-label' ) );
						}
					});

				}

			}
		});
	};

	/* -------------------------------------------------------------------------
		COUNTER
	------------------------------------------------------------------------- */

	$.fn.stCounter = function(){
		var media_query_breakpoint = stMediaQueryBreakpoint();
		if ( media_query_breakpoint > 991 ) {

			var self = $(this),
			duration = parseInt( self.data( 'duration' ) ),
			value = parseInt( self.text() );
			if ( ! isNaN( duration ) ) {

				// RESET
				self.text(0);

				// START AT IN VIEW
				self.one( 'inview', function(){
					var speed = duration / value,
					current_val = 0;
					var counter_action = function(){
						current_val++;
						self.text( current_val );
					};
					for ( var i = 0; i < value; i++ ) {
						setTimeout( counter_action, speed * i );
					}
				});

			}

		}
	};

	/* -------------------------------------------------------------------------
		DRIBBBLE FEED WIDGET
	------------------------------------------------------------------------- */

	$.fn.stDribbbleFeed = function() {
		if ( $.jribbble ) {

			$(this).append( '<div class="widget-feed"></div>' );

			var self = $(this),
			feed = self.find( '.widget-feed' ),
			feed_id = self.data( 'id' ),
			feed_limit = self.data( 'limit' );

			if ( isNaN( feed_limit ) || feed_limit < 1 ) {
				feed_limit = 1;
			}

			// launch jribbble
			$.jribbble.getShotsByPlayerId( feed_id, function( playerShots ) {

				// get number of images to be shown
				var number_of_images = feed_limit;
				if ( playerShots.shots.length < feed_limit ){
					number_of_images = playerShots.shots.length;
				}

				// create blank image list inside feed
				feed.html( '<ul class="image-list clearfix"></ul>' );

				// insert items
				var i;
				for ( i = 0; i < number_of_images; i++ ) {
					feed.find( 'ul' ).append( '<li class="image-list-item"><a class="image-list-link" href="' + playerShots.shots[i].url + '" style="background-image: url(' + playerShots.shots[i].image_teaser_url + ');" title="' + playerShots.shots[i].title + '" target="_blank" rel="external"><img class="image-list-thumb" src="' + playerShots.shots[i].image_teaser_url + '" alt="' + playerShots.shots[i].title + '"></a></li>' );
				}

				// images loaded
				self.stImagesLoaded(function(){
					self.find( '> *' ).not( '.widget-feed' ).fadeOut(300, function(){
						self.find( '.widget-feed' ).fadeIn( 300, function(){
							self.removeClass( 'loading' );
						});
					});
				});

			}, { page: 1, per_page: feed_limit } );

		}
	};

	/* -------------------------------------------------------------------------
		FLICKR FEED WIDGET
	------------------------------------------------------------------------- */

	$.fn.stFlickrFeed = function() {

		$(this).append( '<div class="widget-feed"></div>' );

		var self = $(this),
		feed = $(this).find( '.widget-feed' ),
		feed_id = $(this).data( 'id' ),
		feed_limit = $(this).data( 'limit' );

		if ( isNaN( feed_limit ) || feed_limit < 1 ) {
			feed_limit = 1;
		}

		// create blank image list inside feed
		feed.html( '<ul class="image-list clearfix"></ul>' );

		// get the feed
		$.getJSON( 'http://api.flickr.com/services/feeds/photos_public.gne?id=' + feed_id + '&lang=en-us&format=json&jsoncallback=?', function(data){

			// get number of images to be shown
			var number_of_images = feed_limit;
			if ( data.items.length < feed_limit ) {
				number_of_images = data.items.length;
			}

			// insert items
			var i;
			for ( i = 0; i < number_of_images; i++ ){
				feed.find( 'ul' ).append( '<li class="image-list-item"><a class="image-list-link" href="' + data.items[i].link + '" style="background-image: url(' + data.items[i].media.m + ');" target="_blank" rel="external"><img class="image-list-thumb" src="' + data.items[i].media.m + '" alt="' + data.items[i].title + '"></a></li>' );
			}

			// images loaded
			self.stImagesLoaded(function(){
				self.find( '.loading-anim' ).fadeOut( 300, function(){
					self.find( '.widget-feed' ).fadeIn( 300, function(){
						self.removeClass( 'loading' );
					});
				});
			});

		});

	};

	/* -------------------------------------------------------------------------
		FLUID VIDEOS
	------------------------------------------------------------------------- */

	$.fn.stFluidVideos = function(){

		var all_videos = '';

        var reload_fluid_videos = function(){
			// Resize all videos according to their own aspect ratio
            all_videos.each(function() {
                var el = $(this);
                var el_container = el.parents( '.embed-video' );
                var new_width = el_container.width();
                el.width( new_width ).height( new_width * el.data( 'aspectRatio' ) );
            });
        };

        var generate_fluid_videos = function(){
            // Find all videos
            all_videos = $( '.embed-video iframe' );
            // The element that is fluid width
            //$fluidEl = $('.embed-video').first();
            // Figure out and save aspect ratio for each video
            all_videos.each(function() {
                $(this).data( 'aspectRatio', this.height / this.width )
                    // and remove the hard coded width/height
                    .removeAttr( 'height' )
                    .removeAttr( 'width' );
            });
            reload_fluid_videos();
        };

        generate_fluid_videos();
        $(window).resize(function(){
            reload_fluid_videos();
        });

	};
	$( 'body' ).stFluidVideos();

	/* -------------------------------------------------------------------------
		FORM VALIDATION
	------------------------------------------------------------------------- */

	$.fn.stFormValid = function() {
		function emailValid( email ) {
			var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
			return re.test(email);
		}

		var form = $(this),
		formValid = true;

		form.find( 'input.required, textarea.required, select.required' ).each(function(){

			var field = $(this),
			value = field.val(),
			placeholder = field.data( 'placeholder' ) ? field.data( 'placeholder' ) : false,
			valid = false;

			if ( value.trim() !== '' && ! ( placeholder && value === placeholder ) ) {

				// email field
				if ( field.hasClass( 'email' ) ) {
					if ( ! emailValid( value ) ) {
						field.addClass( 'error' );
					}
					else {
						field.removeClass( 'error' );
						valid = true;
					}
				}

				// select field
				else if ( field.prop( 'tagName' ).toLowerCase() === 'select' ) {
					if ( value === null ) {
						field.addClass( 'error' );
					}
					else {
						field.removeClass( 'error' );
						valid = true;
					}
				}

				// default field
				else {
					field.removeClass( 'error' );
					valid = true;
				}

			}
			else {
				field.addClass( 'error' );
			}
			formValid = ! valid ? false : formValid;

		});

		return formValid;

	};

	/* -------------------------------------------------------------------------
		IMAGES LOADED
	------------------------------------------------------------------------- */

    $.fn.stImagesLoaded = function( options ) {
		if ( $.isFunction( options ) ) {

			var images = $(this).find( 'img' ),
			loaded_images = 0,
			count = images.length;

			if ( count > 0 ) {
				images.one( 'load', function(){
					loaded_images++;
					if ( loaded_images === count ){
						options.call();
					}
				}).each(function() {
					if ( this.complete ) { $(this).load(); }
				});
			}
			else {
				options.call();
			}

		}
    };

	/* -------------------------------------------------------------------------
		INPUT PLACEHOLDERS
	------------------------------------------------------------------------- */

	$.fn.stInputPlaceholders = function() {
		$(this).find( '*[data-placeholder]' ).each(function(){

			var self = $(this),
			placeholder = self.data( 'placeholder' );

			// focus
			self.focus(function(){
				if ( self.val() === placeholder ) {
					self.val( '' );
				}
			});

			// blur
			self.blur(function(){
				if ( self.val().trim() === '' ) {
					self.val( placeholder );
				}
			});

		});
	};

	/* -------------------------------------------------------------------------
		INSTAGRAM FEED WIDGET
	------------------------------------------------------------------------- */

	$.fn.stInstagramFeed = function() {
		if ( $.fn.embedagram ) {

			$(this).append( '<div class="widget-feed"></div>' );

			var self = $(this),
			feed = $(this).find( '.widget-feed' ),
			feed_id = $(this).data( 'id' ),
			feed_limit = $(this).data( 'limit' );

			if ( isNaN( feed_limit ) || feed_limit < 1 ) {
				feed_limit = 1;
			}

			// create blank image list inside feed
			feed.html( '<ul class="image-list clearfix"></ul>' );

			// launch embedagram
			feed.find( 'ul.image-list' ).embedagram({
				instagram_id: feed_id,
				limit: feed_limit,
				success: function(){

					feed.find( 'li' ).addClass( 'image-list-item' );
					feed.find( 'a' ).each(function(){
						$(this).addClass( 'image-list-link' ).css( 'background-image', 'url(' + $(this).find( 'img' ).attr( 'src' ) + ')' ).attr( 'target','_blank' ).attr( 'rel','external' );
						$(this).find( 'img' ).addClass( 'image-list-thumb' );
						if ( $(this).find( 'img' ).attr( 'title' ) ) {
							$(this).find( 'img' ).attr( 'alt', $(this).find( 'img' ).attr( 'title' ) );
							$(this).find( 'img' ).removeAttr( 'title' );
						}
					});

					// images loaded
					self.stImagesLoaded(function(){
						self.find( '> *' ).not( '.widget-feed' ).fadeOut(300, function(){
							self.find( '.widget-feed' ).fadeIn( 300, function(){
								self.removeClass( 'loading' );
							});
						});
					});

				}
			});

		}
	};

	/* -------------------------------------------------------------------------
		INVIEW ANIMATIONS
	------------------------------------------------------------------------- */

	if ( $( 'body' ).hasClass( 'enable-inview-animations' ) ) {
		$( '*[data-inview-animation]' ).one( 'inview', function(){
			var anim_class = $(this).data( 'inview-animation' );
			$(this).addClass( 'animated ' + anim_class );
		});
	}

	/* -------------------------------------------------------------------------
		LIGHTBOX
	------------------------------------------------------------------------- */

	// LIGHTBOX STRINGS SETUP
	$.extend( true, $.magnificPopup.defaults, {
		tClose: 'Close (Esc)',
		tLoading: 'Loading...',
		gallery: {
			tPrev: 'Previous (Left arrow key)', // Alt text on left arrow
			tNext: 'Next (Right arrow key)', // Alt text on right arrow
			tCounter: '%curr% / %total%' // Markup for "1 of 7" counter
		},
		image: {
			tError: '<a href="%url%">The image</a> could not be loaded.' // Error message when image could not be loaded
		},
		ajax: {
			tError: '<a href="%url%">The content</a> could not be loaded.' // Error message when ajax request failed
		}
	});

	// FUNCTION
	$.fn.stInitLightboxes = function(){
		if ( $.fn.magnificPopup ) {
			$(this).find( 'a.lightbox' ).each(function(){

				var self = $(this),
				lightbox_group = self.data( 'lightbox-group' ) ? self.data( 'lightbox-group' ) : false;

				if ( lightbox_group ) {
					$( 'a.lightbox[data-lightbox-group="' + lightbox_group + '"]' ).magnificPopup({
						type: 'image',
						removalDelay: 300,
						mainClass: 'mfp-fade',
						gallery: {
							enabled: true
						}
					});
				}
				else {
					self.magnificPopup({
						type: 'image'
					});
				}

			});
		}
	};

	/* -------------------------------------------------------------------------
		LOAD HIRES IMAGES
	------------------------------------------------------------------------- */

	$.fn.stLoadHiresImages = function() {
		if ( window.devicePixelRatio > 1 ) {
			$(this).find( 'img[data-hires]' ).each(function(){
				$(this).attr( 'src', $(this).data( 'hires' ) );
			});
		}
	};

	/* -------------------------------------------------------------------------
		MAILCHIMP SUBSCRIBE WIDGET
	------------------------------------------------------------------------- */

	$.fn.stMailchimpSubscribe = function() {

		var form = $(this).prop( 'tagName' ).toLowerCase() === 'form' ? $(this) : $(this).find( 'form' ),
		submitBtn = form.find( '.submit' );

		form.submit(function(e){
			e.preventDefault();
			if ( ! form.hasClass( 'loading' ) ) {

				// form valid
				if ( form.stFormValid() ) {

					form.find( 'p.alert-message.warning.validation' ).slideUp(300);
					form.addClass( 'loading' );
					submitBtn.attr( 'data-label', submitBtn.text() ).text( submitBtn.data( 'loading-label' ) );

					// send ajax request
                    $.ajax({
                        type: form.attr( 'method' ),
                        url: form.attr( 'action' ),
                        data: form.serialize(),
                        cache : false,
                        dataType : 'json',
                        contentType: "application/json; charset=utf-8",
                        // wait for a response
                        success: function( data ){

                            if ( data.result === 'success' ) {
                                form.find( '.alert-message' ).hide();
                                form.find( '.alert-message.success' ).append( '<br>' + data.msg ).slideDown(300);
                                form.find( 'input' ).each( function() {
                                    $(this).val( $(this).data( 'placeholder' ) ).addClass( 'placeholder' );
                                });
                                form.find( '.form-fields' ).slideUp(300);
                            }
                            else {
                                form.find( '.alert-message.validation' ).slideUp(300);
                                form.find( '.alert-message.request' ).slideDown(300);
                            }

							form.removeClass( 'loading' );
							submitBtn.text( submitBtn.attr( 'data-label' ) );

                        },
                        error: function(){

                            form.find( '.alert-message.validation' ).slideUp(300);
                            form.find( '.alert-message.request' ).slideDown(300);
                            form.removeClass( 'loading' );
							submitBtn.text( submitBtn.attr( 'data-label' ) );

                        }
                    });

				}

				// form invalid
				else {
					form.find( 'p.alert-message.warning.validation' ).slideDown(300);
					return false;
				}

			}
		});

	};

	/* -------------------------------------------------------------------------
		MEDIA QUERY BREAKPOINT
	------------------------------------------------------------------------- */

	var stMediaQueryBreakpoint = function() {

		if ( $( '#media-query-breakpoint' ).length < 1 ) {
			$( 'body' ).append( '<var id="media-query-breakpoint"><span></span></var>' );
		}
		var value = $( '#media-query-breakpoint' ).css( 'content' );
		if ( typeof value !== 'undefined' ) {
			value = value.replace( "\"", "" ).replace( "\"", "" ).replace( "\'", "" ).replace( "\'", "" );
			if ( isNaN( parseInt( value, 10 ) ) ){
				$( '#media-query-breakpoint span' ).each(function(){
					value = window.getComputedStyle( this, ':before' ).content;
				});
				value = value.replace( "\"", "" ).replace( "\"", "" ).replace( "\'", "" ).replace( "\'", "" );
			}
		}
		else {
			value = 1199;
		}
		return value;

	};

	/* -------------------------------------------------------------------------
		PARALLAX
	------------------------------------------------------------------------- */

	$.fn.stParallax = function() {
	/*
		if ( $.fn.stellar ) {

			var self = $(this),
			media_query_breakpoint = stMediaQueryBreakpoint();

			if ( media_query_breakpoint >= 1200 ) {

				$(window).stellar({
					responsive:true,
							scrollProperty: 'scroll',
							parallaxElements: false,
							parallaxBackgrounds: true,
							horizontalScrolling: false,
							horizontalOffset: 0,
							verticalOffset: 0
				});

			}

		}
		*/


		var self = $(this),
		initial_offset = self.offset().top;

		$(window).resize(function(){
			initial_offset = self.offset().top;
		}).load(function(){
			initial_offset = self.offset().top;
		});

		// UPDATE BACKGROUND POSITION
		var update_parallax = function(){

			var media_query_breakpoint = stMediaQueryBreakpoint();
			if ( media_query_breakpoint >= 1200 ) {
				var pos = -( $(window).scrollTop() / 10 );
				//alert( initial_offset );
				//self.css( 'backgroundPosition', '50% ' + Math.round( ( initial_offset - pos ) * 0.2 ) + 'px' );
				self.css( 'backgroundPosition', '50% ' + Math.round( pos ) + 'px' );
			}

		};

		// CHECK IF IN VIEW
		self.bind( 'inview', function( event, isInView ){
			if ( isInView ) {
				self.addClass( 'is-in-view' );
			}
			else {
				self.removeClass( 'is-in-view' );
			}
		});
		self.one( 'inview', function(){
			update_parallax();
		});


		// UPDATE
		$(window).scroll(function(){
			if ( self.hasClass( 'is-in-view' ) ) {
				update_parallax();
			}
		}).resize(function(){
			if ( self.hasClass( 'is-in-view' ) ) {
				//update_parallax();
			}
		}).load(function(){
			//update_parallax();
		});



	};

	/* -------------------------------------------------------------------------
		PORTFOLIO GALLERY
	------------------------------------------------------------------------- */

	$( '.portfolio-gallery' ).each(function(){

		var portfolio = $(this),
		filter = portfolio.find( '.category-list' ),
		list = portfolio.find( '.gallery-list' ),
		thumbs = list.find( '.gallery-item' );

		// THUMB HOVER
		thumbs.hover(function(){
			$(this).find( '.item-info' ).fadeIn(300);
			$(this).addClass( 'hover' );

		}, function(){
			$(this).find( '.item-info' ).fadeOut(300);
			$(this).removeClass( 'hover' );
		});

		// HORIZONTAL STRIP LAYOUT
		if ( portfolio.hasClass( 'layout-horizontal-strip' ) ) {
			if ( $.fn.owlCarousel ) {

				var items = portfolio.data( 'items' ) ? parseInt( portfolio.data( 'items' ) ) : 5,
				items_desktop =  portfolio.data( 'items-desktop' ) ? parseInt( portfolio.data( 'items-desktop' ) ) : items,
				items_desktop_small =  portfolio.data( 'items-desktop-small' ) ? parseInt( portfolio.data( 'items-desktop-small' ) ) : items,
				items_tablet =  portfolio.data( 'items-tablet' ) ? parseInt( portfolio.data( 'items-tablet' ) ) : items,
				items_mobile =  portfolio.data( 'items-mobile' ) ? parseInt( portfolio.data( 'items-mobile' ) ) : 1,
				interval = portfolio.data( 'interval' ) ? portfolio.data( 'interval' ) : false;

				// INIT CAROUSEL
				list.owlCarousel({
					autoPlay: interval,
					slideSpeed: 300,
					pagination: true,
					paginationSpeed : 400,
					items: items,
					itemsDesktop : [1199,items_desktop],
					itemsDesktopSmall : [979,items_desktop_small],
					itemsTablet: [768,items_tablet],
					itemsMobile: [400,items_mobile]
				});

			}
		}
		// GRID LAYOUT
		else if ( portfolio.hasClass( 'layout-grid' ) ) {
			if ( $.fn.isotope ) {

				list.stImagesLoaded(function(){
					portfolio.find( '.loading-anim-container' ).fadeOut(500);
					list.fadeIn(500);
					list.isotope({
						itemSelector : '.gallery-item',
						layoutMode : 'fitRows'
					});
				});

			}
		}
		// MASONRY LAYOUT
		else if ( portfolio.hasClass( 'layout-masonry' ) ) {
			if ( $.fn.isotope ) {

				list.stImagesLoaded(function(){
					portfolio.find( '.loading-anim-container' ).fadeOut(500);
					list.fadeIn(500);
					list.isotope({
						itemSelector : '.gallery-item',
						layoutMode : 'masonry'
					});
				});

			}
		}

		// FILTER
		if ( filter.length > 0 ) {
			filter.find( 'a' ).each(function(){

				var self = $(this),
				category = self.data( 'category' ),
				active_filter = category !== '*' ? '.' + category : category;

				self.click(function(){
					if ( ! self.hasClass( 'active' ) ) {

						//alert( category );

						list.isotope({ filter: active_filter });
						filter.find( '.active' ).removeClass( 'active' );
						self.addClass( 'active' );
						return false;

					}
				});
			});
		}

		// AJAXED DETAIL
		if ( portfolio.hasClass( 'ajaxed' ) ) {
			thumbs.find( '.item-thumb' ).click(function(){

				var thumb = $(this).parent(),
				link =  $(this).attr( 'href' );

				// CLOSE IF ACTIVE
				if ( thumb.hasClass( 'active' ) ) {
					thumb.removeClass( 'active' );
					portfolio.find( '.gallery-ajaxed-detail' ).slideUp(300,function(){
						$(this).remove();
					});
					return false;
				}

				// OPEN
				else if ( ! thumb.hasClass( 'loading' ) ) {

					portfolio.addClass( 'loading' );
					thumbs.filter( '.active' ).removeClass( 'active' ).filter( '.loading' ).removeClass( 'loading' ).find( '.loading-anim-container' ).remove();
					thumb.addClass( 'loading active' ).append( '<span class="loading-anim-container"><span class="loading-anim"><span></span></span></span>' ).find( '.loading-anim-container' ).fadeIn(300);
					$( '.ajax-loading-container' ).remove();
					$( 'body' ).append( '<div class="ajax-loading-container"></div>' );

					// DETAIL SHOWN FUNCTION
					var ajaxed_detail_shown = function(){

						// INIT LIGHTBOXES
						portfolio.find( '.gallery-ajaxed-detail' ).stInitLightboxes();

						// INIT PROJECT IMAGES
						portfolio.find( '.gallery-ajaxed-detail .project-images' ).stProjectImages();

						// INIT FLUID VIDEOS
						portfolio.find( '.gallery-ajaxed-detail .project-images' ).stFluidVideos();

					};

					// AJAX REQUEST
					$( '.ajax-loading-container' ).load( link + ' .portfolio-detail', function(){
						var data = $(this);
						if ( data.find( '.portfolio-detail' ).length > 0 ) {
							data.stImagesLoaded(function(){

								var html = '<div class="' + data.find( '.portfolio-detail' ).attr( 'class' ) +  '" style="display: none;"><div class="wrapper">' + data.find( '.portfolio-detail' ).html() + '</div></div>';

								// there is already some project shown
								if ( portfolio.find( '.gallery-ajaxed-detail' ).length > 0 ) {
									portfolio.find( '.gallery-ajaxed-detail .portfolio-detail' ).first().slideUp(300, function(){
										$(this).remove();
									});
									portfolio.find( '.gallery-ajaxed-detail' ).append( html );
									portfolio.find( '.gallery-ajaxed-detail .portfolio-detail' ).last().slideDown(300,function(){
										thumb.removeClass( 'loading' );
										portfolio.removeClass( 'loading' );
										thumb.find( '.loading-anim-container' ).fadeOut(300, function(){
											$(this).remove();
										});
									});
									ajaxed_detail_shown();
								}
								// no project shown
								else {
									portfolio.prepend( '<div class="gallery-ajaxed-detail"><button class="gallery-ajaxed-close"><i class="fa fa-times"></i></button>' + html + '</div>' );
									portfolio.find( '.gallery-ajaxed-detail .portfolio-detail' ).slideDown(300, function(){
										ajaxed_detail_shown();
									});
									thumb.removeClass( 'loading' );
									portfolio.removeClass( 'loading' );
									thumb.find( '.loading-anim-container' ).fadeOut(300, function(){
										$(this).remove();
									});
								}

								// ANIMATE TO TO AJAXED DETAIL
								$( 'html, body' ).animate({
									scrollTop: $( '.gallery-ajaxed-detail' ).offset().top - 50
								}, 300);

								// REMOVE DATA
								data.remove();

								// CLOSE BUTTON
								portfolio.find( '.gallery-ajaxed-close' ).click(function(){
									thumb.removeClass( 'active' );
									portfolio.find( '.gallery-ajaxed-detail' ).slideUp(300,function(){
										$(this).remove();
									});
								});

							});
						}
					});

				}

				return false;

			});
		}

	});

	/* -------------------------------------------------------------------------
		PROGRESS BAR
	------------------------------------------------------------------------- */

	$.fn.stProgressBar = function(){

		var self = $(this),
		percentage = self.data( 'percentage' ) ? parseInt( self.data( 'percentage' ) ) : 100,
		inner = self.find( '.progress-bar-inner' ),
		media_query_breakpoint = stMediaQueryBreakpoint();

		// WITH INVIEW ANIMATIONS
		if ( $( 'body' ).hasClass( 'enable-inview-animations' ) && media_query_breakpoint > 991 ) {
			self.one( 'inview', function(){
				inner.css( 'width', percentage + '%' );
			});
		}
		// WITHOUT INVIEW ANIMATIONS
		else {
			inner.css( 'width', percentage + '%' );
		}

	};

	/* -------------------------------------------------------------------------
		TABBED
	------------------------------------------------------------------------- */

	$.fn.stTabbed = function(){

		var self = $(this),
		tabs = self.find( '.tab-title' ),
		contents = self.find( '.tab-content' );

		tabs.click(function(){
			if ( ! $(this).hasClass( 'active' ) ) {
				var index = $(this).index();
				tabs.filter( '.active' ).removeClass( 'active' );
				$(this).addClass( 'active' );
				contents.filter( '.active' ).slideUp( 300, function(){
					$(this).removeClass( 'active' );
				});
				contents.filter( ':eq(' + index + ')' ).slideDown(300).addClass( 'active' );
			}
		});

	};

	/* -------------------------------------------------------------------------
		TWITTER FEED
	------------------------------------------------------------------------- */

	$.fn.stTwitterFeed = function(){
		if ( $.fn.tweet ) {

			var self = $(this),
			feed_id = self.data( 'id' ),
			feed_limit = self.data( 'limit' );

			self.bind( 'loaded', function(){
				self.removeClass( 'loading' );
				if ( self.hasClass( 'paginated' ) && $.fn.owlCarousel ) {
					var interval = self.data( 'interval' ) ? parseInt( self.data( 'interval' ) ) > 0 : false;
					self.find( '.tweet_list' ).owlCarousel({
						autoPlay: interval,
						slideSpeed: 300,
						pagination: true,
						paginationSpeed : 400,
						singleItem:true
					});
				}
			});

			self.tweet({
				username: feed_id,
				modpath: './library/twitter/',
				count: feed_limit,
				loading_text: '<span class="loading-anim"><span></span></span>'
			});

		}
	};

$(document).ready(function(){
/* -----------------------------------------------------------------------------

	2.) GENERAL

----------------------------------------------------------------------------- */

	// get actual media query breakpoint
	var media_query_breakpoint = stMediaQueryBreakpoint();

	// load hires images for HiDPI screens
	$( 'body' ).stLoadHiresImages();

	// init input placeholders
	$( 'body' ).stInputPlaceholders();

	/* -------------------------------------------------------------------------
		ANIMATE HEADER AND CORE ON INVIEW
	------------------------------------------------------------------------- */

	if ( media_query_breakpoint > 1199 && $( 'body' ).hasClass( 'enable-intro-animation' ) ) {
		$( '#core' ).one( 'inview', function(){
			$(this).addClass( 'init' );
			$( '#header' ).addClass( 'init' );
		});
	}

	/* -------------------------------------------------------------------------
		BODY PATTERN PARALLAX
	------------------------------------------------------------------------- */

	$( 'body.enable-body-parallax' ).each(function(){
		var self = $(this),
		inner = self.find( '.body-inner' ),
		header_inner = self.find( '.header-inner' );
		$(window).scroll(function(){
			if ( media_query_breakpoint > 991 ) {

				var scrolled = $(window).scrollTop(),
				step = 0.008;
				inner.css( 'background-position', ( 0 - ( scrolled * step ) ) + 'px ' + ( 0 + ( scrolled * step ) ) + 'px' );
				if ( self.hasClass( 'enable-fixed-header' ) ) {
					header_inner.css( 'background-position', ( 0 - ( scrolled * step ) ) + 'px ' + ( 0 + ( scrolled * step ) ) + 'px' );
				}

			}
		});
	});


/* -----------------------------------------------------------------------------

	3.) HEADER

----------------------------------------------------------------------------- */

	/* -------------------------------------------------------------------------
		COMPACT FIXED HEADER ON SCROLL
	------------------------------------------------------------------------- */

	if ( $( 'body' ).hasClass( 'enable-fixed-header' ) ) {

		if ( $(window).scrollTop() > 300 ) {
			$( '#header' ).addClass( 'compact' );
		}
		$(window).scroll(function(){
			if ( $(window).scrollTop() > 300 ) {
				$( '#header' ).addClass( 'compact' );
			}
			else {
				$( '#header' ).removeClass( 'compact' );
			}
		});

	}

	/* -------------------------------------------------------------------------
		HEADER MENU
	------------------------------------------------------------------------- */

	// MARK LAST & PENULTIMATE
	$( '#header .menu-items > li:last-child' ).addClass( 'last' ).prev().addClass( 'penultimate' );

	// MARK ITEMS WITH SUBMENUS
	$( '#header .menu-items .sub-menu' ).each(function(){
		$(this).parent().addClass( 'has-sub-menu' ).append( '<button class="item-toggle"><i class="ico fa fa-plus"></i></button>' );
	});

	// SHOW SUBMENU ON HOVER
	$( '#header .header-menu .sub-menu' ).each(function(){

		var submenu = $(this);

		submenu.parent().hover(function(){
			if ( media_query_breakpoint > 991 ) {

				$(this).addClass( 'hover' );
				submenu.unbind( 'animationend webkitAnimationEnd oAnimationEnd MSAnimationEnd' );
				// WITH CSS ANIMATIONS
				if ( $( 'html' ).hasClass( 'cssanimations' ) ) {
					submenu.removeClass( 'animated fadeOutDown' ).show().addClass( 'animated fadeInUp' );
				}
				// WITHOUT CSS ANIMATIONS
				else {
					submenu.fadeIn(300);
				}

			}
		}, function(){
			$(this).removeClass( 'hover' );
			if ( media_query_breakpoint > 991 ) {

				// WITH CSS ANIMATIONS
				if ( $( 'html' ).hasClass( 'cssanimations' ) ) {
					submenu.removeClass( 'animated fadeInUp' ).addClass( 'animated fadeOutDown' );
					submenu.bind( 'animationend webkitAnimationEnd oAnimationEnd MSAnimationEnd', function(){
						submenu.hide();
					});
				}
				// WITHOUT CSS ANIMATIONS
				else {
					submenu.fadeOut(300);
				}

			}
		});

	});

	// SHOW SUBMENU ON CLICK
	$( '#header .menu-items .item-toggle' ).click(function(){

		var self = $(this),
		submenu = self.parent().find( '> .sub-menu' );

		if ( submenu.is( ':hidden' ) ) {
			submenu.slideDown(300);
			self.find( '.ico' ).removeClass( 'fa-plus' ).addClass( 'fa-minus' );
		}
		else {
			submenu.slideUp(300);
			self.find( '.ico' ).removeClass( 'fa-minus' ).addClass( 'fa-plus' );
		}

	});


	/* -------------------------------------------------------------------------
		SHOW SEARCH
	------------------------------------------------------------------------- */

	$( '.header-search' ).each(function(){

		var self = $(this),
		ico = self.find( '.header-search-ico' );

		// TOGGLE
		ico.click(function(){

			// SHOW
			if ( ! ico.hasClass( 'active' ) ) {

				$( '#header' ).addClass( 'expanded' );
				ico.addClass( 'active' ).find( 'i' ).removeClass( 'fa-search' ).addClass( 'fa-times' );

				// CLONE IF DOESNT EXIST
				if ( $( '.header-search-clone' ).length < 1 ) {
					var clone = '<div class="header-search-clone">' +  $( '.header-search-inner' ).html() + '</div>';
					$( '#header .header-content' ).before( clone );
					$( '.header-search-clone' ).slideDown(300);
					$( '.header-search-clone' ).stInputPlaceholders();
				}
				else {
					$( '.header-search-clone' ).slideDown(300);
				}

			}
			// HIDE
			else {

				$( '#header' ).removeClass( 'expanded' );
				ico.removeClass( 'active' ).find( 'i' ).removeClass( 'fa-times' ).addClass( 'fa-search' );
				$( '.header-search-clone' ).slideUp(300);

			}

		});

	});

	/* -------------------------------------------------------------------------
		TOGGLE NAVBAR
	------------------------------------------------------------------------- */

	$( '.header-navbar-toggle' ).click(function(){

		var navbar = $( '.header-navbar' );

		if ( navbar.is( ':hidden' ) ) {
			navbar.slideDown(300);
		}
		else {
			navbar.slideUp( 300, function(){
				$(this).removeAttr( 'style' );
			});
		}

	});


/* -----------------------------------------------------------------------------

	4.) CORE

----------------------------------------------------------------------------- */

	/* -------------------------------------------------------------------------
		ACCORDIONS
	------------------------------------------------------------------------- */

	$( '.accordion-container' ).each(function(){
		$(this).stAccordion();
	});

	/* -------------------------------------------------------------------------
		ALERT MESSAGES
	------------------------------------------------------------------------- */

	$( '.alert-message .close-message' ).click(function(){
		$(this).parents( '.alert-message' ).slideUp(300);
	});

	/* -------------------------------------------------------------------------
		CLIENT LIST
	------------------------------------------------------------------------- */

	$( '.client-list.paginated' ).each(function(){
		if ( $.fn.owlCarousel ) {

			var self = $(this),
			interval = self.data( 'interval' ) ? parseInt( self.data( 'interval' ) ) : false,
			items = self.data( 'items' ) ? parseInt( self.data( 'items' ) ) : 4,
			items_desktop =  self.data( 'items-desktop' ) ? parseInt( self.data( 'items-desktop' ) ) : items,
			items_desktop_small =  self.data( 'items-desktop-small' ) ? parseInt( self.data( 'items-desktop-small' ) ) : 3,
			items_tablet =  self.data( 'items-tablet' ) ? parseInt( self.data( 'items-tablet' ) ) : 3,
			items_mobile =  self.data( 'items-mobile' ) ? parseInt( self.data( 'items-mobile' ) ) : 1;

			$(this).owlCarousel({
				autoPlay: interval,
				slideSpeed: 300,
				pagination: true,
				paginationSpeed : 400,
				items : items,
				itemsDesktop : [1199,items_desktop],
				itemsDesktopSmall : [979,items_desktop_small],
				itemsTablet: [768,items_tablet],
				itemsMobile: [400,items_mobile]
			});

		}
	});

	/* -------------------------------------------------------------------------
		COMMENT FORM
	------------------------------------------------------------------------- */

	$( '#comment-form' ).each(function(){

		var form = $(this);
		form.submit(function(){

			if ( form.stFormValid() ) {
				form.find( '.alert-message.success' ).slideDown(300);
				form.find( '.alert-message.validation:visible' ).slideUp(300);
			}
			else {
				form.find( '.alert-message.success:visible' ).slideUp(300);
				form.find( '.alert-message.validation' ).slideDown(300);
			}

			return false;

		});

	});

	/* -------------------------------------------------------------------------
		CONTACT FORM
	------------------------------------------------------------------------- */

	$( '#contact-form' ).each(function(){
		$(this).stContactForm();
	});

	/* -------------------------------------------------------------------------
		COUNTERS
	------------------------------------------------------------------------- */

	$( '.counter-container .counter-data' ).each(function(){
		$(this).stCounter();
	});

	/* -------------------------------------------------------------------------
		EASTER EGG
	------------------------------------------------------------------------- */

	$( '.easter-egg-trigger' ).click(function(){
		var href = $(this).attr( 'href' );
		$( href ).slideDown(300);
		return false;
	});

	/* -------------------------------------------------------------------------
		LIGHTBOXES
	------------------------------------------------------------------------- */

	$( 'body' ).stInitLightboxes();

	/* -------------------------------------------------------------------------
		MAIN SLIDER
	------------------------------------------------------------------------- */

	$( '.main-slider' ).each(function(){
		if ( $.fn.owlCarousel ) {

			var slider = $(this),
			slides_list = slider.find( '.slider-item-list' ),
			slides = slider.find( '.slider-item' ),
			slides_count = slides.length,
			interval = slider.data( 'interval' ) ? parseInt( slider.data( 'interval' ) ) : false,
			stop_on_hover = slider.hasClass( 'stop-on-hover' ) ? true : false;

			// SET SLIDER BACKGROUND IMAGES
			var slider_bg_image = slider.find( '.slider-bg' ).length > 0 ? slider.find( '.slider-bg' ).attr( 'src' ) : false;
			if ( slider_bg_image !== false ) {
				slider.css( 'background-image', 'url(' + slider_bg_image + ')');
			}

			// SET ITEMS BACKGROUND IMAGES
			slides.each(function(){
				var bg_image = $(this).find( '.slider-item-bg' ).length > 0 ? $(this).find( '.slider-item-bg' ).attr( 'src' ) : false;
				if ( bg_image !== false ) {
					$(this).css( 'background-image', 'url(' + bg_image + ')');
				}
			});

			// CREATE CUSTOM PAGINATION
			if ( slides_count > 1 && slider.hasClass( 'has-pagination' ) ) {

				var pagination_html = '<div class="slider-pagination-container"><div class="wrapper"><ul class="slider-pagination">';
				slides.each(function(){
					var label = $(this).data( 'label' ) ? $(this).data( 'label' ) : parseInt( $(this).index() ) + 1;
					if ( $(this).is( ':first-child' ) ) {
						pagination_html += '<li class="active"><button>' + label + '</button></li>';
					}
					else {
						pagination_html += '<li><button>' + label + '</button></li>';
					}
				});
				pagination_html += '</ul></div></div>';
				slider.append( pagination_html );

			}

			// CREATE ARROW NAVIGATION
			if ( slides_count > 1 && slider.hasClass( 'has-navigation' ) ) {

				var arrows_html = '<div class="slider-navigation-prev"><button><i class="fa fa-angle-left"></i></button></div>';
				arrows_html += '<div class="slider-navigation-next"><button><i class="fa fa-angle-right"></i></button></div>';
				slider.append( arrows_html );

			}

			// CREATE INDICATOR
			if ( interval ) {
				slider.append( '<div class="slider-indicator"><span></span></div>' );
			}

			// INIT SLIDER
			slides_list.owlCarousel({
				autoPlay: interval,
				slideSpeed: 300,
				pagination: false,
				singleItem:true,
				stopOnHover: stop_on_hover,
				addClassActive: true,
				beforeMove: function(){

					// REFRESH INDICATOR
					if ( interval ) {
						slider.find( '.slider-indicator > span' ).stop( 0, 1 ).css( 'width', 0 );
					}

				},
				afterMove: function(){

					// REFRESH PAGINATION
					var active_index = slides_list.find( '.owl-item.active' ).index();
					slider.find( '.slider-pagination .active' ).removeClass( 'active' );
					slider.find( '.slider-pagination > li:eq(' + active_index + ')' ).addClass( 'active' );

					// REFRESH INDICATOR
					if ( interval ) {
						slider.find( '.slider-indicator > span' ).animate({ width : "100%" }, interval );
					}

				}
			});

			// PAGINATION NAVIGATION
			slider.find( '.slider-pagination button' ).click(function(){
				var label = $(this).parent(),
				index = label.index();
				if ( ! label.hasClass( 'active' ) ) {
					slides_list.trigger( 'owl.goTo', index );
				}
			});

			// ARROWS NAVIGATION
			slider.find( '.slider-navigation-prev button' ).click(function(){
				slides_list.trigger( 'owl.prev' );
			});
			slider.find( '.slider-navigation-next button' ).click(function(){
				slides_list.trigger( 'owl.next' );
			});

			// INDICATOR ANIMATION
			if ( interval ) {

				// INITIAL ANIMATION
				slider.find( '.slider-indicator > span' ).animate({
					width : "100%"
				}, interval );

				if ( stop_on_hover ) {

				// STOP ON HOVER
					slider.mouseover(function(){
						slider.find( '.slider-indicator > span' ).stop( 0, 0 );
					});
					slider.hover(function(){
						slider.find( '.slider-indicator > span' ).stop( 0, 0 );
					}, function(){
						// CONTINUE ON OUT
						slider.find( '.slider-indicator > span' ).animate({
							width : "100%"
						}, interval );
					});

				}

			}

		}
	});

	/* -------------------------------------------------------------------------
		PARALLAXED
	------------------------------------------------------------------------- */

	$( '.parallaxed' ).each(function(){
		$(this).stParallax();
	});

	/* -------------------------------------------------------------------------
		PORTFOLIO
	------------------------------------------------------------------------- */

	// PROJECT IMAGES
	$.fn.stProjectImages = function() {

		var self = $(this);
		if ( self.hasClass( 'layout-slider' ) ) {
			self.each(function(){
				$(this).owlCarousel({
					slideSpeed: 300,
					pagination: true,
					singleItem:true,
					addClassActive: true
				});
			});
		}

	};
	$( '.portfolio-detail .project-images' ).stProjectImages();

	/* -------------------------------------------------------------------------
		PROGRESS BARS
	------------------------------------------------------------------------- */

	$( '.progress-bar' ).each(function(){
		$(this).stProgressBar();
	});

	/* -------------------------------------------------------------------------
		TABS
	------------------------------------------------------------------------- */

	$( '.tabs-container' ).each(function(){
		$(this).stTabbed();
	});

	/* -------------------------------------------------------------------------
		TESTIMONIAL LIST
	------------------------------------------------------------------------- */

	$( '.client-testimonials' ).each(function(){
		if ( $.fn.owlCarousel ) {

			var self = $(this),
			active_index = 0,
			list = self.find( '.testimonial-list.paginated' ),
			portraits = self.find( '.client-portraits > li' ),
			interval = list.data( 'interval' ) ? parseInt( self.data( 'interval' ) ) : false;

			// INIT SLIDER
			list.owlCarousel({
				autoPlay: interval,
				slideSpeed: 300,
				pagination: false,
				singleItem:true,
				addClassActive: true,
				afterMove: function(){
					active_index = list.find( '.owl-item.active' ).index();
					portraits.filter( '.active' ).removeClass( 'active' );
					portraits.filter( ':eq( ' + active_index + ')' ).addClass( 'active' );
				}
			});

			// PORTRAITS NAVIGATION
			portraits.click(function(){
				var self = $(this),
				index = self.index();
				list.trigger( 'owl.goTo', index );
			});

		}
	});

	/* -------------------------------------------------------------------------
		WIDGETS
	------------------------------------------------------------------------- */

	// DRIBBBLE FEED
	$( '.dribbble-feed' ).each(function(){
		$(this).stDribbbleFeed();
	});

	// FLICKR FEED
	$( '.flickr-feed' ).each(function(){
		$(this).stFlickrFeed();
	});

	// INSTAGRAM FEED
	$( '.instagram-feed' ).each(function(){
		$(this).stInstagramFeed();
	});


/* -----------------------------------------------------------------------------

	5.) BOTTOM PANEL

----------------------------------------------------------------------------- */

	/* -------------------------------------------------------------------------
		PAGINATED WIDGET LIST
	------------------------------------------------------------------------- */

	$( '.bottom-panel-widgets.paginated' ).each(function(){

		var self = $(this),
		widget_list = self.find( '.widget-list' ),
		interval = self.data( 'interval' ) && parseInt( self.data( 'interval' ) ) > 0 ? parseInt( self.data( 'interval' ) ) : false,
		items = self.data( 'items' ) ? parseInt( self.data( 'items' ) ) : 4,
		items_desktop =  self.data( 'items-desktop' ) ? parseInt( self.data( 'items-desktop' ) ) : items,
		items_desktop_small =  self.data( 'items-desktop-small' ) ? parseInt( self.data( 'items-desktop-small' ) ) : 3,
		items_tablet =  self.data( 'items-tablet' ) ? parseInt( self.data( 'items-tablet' ) ) : 3,
		items_mobile =  self.data( 'items-mobile' ) ? parseInt( self.data( 'items-mobile' ) ) : 1;

		widget_list.owlCarousel({
			autoPlay: interval,
			slideSpeed: 300,
			pagination: true,
			addClassActive: true,
			paginationSpeed : 400,
			items : items,
			itemsDesktop : [1199,items_desktop],
			itemsDesktopSmall : [979,items_desktop_small],
			itemsTablet: [768,items_tablet],
			itemsMobile: [400,items_mobile]
		});

	});

	/* -------------------------------------------------------------------------
		BOTTOM PANEL CONNECT
	------------------------------------------------------------------------- */

	// MAILCHIMP SUBSCRIBE
	$( '.mailchimp-subscribe-form' ).each(function(){
		$(this).stMailchimpSubscribe();
	});

	// TWITTER FEED
	$( '.twitter-feed-tweets' ).each(function(){
		$(this).stTwitterFeed();
	});

	/* -------------------------------------------------------------------------
		BACK TO TOP BUTTON
	------------------------------------------------------------------------- */

	$( '#back-to-top' ).each(function(){
		$(this).click(function(){
			$('html, body').animate({
				scrollTop: 0
			}, 300);
		});
	});


/* -----------------------------------------------------------------------------

	6.) SCREEN RESIZE

----------------------------------------------------------------------------- */

	$(window).resize(function(){
		if ( stMediaQueryBreakpoint() !== media_query_breakpoint ) {
			media_query_breakpoint = stMediaQueryBreakpoint();


			// RESET MAIN MENU
			if ( media_query_breakpoint > 991 ) {
				$( '#header .sub-menu, #header .header-navbar' ).removeAttr( 'style' );
				$( '#header .header-menu .item-toggle .fa-minus' ).removeClass( 'fa-minus' ).addClass( 'fa-plus' );
			}
			else {
				$( '#header .sub-menu' ).removeClass( 'animated fadeOutDown' );
			}

			// RESET PARALLAX
			if ( media_query_breakpoint <= 991 ) {
				$( '.parallaxed' ).removeAttr( 'style' );
			}

		}

		// RESET GRID AND MASONRY LAYOUTS
		if ( $.fn.isotope ) {
			$( '.portfolio-gallery.layout-masonry .gallery-list' ).isotope( 'reLayout' );
		}

	});

/* -----------------------------------------------------------------------------

	7.) STYLE SWITCHER

----------------------------------------------------------------------------- */

	if ( $( 'body' ).hasClass( 'enable-style-switcher' ) ) {

		// CREATE STYLE SWITCHER
		var style_switcher_html = '<div id="style-switcher"><button class="style-switcher-toggle"><i class="ico fa fa-cog"></i></button>';
		style_switcher_html += '<div class="style-switcher-content"><ul class="skin-list">';
		style_switcher_html += '<li><button class="skin-default active" data-skin="default"><span>Default</span></button></li>';
		style_switcher_html += '<li><button class="skin-magma" data-skin="magma"><span>Magma</span></button></li>';
		style_switcher_html += '<li><button class="skin-lime" data-skin="lime"><span>Lime</span></button></li>';
		style_switcher_html += '<li><button class="skin-caribic" data-skin="caribic"><span>Caribic</span></button></li>';
		style_switcher_html += '<li><button class="skin-raspberry" data-skin="raspberry"><span>Raspberry</span></button></li>';
		style_switcher_html += '</ul><ul class="switch-list">';
		style_switcher_html += '<li><button class="switch-fixed-header active"><i class="ico fa fa-check-square-o"></i> Fixed Header <small>(desktop only)</small></button></li>';
		style_switcher_html += '<li><button class="switch-boxed-layout active"><i class="ico fa fa-check-square-o"></i> Boxed Layout <small>(desktop only)</small></button></li>';
		style_switcher_html += '</ul></div></div>';
		$( 'body' ).append( style_switcher_html );

		// INIT SWITCHER
		$( '#style-switcher' ).each(function(){

			var switcher = $(this),
			toggle = switcher.find( '.style-switcher-toggle' ),
			skins = switcher.find( '.skin-list button' ),
			switches = switcher.find( '.switch-list button' ),
			style_switcher_settings = {};

			//localStorage.clear();

			// SAVE SETTINGS
			var style_switcher_save = function(){
				if ( $( 'html' ).hasClass( 'localstorage' ) ) {
					localStorage.style_switcher_settings = JSON.stringify( style_switcher_settings );
				}
			};

			// LOAD SETTINGS
			if ( $( 'html' ).hasClass( 'localstorage' ) && localStorage.style_switcher_settings ) {

				style_switcher_settings = JSON.parse( localStorage.style_switcher_settings );

				// LOAD SAVED SKIN
				if ( typeof style_switcher_settings.skin !== 'undefined' ) {
					skins.filter( '.active' ).removeClass( 'active' );
					skins.filter( '[data-skin="' + style_switcher_settings.skin + '"]' ).addClass( 'active' );
					if ( $( 'head #skin-temp' ).length < 1 ) {
						$( 'head' ).append( '<link id="skin-temp" rel="stylesheet" type="text/css" href="library/css/skin/' + style_switcher_settings.skin + '.css">' );
					}
				}
				// LOAD FIXED HEADER SWITCH
				if ( typeof style_switcher_settings.fixed_header !== 'undefined' ) {
					if ( style_switcher_settings.fixed_header ) {
						switches.filter( '.switch-fixed-header' ).addClass( 'active' );
						switches.filter( '.switch-fixed-header' ).find( '.ico' ).removeClass( 'fa-square-o' ).addClass( 'fa-check-square-o' );
						$( 'body' ).addClass( 'enable-fixed-header' );
					}
					else {
						switches.filter( '.switch-fixed-header' ).removeClass( 'active' );
						switches.filter( '.switch-fixed-header' ).find( '.ico' ).removeClass( 'fa-check-square-o' ).addClass( 'fa-square-o' );
						$( 'body' ).removeClass( 'enable-fixed-header' );
					}

				}

				// LOAD BOXED LAYOUT SWITCH
				if ( typeof style_switcher_settings.boxed_layout !== 'undefined' ) {
					if ( style_switcher_settings.boxed_layout ) {
						switches.filter( '.switch-boxed-layout' ).addClass( 'active' );
						switches.filter( '.switch-boxed-layout' ).find( '.ico' ).removeClass( 'fa-square-o' ).addClass( 'fa-check-square-o' );
						$( 'body' ).addClass( 'enable-boxed-layout' );
					}
					else {
						switches.filter( '.switch-boxed-layout' ).removeClass( 'active' );
						switches.filter( '.switch-boxed-layout' ).find( '.ico' ).removeClass( 'fa-check-square-o' ).addClass( 'fa-square-o' );
						$( 'body' ).removeClass( 'enable-boxed-layout' );
					}
					$( '.owl-carousel' ).each(function(){
						$(this).data( 'owlCarousel' ).reinit();
					});
				}

			}

			// TOGGLE SWITCHER
			toggle.click(function(){
				switcher.toggleClass( 'active' );
			});

			// SET SKIN
			skins.click(function(){
				skins.filter( '.active' ).removeClass( 'active' );
				$(this).toggleClass( 'active' );
				style_switcher_settings.skin = $(this).data( 'skin' );
				style_switcher_save();
				if ( $( 'head #skin-temp' ).length < 1 ) {
					$( 'head' ).append( '<link id="skin-temp" rel="stylesheet" type="text/css" href="library/css/skin/' + $(this).data( 'skin' ) + '.css">' );
				}
				else {
					$( '#skin-temp' ).attr( 'href', 'library/css/skin/' + $(this).data( 'skin' ) + '.css' );
				}

			});

			// SET SWITCHES
			switches.click(function(){

				var self = $(this),
				ico = self.find( '.ico' );
				self.toggleClass( 'active' );
				ico.toggleClass( 'fa-check-square-o fa-square-o' );

				// FIXED HEADER
				if ( self.hasClass( 'switch-fixed-header' ) ) {
					$( 'body' ).toggleClass( 'enable-fixed-header' );
					style_switcher_settings.fixed_header = self.hasClass( 'active' ) ? true : false;
				}

				// BODY PATTERN PARALLAX
				if ( self.hasClass( 'switch-boxed-layout' ) ) {
					$( 'body' ).toggleClass( 'enable-boxed-layout' );
					style_switcher_settings.boxed_layout = self.hasClass( 'active' ) ? true : false;
					$( '.owl-carousel' ).each(function(){
						$(this).data( 'owlCarousel' ).reinit();
					});
				}

				style_switcher_save();

			});

		});

	}
    // STYLE SWITCHER END

	


/* END. */
});
})(jQuery);