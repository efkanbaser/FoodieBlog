function createTable() {
    var table;
    table = $("#tblRoles").DataTable({
        "dom": "<'d-flex justify-content-between'lfB>rtip",
        "buttons": [
            "csv", "excel", "pdf", "print", "colvis",
            {
                text: 'Add Role',
                className: 'btn btn-default',
                action: function () {
                    $('#modalAddRole').modal('show');
                },
            }
        ],
        "responsive": true,
        "lengthChange": true,
        "pageLength": 10,
        "autoWidth": false,
        ajax: { url: '/AdminPanel/Role/List', type: 'post' },
        columns: [
            { data: 'id', visible: false },
            { data: 'roleName' },
            { data: 'active' },
            { data: 'id' },
            { data: 'id' }
        ],
        columnDefs: [
            {
                targets: 2,
                render: function (data, type, row, meta) {

                    var RoleId = row["id"];

                    if (data)
                        return '<input RoleId=' + RoleId + ' type="checkbox" data-on-text="ACTIVE" data-off-text="PASSIVE" name="my-checkbox" checked data-bootstrap-switch data-off-color="danger" class="chkActive" data-on-color="success" > ';

                    else
                        return '<input RoleId=' + RoleId + ' type="checkbox" data-on-text="ACTIVE" data-off-text="PASSIVE" name="my-checkbox" data-bootstrap-switch data-off-color="danger" class="chkActive" data-on-color="success" > ';
                }
            },

            {
                targets: 3,
                render: function (data, type, row, meta) {
                    return "<a class='btn btn-info btnEdit' RoleId=" + data + ">   <i class='fas fa-pencil-alt'></i> EDIT</a > ";
                }
            },

            {
                targets: 4,
                render: function (data, type, row, meta) {
                    return "<a class='btn btn-danger btnDelete' RoleId=" + data + "><i class='fas fa-trash'></i> DELETE</a > ";
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
    }).buttons().container().appendTo('#tblRoles_wrapper .col-md-6:eq(0)');
    return table;
}


var table;
$(function () {
    table = createTable();
})

$("#btnAddRole").click(function () {

    var swal = Swal.fire({
        title: "Please Wait...",
        html: "Your Operation Is Under Progress",
        timerProgressBar: true,
        didOpen: () => {

            Swal.showLoading();

        },
    });
    var formData = new FormData();

    var RoleName = $("#RoleName").val();

    formData.append("RoleName", RoleName)

    $.ajax({
        url: "/AdminPanel/Role/Add",
        type: "post",
        dataType: "json",
        processData: false,
        contentType: false,
        data: formData,
        success: function (r) {

            // ---------------Datatable Yok Edildi---------------
            $("#tblRoles").DataTable().destroy();
            //-------------------------------------------------
            // ---------------Datatable Yeniden Oluşturuluyor.---------------
            table = createTable();

            //   $("#UstRoleId").trigger("change"); // change eventı buradan tetiklendi
            // $("#modalUrunEkle").modal("hide");
            swal.close();
            if (r.result) {
                $("#RoleName").val('');
                Swal.fire({
                    title: "Success",
                    text: r.mesaj,
                    icon: "success"
                });
                $('#modalAddRole').modal('hide');
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
            title: "Please Wait...",
            html: "Your Operation Is Under Progress",
            timerProgressBar: true,
            didOpen: () => {

                Swal.showLoading();

            },
        });

        var id = $(this).attr('RoleId');
        var aktifpasif = state;
        $.ajax({
            url: "/AdminPanel/Role/ActiveInactive",
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

        var id = $(this).attr('RoleId');
        $("#HId").val(id);

        $.ajax({
            url: "/AdminPanel/Role/GetRole",
            type: "post",
            dataType: "json",
            data: { id: id },
            success: function (r) {

                if (r.result) {
                    $("#HRoleName").val(r.model.roleName);
                    $("#modalUpdateRole").modal('show');
                }
                swal.close();
            },
            error: function () {

            }
        }
        );
    });


    // DÜZENLE TUŞU
    $(document).on('click', '#btnUpdateRole', function () {
        var swal = Swal.fire({
            title: "Please Wait...",
            html: "Your Operation Is Under Progress",
            timerProgressBar: true,
            didOpen: () => {

                Swal.showLoading();

            },
        });

        var formData = new FormData();

        var RoleName = $("#HRoleName").val();
        var Id = $("#HId").val();

        formData.append("RoleName", RoleName)
        formData.append("Id", Id)


        $.ajax({
            url: "/AdminPanel/Role/Update",
            type: "post",
            dataType: "json",
            processData: false,
            contentType: false,
            data: formData,
            success: function (r) {
                // ---------------Datatable Yok Edildi---------------
                $("#tblRoles").DataTable().destroy();
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
                $("#modalUpdateRole").modal('hide');
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

                var id = $(this).attr('RoleId');
                $("#HId").val(id);
                $.ajax({
                    url: "/AdminPanel/Role/Delete",
                    type: "post",
                    dataType: "json",
                    data: { id: id },
                    success: function (r) {
                        // ---------------Datatable Yok Edildi---------------

                        $("#tblRoles").DataTable().destroy();
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
