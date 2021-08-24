var id = "";
var action = "";
var userFullName = "";
var modalActivationContentTemplate = "¿Está seguro que desea {action} al usuario {userName}?";
var modalDeleteContentTemplate = "¿Está seguro que desea eleminiar al usuario {fullName}?";

function GetUserRow() {
    return document.getElementById(id);
}

function GetUserFullName() {
    return GetUserRow().children[1].innerHTML;
}

function GetButtonsCell() {
    return GetUserRow().children[5];
}

function GetStatusCell() {
    return GetUserRow().children[4];
}

function GetActivationModBtn() {
    return GetButtonsCell().children[0];
}

function GetStatusSpan() {
    return GetStatusCell().children[0];
}

function GetActivationModalPrimeBtn() {
    return document.getElementById("modal-activation-btn-primary");
}

function RiseActivationModal(action, userId) {
    id = userId;
    this.action = action;
    // Get user full name
    var row = GetUserRow();
    userFullName = row.children[1].innerHTML.toString();

    // Update modal title
    var modalTitle = document.getElementById("modal-activation-title");
    modalTitle.innerHTML = action === "activate" ? "Activar usuario" : "Desactivar usuario";

    // Update modal content text
    var modalContent = document.getElementById("modal-activation-content");
    modalText = modalActivationContentTemplate.replace("{action}", action == "activation" ? "activar" : "desactivar")
        .replace("{userName}", userFullName);
    modalContent.innerHTML = modalText;

    var modalPrimeBtn = GetActivationModalPrimeBtn();
    modalPrimeBtn.onclick = SendActivationModRequest;

    ShowActivationModalFooter();
    $("#modifyUserActivationModal").modal();
}

function ShowActivationModalFooter() {
    SetActivationModalFooterVisibility("flex");
}

function HideActivationModelFooter() {
    SetActivationModalFooterVisibility("none");
}

function SetActivationModalFooterVisibility(visibility) {
    var modalFooter = document.getElementById("modal-activation-footer");
    modalFooter.style.display = visibility;
}

function SendActivationModRequest() {
    HideActivationModelFooter();
    var url = "/accounts/" + (action === "activate" ? "activate" : "deactivate");
    $.ajax({
        url: url,
        method: "POST",
        data: {
            id: id
        },
        success: function (response) {
            console.log(response);
            $("#modifyUserActivationModal").modal("hide");
            var statusSpan = GetStatusSpan();
            statusSpan.classList.toggle("fa-ban");
            statusSpan.classList.toggle("text-danger");
            statusSpan.classList.toggle("text-success");
            statusSpan.classList.toggle("fa-check");
            var statusBtn = GetActivationModBtn();
            statusBtn.children[0].classList.toggle("fa-user-slash");
            statusBtn.children[0].classList.toggle("fa-user-check");
            statusBtn.classList.toggle("btn-outline-success");
            statusBtn.classList.toggle("btn-outline-warning");
            statusBtn.onclick = function () {
                RiseActivationModal(action === "activate" ? "deactivate" : "activate", id);
            };
            statusBtn.setAttribute("title", action === "activate" ? "Desactivar usuario" : "Activar usuario");
            ShowAlert(action === "activate" ? "Desactivar usuario" : "Activar usuario", response);
        },
        error: function (response) {
            console.log(response);
            alert("Error en la operación: " + response);
        }
    });
}

function RiseDeleteModal(userId) {
    id = userId;
    var fullName = GetUserFullName();

    var modalContent = document.getElementById("modal-delete-content");
    modalText = modalDeleteContentTemplate.replace("{fullName}", fullName);
    modalContent.innerHTML = modalText;

    var btn = document.getElementById("modal-delete-btn-primary");
    btn.onclick = SendDeleteUserRequest;
    ShowDeleteModalFooter();
    $("#deleteUserModal").modal();
}

function SendDeleteUserRequest() {
    HideDeleteModalFooter();
    $.ajax({
        url: "/accounts/delete",
        method: "DELETE",
        data: {
            id: id
        },
        success: function (response) {
            console.log(response);
            GetUserRow().remove();
            $("#deleteUserModal").modal("hide");
            ShowAlert("Eliminar usuario", response);
        }
    });
}

function ShowDeleteModalFooter() {
    SetDeleteModalFooterVisibility("flex");
}

function HideDeleteModalFooter() {
    SetDeleteModalFooterVisibility("none");
}

function SetDeleteModalFooterVisibility(visibility) {
    var modalFooter = document.getElementById("modal-delete-footer");
    modalFooter.style.display = visibility;
}

var alertTemplate = "<div class='alert alert-info alert-dismissible fade show' role='alert'>"
                        + "<div id = 'alert-content'>"
                        +    "<strong>{title}</strong> {body}"
                        + "</div>"
                        + "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>"
                        +     "<span aria-hidden='true'>&times;</span>"
                        + "</button>"
                        + "</div>";

function ShowAlert(title, text) {
    var alertContainer = document.getElementById("alert-container");
    alertBody = alertTemplate.replace("{title}", title).replace("{body}", text);
    alertContainer.innerHTML = alertBody;
}