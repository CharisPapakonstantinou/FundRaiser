// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#project-details a').on('click', function (e) {
    e.preventDefault()
    $(this).tab('show')
})

var today = new Date().toISOString().split('T')[0];
document.getElementsByName("endDate")[0].setAttribute('min', today);