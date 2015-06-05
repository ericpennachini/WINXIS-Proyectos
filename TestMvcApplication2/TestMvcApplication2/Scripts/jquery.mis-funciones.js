function habilitarInput() {
    $("#check1").click(function () {
        if ($("#telefono1").attr("disabled") == "disabled") {
            $("#telefono1").removeAttr("disabled");
        }
    });
}

// $().removeAttr("disabled");