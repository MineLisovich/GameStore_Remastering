//Глобальные переменные 
var genreIds = [];
var developerIds = [];
var price_from = 0;
var price_to = 0;
var labelIds = [];
var platformIds = [];
var isShare = false;

//Вывод игры при загрузке страницы + если был использован поиск
function AjaxActionGetGamesData(nameGame) {
    var url = GetUrlForLoadGameData("search");
    //отправляем запрос
    $.ajax({
        cache: false,
        type: "GET",
        url: url,
        data: { nameGame: nameGame },
        dataType: "html",
        success: function (data) {
            $("#js-gemes").empty().append(data);
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

    //------ AREA FILTER GAME ------

    //Опеределение инпутов
    var input_genre = $("#GenreIds");
    var input_develop = $("#DeveloperIds");
    var input_price_from = $("#PriceFrom");
    var input_price_to = $("#PriceTo");
    var input_lable = $("#GameLableIds");
    var input_platform = $("#PlatformIds");
    var input_isShare = $("#IsShare");

    //Сбор данных и отпрвка в ajax

    //ЖАНРЫ
    $(input_genre).on('change', function () {
        genreIds = [];
        genreIds = $(this).val();
        AjaxActionFilterGames();
    });

    //РАЗРАБЫ
    $(input_develop).on('change', function () {
        developerIds = [];
        developerIds = $(this).val();
        AjaxActionFilterGames();
    });

    //ЦЕНА ОТ И ДО
    $(input_price_from).on('input', function () {
        price_from = 0;
        price_from = $(this).val();
        if (price_from == "") {
            price_from = 0;
        }
        AjaxActionFilterGames();
    });
    $(input_price_to).on('input', function () {
       
        price_to = $(this).val();
        if (price_to == "") {
            price_to = 0;
        } 
        AjaxActionFilterGames();
    });

    //ОСОБЕННОСТИ ИГРЫ
    $(input_lable).on('change', function () {
        labelIds = [];
        labelIds = $(this).val();
        AjaxActionFilterGames();
    });
    //ПЛАТФОРМЫ
    $(input_platform).on('change', function () {
        platformIds = [];
        platformIds = $(this).val();
        AjaxActionFilterGames();
    });

    //СКИДОЧНЫЕ ИГРЫ
    $(input_isShare).on('change', function () {
        if ($(this).is(':checked')) {
            isShare = true;
        } else {
         
            isShare = false;
        }
        AjaxActionFilterGames();
    });

    //------------------------------
}

function AjaxActionFilterGames() {
    var data = {
        Genres: genreIds,
        Develops: developerIds,
        PriceFrom: price_from,
        PriceTo: price_to,
        Lables: labelIds,
        Platforms: platformIds,
        IsShare: isShare
    };

    console.log("data = ", data);

    var url = GetUrlForLoadGameData("filter");
    //отправляем запрос
    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        contentType: "application/json", // Устанавливаем заголовок
        data: JSON.stringify(data), // Сериализация данных
        dataType: "html",
        success: function (data) {
            $("#js-gemes").empty().append(data);
            SetSettings();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Ошибка загрузки данных!!!');
        }
    });
}
