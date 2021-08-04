//loading every single ajax call
$(function () {
    $("#loaderbody").addClass('hide');
    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});

//animate buttons using animate.css
function AnimateButton(elementId) {
    var effects = 'animate__animated animate__headShake';
    var effectsend = 'animationend';
    $('#'.concat(elementId)).hover(function () {
        $(this).addClass(effects).on(effectsend, function () {
            $(this).removeClass(effects);
        });
    });
}

//animate buttons using animate.css
function AnimateButton2(elementId) {
    var effects = 'animate__animated animate__headShake';
    var effectsend = 'animationend';
    $('#'.concat(elementId)).hover(
        function () { $(this).toggleClass(effects); }
    );
}

//##############################################
function SetNumericValueToZeroIfEmpty(elementId) {
    if ($(elementId).val() == '') {
        $(elementId).val(0);
    }
}
//##############################################
var Popup, dataTable, datatable2
//function GetDataTable(TableName, ActionURL, TableColumnsArray) {
//    dataTable = $('#'.concat(TableName)).DataTable({
//        "scrollX": "200px",
//        "ajax": {
//            "url": ActionURL,
//            "type": "GET",
//            "datatype": "json"
//        },
//        "columns": TableColumnsArray,
//    });
//}
function GetDataTableWidthPDF(TableName, ActionURL, TableColumnsArray) {
    dataTable = $('#'.concat(TableName)).DataTable({
        "scrollX": "200px",
        "ajax": {
            "url": ActionURL,
            "type": "GET",
            "datatype": "json"
        },
        "columns": TableColumnsArray,
        "dom": "lBfrtip",
        buttons: [
            {
                extend: 'excel',
                className: 'dtbutton',
                text: '<i class="fa fa-file-excel-o" aria-hidden="true"></i> Excel'
            },
            {
                extend: 'pdf',
                text: '<i class="fa fa-file-pdf-o" aria-hidden="true"></i> PDF'
            },
            {
                extend: 'print',
                text: '<i class="fa fa-print" aria-hidden="true"></i> Print'
            }
        ]
    });
}

function DropDownSuggestions(dropDownId) {
    $('#'.concat(dropDownId)).chosen({
        rtl: true,
        disable_search_threshold: 2,
        width: "100%"
    })
}

