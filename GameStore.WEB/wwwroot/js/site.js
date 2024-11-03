//Tooltips
var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
});
//глобальная переменная, которая хранит название area, в которой используются функции этого файла
var moduleName;
//глобальная переменная, которая хранит данные формы сразу после загрузки модального окна CREATE & EDIT
var editData;


//Warning Modal
function PrepareWarningModal(keydata, title, message, actionType, section) {
    //1) очищаем информационные поля модалки
    $("#key").empty().text(keydata);
    $("#actionTitle").empty().text(title);
    $("#actionMessage").empty().text(message);
    $("#section").empty().text(section);
    //2) добавляем общие данные
    $("#btnGoToAction").data("key", keydata);
    $("#btnGoToAction").data("action", actionType);
    $("#btnGoToAction").data("section", section);

}

//возврат к несохраненным данным окна CREATE & EDIT
function ReturnToUnsavedData_FromModalDataNotSaved(modalId) {
    $('#' + modalId).modal('show');
}

//окончательный выход из окна CREATE & EDIT при несохраненных данных 
function Exit_FromModalDataNotSaved(modalId) {
    //обнуляем сохраненные на стороне клиента данные
    editData = "";
    //удаляем окно CREATE & EDIT
    $("#" + modalId).remove();
}

//вызов модалки CREATE & EDIT по нажатию на кнопку "Далее" в модалке Warning
function CallCreateEditModal_FromModalWarning() {
    var url, modal;
    var id = $(this).data("key");
    var actionType = $(this).data("action");
    var section = $(this).data("section");

    console.log("url: " + url);
    console.log("modal: " + modal);
    console.log("actionType: " + actionType);
    console.log("section: " + section);
    console.log("id: " + id);
    AjaxActionGetCreateEditModal(actionType, id, section);
}

//настройка тостов и модальных окон стандартных действий (lastAction, Warning, DataNotSaved, Error&Success)
function AddHandlerClickForButtonsOfActionModals(createEditModalId, areaName) {
    //инициализация глобальной переменной
    //(например, для модуля профиля пользователя - moduleName == "profile")
    moduleName = areaName;
    console.log("AddHandlerClickForButtonsOfActionModals- " + "createEditModalId: " + createEditModalId + " areaName: " + areaName)
    ////----------(1)------------MODAL DataNotSaved
    //1) возврат к несохраненным данным пользователя (действие из модалки DataNotSaved)
    $("#btnReturnToUnsavedData").on("click", () => ReturnToUnsavedData_FromModalDataNotSaved(createEditModalId));
    //2) очистка данных формы и удаление модального окна (действие из модалки DataNotSaved)
    $("#btnExitFromUnsavedData").on("click", () => Exit_FromModalDataNotSaved(createEditModalId));
    ////----------(2)------------MODAL Warning
    $("#btnGoToAction").on("click", CallCreateEditModal_FromModalWarning);
    ////----------(3)------------TOAST Success & MODAL Error
    ShowModalLastAction();
}

//выход из модалки CREATE & EDIT (если данные изменены - скрываем окно + выводим Warning, иначе - удаляем модалку)
function Exit_FromModalCreateEdit(createEditModalId, formId) {
    console.log("Exit_FromModalCreateEdit - " + "createEditModalId: " + createEditModalId);
    //если внесены изменения, отображаем предупреждение пользователю
    if (editData != $("#" + formId).serialize()) {
        $('#modalActionDataNotSaved').modal('show');
    }
    //иначе уничтожаем окно после его скрытия
    else {
        $("#" + createEditModalId).on('hidden.bs.modal', function (event) {
            $(this).remove();
        });
    }
}
//проверка наличия изменнеий в данных и возврат true (разрешения для submit отправки на сервер) либо false (если изменений не было - окно удаляется)
function CheckDataChanges_FromModalCreateEdit(createEditModalId, formId) {
    console.log("CheckDataChanges_FromModalCreateEdit");
    //если данные формы не изменены
    if (editData == $("#" + formId).serialize()) {
        //1) очистить стартовые данные
        editData = "";
        //2) добавляем обработчик: если окно скрыто - удали его (сразу вызвать удаление нельзя, фокус к странице не вернется)
        $("#" + createEditModalId).on('hidden.bs.modal', function (event) {
            $(this).remove();
        });
        //3) скрываем окно
        $('#' + createEditModalId).modal('hide');
        //прерываем submit возвратом false
        return false;
    }
    else {
        return true;
    }
}

