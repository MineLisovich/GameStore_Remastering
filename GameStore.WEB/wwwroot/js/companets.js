function InitializationMultiSelect2(conteiner) {
    $('.' + conteiner).select2({
        closeOnSelect: false,
    });
}

function InitializationSelect2(conteiner) {
    $('.' + conteiner).select2({
        closeOnSelect: true,
    });
}

function ToggleLoadingView(isLoading) {
    if (isLoading == true) {
        $("#js-loading").addClass("loader");
    }
    else {
        $("#js-loading").removeClass("loader").addClass("loaded");
    }
}