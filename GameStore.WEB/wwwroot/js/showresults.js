//Toast - Результат действия пользователя
function ShowModalLastAction() {
    //1) получаем результаты последнего действия
    var actionId = $("#actionId").val(); //тип действия (actionId соответствует предопределенным значениям UserActionTypes в папке ServiceVariablesClasses NRVK.Web)
    if (actionId == 0) return; //если не было действий с пользователями, завершаем функцию без вызова модальных окон
    var actionResult = $("#actionResult").is(':checked'); // успешно / не успешно
    var actionDopInfo = $("#actionDopInfo").val(); // доп информация (string) - отправлено ли почтовое сообщение, сколько удалено и пр.
    // 2) определяем содержимое модального окна
    var message = "";
    switch (actionId) {
        case "1":
            message = (actionResult) ? ("Запись успешно создана. " + actionDopInfo) : ("Запись не была создана. " + actionDopInfo);
            break;
        case "2":
            message = (actionResult) ? ("Данные успешно изменены. " + actionDopInfo) : ("Данные не были изменены. " + actionDopInfo);
            break;
        case "3":
            message = (actionResult) ? ("Запись успешно удалена. " + actionDopInfo) : ("Запись не была удалена. " + actionDopInfo);
            break;
        case "4":
            message = (actionResult) ? ("Оповещение успешно отправлено. " + actionDopInfo) : ("Опевещение не было отправлено. " + actionDopInfo);
            break;
        case "5":
            message = (actionResult) ? ("Пароль сохранён. " + actionDopInfo) : ("Не удалось сохранить пароль. " + actionDopInfo);
            break;
        case "6":
            message = (actionResult) ? ("Подтверждение Eamil. " + actionDopInfo) : ("Не удалось подтвердить Email. " + actionDopInfo);
            break;
        case "7":
            message = (actionResult) ? ("Email отвязан. " + actionDopInfo) : ("Не удалось отвязать Email. " + actionDopInfo);
            break;
        case "8":
            message = (actionResult) ? ("2FA включена. " + actionDopInfo) : ("Не удалось включить 2FA. " + actionDopInfo);
            break;
        case "9":
            message = (actionResult) ? ("2FA выключена. " + actionDopInfo) : ("Не удалось выключить 2FA. " + actionDopInfo);
            break;
    }
    // 3) выбираем toast или  modal и заполняем данными
    var toast = (actionResult == true) ? $("#modalActionSuccess") : $("#modalActionError");
    $(toast).find(".modal-message-js").first().empty().text(message);
    $(toast).toast('show');
}