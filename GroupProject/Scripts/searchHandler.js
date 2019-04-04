$(document).ready(function () {

    $('#adminSearchButton').click(function (event) {
        var category = $("#searchList").val();
        var term = $("#SearchTextbox").val();
        var urlString = '/Admin/Search/' + category + '/' + term;
        HandleSearch(urlString);
    });
    $('#guestSearchButton').click(function (event) {
        var category = $("#searchList").val();
        var term = $("#SearchTextbox").val();
        var urlString = 'Search/' + category + '/' + term;
        HandleSearch(urlString);
    });
});

function HandleSearch(urlString) {
    $.ajax({
        type: 'GET',
        url: urlString,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        success: function (data, status) {
            $.each(data,
                function (index, item) {
                    var blogPostId = item.BlogPostId;
                    var title = item.BlogPostTitle;
                    var message = item.BlogPostMessage;
                    var dateAdded = item.DateAdded;
                    var dateEdited = item.DateEdited;
                });
        },
        error: function (message) {
            alert(message.responseText);

        }

    });
}