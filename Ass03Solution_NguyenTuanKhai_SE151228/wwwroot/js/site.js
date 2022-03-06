// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const { signalR } = require("../microsoft/signalr/dist/browser/signalr");

// Write your JavaScript code.
$(() => {
    LoadAppUserData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();

    connection.on("LoadAppUsers", function ()){
        LoadAppUserData();
    }
    function LoadAppUserData() {
        var tr = '';

    }

})