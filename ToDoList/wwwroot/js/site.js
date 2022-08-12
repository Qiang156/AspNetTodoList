// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function() {

    // Wire up all of the checkboxes to run markCompleted()
    $('.done-checkbox').on('click', function(e) {
        markCompleted(e.target);
    });
    
    $('.btn-delete').on('click', (e) => {
        deleteItem(e.target.value);
    });
});

function markCompleted(checkbox) {
    checkbox.disabled = true;
    var row = checkbox.closest('tr');
    $(row).addClass('done');

    $("#action").val('MarkDone');
    $("#id").val(checkbox.value);
    $("#updateForm").submit();
}

function deleteItem(value) {
    $("#action").val('Delete');
    $("#id").val(value);
    $("#updateForm").submit();
}