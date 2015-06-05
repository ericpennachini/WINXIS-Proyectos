//Esta funcion valida que los campos del alta de usuario no esten vacíos

function validarCampos() {
    var campos = [];
    campos = [document.forms["formAltaUsuario"]["dni"].value ,
               document.forms["formAltaUsuario"]["nombre"].value,
               document.forms["formAltaUsuario"]["apellido"].value,
               document.forms["formAltaUsuario"]["fecha_nacimiento"].value,
               document.forms["formAltaUsuario"]["domicilio"].value,
               document.forms["formAltaUsuario"]["localidad"].value, 
               ];
    if (campos[0] == "" || campos[1] == "" || campos[2] == "" || campos[3] == "" || campos[4] == "" || campos[5] == "") {
        window.alert("Hay campos vacíos! Intente de nuevo.");
        return false;
    }
}