function GetDataTable(TableName, ActionURL, TableColumnsArray) {
    dataTable = $('#'.concat(TableName)).DataTable({
        //select: 'single',
        stateSave: true,
        "processing": true,
        "scrollX": "200px",
        "ajax": {
            "url": ActionURL,
            "type": "GET",
            "datatype": "json"

        },
        "columns": TableColumnsArray,
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        "order": [[1, 'asc']],
        "language": {
            "processing": "فضلا انتظر ...",
            "emptyTable": "لا يوجد بيانات فضلا اضغط على زر <b> اضافة</b>",
            "search": "البحث  ",
            "lengthMenu": "عرض  _MENU_  نتيجة",
            "zeroRecords": "لا يوجد نتائج لهذا البحث",
            "info": "عرض صفحة _PAGE_ من _PAGES_ - العدد الكلي _MAX_",
            "infoEmpty": "يتم التحميل ...",
            "infoFiltered": "(تم البحث من _MAX_ نتيجة)",
            "paginate": {
                "previous": "السابق",
                "next": "التالي"
            }
        }
    });

    $('#'.concat(TableName).concat(' tbody'))
        .on('mouseenter', 'td', function () {
            var colIdx = dataTable.cell(this).index().column;

            $(dataTable.cells().nodes()).removeClass('highlight');
            $(dataTable.column(colIdx).nodes()).addClass('highlight');
        });


    dataTable.on('order.dt search.dt', function () {
        dataTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

}

function GetServerSideDataTableCustomeSearch(TableName, ActionURL, TableColumnsArray, fromDate, toDate, SearchQuery) {
    dataTable = $('#'.concat(TableName)).DataTable({
        "scrollX": "200px",

        "ajax": {
            "url": ActionURL,
            "type": "POST",
            "datatype": "json",
            "data": function (data) {
                var startDate = $('#'.concat(fromDate)).val();
                var endDate = $('#'.concat(toDate)).val();
                var search = $('#'.concat(SearchQuery)).val();

                data.startDate = startDate;
                data.endDate = endDate;
                data.searchQuery = search;
            },
        },
        "columns": TableColumnsArray,
        //"columnDefs": [{
        //    "searchable": false,
        //    "orderable": false,
        //    "targets": 0
        //}],
        "serverSide": "true",
        "order": [[0, "asc"]],
        "processing": "true",
        "language": {
            "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>",
            "emptyTable": "لا يوجد بيانات فضلا اضغط على زر <b> اضافة</b>",
            "search": "البحث  ",
            "lengthMenu": "عرض  _MENU_  نتيجة",
            "zeroRecords": "لا يوجد نتائج لهذا البحث",
            "info": "عرض صفحة _PAGE_ من _PAGES_ - العدد الكلي _MAX_",
            "infoEmpty": "يتم التحميل ...",
            "infoFiltered": "(تم البحث من _MAX_ نتيجة)",
            "paginate": {
                "previous": "السابق",
                "next": "التالي"
            }
        },
        "dom": "lBfrtip",
        buttons: [
            {
                extend: 'excel',
                className: 'dtbutton',
                text: '<i class="fa fa-file-excel-o" aria-hidden="true"></i> Excel'
            },          
            {
                extend: 'print',
                text: '<i class="fa fa-print" aria-hidden="true"></i> Print'
            }
        ]
    });

    $('#'.concat(TableName).concat(' tbody'))
        .on('mouseenter', 'td', function () {
            var colIdx = dataTable.cell(this).index().column;

            $(dataTable.cells().nodes()).removeClass('highlight');
            $(dataTable.column(colIdx).nodes()).addClass('highlight');
        });


    //dataTable.on('order.dt search.dt', function () {
    //    dataTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
    //        cell.innerHTML = i + 1;
    //    });
    //}).draw();

}


function GetServerSideDataTable(TableName, ActionURL, TableColumnsArray) {
    dataTable = $('#'.concat(TableName)).DataTable({
        "scrollX": "200px",
        "ajax": {
            "url": ActionURL,
            "type": "POST",
            "datatype": "json"

        },
        "columns": TableColumnsArray,
        //"columnDefs": [{
        //    "searchable": false,
        //    "orderable": false,
        //    "targets": 0
        //}],
        "serverSide": "true",
        "order": [[0, "asc"]],
        "processing": "true",
        "language": {
            "processing": "فضلا انتظر ...",
            "emptyTable": "لا يوجد بيانات فضلا اضغط على زر <b> اضافة</b>",
            "search": "البحث  ",
            "lengthMenu": "عرض  _MENU_  نتيجة",
            "zeroRecords": "لا يوجد نتائج لهذا البحث",
            "info": "عرض صفحة _PAGE_ من _PAGES_ - العدد الكلي _MAX_",
            "infoEmpty": "يتم التحميل ...",
            "infoFiltered": "(تم البحث من _MAX_ نتيجة)",
            "paginate": {
                "previous": "السابق",
                "next": "التالي"
            }
        }
    });

    $('#'.concat(TableName).concat(' tbody'))
        .on('mouseenter', 'td', function () {
            var colIdx = dataTable.cell(this).index().column;

            $(dataTable.cells().nodes()).removeClass('highlight');
            $(dataTable.column(colIdx).nodes()).addClass('highlight');
        });


    //dataTable.on('order.dt search.dt', function () {
    //    dataTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
    //        cell.innerHTML = i + 1;
    //    });
    //}).draw();

}


//Datatable row on click event
////dont forget to include select css file and js
//var table = $('#expensesTable').DataTable();
//table.on('select', function (e, dt, type, indexes) {
//    if (type === 'row') {
//        var data = table.rows(indexes).data().pluck('Id');
//        console.log(data[0]);
//        // do something with the ID of the selected items
//    }
//});




function GetDataTable2(TableName, ActionURL, TableColumnsArray) {
    dataTable2 = $('#'.concat(TableName)).DataTable({
        //select: 'single',
        stateSave: true,
        "processing": true,
        "scrollX": "200px",
        "ajax": {
            "url": ActionURL,
            "type": "GET",
            "datatype": "json"

        },
        "columns": TableColumnsArray,
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        "order": [[1, 'asc']],
        "language": {
            "processing": "فضلا انتظر ...",
            "emptyTable": "لا يوجد بيانات فضلا اضغط على زر <b> اضافة</b>",
            "search": "البحث  ",
            "lengthMenu": "عرض  _MENU_  نتيجة",
            "zeroRecords": "لا يوجد نتائج لهذا البحث",
            "info": "عرض صفحة _PAGE_ من _PAGES_ - العدد الكلي _MAX_",
            "infoEmpty": "لا يوجد بيانات",
            "infoFiltered": "(تم البحث من _MAX_ نتيجة)",
            "paginate": {
                "previous": "السابق",
                "next": "التالي"
            }
        }
    });

    $('#'.concat(TableName).concat(' tbody'))
        .on('mouseenter', 'td', function () {
            var colIdx2 = dataTable2.cell(this).index().column;

            $(dataTable2.cells().nodes()).removeClass('highlight');
            $(dataTable2.column(colIdx2).nodes()).addClass('highlight');
        });


    dataTable2.on('order.dt search.dt', function () {
        dataTable2.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}



function convertJsonDateToShortDate(data) {
    // This function converts a json date to a short date
    // e.g. /Date(1538377200000)/ to 10/1/2018
    if (data != null) {
        const dateValue = new Date(parseInt(data.substr(6)));
        month = '' + (dateValue.getMonth() + 1),
           day = '' + dateValue.getDate(),
           year = dateValue.getFullYear();
        return [year, month, day].join('-');
    }
    return '';

}

function convertJsonDateToShortDateTime(data) {
    // This function converts a json date to a short date
    // e.g. /Date(1538377200000)/ to 10/1/2018
    if (data != null) {
        const dateValue = new Date(parseInt(data.substr(6)));
        month = '' + (dateValue.getMonth() + 1),
       day = '' + dateValue.getDate(),
       year = dateValue.getFullYear(),
        hour = dateValue.getHours(),
        minute = dateValue.getMinutes();

        let thedate = [year, month, day].join('-') + ' ' + '(' + [hour, minute].join(':') + ')';

        return thedate;
    }
    return '';

}

function PopupForm(url) {
    var formDiv = $('<div/>');
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                show: {
                    effect: "fade",
                    duration: 300
                },
                hide: {
                    effect: "fade",
                    duration: 300
                },
                autoOpen: true,
                modal: true,
                //title:'hihi',
                resizable: false,
                height: 'auto',
                width: 'auto',
                close: function () {
                    Popup.dialog('destroy').remove();
                    //dataTable2.ajax.reload(null, false);
                    //updateDatatable2();
                    //dataTable.ajax.reload();
                    //dataTable2.ajax.reload();
                    //dataTable.draw();
                }
            });
        });
}

