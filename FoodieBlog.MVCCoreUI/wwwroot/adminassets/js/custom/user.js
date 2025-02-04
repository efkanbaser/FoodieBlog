function createTable() {
    var table;
    table = $("#tblUsers").DataTable({
        "dom": "<'d-flex justify-content-between'lfB>rtip",
        "buttons": [
            "csv", "excel", "pdf", "print", "colvis",
            {
                text: 'Add User',
                className: 'btn btn-default',
                action: function () {
                    $('#modalAddUser').modal('show');
                },
            }
        ],
        "responsive": true,
        "lengthChange": true,
        "pageLength": 10,
        "autoWidth": false,
        ajax: { url: '/AdminPanel/User/List', type: 'post' },
        columns: [
            { data: 'id', visible: false },
            { data: 'userName' },
            { data: 'email' },
            { data: 'bio' },
            { data: 'profilePic' },
            { data: 'publicationDate' },
            { data: 'active' },
            { data: 'id' },
            { data: 'id' }
        ],
        columnDefs: [
            {
                targets: 6,
                render: function (data, type, row, meta) {

                    var userId = row["id"];

                    if (data)
                        return '<input userId=' + userId + ' type="checkbox" data-on-text="ACTIVE" data-off-text="PASSIVE" name="my-checkbox" checked data-bootstrap-switch data-off-color="danger" class="chkActive" data-on-color="success" > ';

                    else
                        return '<input userId=' + userId + ' type="checkbox" data-on-text="ACTIVE" data-off-text="PASSIVE" name="my-checkbox" data-bootstrap-switch data-off-color="danger" class="chkActive" data-on-color="success" > ';
                }
            },

            {
                targets: 7,
                render: function (data, type, row, meta) {
                    return "<a class='btn btn-info btnEdit' userId=" + data + ">   <i class='fas fa-pencil-alt'></i> EDIT</a > ";
                }
            },

            {
                targets: 8,
                render: function (data, type, row, meta) {
                    return "<a class='btn btn-danger btnDelete' userId=" + data + "><i class='fas fa-trash'></i> DELETE</a > ";
                }
            },
        ],

        initComplete: function (settings, json) {

            // Datatable initilize olduğunda
            $("input[data-bootstrap-switch]").each(function () {
                $(this).bootstrapSwitch();
            })
        },

        drawCallback: function () {

            // Arama Yapıldığında, Sayfalama Yapıldığında, Sıralama Yapıldığında Draw yapılıyor
            $("input[data-bootstrap-switch]").each(function () {
                $(this).bootstrapSwitch();
            })
        }
    }).buttons().container().appendTo('#tblUsers_wrapper .col-md-6:eq(0)');
    return table;
}


var table;
$(function () {
    table = createTable();
})

$("#btnAddUser").click(function () {

    var swal = Swal.fire({
        title: "Please Wait...",
        html: "Your Operation Is Under Progress",
        timerProgressBar: true,
        didOpen: () => {

            Swal.showLoading();

        },
    });
    var formData = new FormData();

    var UserName = $("#UserName").val();
    var Email = $("#Email").val();
    var Bio = $("#Bio").val();
    var ProfilePic = $("#ProfilePic").val();

    formData.append("UserName", UserName)
    formData.append("Email", Email)
    formData.append("Bio", Bio)
    formData.append("ProfilePic", ProfilePic)

    $.ajax({
        url: "/AdminPanel/User/Add",
        type: "post",
        dataType: "json",
        processData: false,
        contentType: false,
        data: formData,
        success: function (r) {

            // ---------------Datatable Yok Edildi---------------
            $("#tblUsers").DataTable().destroy();
            //-------------------------------------------------
            // ---------------Datatable Yeniden Oluşturuluyor.---------------
            table = createTable();

            //   $("#UstUserId").trigger("change"); // change eventı buradan tetiklendi
            // $("#modalUrunEkle").modal("hide");
            swal.close();
            if (r.result) {
                $("#UserName").val('');
                $("#Email").val('');
                $("Bio").val('');
                Swal.fire({
                    title: "Success",
                    text: r.mesaj,
                    icon: "success"
                });
                $('#modalAddUser').modal('hide');
            }
        },
        error: function () {

        }
    }
    );
});




