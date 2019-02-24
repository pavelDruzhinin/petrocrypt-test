
/// <reference path="~/Scripts/System/knockout-2.2.0.js" />
/// <reference path="~/Scripts/System/knockout.mapping-latest.js" />
/// <reference path="~/Scripts/System/jquery-2.0.0.min.js" />

$(document).ready(function () {

});

function DatePicker() {
    $.datepicker.setDefaults($.datepicker.regional['ru']);
    $("input.date").datepicker();
}

function createNewRow() {
    $("#table").hide();
    $("#createTable").show();
}

function cancelNewRow() {
    $('#video tbody tr.NewRow:eq(0)').remove();
}

function videoRow(data, genres, authors) {
    this.VideoID = ko.observable(data.VideoID);
    this.AuthorID = ko.observable(data.AuthorID);
    this.Title = ko.observable(data.Title);
    this.DatePremiere = ko.observable(data.DatePremiere);
    this.GenreID = ko.observable(data.GenreID);
    this.genre = ko.observable(data.Genre.KindGenre);
    this.author = ko.computed(function () {
        return data.Author.FirstName + " " + data.Author.LastName;
    });
}

function VideoViewModel() {
    var self = this;
    //Data
    self.Videos = ko.observableArray([]);
    self.Genres = ko.observableArray([]);
    self.Authors = ko.observableArray([]);

    //Actions
    self.createRow = function () {
        if (!$('#video tbody tr').is(".NewRow")) {
            $('#video').append($('#createTable tbody tr.NewRow:eq(0)').clone());
            var cancelRow = document.getElementById('cancel');
            cancelRow.onclick = cancelNewRow;
            $('#DatePrimiere').datepicker();
            $('.add').click(self.addVideo);
        }
    };

    self.addVideo = function () {
        var JSON = ko.toJSON({
            Title: $('#Title').val(),
            DatePremiere: $("#DatePrimiere").val(),
            AuthorID: $('#AuthorID').val(),
            GenreID: $('#GenreID').val()
        });
        $.ajax('/Home/AddRow', {
            data: JSON,
            type: "POST",
            contentType: "application/json",
            success: function (data) {
                self.Videos.push(new videoRow(data, self.Genres, self.Authors));
                cancelNewRow();
            }
        });
    };

    self.editRow = function (item) {
        var index = self.Videos.indexOf(item);
        $('#video tbody tr.text:eq(' + (index) + ')').hide();
        $('#video tbody tr.edit:eq(' + (index) + ')').show();
    };

    self.updateRow = function (item) {
        var index = self.Videos.indexOf(item);
        var JSON = ko.toJSON({
            AuthorID: item.AuthorID().AuthorID(),
            GenreID: item.GenreID().GenreID(),
            DatePremiere: item.DatePremiere,
            Title: item.Title,
            VideoID: item.VideoID
        });
        $.ajax('/Home/UpdateRow', {
            data: JSON,
            type: "POST",
            contentType: "application/json",
            success: function (data) {
                var mappedVideo = $.map(data, function (item) { return new videoRow(item, self.Genres, self.Authors) });
                self.Videos(mappedVideo);
                $('#video tbody tr.text:eq(' + (index) + ')').show();
                $('#video tbody tr.edit:eq(' + (index) + ')').hide();
            }
        });
    };

    self.removeRow = function (item) {
        var JSON = ko.toJSON({ id: item.VideoID });
        $.ajax('/Home/DeleteRow',
            {
                data: JSON,
                type: "POST",
                contentType: "application/json",
                success: function (data) {
                    self.Videos.remove(item);
                }
            });
    };

    //Load default data
    $.getJSON("/Home/JsonData", function (data) {
        ko.mapping.fromJS(data.Genres, {}, self.Genres);
        ko.mapping.fromJS(data.Authors, {}, self.Authors);
        var mappedVideo = $.map(data.Videos, function (item) { return new videoRow(item, data.Genres, data.Authors) });
        self.Videos(mappedVideo);
        $('#loader').hide();
        $('#table').show();
    }).error(function () { alert("Ошибка! Данные JSON не приняты!"); });

    self.datepicker = function () {
        $.datepicker.setDefaults($.datepicker.regional['ru']);
        $("input.date").datepicker();
    }
}

ko.applyBindings(new VideoViewModel());


