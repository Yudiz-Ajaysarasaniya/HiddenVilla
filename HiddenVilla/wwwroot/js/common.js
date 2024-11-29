window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "operation successfull", { timeOut:10000 });
    }
    if (type === "error") {
        toastr.error(message, "operation failed", { timeOut: 10000 });
    }
}

/*window.ShowSwal(type,message) {
    if (type === "success") {
        Swal.fire(
            'Successfull Notification',
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
}*/



function ShowDeleteConfirmation()
{
    $('#deleteConfirmationModal').modal('show');
}

/*
*//*window.HideDeleteConfirmation = function () {
    $('#deleteConfirmationModalz').modal('hide');
}*/

function HideDeleteConfirmation(){
    $('#deleteConfirmationModal').model('hide');
}


