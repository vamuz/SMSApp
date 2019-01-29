
$("#Create").on("", function () { // wire up the OK button to dismiss the modal when shown
    $("#Create a.btn").on("click",
        function (e) {
            $("#Create").modal('hide'); // dismiss the dialog
        });
});
$("#Create").on("hide", function () { // remove the event listeners when the dialog is dismissed
    $("#Create a.btn").off("click");
});

$("#mCreate").on("hidden", function () { // remove the actual elements from the DOM when fully hidden
    $("#Create").remove();
});

$("#Create").modal({ // wire up the actual modal functionality and show the dialog
    "backdrop": true,
    "keyboard": true,
    "show": false // ensure the modal is shown immediately

});


//post data to db using webapi
$(function () {
    $("#btnSave").click(function () {
        var FileLength = $("#file")[0].files.length;
        if (FileLength === 0) {
            alert("No file selected.");
            return false;
        }

        //check if the file has been selected
        //var checkFile = $("#file").get(0).files;
        //if (checkFile.length === 0) {
        //    alert('Please select the upload file');
        //    return;
        //}
        //check the file type --pdf only
        var extension = $('#file').val().split('.').pop().toLowerCase();

        if ($.inArray(extension, ['pdf']) === -1) {
            alert('Sorry, invalid extension. PDF file ONLY');
            return false;
        }
        var SMSAppRegistrationId = $('#SMSAppRegistrationId').val();
        var FullNames = $("#FullNames").val();
        var NationalIDNo = $("#NationalIDNo").val();
        var YearofBirth = $("#YearofBirth").val();
        var PhoneNo = $("#PhoneNo").val();
        var EmailAddress = $("#EmailAddress").val();
        var MaritalStatusId = $("#MaritalStatusId").val();
        var GenderId = $("#GenderId").val();
        var Occupation = $("#Occupation").val();
        var CountyId = $("#CountyId").val();
        var ConstituencyId = $("#ConstituencyId").val();
        var Location = $("#Location").val();
        var PWDCategoryId = $("#PWDCategoryId").val();

        if (SMSAppRegistrationId === "")
            SMSAppRegistrationId = 0;

        var userData = {
            SMSAppRegistrationId: SMSAppRegistrationId,
            FullNames: FullNames,
            NationalIDNo: NationalIDNo,
            YearofBirth: YearofBirth,
            PhoneNo: PhoneNo,
            EmailAddress: EmailAddress,
            MaritalStatusId: MaritalStatusId,
            GenderId: GenderId,
            Occupation: Occupation,
            CountyId: CountyId,
            ConstituencyId: ConstituencyId,
            Location: Location,
            PWDCategoryId: PWDCategoryId

        };
        var json = JSON.stringify(userData);

        $.ajax({
            type: "POST",
            url: '/api/UserDetails',
            data: json,
            contentType: "application/json",
            success: function () {
                bootbox.alert('Data Saved Successfully');

                $("#test").trigger("reset");

                $("#ConstituencyId").html(""); // clear before appending new list
                $("#ConstituencyId").append($('<option></option>').val(1).html('Select Constituency'));


                //var html = 'Data Saved Successfully';
                //$('#notification').html(html).removeClass();
                //$('#notification').html(html).addClass('alert alert-success');
                ////alert("Data Saved Successfully");


                var data = new FormData();
                //Add the Multiple selected files into the data object
                var file = $("#file").get(0).files;
                for (i = 0; i < file.length; i++) {
                    data.append("file" + i, file[i]);
                }

                data.append("idno", $("#NationalIDNo").val());
                //Post the data (files) to the server

                if (file.length > 0) {
                    $.ajax({
                        type: 'POST',
                        url: "/SMSAppRegistrations/Upload",
                        data: data,
                        contentType: false,
                        processData: false,
                        success: function (result) {
                            alert(result);
                        },

                    });


                }
            }
        });

    });
});


//on county click
$("#CountyId").on("change",
    function () {
        var county = $('#CountyId').val();
        $("#ConstituencyId").removeProp('disabled');
        $.ajax({
            url: '/SMSAppRegistrations/FillConstituency',
            type: "GET",
            dataType: "JSON",
            data: { location: county },
            success: function (location) {
                $("#ConstituencyId").html(""); // clear before appending new list
                $.each(location,
                    function (i, city) {
                        $("#ConstituencyId").append(
                            $('<option></option>').val(city.Id).html(city.ConstituencyName));
                    });
            }
        });
    });

function FillConstituency() {
    var county = $('#CountyId').val();
    $("#ConstituencyId").removeProp('disabled');
    $.ajax({
        url: '/SMSAppRegistrations/FillConstituency',
        type: "GET",
        dataType: "JSON",
        data: { location: county },
        success: function (location) {
            $("#ConstituencyId").html(""); // clear before appending new list
            $.each(location,
                function (i, city) {
                    $("#ConstituencyId").append(
                        $('<option></option>').val(city.Id).html(city.ConstituencyName));
                });
        }
    });

}

$("#NationalIDNo").on("change keyup",
    function () {
        var idNo = $('#NationalIDNo').val();
        $.ajax({
            url: '/SMSAppRegistrations/ValidateNationalID',
            type: "GET",
            dataType: "JSON",
            data: { idno: idNo },
            success: function (status) {
                if (status === true) {
                    var html = 'The national idno is already on the database';
                    $('#notification').html(html).addClass('alert alert-danger');
                }

            }
        });
    });


