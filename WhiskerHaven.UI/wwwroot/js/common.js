﻿window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Correct operation", { timeOut: 10000 });
    }
    if (type === "error") {
        toastr.error(message, "Failed Operation", { timeOut: 10000 });
    }
}

window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire(
            'Success Notification',
            message,
            'success'
        );
    }
    if (type === "error") {
        Swal.fire(
            'Error Notification',
            message,
            'error'
        );
    }
}

function ShowModalConfirmDelete() {
    $('#modalConfirmDelete').modal('show');
}

function HideModalConfirmDelete() {
    $('#modalConfirmDelete').modal('hide');
}