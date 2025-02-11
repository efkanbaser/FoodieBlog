function createTable() {
    var table;
    table = $("#tblMenuAuths").DataTable({
        "dom": "<'d-flex justify-content-between'lfB>rtip",
        "buttons": [
            "csv", "excel", "pdf", "print", "colvis",
            {
                text: 'Add User Authorization',
                className: 'btn btn-default',
                action: function () {
                    $('#modalAddMenuAuth').modal('show');
                },
            }
        ],
        "responsive": true,
        "lengthChange": true,
        "pageLength": 10,
        "autoWidth": false,
        ajax: { url: '/AdminPanel/MenuAuth/List', type: 'post' },
        columns: [
            { data: 'id', visible: false },
            { data: 'roleName' },
            { data: 'menuName' },
            { data: 'insertAuthorization' },
            { data: 'updateAuthorization' },
            { data: 'deleteAuthorization' },
            { data: 'selectAuthorization' },
            { data: 'active' },
            { data: 'id' },
            { data: 'id' }
        ],
        columnDefs: [
            {
                targets: 7,
                render: function (data, type, row, meta) {

                    var userId = row["id"];

                    if (data)
                        return '<input userId=' + userId + ' type="checkbox" data-on-text="ACTIVE" data-off-text="PASSIVE" name="my-checkbox" checked data-bootstrap-switch data-off-color="danger" class="chkActive" data-on-color="success" > ';

                    else
                        return '<input userId=' + userId + ' type="checkbox" data-on-text="ACTIVE" data-off-text="PASSIVE" name="my-checkbox" data-bootstrap-switch data-off-color="danger" class="chkActive" data-on-color="success" > ';
                }
            },

            {
                targets: 8,
                render: function (data, type, row, meta) {
                    return "<a class='btn btn-info btnEdit' userId=" + data + ">   <i class='fas fa-pencil-alt'></i> EDIT</a > ";
                }
            },

            {
                targets: 9,
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
    }).buttons().container().appendTo('#tblMenuAuths_wrapper .col-md-6:eq(0)');
    return table;
}


var table;
$(function () {
    table = createTable();
})

$("#btnAddMenuAuth").click(function () {

    var swal = Swal.fire({
        title: "Please Wait...",
        html: "Your Operation Is Under Progress",
        timerProgressBar: true,
        didOpen: () => {

            Swal.showLoading();

        },
    });
    var formData = new FormData();

    var RoleName = $("#RoleName option:selected").val();
    var MenuName = $("#MenuName option:selected").val();
    var InsertAuthorization = $("#InsertAuthorization option:selected").val();
    var UpdateAuthorization = $("#UpdateAuthorization option:selected").val();
    var DeleteAuthorization = $("#DeleteAuthorization option:selected").val();
    var SelectAuthorization = $("#SelectAuthorization option:selected").val();

    formData.append("RoleName", RoleName)
    formData.append("MenuName", MenuName)
    formData.append("InsertAuthorization", InsertAuthorization)
    formData.append("UpdateAuthorization", UpdateAuthorization)
    formData.append("DeleteAuthorization", DeleteAuthorization)
    formData.append("SelectAuthorization", SelectAuthorization)

    $.ajax({
        url: "/AdminPanel/MenuAuth/Add",
        type: "post",
        dataType: "json",
        processData: false,
        contentType: false,
        data: formData,
        success: function (r) {

            // ---------------Datatable Yok Edildi---------------
            $("#tblMenuAuths").DataTable().destroy();
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
                $('#modalAddMenuAuth').modal('hide');
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
            url: "/AdminPanel/MenuAuth/ActiveInactive",
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
            url: "/AdminPanel/MenuAuth/GetUser",
            type: "post",
            dataType: "json",
            data: { id: id },
            success: function (r) {

                if (r.result) {
                    $("#HRoleName").val(r.model.roleId);
                    $("#HMenuName").val(r.model.menuId);
                    $("#HInsertAuthorization").val(r.model.insertAuthorization.toString());
                    $("#HUpdateAuthorization").val(r.model.updateAuthorization.toString());
                    $("#HDeleteAuthorization").val(r.model.deleteAuthorization.toString());
                    $("#HSelectAuthorization").val(r.model.selectAuthorization.toString());
                    $("#modalUpdateMenuAuth").modal('show');
                }
                swal.close();
            },
            error: function () {

            }
        }
        );
    });


    // DÜZENLE TUŞU
    $(document).on('click', '#btnUpdateMenuAuth', function () {
        var swal = Swal.fire({
            title: "Please Wait...",
            html: "Your Operation Is Under Progress",
            timerProgressBar: true,
            didOpen: () => {

                Swal.showLoading();

            },
        });

        var formData = new FormData();

        var Id = $("#HId").val();
        var RoleName = $("#HRoleName option:selected").val();
        var MenuName = $("#HMenuName option:selected").val();
        var InsertAuthorization = $("#HInsertAuthorization option:selected").val();
        var UpdateAuthorization = $("#HUpdateAuthorization option:selected").val();
        var DeleteAuthorization = $("#HDeleteAuthorization option:selected").val();
        var SelectAuthorization = $("#HSelectAuthorization option:selected").val();

        formData.append("Id",Id)
        formData.append("RoleName", RoleName)
        formData.append("MenuName", MenuName)
        formData.append("InsertAuthorization", InsertAuthorization)
        formData.append("UpdateAuthorization", UpdateAuthorization)
        formData.append("DeleteAuthorization", DeleteAuthorization)
        formData.append("SelectAuthorization", SelectAuthorization)


        $.ajax({
            url: "/AdminPanel/MenuAuth/Update",
            type: "post",
            dataType: "json",
            processData: false,
            contentType: false,
            data: formData,
            success: function (r) {
                // ---------------Datatable Yok Edildi---------------
                $("#tblMenuAuths").DataTable().destroy();
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
                $("#modalUpdateMenuAuth").modal('hide');
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
                    url: "/AdminPanel/MenuAuth/Delete",
                    type: "post",
                    dataType: "json",
                    data: { id: id },
                    success: function (r) {
                        // ---------------Datatable Yok Edildi---------------

                        $("#tblMenuAuths").DataTable().destroy();
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
