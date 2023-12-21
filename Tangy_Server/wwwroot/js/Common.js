window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Successful", { timeOut: 2000 });
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed", { timeOut: 2000 });
    }
}