$(function () {
    // Summernote
    $("input[data-bootstrap-switch]").each(function () {
        $(this).bootstrapSwitch();
    })
    $(document).on('switchChange.bootstrapSwitch', '.chkActive', function (event, state) {

        var swal = Swal.fire({
            title: "LÜTFEN BEKLEYİNİZ...",
            html: "İşleminiz Yapılıyor",
            timerProgressBar: true,
            didOpen: () => {

                Swal.showLoading();

            },
        });

        var id = $(this).attr('userId');
        var aktifpasif = state;
        $.ajax({
            url: "/AdminPanel/User/ActiveInactive",
            type: "post",
            dataType: "json",
            data: { id: id, active: aktifpasif },
            success: function (r) {
                swal.close();
                if (r.result) {
                    Swal.fire({
                        title: "Success",
                        text: r.mesaj,
                        icon: "success"
                    });
                }
            },
            error: function () {

            }
        }
        );
    });

    $(document).on('click', '.btnEdit', function () {
        var swal = Swal.fire({
            title: "Please Wait...",
            html: "Your Operation Is Under Progress",
            timerProgressBar: true,
            didOpen: () => {

                Swal.showLoading();

            },
        });

        var id = $(this).attr('userId');
        $("#HId").val(id);

        $.ajax({
            url: "/AdminPanel/User/GetUser",
            type: "post",
            dataType: "json",
            data: { id: id },
            success: function (r) {

                if (r.result) {
                    $("#HUserName").val(r.model.userName);
                    $("#HEmail").val(r.model.email);
                    $("#HBio").val(r.model.bio);
                    $("#HProfilePic").val(r.model.profilePic);
                    $("#modalUpdateUser").modal('show');
                }
                swal.close();
            },
            error: function () {

            }
        }
        );
    });


    // DÜZENLE TUŞU
    $(document).on('click', '#btnUpdateUser', function () {
        var swal = Swal.fire({
            title: "Please Wait...",
            html: "Your Operation Is Under Progress",
            timerProgressBar: true,
            didOpen: () => {

                Swal.showLoading();

            },
        });

        var formData = new FormData();

        var UserName = $("#HUserName").val();
        var Email = $("#HEmail").val();
        var Bio = $("HBio").val();
        var Id = $("#HId").val();

        formData.append("UserName", UserName)
        formData.append("Email", Email)
        formData.append("Bio", Bio)
        formData.append("Id", Id)


        $.ajax({
            url: "/AdminPanel/User/Update",
            type: "post",
            dataType: "json",
            processData: false,
            contentType: false,
            data: formData,
            success: function (r) {
                // ---------------Datatable Yok Edildi---------------
                $("#tblUsers").DataTable().destroy();
                //-------------------------------------------------
                // ---------------Datatable Yeniden Oluşturuluyor.---------------

                table = createTable();

                swal.close();
                if (r.result) {
                    Swal.fire({
                        title: "Success",
                        text: r.mesaj,
                        icon: "success"
                    });
                }
                $("#modalUpdateUser").modal('hide');
            },
            error: function () {

            }
        }
        );
    });



    // SİL TUŞU
    $(document).on('click', '.btnDelete', function () {

        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {

                var id = $(this).attr('userId');
                $("#HId").val(id);
                $.ajax({
                    url: "/AdminPanel/User/Delete",
                    type: "post",
                    dataType: "json",
                    data: { id: id },
                    success: function (r) {
                        // ---------------Datatable Yok Edildi---------------

                        $("#tblUsers").DataTable().destroy();
                        //-------------------------------------------------

                        // ---------------Datatable Yeniden Oluşturuluyor.---------------
                        table = createTable();

                        if (r.result) {
                            Swal.fire({
                                title: "Success",
                                text: r.mesaj,
                                icon: "success"
                            });
                        }
                    },
                    error: function () {


                    }
                });
                Swal.fire({
                    title: "Deleted!",
                    text: "Your file has been deleted.",
                    icon: "success"
                });
            }
        });
    });
})
