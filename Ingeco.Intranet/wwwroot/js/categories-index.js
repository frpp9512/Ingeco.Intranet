var id = "";

function RemoveCategory(id) {
    this.id = id;
    var row = document.getElementById(id);
    var name = row.children[0].innerHTML;
    console.log(id, name);
    RiseDeleteModal(name);
}

function RiseDeleteModal(categoryName) {
    var modalContent = document.getElementById('modal-delete-content');
    modalContent.innerHTML = "¿Esta seguro que desea eliminar la categoría '" + categoryName + "'?";

    var btn = document.getElementById('modal-delete-btn-primary');
    btn.onclick = SendDeleteRequest;

    ShowDeleteModalFooter();
    $("#deleteCategoryModal").modal();
}

function SendDeleteRequest() {
    HideDeleteModalFooter();
    $.ajax({
        url: "/categories/delete",
        method: "DELETE",
        data: {
            id: id
        },
        success: function (response) {
            console.log(response);
            var row = document.getElementById(id);
            row.remove();
            $("#deleteCategoryModal").modal("hide");
            SubstractCategoriesTotal();
            ShowAlert("Eliminar categoría", response, false);
        },
        error: function (response) {
            console.log(response);
            $("#deleteCategoryModal").modal("hide");
            ShowAlert("Eliminar categoría", "Error eliminando la categoría. Mensaje de error: " + response.responseText, true);
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

var alertTemplate = "<div class='alert alert-{alert-type} alert-dismissible fade show' role='alert'>"
    + "<div id = 'alert-content'>"
    + "<strong>{title}</strong> {body}"
    + "</div>"
    + "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>"
    + "<span aria-hidden='true'>&times;</span>"
    + "</button>"
    + "</div>";

function ShowAlert(title, text, isError) {
    var alertContainer = document.getElementById("alert-container");
    alertBody = alertTemplate.replace("{title}", title).replace("{body}", text).replace("{alert-type}", isError ? "danger" : "info");
    alertContainer.innerHTML = alertBody;
}

function SubstractCategoriesTotal() {
    var catTotalSpan = document.getElementById("categoriesTotal");
    var total = parseInt(catTotalSpan.innerHTML);
    total = total - 1;
    catTotalSpan.innerHTML = total;
}