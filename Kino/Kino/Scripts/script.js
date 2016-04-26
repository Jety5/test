﻿/*$(function () {
    $('.navbar-toggle').click(function () {
        $('.navbar-nav').toggleClass('slide-in');
        $('.side-body').toggleClass('body-slide-in');
        $('#search').removeClass('in').addClass('collapse').slideUp(200);

        /// uncomment code for absolute positioning tweek see top comment in css
        //$('.absolute-wrapper').toggleClass('slide-in');

    });

    // Remove menu for searching
    $('#search-trigger').click(function () {
        $('.navbar-nav').removeClass('slide-in');
        $('.side-body').removeClass('body-slide-in');

        /// uncomment code for absolute positioning tweek see top comment in css
        //$('.absolute-wrapper').removeClass('slide-in');

    });
});*/





$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});
$("#menu-toggle-2").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled-2");
    $('#menu ul').hide();
});

function initMenu() {
    $('#menu ul').hide();
    $('#menu ul').children('.current').parent().show();
    //$('#menu ul:first').show();
    $('#menu li a').click(
      function () {
          var checkElement = $(this).next();
          if ((checkElement.is('ul')) && (checkElement.is(':visible'))) {
              return false;
          }
          if ((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
              $('#menu ul:visible').slideUp('normal');
              checkElement.slideDown('normal');
              return false;
          }
      }
      );
}
$(document).ready(function () { initMenu(); });


function show() {
    $.ajax({
        url: "Index"
    }).done(function (data) {
        $('#page-content-wrapper').html(data);

    });
}

$("#btn-minus").click(function () {

    // Get the modal
    var modal = document.getElementById('minusModal');

    // Get the button that opens the modal
    //var btn = document.getElementById("btn-minus");


    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks the button, open the modal 
    //btn.onclick = function () {
    modal.style.display = "block";
    //}

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
    }

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }

});

$("#btn-plus").click(function () {

    // Get the modal
    var modal_plus = document.getElementById('plusModal');




    // Get the <span> element that closes the modal
    var span_plus = document.getElementsByClassName("close_plus")[0];

    modal_plus.style.display = "block";


    // When the user clicks on <span> (x), close the modal
    span_plus.onclick = function () {
        modal_plus.style.display = "none";
    }

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal_plus) {
            modal_plus.style.display = "none";
        }
    }
});

function deleteCinema() {
    //$(".col-md-4 text-center Cinema").html();
    idCinema = $("#deleteCinema").val();

    $.ajax({
        url: "/Cinema/deleteCinemaById?idCinema=" + idCinema
    }).done(function (data) {

        $("#cinemaList").html(data);

    });
    //$('#Cinema').empty();
}

//getMovieList
$( "#Movies" ).click(function() {

    $.ajax({
        url: "../Movie/Index"
    }).done(function (data) {
        $('#page-content-wrapper').html(data);

    });
   
});
   
//getRezerwationList
$("#rezerwacja").click(function () {

    $.ajax({
        url: "../Reservation/Index"
    }).done(function (data) {
        $('#page-content-wrapper').html(data);

    });

});


//getRepertoryList
$("#Repertory").click(function () {

    $.ajax({
        url: "../Repertory/Index"
    }).done(function (data) {
        $('#page-content-wrapper').html(data);

    });

});

//getDetailsCinema
function getDetailsMovie(id) {
    //  var id = $('#detailMovie').val();
    $.ajax({
        url: "../Movie/Details?id=" + id
    }).done(function (data) {
        $('#detailModal .detailCinema').html(data);

    });

   // $('#detailModal title').html("Szczegóły filmu");
    $("#detailModal").dialog({
             title: 'Szczegóły filmu'
    });

}

function editReservation(id) {
    // $('#title').html("Edytuj film");
    //  var id = $('#detailMovie').val();
    $.ajax({
        url: "../Reservation/Edit?id=" + id
    }).done(function (data) {
        $('#editReservationModal .editResevation').html(data);

    });


    $("#editReservationModal").dialog({     
        title: 'Edytuj rezerwację'
    });



}
function detailsReservation(id) {
    // $('#title').html("Edytuj film");
    //  var id = $('#detailMovie').val();
    $.ajax({
        url: "../Reservation/Details?id=" + id
    }).done(function (data) {
        $('#detailsReservationModal .detailsResevation').html(data);

    });


    $("#detailsReservationModal").dialog({      
        title: 'Szczegoły rezerwacji'
    });



}

function editMovie(id) {
   // $('#title').html("Edytuj film");
    //  var id = $('#detailMovie').val();
    $.ajax({
        url: "../Movie/Edit?id=" + id
    }).done(function (data) {
        $('#editModal .detailCinema').html(data);

    });
   
    
    $("#editModal").dialog({
        height:340,
        width: 430,
        title: 'Edytuj film'
    });


   
}
function addMovie() {
    // $('#title').html("Edytuj film");
    //  var id = $('#detailMovie').val();
    $.ajax({
        url: "../Movie/Create"
    }).done(function (data) {
        $('#addModal .detailCinema').html(data);

    });


    $("#addModal").dialog({
        height: 340,
        width: 430,
        title: 'Dodaj film'
    });



}


