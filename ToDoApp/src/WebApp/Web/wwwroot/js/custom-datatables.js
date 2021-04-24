function formatData(data) {
    var splitString = data.split(" as ");

    if (splitString.length === 1) {
        splitString = data.split(" AS ");
    }

    if (splitString.length === 1) {
        splitString = data.split(" aS ");
    }

    if (splitString.length === 1) {
        splitString = data.split(" As ");
    }

    var splitStringValue = splitString[0].replace(" ", "").replace("[", "").replace("]", "");

    return splitStringValue;
}

function formatTitle(data) {
    var splitString = data.split(" as ");

    if (splitString.length === 1) {
        splitString = data.split(" AS ");
    }

    if (splitString.length === 1) {
        splitString = data.split(" aS ");
    }

    if (splitString.length === 1) {
        splitString = data.split(" As ");
    }

    if (splitString.length > 1) {
        return _.startCase(splitString[1].replace("[", "").replace("]", ""));
    }
    return _.startCase(splitString[0]);
}

function loadDatatables(componentId, controllerName, url, columns, actions) {
    url = baseUrl + url;
    var baseControllerName = baseUrl + "/" + controllerName;

    var statusData = [
        { 'name': true, 'title': 'Active', 'css_class': 'label label-lg font-weight-bold label-light-success label-inline' },
        { 'name': false, 'title': 'Inactive', 'css_class': 'label label-lg font-weight-bold label-light-danger label-inline' }
    ];

    var templates = [
        {
            "name": "Edit",
            "class_button": "btn btn-sm btn-primary mt-2",
            "class_icon": '<i class="fas fa-edit"></i>'
        },
        {
            "name": "Delete",
            "class_button": "btn btn-sm btn-danger mt-2",
            "class_icon": '<i class="fas fa-trash text-white"></i>'
        },
        {
            "name": "Details",
            "class_button": "btn btn-sm btn-success mt-2",
            "class_icon": '<i class="fas fa-list-ul"></i>'
        }
    ];

    var dataColumns = [];

    for (let x = 0; x < columns.length; x++) {
        var item = {};

        if (columns[x].type === "boolean") {
            item = {
                data: formatData(columns[x].name),
                title: formatTitle(columns[x].name),
                name: columns[x].type,
                autoWidth: true,
                orderable: false,
                searchable: false,
                render: function (data, type, row) {
                    var template = statusData.filter(p => p.name == data);
                    if (template.length > 0) {
                        return '<span class="' + template[0].css_class + '">' + template[0].title + '</span>';
                    }
                    else {
                        return data;
                    }
                }
            }
        }
        else if (columns[x].type === "datetime") {
            item = {
                data: formatData(columns[x].name),
                title: formatTitle(columns[x].name),
                name: columns[x].type,
                orderable: true,
                searchable: true,
                render: function (data, type, row) {
                    return moment(data).format("YYYY-MM-DD HH:mm:ss");
                }
            }
        }
        else if (columns[x].type === "date") {
            item = {
                data: formatData(columns[x].name),
                title: formatTitle(columns[x].name),
                name: columns[x].type,
                orderable: true,
                searchable: true,
                render: function (data, type, row) {
                    return moment(data).format("YYYY-MM-DD");
                }
            }
        }
        else if (columns[x].type === "money") {
            item = {
                data: formatData(columns[x].name),
                title: formatTitle(columns[x].name),
                name: columns[x].type,
                orderable: true,
                searchable: true,
                render: function (data, type, row) {
                    if (data === null) {
                        return "";
                    }
                    else {
                        return data.toLocaleString();
                    }
                }
            }
        }
        else {
            if (formatData(columns[x].name).includes(',') && columns[x].type == "string") {
                var formatDataResults = formatData(columns[x].name).split(",");
                item = {
                    data: formatDataResults[formatDataResults.length - 1],
                    title: formatTitle(columns[x].name),
                    name: columns[x].type,
                    orderable: true,
                    searchable: true,
                    render: function (data, type, row) {
                        if (typeof data === 'undefined') {
                            return "";
                        }
                        else {
                            var result = "<span>";
                            for (let i = 0; i < formatDataResults.length; i++) {
                                var splitString = formatDataResults[i].split(".");
                                result = result + row[splitString[0]][splitString[1]];

                                if ((i + 1) < formatDataResults.length) {
                                    result = result + " - ";
                                }
                            }

                            return result + "</span>";
                        }
                    }
                }
            }
            else {
                if (formatData(columns[x].name).split(".").length > 2 && !formatData(columns[x].name).includes(',')) {
                    var formatDataColumns = formatData(columns[x].name).split(".");
                    item = {
                        data: "",
                        title: formatTitle(columns[x].name),
                        name: columns[x].type,
                        orderable: false,
                        searchable: false,
                        render: function (data, type, row) {
                            if (row[formatDataColumns[0]] != null) {
                                if (formatData(columns[x].name).split(".").length == 4) {
                                    var formatDataColumnsAll = formatData(columns[x].name).split(".");
                                    return row[formatDataColumnsAll[0]][formatDataColumnsAll[1]][formatDataColumnsAll[2]][formatDataColumnsAll[3]];
                                }
                                else if (formatData(columns[x].name).split(".").length == 5) {
                                    var formatDataColumnsAll = formatData(columns[x].name).split(".");
                                    return row[formatDataColumnsAll[0]][formatDataColumnsAll[1]][formatDataColumnsAll[2]][formatDataColumnsAll[3]][formatDataColumnsAll[4]];
                                }
                                else if (formatData(columns[x].name).split(".").length == 6) {
                                    var formatDataColumnsAll = formatData(columns[x].name).split(".");
                                    return row[formatDataColumnsAll[0]][formatDataColumnsAll[1]][formatDataColumnsAll[2]][formatDataColumnsAll[3]][formatDataColumnsAll[4]][formatDataColumnsAll[5]];
                                }
                                else if (formatData(columns[x].name).split(".").length == 7) {
                                    var formatDataColumnsAll = formatData(columns[x].name).split(".");
                                    return row[formatDataColumnsAll[0]][formatDataColumnsAll[1]][formatDataColumnsAll[2]][formatDataColumnsAll[3]][formatDataColumnsAll[4]][formatDataColumnsAll[5]][formatDataColumnsAll[6]];
                                }
                                else if (formatData(columns[x].name).split(".").length == 8) {
                                    var formatDataColumnsAll = formatData(columns[x].name).split(".");
                                    return row[formatDataColumnsAll[0]][formatDataColumnsAll[1]][formatDataColumnsAll[2]][formatDataColumnsAll[3]][formatDataColumnsAll[4]][formatDataColumnsAll[5]][formatDataColumnsAll[6]][formatDataColumnsAll[7]];
                                }
                                else if (formatData(columns[x].name).split(".").length == 9) {
                                    var formatDataColumnsAll = formatData(columns[x].name).split(".");
                                    return row[formatDataColumnsAll[0]][formatDataColumnsAll[1]][formatDataColumnsAll[2]][formatDataColumnsAll[3]][formatDataColumnsAll[4]][formatDataColumnsAll[5]][formatDataColumnsAll[6]][formatDataColumnsAll[7]][formatDataColumnsAll[8]];
                                }
                                else if (formatData(columns[x].name).split(".").length == 10) {
                                    var formatDataColumnsAll = formatData(columns[x].name).split(".");
                                    return row[formatDataColumnsAll[0]][formatDataColumnsAll[1]][formatDataColumnsAll[2]][formatDataColumnsAll[3]][formatDataColumnsAll[4]][formatDataColumnsAll[5]][formatDataColumnsAll[6]][formatDataColumnsAll[7]][formatDataColumnsAll[8]][formatDataColumnsAll[9]];
                                }
                                else if (formatData(columns[x].name).split(".").length == 11) {
                                    var formatDataColumnsAll = formatData(columns[x].name).split(".");
                                    return row[formatDataColumnsAll[0]][formatDataColumnsAll[1]][formatDataColumnsAll[2]][formatDataColumnsAll[3]][formatDataColumnsAll[4]][formatDataColumnsAll[5]][formatDataColumnsAll[6]][formatDataColumnsAll[7]][formatDataColumnsAll[8]][formatDataColumnsAll[9]][formatDataColumnsAll[10]];
                                }

                                return row[formatDataColumns[0]][formatDataColumns[1]][formatDataColumns[2]];
                            }

                            return "";
                        }
                    }
                }
                else {
                    item = {
                        data: formatData(columns[x].name),
                        title: formatTitle(columns[x].name),
                        name: columns[x].type,
                        orderable: true,
                        searchable: true,
                        render: function (data, type, row) {
                            if (typeof data === 'undefined') {
                                return "";
                            }
                            else {
                                return data;
                            }
                        }
                    }
                }
            }
        }

        dataColumns.push(item);
    }

    var actionObjects = {
        data: "Actions",
        title: "Actions",
        width: 110,
        responsivePriority: -1,
        orderable: false,
        searchable: false,
        render: function (data, type, row) {
            var result = "";
            for (let i = 0; i < actions.length; i++) {
                var template = templates.filter(p => p.name == actions[i]);
                if (template.length > 0) {
                    if (actions[i].toLowerCase() == "delete") {
                        var url = "/" + controllerName + "/" + actions[i] + "/" + row.Id;
                        result = result + '\
                                    <a href="'+ url + '" class="' + template[0].class_button + '" title="' + title + '">\
                                      '+ template[0].class_icon + ' \
                                    </a>';
                    }
                    else if (actions[i] == "DetailIdeation") {
                        title = "Detail Ideation";

                        result = result + '\
                                    <a href="'+ baseControllerName + '/Document/' + row.Id + '" class="' + template[0].class_button + '" title="' + title + '">\
                                      '+ template[0].class_icon + ' \
                                    </a>';
                    }
                    else if (actions[i] == "DetailProject") {
                        title = "Detail Project";

                        result = result + '\
                                    <a href="'+ baseControllerName + '/Summary/' + row.Id + '" class="' + template[0].class_button + '" title="' + title + '">\
                                      '+ template[0].class_icon + ' \
                                    </a>';
                    }
                    else if (actions[i] == "DeleteHighlight") {
                        title = "Delete Highlight";

                        var url = "/" + controllerName + "/" + actions[i] + "/" + row.Id;
                        result = result + '\
                                    <a class="' + template[0].class_button + '" title="' + actions[i] + '" onclick="deleteData(\'' + url + '\');">\
                                       '+ template[0].class_icon + ' \
                                    </a>';
                    }
                    else {
                        var title = actions[i];
                        if (actions[i] == "DetailEdit") {
                            title = "Detail Edit";
                        }
                        else if (actions[i] == "SubTask") {
                            title = "Add Sub Task";
                        }

                        result = result + '\
                                    <a href="'+ baseControllerName + '/' + actions[i] + '/' + row.Id + '" class="' + template[0].class_button + '" title="' + title + '">\
                                      '+ template[0].class_icon + ' \
                                    </a>';
                    }
                }
            }

            return result;
        }
    }

    dataColumns.push(actionObjects);

    var table = $('#' + componentId).DataTable({
        buttons: [
            {
                extend: 'print',
            },
            {
                extend: 'excel',
            },
            {
                extend: 'csv',
            },
            {
                extend: 'pdf',
            }
        ],
        language: {
            processing: "Loading Data...",
            zeroRecords: "No matching records found",
            paginate: {
                "first": "<i class='fa fa-angle-double-left'></i>",
                "last": "<i class='fa fa-angle-double-right'></i>",
                "previous": "<i class='fa fa-angle-left'></i>",
                "next": "<i class='fa fa-angle-right'></i>"
            },
        },
        responsive: true,
        searchDelay: 500,
        lengthMenu: [10, 50, 100, 200, 500],
        processing: true,
        serverSide: true,
        autoWidth: true,
        paginationType: "full_numbers",
        ajax: {
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            async: true,
            data: function (data) {
                return JSON.stringify(data);
            }
        },
        columns: dataColumns
    });

    $('#' + componentId + '_filter input').unbind();
    $('#' + componentId + '_filter input').bind('keyup', function (e) {
        if (e.keyCode == 13) {
            table.search(this.value).draw();
        }
    });

    $("#dt-print-page").on("click", function () {
        table.button('.buttons-print').trigger();
    });

    $("#dt-export-excel").on("click", function () {
        table.button('.buttons-excel').trigger();
    });

    $("#dt-export-csv").on("click", function () {
        table.button('.buttons-csv').trigger();
    });

    $("#dt-export-pdf").on("click", function () {
        table.button('.buttons-pdf').trigger();
    });
}
