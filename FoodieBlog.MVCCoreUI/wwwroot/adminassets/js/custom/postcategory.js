function createTable() {
    var table;
    table = $("#tblPostCategorys").DataTable({
        "dom": "<'d-flex justify-content-between'lfB>rtip",
        "buttons": [
            "csv", "excel", "pdf", "print", "colvis"
        ],
        "responsive": true,
        "lengthChange": true,
        "pageLength": 10,
        "autoWidth": false,
        ajax: { url: '/AdminPanel/PostCategory/List', type: 'post' },
        columns: [
            { data: 'id', visible: false },
            { data: 'postName' },
            { data: 'categoryNames' },
            { data: 'id' },
            { data: 'id' }
        ],
        columnDefs: [
            {
                targets: 2,
                render: function (data, type, row, meta) {

                    if (Array.isArray(data)) {
                        return data.join(', ');
                    }
                    return data; // Fallback in case data is not an array
                   
                }
            },

            {
                targets: 3,
                render: function (data, type, row, meta) {
                    return "<a class='btn btn-info btnEdit' postcategoryid=" + data + ">   <i class='fas fa-pencil-alt'></i> ADD CATEGORY</a > ";
                }
            },

            {
                targets: 4,
                render: function (data, type, row, meta) {
                    return "<a class='btn btn-danger btnDelete' postcategoryid=" + data + "><i class='fas fa-trash'></i> DELETE CATEGORY</a > ";
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
    }).buttons().container().appendTo('#tblPostCategorys_wrapper .col-md-6:eq(0)');
    return table;
}


var table;
$(function () {
    table = createTable();
})



// Düzenle tuşuna ilk tıklandığında modala gelen veriler
$(document).on('click', '.btnEdit', function () {
    var swal = Swal.fire({
        title: "Please Wait...",
        html: "Your Operation Is Under Progress",
        timerProgressBar: true,
        didOpen: () => {

            Swal.showLoading();

        },
    });

    var id = $(this).attr('PostCategoryId');
    $("#HId").val(id);

    $.ajax({
        url: "/AdminPanel/PostCategory/GetPostCategory",
        type: "post",
        dataType: "json",
        data: { id: id },
        success: function (r) {

            if (r.result) {
                $('#HCategoryName').text('');
                $("#HPostName").text('');

                var iterations = r.model.length;

                for (const element of r.model) {
                    if (!--iterations)
                        $('#HCategoryName').append(element.categoryName.toString());
                    else
                        $('#HCategoryName').append(element.categoryName.toString() + ", ");
                }
                $("#HPostName").append(r.model[0].postName);

                for (const element of r.model) {
                    $('#HCategorySelect option[value=' + element.categoryName.toString() + ']').remove();    
                }

                $("#modalUpdatePostCategory").modal('show');
            }
            swal.close();
        },
        error: function () {

        }
    }
    );
});


 // DÜZENLE TUŞU
 $(document).on('click', '#btnUpdatePostCategory', function () {
     var swal = Swal.fire({
         title: "Please Wait...",
         html: "Your Operation Is Under Progress",
         timerProgressBar: true,
         didOpen: () => {

             Swal.showLoading();

         },
     });

     var formData = new FormData();

     var PostName = $("#HPostName").text();
     var CategorySelect = $("#HCategorySelect").val();
     var Id = $("#HId").val();

     formData.append("PostName", PostName)
     formData.append("CategorySelect", CategorySelect)
     formData.append("Id", Id)


     $.ajax({
         url: "/AdminPanel/PostCategory/Update",
         type: "post",
         dataType: "json",
         processData: false,
         contentType: false,
         data: formData,
         success: function (r) {
             // ---------------Datatable Yok Edildi---------------
             $("#tblPostCategorys").DataTable().destroy();
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
             $("#modalUpdatePostCategory").modal('hide');
         },
         error: function () {

         }
     }
     );
 });







// Sil tuşuna ilk tıklandığında modala gelen veriler
$(document).on('click', '.btnDelete', function () {
    var swal = Swal.fire({
        title: "Please Wait...",
        html: "Your Operation Is Under Progress",
        timerProgressBar: true,
        didOpen: () => {

            Swal.showLoading();

        },
    });

    var id = $(this).attr('PostCategoryId');
    $("#HId").val(id);

    $.ajax({
        url: "/AdminPanel/PostCategory/GetPostCategory",
        type: "post",
        dataType: "json",
        data: { id: id },
        success: function (r) {

            if (r.result) {
                $('#HCategoryNameDelete').text('');
                $("#HPostNameDelete").text('');
                $('#HCategorySelectDelete').empty();

                var iterations = r.model.length;

                for (const element of r.model) {
                    if (!--iterations)
                        $('#HCategoryNameDelete').append(element.categoryName.toString());
                    else
                        $('#HCategoryNameDelete').append(element.categoryName.toString() + ", ");
                }
                $("#HPostNameDelete").append(r.model[0].postName);

                for (const element of r.model) {
                    $('#HCategorySelectDelete').append($('<option>', {
                        value: element.categoryName.toString(),
                        text: element.categoryName.toString()
                    }));
                }

                $("#modalDeletePostCategory").modal('show');
            }
            swal.close();
        },
        error: function () {

        }
    }
    );
});



    // SİL TUŞU
$(document).on('click', '#btnDeletePostCategory', function () {
    var swal = Swal.fire({
        title: "Please Wait...",
        html: "Your Operation Is Under Progress",
        timerProgressBar: true,
        didOpen: () => {

            Swal.showLoading();

        },
    });

    var formData = new FormData();

    var PostName = $("#HPostNameDelete").text();
    var CategorySelect = $("#HCategorySelectDelete").val();
    var Id = $("#HIdDelete").val();

    formData.append("PostName", PostName)
    formData.append("CategorySelect", CategorySelect)
    formData.append("Id", Id)


    $.ajax({
        url: "/AdminPanel/PostCategory/Delete",
        type: "post",
        dataType: "json",
        processData: false,
        contentType: false,
        data: formData,
        success: function (r) {
            // ---------------Datatable Yok Edildi---------------
            $("#tblPostCategorys").DataTable().destroy();
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
            $("#modalDeletePostCategory").modal('hide');
        },
        error: function () {

        }
    }
    );
});

