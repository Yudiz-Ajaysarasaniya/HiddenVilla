window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "operation successfull", { timeOut: 10000 });
    }
    if (type === "error") {
        toastr.error(message, "operation failed", { timeOut: 10000 });
    }
}
