var getParentHtmlName;
(function() {
    "use strict";

    // custom scrollbar

    $("html").niceScroll({styler:"fb",cursorcolor:" #2042ae", cursorwidth: '6', cursorborderradius: '0px', background: '#dbbb40 ', spacebarenabled:false, cursorborder: '0',  zindex: '1000'});

    $(".left-side").niceScroll({styler:"fb",cursorcolor:" #2042ae", cursorwidth: '3', cursorborderradius: '0px', background: '#dbbb40 ', spacebarenabled:false, cursorborder: '0'});


    $(".left-side").getNiceScroll();
    if ($('body').hasClass('left-side-collapsed')) {
        $(".left-side").getNiceScroll().hide();
    }

    // Added By Hitesh to style up the srcoll bar of adv-table div
    $('.nice-scroll-grid').niceScroll({ styler: "fb", cursorcolor: " #2042ae", cursorwidth: '6', cursorborderradius: '0px', background: '#dbbb40 ', spacebarenabled: false, cursorborder: '0', zindex: '1000' });

    // Added By Hvb to Open Collapsed panel
    $('.pnl-opener').each(function () {
        var openText = $(this).data('open-on');
        if ($(this).text().trim() == openText) {
            if ($(this).data("open-panels").indexOf(",") > -1) {
                var splitedIds = $(this).data("open-panels").split(",");
                for (var iPanelCount = 0; iPanelCount < splitedIds.length; iPanelCount++) {
                    openPanel(splitedIds[iPanelCount]);
                }
            }
            else {
                openPanel($(this).data("open-panels"));
            }
        }
    });

    function openPanel(panelId) {
        var pnl = $('#' + panelId);
        pnl.find('.pull-right').find('a').removeClass("fa-chevron-down").addClass("fa-chevron-up");
        if (pnl.find('.pull-right').find('.collapsible-server-hidden').children().length > 0)
            pnl.find('.pull-right').find('.collapsible-server-hidden').children().val("o");
        pnl.find('.panel-body').slideDown(200);
    }


    //To get name of page currently open
    getParentHtmlName = function() {
        return window.location.pathname.split("/").pop();
    }


    // Toggle Left Menu
   jQuery('.menu-list > a').click(function() {
      
      var parent = jQuery(this).parent();
      var sub = parent.find('> ul');
      
      if(!jQuery('body').hasClass('left-side-collapsed')) {
         if(sub.is(':visible')) {
            sub.slideUp(200, function(){
               parent.removeClass('nav-active');
               jQuery('.main-content').css({height: ''});
               mainContentHeightAdjust();
            });
         } else {
            visibleSubMenuClose();
            parent.addClass('nav-active');
            sub.slideDown(200, function(){
                mainContentHeightAdjust();
            });
         }
      }
      return false;
   });

   function visibleSubMenuClose() {
      jQuery('.menu-list').each(function() {
         var t = jQuery(this);
         if(t.hasClass('nav-active')) {
            t.find('> ul').slideUp(200, function(){
               t.removeClass('nav-active');
            });
         }
      });
   }

   function mainContentHeightAdjust() {
      // Adjust main content height
      var docHeight = jQuery(document).height();
      if(docHeight > jQuery('.main-content').height())
         jQuery('.main-content').height(docHeight);
   }

   //  class add mouse hover
   jQuery('.custom-nav > li').hover(function(){
      jQuery(this).addClass('nav-hover');
   }, function(){
      jQuery(this).removeClass('nav-hover');
   });


   // Menu Toggle
   jQuery('.toggle-btn').click(function(){
       $(".left-side").getNiceScroll().hide();
       
       if ($('body').hasClass('left-side-collapsed')) {
           $(".left-side").getNiceScroll().hide();
       }
      var body = jQuery('body');
      var bodyposition = body.css('position');

      if(bodyposition != 'relative') {

         if(!body.hasClass('left-side-collapsed')) {
            body.addClass('left-side-collapsed');
            jQuery('.custom-nav ul').attr('style','');

            jQuery(this).addClass('menu-collapsed');

         } else {
            body.removeClass('left-side-collapsed chat-view');
            jQuery('.custom-nav li.active ul').css({display: 'block'});

            jQuery(this).removeClass('menu-collapsed');

         }
      } else {

         if(body.hasClass('left-side-show'))
            body.removeClass('left-side-show');
         else
            body.addClass('left-side-show');

         mainContentHeightAdjust();
      }

   });
   

   searchform_reposition();

   jQuery(window).resize(function(){

      if(jQuery('body').css('position') == 'relative') {

         jQuery('body').removeClass('left-side-collapsed');

      } else {

         jQuery('body').css({left: '', marginRight: ''});
      }

      searchform_reposition();

   });

   function searchform_reposition() {
      if(jQuery('.searchform').css('position') == 'relative') {
         jQuery('.searchform').insertBefore('.left-side-inner .logged-user');
      } else {
         jQuery('.searchform').insertBefore('.menu-right');
      }
   }

   
    // Sets panel to it's previous status on server postback.
   $('.collapsible-server-hidden').each(function () {
       // parent is span
       // first child is scroller button

       var panelBodyJQObject = $(this.parentNode.parentNode.parentNode.children[1]);
       var sliderButtonJQObject = $(this.parentNode.children[0]);

       if (this.children[0].value == "c")
       {
           // Close the panel
           panelBodyJQObject.addClass("collapse");
           sliderButtonJQObject.removeClass("fa-chevron-up").addClass("fa-chevron-down");
       }
       else if (this.children[0].value == "o")
       {
           // Open the panel
           panelBodyJQObject.removeClass("collapse");
           sliderButtonJQObject.removeClass("fa-chevron-down").addClass("fa-chevron-up");
       }

   });

    // panel collapsible
   $('.panel .tools .fa').click(function () {
       // Modified By Hitesh to have panel inside tab
       // Original function
        //var el = $(this).parents(".panel").children(".panel-body");
        //if ($(this).hasClass("fa-chevron-down")) {
        //    $(this).removeClass("fa-chevron-down").addClass("fa-chevron-up");
        //    el.slideUp(200);
        //} else {
        //    $(this).removeClass("fa-chevron-up").addClass("fa-chevron-down");
       //    el.slideDown(200); }
       var el;

       if (this.parentNode.parentNode.parentNode.children.length > 1) {
           el = this.parentNode.parentNode.parentNode.children[1];
           /* 
              parentNode1 = Span 
              parentNode2 = Header
              parentNode3 = Panel
              childern[1] = panel body
           */
           el = $(el); // cast el to jQuery Object
       }
       else
           el = $(this).parents(".panel").children(".panel-body");

       if ($(this).hasClass("fa-chevron-up")) {
               $(this).removeClass("fa-chevron-up").addClass("fa-chevron-down");
               el.slideUp(200);
               //set hidden field to closed status
               if (this.parentNode.children.length == 2)
                   if (this.parentNode.children[1].children.length == 1)
                       this.parentNode.children[1].children[0].value = "c";
           } else {
               $(this).removeClass("fa-chevron-down").addClass("fa-chevron-up");
               el.slideDown(200);
              //set hidden field to open status
              if (this.parentNode.children.length == 2)
                  if (this.parentNode.children[1].children.length == 1)
                      this.parentNode.children[1].children[0].value = "o";
           }
    });

    $('.todo-check label').click(function () {
        $(this).parents('li').children('.todo-title').toggleClass('line-through');
    });

    $(document).on('click', '.todo-remove', function () {
        $(this).closest("li").remove();
        return false;
    });

    $("#sortable-todo").sortable();


    // panel close
    $('.panel .tools .fa-times').click(function () {
        $(this).parents(".panel").parent().remove();
    });


    // tool tips
    $('.tooltips').tooltip();

    // popovers
    $('.popovers').popover();



})(jQuery);