//Вызов модального окна добавления в корзину
$(".btn-add-shopcart-js").off("click").on("click", function () {
    var gameId = $(this).attr("data-gameId");
    var action = "edit";

    console.log("GAME ID: ", gameId);
    AjaxActionGetCreateEditModal(action, gameId, "");
});