function updateDatatable2() {
    if (datatable2 != null || !isNaN(datatable2)) {
        datatable2.ajax.reload(null, false);
    }
}

function SubmitForm(form) {
    //validate
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: "POST",
            url: form.action,
            data: $(form).serialize(),
            success: SuccessFunc
        });
    }
    return false;
}
function SubmitFormWithImage(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var ajaxConfig = {
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: SuccessFunc,
            error: SuccessFunc,
            complete: SuccessFunc,
        }
        $.ajax(ajaxConfig);
    }
    return false;
}

function ShowImagePreview(imageUploader, previewImage) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}
function Download(url) {
    $.ajax({
        type: "GET",
        url: url
    })
}

//function Delete(url, deletedData) {
//    if (confirm('هل انت متأكد من رغبتك في مسح هذا ' + deletedData)) {
//        $.ajax({
//            type: "POST",
//            url: url,
//            success: SuccessFunc
//        })
//    }
//}

var locale = {
    OK: 'I Suppose',
    CONFIRM: 'Go Ahead',
    CANCEL: 'Maybe Not'
};
bootbox.addLocale('custom', locale);
function Delete(url, deletedData) {
    bootbox.confirm({
        onEscape: 'true',
        locale: 'custom',
        title: "مسح",
        message: "هل انت متأكد من رغبتك في مسح هذا " + deletedData,
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> الغاء',
                className: 'btn-default',
                callback: function () {
                    bootbox.hideAll();
                }
            },
            confirm: {
                label: '<i class="fa fa-check"></i> تأكيد المسح',
                className: 'btn-danger',
                callback: function () {
                    $.ajax({
                        type: "POST",
                        url: url,
                        success: SuccessFunc
                    })
                }
            }
        },
        callback: function (result) {
            console.log('This was logged in the callback: ' + url);
            if (result) {
                $.ajax({
                    type: "POST",
                    url: url,
                    success: SuccessFunc
                })
            }
        }
    });
}



