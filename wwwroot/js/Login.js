function pageRedirect() {

    if ($("#txtUsername").val() == "admin" && $("#txtPassword").val() == "password") {
        window.location.replace("https://localhost:7085/Supplier/Index");
    }
    else {
        $("#txtUsername").val('');
        $("#txtPassword").val('');
        alert("Please enter correct credentials");
    }
};
$(document).ready(function () {
    $("#btnLogin").click(function () {
        pageRedirect();
    });
});

$(document).ready(function () {
    $("#btnCancel").click(function () {
        $("#txtUsername").val('');
        $("#txtPassword").val('');
    });
});