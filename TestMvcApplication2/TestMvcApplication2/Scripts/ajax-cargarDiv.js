function cargarDivNuevoUsuario() {
    $.ajax({
        url: "/Home/NuevoUsuario",
        type: "POST",
        async: true,
        dataType: "html",
        success: function (result) {
            $("#contenidoDinamico").html(result);
        }
    });
}

function cargarDivConsultarDetalle() {
    $.ajax({
        url: "/Home/MostrarDatosCompletos",
        type: "POST",
        async: true,
        dataType: "html",
        success: function (result) {
            $("#contenidoDinamico").html(result);
        }
    });
}

function cargarDivDetallesUsuario(id) {
    $.ajax({
        url: "/Home/DetallesUsuario/" + id,
        type: "POST",
        async: true,
        dataType: "html",
        success: function (result) {
            $("#contenidoDetalle").html(result);
        }
    });
}

function cargarDivInfoUsuarioModif(id) {
    $.ajax({
        url: "/Abm/CargarInfoUsuario/" + id,
        type: "POST",
        async: true,
        dataType: "html",
        success: function (result) {
            $("#contenidoDinamico").html(result);
        }
    });
}

// ------- PRUEBAS ------------------------------------------------------------------

//$(document).ready(function () {
//    $("#nuevoUsuarioOpcion").click(function () {
//        $("#contenidoDinamico").load("/Home/NuevoUsuario");
//    });

//    $("#consultarDetalleOpcion").click(function () {
//        $("#contenidoDinamico").load("/Home/MostrarDatosCompletos");
//    });
//});

//--- Ajax sin jQuery
//    var xmlhttp;
//    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
//        xmlhttp = new XMLHttpRequest();
//    }
//    else {// code for IE6, IE5
//        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
//    }
//    xmlhttp.onreadystatechange = function () {
//        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
//            document.getElementById("contenidoDinamico").innerHTML = xmlhttp.responseText;
//        }
//    }
//    xmlhttp.open("POST", "/Home/MostrarDatosCompletos", true);
//    xmlhttp.send();
//--- Prueba con jQuery Ajax