//Глобальные переменные 
var genreIds = [];
var developerIds = [];
var price_from = 0;
var price_to = 0;
var labelIds = [];
var platformIds = [];
var isShare = false;

var pageNumber = 1;
var pageSize = 6;

//Вывод игры при загрузке страницы + если был использован поиск
function AjaxActionGetGamesData(nameGame, pageNumber, pageSize) {
    var url = GetUrlForLoadGameData("search");
    //отправляем запрос
    $.ajax({
        cache: false,
        type: "GET",
        url: url,
        data: {
            nameGame: nameGame,
            pageNumber: pageNumber,
            pageSize: pageSize
        },
        dataType: "html",
        success: function (data) {
            if (nameGame != "") {
                $("#js-gemes").empty();
            }
            ToggleLoadingView(false);
            $("#js-gemes").append(data);
            SetSettings();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Ошибка загрузки данных!!!');
        }
    });
}


function GameBTNControll() {
    $("#loadMoreBtn").on("click", function () {
        ToggleLoadingView(true);
        pageNumber++;
        AjaxActionGetGamesData("", pageNumber, pageSize);
    });


    //Поиск игры по наименованию
    $("#searchGame").on("input", function () {
        var inpdata = $("#searchGame").val();
        if (inpdata != "") {
            $('#showSearch-area').removeClass('d-none');
        }
        else {
            $('#showSearch-area').addClass('d-none');
        }
      //  AjaxActionGetGamesData(inpdata);
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
    $('#js-filterBtn').off('click').on('click', function () {
        genreIds = [];
        genreIds = $(input_genre).val();
        developerIds = [];
        developerIds = $(input_develop).val();

        price_from = $(input_price_from).val();
        price_to = $(input_price_to).val();

        labelIds = [];
        labelIds = $(input_lable).val();
        platformIds = [];
        platformIds = $(input_platform).val();

        if ($(input_isShare).is(':checked')) {
            isShare = true;
        } else {

            isShare = false;
        }
        AjaxActionFilterGames();
    });

    $('#js-clearfilterBtn').off('click').on('click', function () {
        genreIds = [];
        developerIds = [];
        price_from = 0;
        price_to = 0;
        labelIds = [];
        platformIds = [];
        isShare = false;

        $(input_genre).val(null).trigger('change');
        $(input_develop).val(null).trigger('change');
        $(input_price_from).val('0');
        $(input_price_to).val('0');
        $(input_lable).val(null).trigger('change');
        $(input_platform).val(null).trigger('change');
        $(input_isShare).prop('checked', false); 
        
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