$("#PhoneNo").on("change keyup",
    function () {
        var pnumber = $('#PhoneNo').val();
        $.ajax({
            url: '/SMSAppRegistrations/ValidatePhoneNo',
            type: "GET",
            dataType: "JSON",
            data: { Phone: pnumber },
            success: function (status) {
                if (status === true) {
                    var html = 'The Phone number is already on the database';
                    $('#notification').html(html).addClass('alert alert-danger');
                }
            }
        });
    });

$("#EmailAddress").on("change keyup",
    function () {
        var contact = $('#EmailAddress').val();
        $.ajax({
            url: '/SMSAppRegistrations/ValidateEmail',
            type: "GET",
            dataType: "JSON",
            data: { emailadd: contact },
            success: function (status) {
                if (status === true) {
                    var html = 'The email address is already on the database';
                    $('#notification').html(html).addClass('alert alert-danger');
                }
            }
        });
    });




$("#SMSAppRegistrations").DataTable({
    responsive: !0,
    colReorder: !0,
    "order": [[0, "asc"]],
    dom: 'Bfrtip',
    buttons: ['pdf', 'csv', 'print', 'excel'],
    ajax: {
        url: '/SMSAppRegistrations/GetUserDetails',
        type: "Get",
        dataType: "JSON",
        "dataSrc": ""
    },
    columns: [
        { data: "FullNames" },
        { data: "NationalIDNo" },
        { data: "YearofBirth" },
        { data: "GenderType" },
        { data: "MaritalStatusType" },
        { data: "PhoneNo" },
        { data: "ConstituencyName" },
        { data: "CountyName" },
        { data: "PWDCategoryType" },
        { data: "Occupation" },
        { data: "Location" },
        { data: "EmailAddress" },
        {
            data: { id: "id" },
            "render": function (data, type, row, meta) {
                data =
                    '<button type="button" class="EditSMSDetails btn m-btn--pill btn-outline-success btn-sm" data-id="' +
                    data.id +
                    '">Edit</button>';
                return data;
            }
        },
        {
            data: { id: "id" },
            "render": function (data, type, row, meta) {
                data =
                    '<button type="button" class="DeleteSMSDetails btn m-btn--pill btn-outline-danger btn-sm" data-id="' +
                    data.id +
                    '">Delete</button>';
                return data;
            }
        }
    ]
});

$(document).on('click', '.EditSMSDetails', function () {
    var id = $(this).data('id');
    $.ajax({
        type: "GET",
        DataType: "json",
        url: '/SMSAppRegistrations/EditSmsDetails',
        contentType: 'application/json; charset=utf-8',
        data: { id: id },
        //data: id,
        success: function (result) {
            $('#SMSAppRegistrationId').val(result.SMSAppRegistrationId);
            $('#FullNames').val(result.FullNames);
            $('#NationalIDNo').val(result.NationalIDNo);
            $('#YearOfBirth').val(result.YearOfBirth);
            $('#PhoneNo').val(result.PhoneNo);
            $('#EmailAddress').val(result.EmailAddress);
            $('#MaritalStatusId').val(result.MaritalStatusId);
            $('#GenderId').val(result.GenderId);
            $('#Occupation').val(result.Occupation);
            $('#CountyId').val(result.CountyId);
            $("#CountyId").on("change",
                function () {
                    var county = $('#CountyId').val();
                    $("#ConstituencyId").removeProp('disabled');
                    $.ajax({
                        url: '/SMSAppRegistrations/FillConstituency',
                        type: "GET",
                        dataType: "JSON",
                        data: { location: county },
                        success: function (location) {
                            $("#ConstituencyId").html(""); // clear before appending new list
                            $.each(location,
                                function (i, city) {
                                    $("#ConstituencyId").append(
                                        $('<option></option>').val(city.Id).html(city.ConstituencyName));
                                });
                        }
                    });
                });


            $('#ConstituencyId').val(result.ConstituencyId);

            function FillConstituency() {
                var county = $('#CountyId').val();
                $("#ConstituencyId").removeProp('disabled');
                $.ajax({
                    url: '/SMSAppRegistrations/FillConstituency',
                    type: "GET",
                    dataType: "JSON",
                    data: { location: county },
                    success: function (location) {
                        $("#ConstituencyId").html(""); // clear before appending new list
                        $.each(location,
                            function (i, city) {
                                $("#ConstituencyId").append(
                                    $('<option></option>').val(city.Id).html(city.ConstituencyName));
                            });
                    }
                });

            }

            $('#Location').val(result.Location);
            $('#PWDCategoryId').val(result.PWDCategoryId);
            $('#SmsDetailsPopup').modal();

        },


        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] === "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });

    $('#SMSAppRegistrations').DataTable().ajax.reload();
});


$(document).on('click', '.DeleteSMSDetails', function () {
    var id = $(this).data('id');
    var deleterecord = confirm("Are you sure you want to delete the entry?");
    console.log(deleterecord);
    if (deleterecord === true) {
        $.ajax({
            type: "GET",
            DataType: "json",
            url: '/SMSAppRegistrations/DeleteSmsDetails',
            contentType: 'application/json; charset=utf-8',
            data: { id: id },
            success: function () {
                alert("entry deleted successfully");
                $('#SMSAppRegistrations').DataTable().ajax.reload();
            }
        });
    }
});


//$("#Create").on("hidden.bs.modal", function () {
//    //code to reset the form

//});