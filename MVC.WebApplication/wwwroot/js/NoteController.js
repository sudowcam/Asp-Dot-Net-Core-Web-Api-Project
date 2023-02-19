// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    console.log("Welcome");
});

/**
 * Index page, button '#ButtonView' onClick event handler
 */
$(document).on("click", '#ButtonView', function (event) {
    let NoteId = $(this).closest("tr").find('#NoteId').val();
    //console.log("NoteId: [" + NoteId + "]");

    // Get then load Bootstrap modal
    if ($.isNumeric(NoteId)) {
        $.when(
            LoadDetailsPage(NoteId, "view")
        )
        .done(function () {
            ToggleData("view"),
            $('#ModalDetails_ButtonSubmit').prop('value', 'ButtonUpdate')
        });
    }
    else {
        alert("Invalid/unknown Note id!");
        console.error("Invalid/unknown Note id!");
        event.preventDefault();
    }
});


/**
 * Bootstrap modal button '#ModalDetails_ButtonCreate' onClick event handler
 */
$(document).on("click", '#ModalDetails_ButtonCreate', function (event) {
    $.when(
        LoadDetailsPage()
    )
    .then(function () {
        //GetStatusList(),
        //GetUserList()
    })
    .done(function () {
        ToggleData("edit"),
        $('#ModalDetails_ButtonSubmit').prop('value', 'ButtonCreate')
    });
});

/**
 * Bootstrap modal button '#ModalDetails_ButtonEdit' onClick event handler
 */
$(document).on("click", '#ModalDetails_ButtonEdit', function (event) {

    console.log("ModalDetails_ButtonEdit");
    ToggleData("edit"),
    $('#ModalDetails_ButtonSubmit').prop('value', 'ButtonUpdate')
});

/**
 * Bootstrap modal button '#ModalDetails_ButtonCancel' onClick event handler
 */
$(document).on("click", '#ModalDetails_ButtonCancel', function (event) {
    let NoteId = $('#ModalDetails').find('#NoteId').val();
    //console.log("NoteId: [" + NoteId + "]");

    if (NoteId != 0) {
        ToggleData("view");
    }
    else {
        $('#ModalDetails').modal('toggle');
    }
});

/**
 * Bootstrap modal button '#ModalDetails_ButtonDelete' onClick event handler
 */
$(document).on("click", '#ModalDetails_ButtonDelete', function (event) {
    // Set changes
    $('#ModalDetails').css("opacity", "0.75");
    $('#ModalDelete').css("top", "5px");
    $('#ModalDelete').modal('show');
});

/**
 * Bootstrap modal button '#ModalDelete_ButtonCancel' onClick event handler
 */
$(document).on("click", '#ModalDelete_ButtonCancel', function (event) {
    // Reset changes
    $('#ModalDetails').css("opacity", "1");
});

/**
 * Search functionality keyup event hander
 */
$(document).on("keyup", '#Search', function () {
    var SearchParam = $(this).val().toLowerCase();
    console.log("SearchParam: " + SearchParam);

    $('#TableNoteList > tbody tr').each(function (trindex, tr) {
        $(tr).find('td').each(function (tdindex, td) {
            // Skip index 0, '#ButtonView'
            if (tdindex == 0) {
                // Returning non-false is the same as a continue statement in a for loop, it will skip immediately to the next iteration.
                return true;
            }

            // show or hide depends on success match
            if ($(td).html().toLowerCase().indexOf(SearchParam) != -1) {
                $(tr).show();
                // break loop
                return false;
            }
            else {
                $(tr).hide();
            }
        });
    });
});

/**
 * Dropdown #OwnerId on change event hander
 */
$(document).on("change", '#OwnerId', function (event) {
    let ownerIdSelected = $(this).parent().find('#OwnerId :selected').val();
    let ownerNameSelected = $(this).parent().find('#OwnerId :selected').text();
    console.log("Owner: [" + ownerIdSelected + "][" + ownerNameSelected + "]");

    $(this).parent().find('input[id=OwnerName]').val(ownerNameSelected);
});

/**
 * Dropdown #StatusId on change event hander
 */
$(document).on("change", '#StatusId', function (event) {
    let statusIdSelected = $(this).parent().find('#StatusId :selected').val();
    let statusNameSelected = $(this).parent().find('#StatusId :selected').text();
    console.log("Status: [" + statusIdSelected + "][" + statusNameSelected + "]");

    $(this).parent().find('input[id=StatusName]').val(statusNameSelected);
});




/**
 * Toggle bootstrap modal input fields based on mode.
 * 
 * @param mode      Display mode (view or edit)
 * @return null     Return nothing
 */
