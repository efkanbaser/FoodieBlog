function createTable() {
    var table;
    table = $("#tblTags").DataTable({
        "dom": "<'d-flex justify-content-between'lfB>rtip",
        "buttons": [
            "csv", "excel", "pdf", "print", "colvis",
            {
                text: 'Add Tag',
                className: 'btn btn-default',
                action: function () {
                    $('#modalAddTag').modal('show');
                },
            }
        ],
        "responsive": true,
        "lengthChange": true,
        "pageLength": 10,
        "autoWidth": false,
        ajax: { url: '/AdminPanel/Tag/List', type: 'post' },
        columns: [
            { data: 'id', visible: false },
            { data: 'tagName' },           
            { data: 'active' },
            { data: 'id' },
            { data: 'id' }
        ],
        columnDefs: [
            {
                targets: 2,
                render: function (data, type, row, meta) {

                    var TagId = row["id"];

                    if (data)
                        return '<input TagId=' + TagId + ' type="checkbox" data-on-text="ACTIVE" data-off-text="PASSIVE" name="my-checkbox" checked data-bootstrap-switch data-off-color="danger" class="chkActive" data-on-color="success" > ';

                    else
                        return '<input TagId=' + TagId + ' type="checkbox" data-on-text="ACTIVE" data-off-text="PASSIVE" name="my-checkbox" data-bootstrap-switch data-off-color="danger" class="chkActive" data-on-color="success" > ';
                }
            },

            {
                targets: 3,
                render: function (data, type, row, meta) {
                    return "<a class='btn btn-info btnEdit' TagId=" + data + ">   <i class='fas fa-pencil-alt'></i> EDIT</a > ";
                }
            },

            {
                targets: 4,
                render: function (data, type, row, meta) {
                    return "<a class='btn btn-danger btnDelete' TagId=" + data + "><i class='fas fa-trash'></i> DELETE</a > ";
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
    }).buttons().container().appendTo('#tblTags_wrapper .col-md-6:eq(0)');
    return table;
}


var table;
$(function () {
    table = createTable();
})

$("#btnAddTag").click(function () {

    var swal = Swal.fire({
        title: "Please Wait...",
        html: "Your Operation Is Under Progress",
        timerProgressBar: true,
        didOpen: () => {

            Swal.showLoading();

        },
    });
    var formData = new FormData();

    var TagName = $("#TagName").val();

    formData.append("TagName", TagName)

    $.ajax({
        url: "/AdminPanel/Tag/Add",
        type: "post",
        dataType: "json",
        processData: false,
        contentType: false,
        data: formData,
        success: function (r) {

            // ---------------Datatable Yok Edildi---------------
            $("#tblTags").DataTable().destroy();
            //-------------------------------------------------
            // ---------------Datatable Yeniden Oluşturuluyor.---------------
            table = createTable();

            //   $("#UstTagId").trigger("change"); // change eventı buradan tetiklendi
            // $("#modalUrunEkle").modal("hide");
            swal.close();
            if (r.result) {
                $("#TagName").val('');
                Swal.fire({
                    title: "Success",
                    text: r.mesaj,
                    icon: "success"
                });
                $('#modalAddTag').modal('hide');
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

        var id = $(this).attr('TagId');
        var aktifpasif = state;
        $.ajax({
            url: "/AdminPanel/Tag/ActiveInactive",
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

        var id = $(this).attr('TagId');
        $("#HId").val(id);

        $.ajax({
            url: "/AdminPanel/Tag/GetTag",
            type: "post",
            dataType: "json",
            data: { id: id },
            success: function (r) {

                if (r.result) {
                    $("#HTagName").val(r.model.TagName);
                    $("#modalUpdateTag").modal('show');
                }
                swal.close();
            },
            error: function () {

            }
        }
        );
    });


    // DÜZENLE TUŞU
    $(document).on('click', '#btnUpdateTag', function () {
        var swal = Swal.fire({
            title: "Please Wait...",
            html: "Your Operation Is Under Progress",
            timerProgressBar: true,
            didOpen: () => {

                Swal.showLoading();

            },
        });

        var formData = new FormData();

        var TagName = $("#HRoleName").val();
        var Id = $("#HId").val();

        formData.append("RoleName", RoleName)
        formData.append("Id", Id)


        $.ajax({
            url: "/AdminPanel/Tag/Update",
            type: "post",
            dataType: "json",
            processData: false,
            contentType: false,
            data: formData,
            success: function (r) {
                // ---------------Datatable Yok Edildi---------------
                $("#tblTags").DataTable().destroy();
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
                $("#modalUpdateTag").modal('hide');
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

                var id = $(this).attr('TagId');
                $("#HId").val(id);
                $.ajax({
                    url: "/AdminPanel/Tag/Delete",
                    type: "post",
                    dataType: "json",
                    data: { id: id },
                    success: function (r) {
                        // ---------------Datatable Yok Edildi---------------

                        $("#tblTags").DataTable().destroy();
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
