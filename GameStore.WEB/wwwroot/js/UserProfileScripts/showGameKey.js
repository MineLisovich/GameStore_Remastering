var currentDivId = "";
function ShowGameKey() {

    $('.js-showKeyBtn').off('click').on('click', function () {
        var gameKeyId = $(this).attr("data-gameKeyId");
        currentDivId = "#showKey_" + gameKeyId;
        AjaxActionShowGameKey(gameKeyId, currentDivId);
 
    });
}

function AjaxActionShowGameKey(gameKeyId, currentDivId) {
    var url = GetUrlForShowGameGey();
    console.log(url);
    console.log(gameKeyId);
    console.log(currentDivId);
    $.ajax({
        cache: false,
        type: "GET",
        url: url,
        data: { gameKeyId: gameKeyId },
        dataType: "json",
        success: function (data) {
            console.log(data)
            $(currentDivId).empty().text(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Ошибка !!!');
        }
    });
}