function createTable() {
    var table;
    table = $("#tblUserRoles").DataTable({
        "dom": "<'d-flex justify-content-between'lfB>rtip",
        "buttons": [
            "csv", "excel", "pdf", "print", "colvis"
        ],
        "responsive": true,
        "lengthChange": true,
        "pageLength": 10,
        "autoWidth": false,
        ajax: { url: '/AdminPanel/UserRole/List', type: 'post' },
        columns: [
            { data: 'id', visible: false },
            { data: 'userName' },            
            { data: 'roleName' },            
            { data: 'active' },
            { data: 'id' },
            { data: 'id' }
        ],
        columnDefs: [
            {
                targets: 3,
                render: function (data, type, row, meta) {

                    var userroleId = row["id"];

                    if (data)
                        return '<input userroleId=' + userroleId + ' type="checkbox" data-on-text="ACTIVE" data-off-text="PASSIVE" name="my-checkbox" checked data-bootstrap-switch data-off-color="danger" class="chkActive" data-on-color="success" > ';

                    else
                        return '<input userroleId=' + userroleId + ' type="checkbox" data-on-text="ACTIVE" data-off-text="PASSIVE" name="my-checkbox" data-bootstrap-switch data-off-color="danger" class="chkActive" data-on-color="success" > ';
                }
            },

            {
                targets: 4,
                render: function (data, type, row, meta) {
                    return "<a class='btn btn-info btnEdit' userroleId=" + data + ">   <i class='fas fa-pencil-alt'></i> EDIT</a > ";
                }
            },

            {
                targets: 5,
                render: function (data, type, row, meta) {
                    return "<a class='btn btn-danger btnDelete' userroleId=" + data + "><i class='fas fa-trash'></i> DELETE</a > ";
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
    }).buttons().container().appendTo('#tblUserRoles_wrapper .col-md-6:eq(0)');
    return table;
}


var table;
$(function () {
    table = createTable();
})



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

        var id = $(this).attr('userroleId');
        var aktifpasif = state;
        $.ajax({
            url: "/AdminPanel/UserRole/ActiveInactive",
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

        var id = $(this).attr('userroleId');
        $("#HId").val(id);

        $.ajax({
            url: "/AdminPanel/UserRole/GetUserRole",
            type: "post",
            dataType: "json",
            data: { id: id },
            success: function (r) {

                if (r.result) {
                    $("#HUserName").text(r.model.userName);
                    /*$("#HRoleName").val(r.model.roleId).trigger("change");*/
                    $("#modalUpdateUserRole").modal('show');
                }
                swal.close();
            },
            error: function () {

            }
        }
        );
    });


    // DÜZENLE TUŞU
    $(document).on('click', '#btnUpdateUserRole', function () {
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
        var RoleName = $("#HRoleName").val();
        var Id = $("#HId").val();

        formData.append("UserName", UserName)
        formData.append("RoleName", RoleName)
        formData.append("Id", Id)


        $.ajax({
            url: "/AdminPanel/UserRole/Update",
            type: "post",
            dataType: "json",
            processData: false,
            contentType: false,
            data: formData,
            success: function (r) {
                // ---------------Datatable Yok Edildi---------------
                $("#tblUserRoles").DataTable().destroy();
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
                $("#modalUpdateUserRole").modal('hide');
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

                var id = $(this).attr('userroleId');
                $("#HId").val(id);
                $.ajax({
                    url: "/AdminPanel/UserRole/Delete",
                    type: "post",
                    dataType: "json",
                    data: { id: id },
                    success: function (r) {
                        // ---------------Datatable Yok Edildi---------------

                        $("#tblUserRoles").DataTable().destroy();
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
