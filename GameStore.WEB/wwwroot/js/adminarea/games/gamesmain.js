function GameManagerBTNControll() {

    //Поиск игры по наименованию
    $("#searchGame").on("input", function () {
        var inpdata = $("#searchGame").val();
        AjaxActionGetGamesData(inpdata);
    });

    //Обработчик нажатия на кнопки
    $('#js-gemes').off('click').on('click', '.js-gameBTN', function () {

        var gameId = $(this).attr('data-GameId');
        var action = $(this).attr('data-action');
        var message = GetTextForModalWarning(action);

        //LOGS
        console.log("GAME MANAGER BTN - GameId: " + gameId);
        console.log("GAME MANAGER BTN - action: " + action);
        console.log("GAME MANAGER BTN - message: " + message);

        ///

        PrepareWarningModal(gameId, "", message, action, "");
        $("#modalActionWarning").modal('show');
    });
}

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
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Ошибка загрузки данных!!!');
        }
    });
}

function GetTextForModalWarning(action) {
    var message = "";

    switch (action) {
        case "edit":
            message = "Вы точно хотите войти в режим быстрого редактирования?";
            break;
        case "delete":
            message = "Вы уверены, что хотите удалить игру?";
            break;
        case "hardDelete":
            message = "Вы уверены, что хотите полностью удалить игру из базы данных?";
            break;
    }

    return message;
}

function SettingsSharePriceInput() {
    $('#Game_IsShare').on('change', function () {
        if ($(this).is(':checked')) {
            $('#Game_SharePrice').removeAttr('disabled');
        }
        else {
            $('#Game_SharePrice').attr('disabled', 'disabled');
            $('#Game_SharePrice').val(null);
        }
    });
}

function SettingsGameKeyInput() {

    // editData == $("#fastEditGameDataForm").serialize()
    tempSaveFormData = $("#fastEditGameDataForm").serialize();

    $('#Game_UploadGameKeys').on('change', function () {
        if ($(this).val()) {
            editData = "";
        }
        else
        {
            editData = tempSaveFormData;
        }
    });
}