function ToggleData(mode) {
    if (mode == "view") {
        console.log("View mode");
        // buttons
        $('#ModalDetails').find('button[id=ModalDetails_ButtonEdit]').show();
        $('#ModalDetails').find('button[id=ModalDetails_ButtonDelete]').show();
        $('#ModalDetails').find('button[id=ModalDetails_ButtonSubmit]').hide();
        $('#ModalDetails').find('button[id=ModalDetails_ButtonCancel]').hide();
        // inputs
        $('#ModalDetails').find('input[id=Title]').prop('readonly', true);
        $('#ModalDetails').find('textarea[id=Description]').prop('readonly', true);
        $('#ModalDetails').find('input[id=DueDate]').prop('readonly', true);

        $('#ModalDetails').find('input[id=OwnerName]').prop('readonly', true).show();
        $('#ModalDetails').find('select[id=OwnerId]').hide();
        $('#ModalDetails').find('input[id=StatusName]').prop('readonly', true).show();
        $('#ModalDetails').find('select[id=StatusId]').hide();
    }
    if (mode == "edit") {
        console.log("Edit mode");
        //button
        $('#ModalDetails').find('button[id=ModalDetails_ButtonEdit]').hide();
        $('#ModalDetails').find('button[id=ModalDetails_ButtonDelete]').hide();
        $('#ModalDetails').find('button[id=ModalDetails_ButtonSubmit]').show();
        $('#ModalDetails').find('button[id=ModalDetails_ButtonCancel]').show();
        //inputs
        $('#ModalDetails').find('input[id=Title]').prop('readonly', false);
        $('#ModalDetails').find('textarea[id=Description]').prop('readonly', false);
        $('#ModalDetails').find('input[id=DueDate]').prop('readonly', false);

        $('#ModalDetails').find('input[id=StatusName]').prop('readonly', false).hide();
        $('#ModalDetails').find('select[id=StatusId]').show();
        $('#ModalDetails').find('input[id=OwnerName]').hide();
        $('#ModalDetails').find('select[id=OwnerId]').show();
    }
}

/**
 * Load todo note details, triggered when loading bootstrap modal.
 * 
 * @param Id                Note Id
 * @return partialView      Return and render ASP.Net partial view
 */
function LoadDetailsPage(Id) {
    let Object = $('#ModalDetails');
    return $.ajax({
        type: "GET",
        url: "/Note/PartialViewDetails",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: { id: Id },
        dataType: "html",
        cache: false,
        success: function (response) {
            console.log("[HTTPGet] PartialViewDetails success!");

            // Add response to modal body
            Object.find('.modal-body').html(response);
            Object.find('.Title').html("Todo Details");

            // Prepend dropdown
            let DropdownList_status = $(Object).find('select[id=StatusId]');
            DropdownList_status.prepend('<option disabled selected value>-- Select Status --</option>');
            let DropdownList_user = $(Object).find('select[id=OwnerId]');
            DropdownList_user.prepend('<option disabled selected value>-- Select User --</option>');

            Object.modal('show');
        },
        error: function (xhr, status, thrownError) {
            alert("Status code : " + xhr.status);
            alert(thrownError);
        }
    });
}


function GetStatusList() {
    let Object = $('#ModalDetails').find('select[id=StatusId]');
    let SelectedStatusId = $('#ModalDetails').find('input[id=StatusId]').val();
    console.log("SelectedStatusId: [" + SelectedStatusId + "]");

    return $.ajax({
        type: "GET",
        url: "/Note/GetStatusList/",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: {},
        dataType: "json",
        cache: false,
        success: function (response) {
            console.log("[HttpGet] GetStatusList successful!");

            // Populate option from response
            Object.find('option').remove();
            Object.append('<option disabled selected value>-- Select Status --</option>');
            for (var i in response) {
                Object.append('<option value="' + response[i].value + '">' + response[i].text + '</option>');
            }

            // Reselect dropdown
            if ($.isNumeric(SelectedStatusId)) {
                $('#ModalDetails').find('#StatusId > option[value=' + SelectedStatusId + ']').attr('selected', 'selected');
            }
        },
        error: function (xhr, status, thrownError) {
            alert("Status code : " + xhr.status);
            alert(thrownError);
        }
    });
}

function GetUserList() {
    let Object = $('#ModalDetails').find('select[id=OwnerId]');
    let SelectedOwnerId = $('#ModalDetails').find('input[id=OwnerId]').val();
    //console.log("SelectedOwnerId: [" + SelectedOwnerId + "]");

    return $.ajax({
        type: "GET",
        url: "/Note/GetUserList/",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: {},
        dataType: "json",
        cache: false,
        success: function (response) {
            console.log("[HttpGet] GetUserList successful!");

            // Populate option from response
            Object.find('option').remove();
            Object.append('<option disabled selected value>-- Select User --</option>');
            for (var i in response) {
                Object.append('<option value="' + response[i].value + '">' + response[i].text + '</option>');
            }

            // Reselect dropdown
            if ($.isNumeric(SelectedOwnerId)) {
                $('#ModalDetails').find('#OwnerId > option[value=' + SelectedOwnerId + ']').attr('selected', 'selected');
            }
        },
        error: function (xhr, status, thrownError) {
            alert("Status code : " + xhr.status);
            alert(thrownError);
        }
    });
}