function DisableOrder(url) {
    bootbox.confirm({
        onEscape: 'true',
        locale: 'custom',
        title: "انهاء رحلة",
        message: "هل انت متأكد من رغبتك في انهاء هذا الرحلة؟ ركز ابوس ايدك قبل ما تنهية او اصبر يوم كدة",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> خروج',
                className: 'btn-default',
                callback: function () {
                    bootbox.hideAll();
                }
            },
            confirm: {
                label: '<i class="fa fa-check"></i> انهاء الرحلة',
                className: 'btn-danger',
                callback: function () {
                    $.ajax({
                        type: "POST",
                        url: url,
                        success: SuccessFunc
                    })
                }
            }
        },
        callback: function (result) {
            console.log('This was logged in the callback: ' + url);
            if (result) {
                $.ajax({
                    type: "POST",
                    url: url,
                    success: SuccessFunc
                })
            }
        }
    });
}
function ToggleStatus(url) {
    bootbox.confirm({
        onEscape: 'true',
        locale: 'custom',
        title: 'تحذير',
        message: 'هل انت متأكد من رغبتك في تغيير الحالة؟',
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> خروج',
                className: 'btn-default',
                callback: function () {
                    bootbox.hideAll();
                }
            },
            confirm: {
                label: '<i class="fa fa-check"></i> تأكيد',
                className: 'btn-success',
                callback: function () {
                    $.ajax({
                        type: "POST",
                        url: url,
                        success: SuccessFunc
                    })
                }
            }
        },
        callback: function (result) {
            console.log('This was logged in the callback: ' + url);
            if (result) {
                $.ajax({
                    type: "POST",
                    url: url,
                    success: SuccessFunc
                })
            }
        }
    });
}


function ExportThePayment(url) {
    bootbox.confirm({
        onEscape: 'true',
        locale: 'custom',
        title: 'تحذير',
        message: 'هل انت متأكد من رغبتك في توريد كل مبيعات هذا الموزع؟',
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> خروج',
                className: 'btn-default',
                callback: function () {
                    bootbox.hideAll();
                }
            },
            confirm: {
                label: '<i class="fa fa-check"></i> تأكيد',
                className: 'btn-success',
                callback: function () {
                    $.ajax({
                        type: "POST",
                        url: url,
                        success: SuccessFunc
                    })
                }
            }
        },
        callback: function (result) {
            console.log('This was logged in the callback: ' + url);
            if (result) {
                $.ajax({
                    type: "POST",
                    url: url,
                    success: SuccessFunc
                })
            }
        }
    });
}


function Redirect(url) {
    window.location = url;
}


function SuccessFunc(data) {
    if (data.success) {
        try { Popup.dialog('close'); } catch (e) { }
        dataTable.ajax.reload(null, false);
        try { dataTable2.ajax.reload(null, false); } catch (e) { }
        $.notify(data.message, {
            globalPosition: "bottom right",
            className: "success",
        });
    }
    if (data.error) {
        //try { Popup.dialog('close'); } catch (e) { }
        $.notify(data.message, {
            globalPosition: "bottom right",
            className: "error",
        });
    }
}



/*$(document).ready(function () {
    dataTable = $('#clubTable').DataTable({

        "ajax": {
            "url": "/Club/ViewAllClubs",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "ID" },
            { "data": "ClubName" },
            {
                "data": "ID",
                "render": function (ID) {
                    return "<a class='btn btn-success btn-sm' style='margin-right:5px;' onclick=PopupForm('@Url.Action("AddEditClub","Club")/" + ID + "')><i class='fa fa-pencil'></i> تعديل </a>\
                                    <a class='btn btn-danger btn-sm' style='margin-right:5px;' onclick=Delete('@Url.Action("DeleteClub","Club")/" + ID + "','النادي؟')><i class='fa fa-trash'></i> حذف </a>"
                },
                "orderable": false,
                "searchable": false,
                "width": "150px"
            },
        ],
        "language": {
            "emptyTable": "لا يوجد نوادي فضلا اضغط على زر <b>اضافة نادي</b>",
            "search": "البحث  ",
            "lengthMenu": "عرض  _MENU_  نتيجة",
            "zeroRecords": "لا يوجد نتائج لهذا البحث",
            "info": "عرض صفحة _PAGE_ من _PAGES_",
            "infoEmpty": "لا توجد نتائج",
            "infoFiltered": "(تم البحث من _MAX_ نتيجة)",
            "paginate": {
                "previous": "السابق",
                "next": "التالي"
            }
        },

    });
});*/
//Auto Refresh Datatable:
//setInterval(function () {
//    dataTable.ajax.reload(null, false); // user paging is not reset on reload
//});