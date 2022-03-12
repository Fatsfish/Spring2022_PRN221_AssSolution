// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//const { signalR } = require("../microsoft/signalr/dist/browser/signalr");

// Write your JavaScript code.
$(() => {
    LoadAppUserData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();

    connection.on("LoadAppUsers", function (){
        LoadAppUserData();
    })

    LoadAppUserData();

    function LoadAppUserData() {
        var tr = '';
        $.ajax({
            url: '/AppUsers/GetAppUsers',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr>
                        <td> ${v.fullName} </td>
                        <td> ${v.address} </td>
                        <td> ${v.password} </td>
                        <td> ${v.email} </td>
                        <td>
                <a href='../AppUsers/Edit/${v.userId}'>Edit</a> |
                <a href='../AppUsers/Details/${v.userId}'>Details</a> |
                <a href='../AppUsers/Delete/${v.userId}'>Delete</a> |
                </td> </tr>`
                })
                $("#tableBody").html(tr);
            },
            error: (error) => {
                console.log(error)
            }
        });
    }
})