function deleteMovie(id) {
    //$('#title').html("Szczegóły filmu");
    //  var id = $('#detailMovie').val();
    $.ajax({
        url: "../Movie/Delete?id=" + id
    }).done(function (data) {
        $('#deleteModal .detailCinema').html(data);
        
    });

    
    $("#deleteModal").dialog({
        title: 'Usun film'
    });

}







function deleteMovieForm(id) {

    $.ajax({
        url: "../Movie/DeleteConfirmed?id=" + id,
    }).done(function (data) {
        showFilms();
      //  $(".ui-dialog").hide();
       // $('#page-content-wrapper').html(data);
     
    });

   
   
}

//edytuj film
$("#editMovieButton").click('submit', function (e) {

        e.preventDefault();
        $.ajax({
            type: "POST",
            url: "../Movie/Edit",
            data: $("#formEditMovie").serialize(),
            success: function (data) {
                // data is ur summary
                //alert(data);
                showFilms();
                //debugger;
            }


        });

    });


//edytuj rezerwacje

$("#editReservationButton").click('submit', function (e) {

    e.preventDefault();
    $.ajax({
        type: "POST",
        url: "../Reservation/Edit",
        data: $("#formEditReservation").serialize(),
        success: function (data) {
            // data is ur summary
            //alert(data);
            showReservation();
            //debugger;
        }


    });

});


//dodajFilm

$("#addMovieButton").click('submit', function (e) {

        e.preventDefault();  
        $.ajax({
            type: "POST",
            url: "../Movie/Create",
            data: $("#formAddMovie").serialize(),            
            success: function (data) {
                // data is ur summary
                //alert(data);
                showFilms();
                //debugger;
            }
           

        });
        
    });


function showFilms(){
    $.ajax({
        //    type: "POST",
        url: "../Movie/Index",
        // data: dataString,
        //   dataType: "json",
        //   traditional: true,
    }).done(function (data) {
        // console.log(data);
        //
        $(".ui-dialog").hide();
        $('#page-content-wrapper').html(data);
        //$(".ui-dialog").hide();

    });

}

function showReservation() {
    $.ajax({
        //    type: "POST",
        url: "../Reservation/Index",
        // data: dataString,
        //   dataType: "json",
        //   traditional: true,
    }).done(function (data) {
        // console.log(data);
        //
        $(".ui-dialog").hide();
        $('#page-content-wrapper').html(data);
        //$(".ui-dialog").hide();

    });

}

function addReservation() {
    // $('#title').html("Edytuj film");
    //  var id = $('#detailMovie').val();
    $.ajax({
        url: "../Reservation/Create",
    }).done(function (data) {
        $('#addModal .addReservation').html(data);

    });


    $("#addModal").dialog({
        height: 400,
        width: 430,
        title: 'Dodaj rezerwację'
    });



}

$('.datepicker').datepicker();
/*/
$('#formAddMovie').submit(function (evt) {
    //prevent the browsers default function
    evt.preventDefault();
    //grab the form and wrap it with jQuery
    dataString = " ";
    

    //send your ajax request   
    $.ajax({
        type: "POST",
        url: "../Movie/Create",
        data: dataString,
        dataType: "json",  
    }).done(function (data) {
        //  console.log(data);
        //$('#page-content-wrapper').html(data);

    });

    $.ajax({
        //    type: "POST",
        url: "../Movie/Index",
        // data: dataString,
        //   dataType: "json",
        //   traditional: true,
    }).done(function (data) {
        // console.log(data);
        //
        $('#page-content-wrapper').html(data);
        //$(".ui-dialog").hide();

    });

    return false;


});*/
/*function deleteConfirm(id) {

    $.ajax({
        url: "../Movie/Delete",
        method: "POST",
        data: { id: id }
    }).done(function (data) {
        $('#page-content-wrapper').html(data);

    });
}



/*function getMenu(CinemaId) {
    $.ajax({
        url: "../Cinema/Main?CinemaId=" + CinemaId
    }).done(function (data) {
        $('#cinemaList').html(data);

    });
}*/
  /*  var modal = document.getElementById('detailModal');

    // Get the button that opens the modal
    var btn = document.getElementById("detailMovie");


    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks the button, open the modal 
    btn.onclick = function () {
        modal.style.display = "block";
    }

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
    }

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
}*/





function addCinema() {
    //$(".col-md-4 text-center Cinema").html();
    cinemaName = $("#cinemaName").val();

    cinemaAdress = $("#cinemaAdress").val();
    if (cinemaAdress != "" || cinemaName != "") {

        $.ajax({
            url: "/Cinema/addCinemaById?cinemaName=" + cinemaName + "&cinemaAdress=" + cinemaAdress
        }).done(function (data) {

            $("#cinemaList").html(data);

        });
    }
    //$('#Cinema').empty();
}
