//Вывод игры при загрузке страницы + если был использован поиск
function AjaxActionGetGamesData(nameGame) {
    var url = GetUrlForLoadGameData();
    //отправляем запрос
    $.ajax({
        cache: false,
        type: "GET",
        url: url,
        data: { nameGame: nameGame },
        dataType: "html",
        success: function (data) {
            $("#js-gemes").empty();
            $("#js-gemes").append(data);
            SetSettings();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Ошибка загрузки данных!!!');
        }
    });
}


function GameBTNControll() {

    //Поиск игры по наименованию
    $("#searchGame").on("input", function () {
        var inpdata = $("#searchGame").val();
        AjaxActionGetGamesData(inpdata);
    });
}