//настройка отображения модального окна создания и редактирования (CREATE & EDIT)
function AddHandlerShownModalForCreateEditModal(createEditModalId, formId) {
    ////----------(5)------------MODAL CREATE & EDIT
    //проверка наличия несохраненных изменений
    //важно: обработчик вещаем на контейнер модальных окон (modalWrapper),
    //так как модалкa CreateEdit на момент создания страницы отсутствует
    console.log("AddHandlerShownModalForCreateEditModal - " + "createEditModalId: " + createEditModalId + " formId: " + formId);

    $('#modalWrapper').on('shown.bs.modal', function (event) {
        //если начата работа с данными пользователя (нового или уже существующего)
        if ($(".modal-js").attr("id") == createEditModalId) {
            //на кнопку "назад" => обработчик с проверкой были ли какие-то изменения данных

            $("#" + createEditModalId).find("#btnBackFromCreateEditMode").on("click", () => Exit_FromModalCreateEdit(createEditModalId, formId));
            //на отправку формы изменения / создания пользователя => обработчик с проверкой были ли какие-то изменения данных
            $("#" + formId).on("submit", () => CheckDataChanges_FromModalCreateEdit(createEditModalId, formId));
        }
    });
}

//Цель => получение data для метода AJAX
function GetData(actionType, id, section) {
    //данные для запросв по дефолту
    var data = { id: id };
    console.log("GetData - " + "moduleName: " + moduleName);
    console.log("GetData - " + "actionType: " + actionType);
    console.log("GetData - " + "id: " + id);
    console.log("GetData - " + "section: " + section);
    console.log("--------------");
    switch (moduleName) {
        case "profile":
            switch (actionType) {
                case "edit":
                    switch (section) {
                        case "userdata":
                            data = { userId: id };
                            break;
                        case "changePass":
                            data = { userId: id, isChangePassword: true };
                            break;
                    }
                    break;
                case "delete":
                    data = { userId: id };
                    break;
            }
            break;
        case "userManager":
            switch (actionType) {
                case "edit":
                    data = { userId: id };
                    break;
                case "delete":
                    data = { userId: id };
                    break;
            }
            break;
        case "dictionaries":
            switch (actionType) {
                case "edit":
                    data = { id: id, sectionName: section };
                    break;
                case "delete":
                    data = { id: id, sectionName: section };
                    break;
            }
            break;
  
    }
    return data;
}




//Цель => вызов модального окна CREATE & EDIT
function AjaxActionGetCreateEditModal(actionType, id, section) {

    var url = GetURLForAjaxByActionType(actionType);
    console.log("AjaxActionGetCreateEditModal - " + "url: " + url);
    console.log("AjaxActionGetCreateEditModal - " + "actionType: " + actionType);
    console.log("AjaxActionGetCreateEditModal - " + "id: " + id);
    console.log("AjaxActionGetCreateEditModal - " + "section: " + section);
    console.log("--------------");
    //отправляем запрос
    $.ajax({
        cache: false,
        type: "GET",
        url: url,
        data: GetData(actionType, id, section),
        dataType: "html",
        success: function (data) {
            switch (actionType) {
                case "edit":
                    //1) добавляем модального окно в контейнер
                    $("#modalWrapper").append(data);
                    //2) переподключаем валидацию
                    $("form", "#modalWrapper").removeData("validator").removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse($("form", "#modalWrapper"));
                    //3) получаем Ids для настройки элементов модального окна
                    var boxId = "#" + $("form").attr("id");
                    var modalId = "#" + $(".modal").attr("id");
                    //сохраняем стартовую модель данных формы
                    editData = $(boxId).serialize();
                    console.log("editData: " + editData)
                    //4)Доп функции
                    SettingBehaviorOfModalWindow(boxId, section);
                    //5) выводим пользователю окно с формой
                    $(modalId).modal('show');
                    break;
                case "delete":
                    location.reload();
                    break;

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Ошибка загрузки модального окна!!!');
        }
    });
}


