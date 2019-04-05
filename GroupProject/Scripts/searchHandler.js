$(document).ready(function () {
    $('#posts2').hide();
    $('#searchForm').on('submit',
        function(event) {
            event.stopImmediatePropagation();
            event.preventDefault();
            //Haha, no form submitting for you!
        });
    $('#adminSearchButton').click(function (event) {
        var category = $("#searchList").val();
        if (category == null) {
            alert('Please enter a search category');
            return;
        }
        var term = $("#SearchTextbox").val();
        var urlString = '/Admin/Search/' + category + '/' + term;
        HandleSearch(urlString);
    });
    $('#guestSearchButton').click(function (event) {
        var category = $("#searchList").val();
        if (category == null) {
            alert('Please enter a search category');
            return;
        }
        var term = $("#SearchTextbox").val();
        var urlString = '/Search/' + category + '/' + term;
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
            $('#posts2').text('');
            $.each(data,
                function (index, item) {
                    switch (urlString.charAt(1)) {
                        case 'A':
                            RenderAdmin(item);
                            break;
                        case 'S':
                            RenderUser(item);
                            break;
                        default:
                            alert('How did you break it THAT badly?');
                            return;
                    }
                });
            $('#nonSearch').hide();
            $('#posts2').show();
        },
        error: function (message) {
            alert(message.responseText);

        }

    });
}

function RenderAdmin(item) {
    var row = '<div class="row">';
    row += '<div class="col-md-1">';
    row += '<p>' + item.BlogPostId + '</p>';
    row += '</div>';
    row += '<div class="col-md-4">';
    row += '<h3>' + item.Title + '</h3>';
    row += '</div>';
    row += '<div class="col-md-2">';
    row += '<p><a href="/admin/update/' + item.BlogPostId + '" class="btn-sm btn-primary">Update</a></p>';
    row += '</div>';
    row += '<div class="col-md-2">';
    row += '<p><a href="/admin/delete/' + item.BlogPostId + '" class="btn-sm btn-danger">Delete</a></p>';
    row += '</div></div>';
    $('#posts2').append(row);
}

function RenderUser(item) {
    var row =
        '<div class="container"><div class="posts1"><div class="container"><div class="row"><div class="col-12"><h3>';
    row += item.Title;
    row += '</h3></div></div><div class="row"><div class="col-12">';
    row += item.Message;
    row += '</div></div><p></p></div><div class="col-md-12"><div class="row ending"><div class="col-md-4"><p>';
    row += Date(item.DateAdded.match(/\d+/)).toLocaleString('en-US').match(/^[^G]+/); //What am I even doing at this point
    row += '</p></div><div class="col-md-4"><p>';
    row += Date(item.DateAdded.match(/\d+/)).toLocaleString('en-US').match(/^[^G]+/);
    row += '</p></div><div class="col-md-4"><p>@theOwner</p></div></div></div></div><br /></div>';
    $('#posts2').append(row);
}