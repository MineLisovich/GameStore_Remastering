var pageNumber = 1;
var pageSize = 50;

$('#js-sendReviewBtn').off('click').on('click', function () {
    var reviewMess = $('#js-review').val();
    var gameId = $('#Game_Id').val();
    var userEmail = $('#js-userEmail').val();
    if (reviewMess.trim() != "") {
        AjaxActionReviewGame(gameId, userEmail, reviewMess);
    }
  

});

function AjaxActionReviewGame(gameId, userEmail, reviewMess) {
    var url = GetURLForAjaxByActionType("review");

    var data = {
        GameId: gameId,
        UserEmail: userEmail,
        Review: reviewMess
    };

    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        contentType: "application/json", // Устанавливаем заголовок
        data: JSON.stringify(data), // Сериализация данных
        dataType: "json",
        success: function (data) {
            console.log(data);
            //Результат выполнения
            var toastRev = (data.isSucceeded == true) ? $("#modalActionSuccess") : $("#modalActionError");
            var reмMess = (data.isSucceeded == true) ? "Отзыв успешно добавлен" : "Не удалось добавить отзыв";
            $(toastRev).find(".modal-message-js").first().empty().text(reмMess);
            $(toastRev).toast('show');

            //Очистка поля ввода отзыва
            $('#js-review').val('');

            //Обновить список отзывов
            UpdateContainerReviews();
          
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Ошибка загрузки данных!!!');
        }
    });
}

function AjaxActionGetGameReviews(pageNumber, pageSize) {
    var gameId = $('#Game_Id').val();
    var url = GetURLForAjaxByActionType("showReview");
    $.ajax({
        cache: false,
        type: "GET",
        url: url,
        data: {
            gameId: gameId,
            pageNumber: pageNumber,
            pageSize: pageSize
        },
        dataType: "html",
        success: function (data) {
           ToggleLoadingView(false);
            $("#js-reviewsArea").append(data);
            ReviewBTNManager();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Ошибка загрузки данных!!!');
        }
    });
}

$("#loadMoreReviewBtn").off('click').on('click', function () {
    ToggleLoadingView(true);
    pageNumber++;
    AjaxActionGetGameReviews(pageNumber, pageSize);
});

function UpdateContainerReviews() {
    pageNumber = 1;
    $("#js-reviewsArea").empty();
    AjaxActionGetGameReviews(pageNumber, pageSize);
}

function ReviewBTNManager() {
    $('.js-reviewBTN').off('click').on('click', function () {
        var action = $(this).attr('data-reviewAction');
        var reviewId = $(this).attr('data-reviewId');
        AjaxActionReviewBTN(action, reviewId);
    });
}

function AjaxActionReviewBTN(action, reviewId) {
    var url = GetURLForAjaxByActionType(action);
    var contentType = (action == "elasticRemove") ? "application/json" : "";
    var dataType = (action == "elasticRemove") ? "json" : "html";

    $.ajax({
        cache: false,
        type: "GET",
        url: url,
        contentType: contentType, // Устанавливаем заголовок
        data: { reviewId: reviewId },
        dataType: dataType,
        success: function (data) {
            console.log(data);

            switch (action) {
                case "elasticRemove":
                    //Результат выполнения
                    var toastRev = (data.isSucceeded == true) ? $("#modalActionSuccess") : $("#modalActionError");
                    var reмMess = (data.isSucceeded == true) ? "Отзыв успешно удалён" : "Не удалось удалить отзыв";
                    $(toastRev).find(".modal-message-js").first().empty().text(reмMess);
                    $(toastRev).toast('show');

                    //Обновить список отзывов
                    UpdateContainerReviews();
                    break;
                case "editReview":
                    $("#modalWrapper").append(data);
                    $("form", "#modalWrapper").removeData("validator").removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse($("form", "#modalWrapper"));
                    $('#modalEditReview').modal('show');
                    ManagerModal();
                    break;
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Ошибка загрузки данных!!!');
        }
    });
}

function ManagerModal() {
    $('#removeEditReviewModal').off('click').on('click', function () {
        // Удаляем модальное окно из DOM
        $('#modalEditReview').remove();
    });
}

