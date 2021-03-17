// Compare two arrays
// Warn if overriding existing method
if (Array.prototype.equals)
    console.warn("Overriding existing Array.prototype.equals. Possible causes: New API defines the method, there's a framework conflict or you've got double inclusions in your code.");
// attach the .equals method to Array's prototype to call it on any array
Array.prototype.equals = function (array) {
    // if the other array is a falsy value, return
    if (!array)
        return false;

    // compare lengths - can save a lot of time
    if (this.length != array.length)
        return false;

    for (var i = 0, l = this.length; i < l; i++) {
        // Check if we have nested arrays
        if (this[i] instanceof Array && array[i] instanceof Array) {
            // recurse into the nested arrays
            if (!this[i].equals(array[i]))
                return false;
        }
        else if (this[i] != array[i]) {
            // Warning - two different object instances will never be equal: {x:20} != {x:20}
            return false;
        }
    }
    return true;
}
// Hide method from for-in loops
Object.defineProperty(Array.prototype, "equals", { enumerable: false });
// Compare two arrays

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return "";
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function disableEnableTab(element, isDisable) {
    if (isDisable) {
        $("#" + element).addClass("t-tab-disable");
        // $("#" + element + " a[data-toggle='tab']").attr("title","Vui lòng tạo mới công ty lữ hành trước.");
    } else {
        $("#" + element).removeClass("t-tab-disable");
    }
}
function disableButton(element) {
    $("#" + element).addClass("disabled-button");
};
function enableButton(element) {
    $("#" + element).removeClass("disabled-button");
};
function disableCheckbox(element) {
    $("#" + element).attr("disabled", "disabled");
};
function enableCheckbox(element) {
    $("#" + element).removeAttr("disabled");
};
function showOrHideElement(element, style) {
    $("#" + element).css("display", style);
};
function hideButton(element) {
    $("#" + element).addClass("hided-button");
};
function showButton(element) {
    $("#" + element).removeClass("hided-button");
};
function togglePanel(e) {
    $(e).parent().siblings().slideToggle();
};
function addpendLoading(id, parent) {
    var divLoading = "<div class=\"t-loading-panel\" id=\"" + id + "\"></div>";
    $("#" + parent).append(divLoading);
};
function appendLoading(id, parent) {
    var divLoading = "<div class=\"t-loading-panel\" id=\"" + id + "\"></div>";
    $("#" + parent).append(divLoading);
};
function appendTextLoading(id, parent) {
    var divLoading = "<label class=\"t-loading-text control-label t-overwrite-label\" id=\"" + id + "\">Đang tải dữ liệu...</label>";
    $("#" + parent).append(divLoading);
};

function removeTextLoading(id) {
    $("label#" + id).remove();
};

function removeElement(element) {
    $("#" + element).html("");
};
function removeElementByClass(parentId, element) {
    $("#" + parentId + " ." + element).html("");
};
function addpendElementLoading(id, parent) {
    var divLoading = "<div class=\"t-loading-element\" id=\"" + id + "\"></div>";
    $("#" + parent).append(divLoading);
};
function removeLoading(id) {
    $("div#" + id).remove();
    $("div#" + id).fadeOut("fast", "swing", function () { $("div#" + id).remove(); });
};

function removeOrAppendAttribute(elementId, type, attribute, attributeValue) {
    if (type === "remove") {
        $("#" + elementId).removeAttr(attribute);
    } else {
        $("#" + elementId).attr(attribute, attributeValue);
    }
};
function focusElement(element) {
    $("#" + element).focus();
};
function LoadIoTSelect(url, parameter, parentId) {
    //debugger;
    $.ajax({
        url: url,
        type: "POST",
        data: parameter,
        success: function (data) {
            //debugger;
            $("#" + parentId).html(data);
        },
        error: function () {
        }
    });
};

function LoadCommonAjax(url, parameter, parentId) {
    $.ajax({
        url: url,
        type: "POST",
        data: parameter,
        success: function (data) {
            $("#" + parentId).html(data);
        },
        error: function () {
        }
    });
};

function ShowPopupAddOrEdit(url, parameter, typeName, titleModelName, readonly) {
    // typeName: complex
    // typeName + "Model" = divComplexModel
    // title + typeName + "Model" = complexTitleModel
    // body + typeName + "Model" = complexBodyModel
    var modelContainer = $("#" + typeName + "Model");
    var titleModel = $("#" + typeName + "TitleModel");
    var bodyModel = $("#" + typeName + "BodyModel");
    $.ajax({
        url: url,
        type: "POST",
        data: parameter,
        success: function (data) {
            modelContainer.modal("show");
            titleModel.html(titleModelName);
            bodyModel.html(data);
            if (readonly === true) {
                $("#" + typeName + "BodyModel .js-responsive").prop("disabled", true);
                $("div.add-or-edit-form-container :button").attr("disabled", "disabled");
                $("div.add-or-edit-form-container input[type=text], textarea").attr("disabled", "disabled");
                $("div.add-or-edit-form-container input[type='radio']").prop("disabled", true);
                $("div.add-or-edit-form-container input[type='checkbox']").prop("disabled", "disabled");
                $("div.add-or-edit-form-container input[type='checkbox']").prop("readonly", true);
                // for form addoredit of mr Quang
                $("#" + typeName + "BodyModel :button").attr("disabled", "disabled");
                $("#" + typeName + "BodyModel select").attr("disabled", true);
                $("#" + typeName + "BodyModel input[type=text], textarea").attr("disabled", "disabled");
                $("#" + typeName + "BodyModel input[type='radio']").prop("disabled", true);
            }
        },
        error: function () {
        }
    });
};

function UpdatePopupAddOrEdit(url, parameter, typeName) {
    // typeName: complex
    // typeName + "Model" = divComplexModel
    // title + typeName + "Model" = complexTitleModel
    // body + typeName + "Model" = complexBodyModel
    var bodyModel = $("#" + typeName + "BodyModel");
    $.ajax({
        url: url,
        type: "POST",
        data: parameter,
        success: function (data) {
            bodyModel.html(data);
        },
        error: function () {
        }
    });
};
function toggleSelect2Dropdown(id, type) {
    if (type === "disable") {
        $("#" + id).prop("disabled", true);
    } else {
        $("#" + id).removeAttr("disabled");
    }
}

function LoadAjaxFormByButton(url, parameter, type, key, parentId) {
    appendLoading("loadingPanel" + type, "div" + type);
    $.ajax({
        type: "POST",
        data: parameter,
        url: url,
        success: function (data) {
            $("#" + parentId + key + " #div" + type).html(data);
            removeLoading("loadingPanel" + type);
        },
        error: function () {
            removeLoading("loadingPanel" + type);
        }
    });
};

function LoadAjaxAddNewForm(url, parameter, parentId) {
    $.ajax({
        type: "POST",
        data: parameter,
        url: url,
        success: function (data) {
            $("#" + parentId).html(data);
            ScrollToWorkingView(parentId);
        },
        error: function () {
        }
    });
};
// Load google map Partial view
function LoadGoogleMap(url, parameter, container) {
    if (url === "") url = '/Common/GetGoogleMap';
    var geography = {
        Address: DefaultAddress,
        Latitude: DefaultLatitude,
        Longitude: DefaultLongtitude,
        ProvinceId: DefaultProvinceId,
        DistrictId: DefaultDistrictId,
        WardId: DefaultWardId,
        StreetId: DefaultStreetId
    };

    if (parameter === null) {

        parameter = { Geography: geography };
    } else {
        if (parameter.Geography === null) {
            parameter.Geography = geography;
        }
    }

    if (parameter.Geography.Address == null || parameter.Geography.Address == "") {
        parameter.Geography.Address = DefaultAddress;
    }
    if (!parameter.Geography.Latitude > 0) {
        parameter.Geography.Latitude = DefaultLatitude;
    }
    if (!parameter.Geography.Longitude > 0) {
        parameter.Geography.Longitude = DefaultLongtitude;
    }
    if (!parameter.Geography.ProvinceId > 0) {
        parameter.Geography.ProvinceId = DefaultProvinceId;
    }
    if (!parameter.Geography.DistrictId > 0) {
        parameter.Geography.DistrictId = DefaultDistrictId;
    }
    if (!parameter.Geography.WardId > 0) {
        parameter.Geography.WardId = DefaultWardId;
    }
    if (!parameter.Geography.StreetId > 0) {
        parameter.Geography.StreetId = DefaultStreetId;
    }

    $.ajax({
        type: "POST",
        data: parameter,
        url: url,
        success: function (data) {
            $("#" + container).html(data);
        },
        error: function () {
        }
    });
};

function RemoveAddNewForm(divWrapper) {
    $("#" + divWrapper).html("");
};

function LoadEditForm(tableClass, divWrapperEditForm, editBodyClass, parent, keyId, editFormId) {
    $("." + editBodyClass).html("");
    var divWrapper = "<div id=\"" + divWrapperEditForm + "\" style=\"margin:15px;position:relative;\">" + $("#" + editFormId).html() + "</div>";
    $("#tdBody" + parent + keyId).html(divWrapper);
    HighlightEditingRowById(tableClass + parent, "trHead" + parent + keyId, "trBody" + parent + keyId);
};

function LoadEditForm(type, id) {
    $(".tdBody" + type).html("");
    var divWrapper = "<div id=\"div" + type + "\" style=\"margin:15px;position:relative;\"></div>";
    $("#tdBody" + type + id).html(divWrapper);
    HighlightEditingRowById("table" + type, "trHead" + type + id, "trBody" + type + id);
};

function HighlightEditingRow(rowId) {
    $(".hight-light-editing-row-head").removeClass("hight-light-editing-row-head");
    $(".hight-light-editing-row-body").removeClass("hight-light-editing-row-body");
    $("#trHead" + rowId).addClass("hight-light-editing-row-head");
    $("#trBody" + rowId).addClass("hight-light-editing-row-body");
};
function RemoveHighlightEditingRow(parentId) {
    $("#" + parentId + " tr.hight-light-editing-row-head").removeClass("hight-light-editing-row-head");
    $("#" + parentId + " tr.hight-light-editing-row-body").removeClass("hight-light-editing-row-body");
};
function HighlightEditingRowById(parentId, headId, bodyId) {
    $("#" + parentId + " tr.hight-light-editing-row-head").removeClass("hight-light-editing-row-head");
    $("#" + parentId + " tr.hight-light-editing-row-body").removeClass("hight-light-editing-row-body");
    $("#" + headId).addClass("hight-light-editing-row-head");
    $("#" + bodyId).addClass("hight-light-editing-row-body");
    $("html,body").animate({
        scrollTop: $("#" + headId).offset().top
    }, "fast");
};
function HighlightEditingRowByIdNotAnimate(parentId, headId, bodyId) {
    $("#" + parentId + " tr.hight-light-editing-row-head").removeClass("hight-light-editing-row-head");
    $("#" + parentId + " tr.hight-light-editing-row-body").removeClass("hight-light-editing-row-body");
    $("#" + headId).addClass("hight-light-editing-row-head");
    $("#" + bodyId).addClass("hight-light-editing-row-body");
};
function ScrollToWorkingView(elementId) {
    $("html,body").animate({
        scrollTop: $("#" + elementId).offset().top
    }, "fast");
};


var dotNotification;
$(function () {
    //// Register Kendo Notification
    //dotNotification = $("#doTNotification").kendoNotification({
    //    position: {
    //        pinned: true,
    //        top: 30,
    //        right: 30
    //    },
    //    autoHideAfter: 10000,
    //    stacking: "down",
    //    templates: [{
    //        type: "info",
    //        template: $("#doTSuccessTemplate").html()
    //    }, {
    //        type: "error",
    //        template: $("#doTErrorTemplate").html()
    //    }]

    //}).data("kendoNotification");
});

function callDoTNotification(title, message, type) {
    //dotNotification.show({
    //    title: title,
    //    message: message
    //}, type);

    alert(message);
};


function GetCenterFromDegrees(data) {
    if (!(data.length > 0)) {
        return false;
    }

    var num_coords = data.length;

    var X = 0.0;
    var Y = 0.0;
    var Z = 0.0;
    var lat;
    var lon;
    for (i = 0; i < data.length; i++) {
        lat = data[i].latitude * Math.PI / 180;
        lon = data[i].longitude * Math.PI / 180;
        var a = Math.cos(lat) * Math.cos(lon);
        var b = Math.cos(lat) * Math.sin(lon);
        var c = Math.sin(lat);

        X += a;
        Y += b;
        Z += c;
    }

    X /= num_coords;
    Y /= num_coords;
    Z /= num_coords;
    lon = Math.atan2(Y, X);
    var hyp = Math.sqrt(X * X + Y * Y);
    lat = Math.atan2(Z, hyp);
    var newX = (lat * 180 / Math.PI);
    var newY = (lon * 180 / Math.PI);

    return { latitude: newX, longitude: newY };
}

//QuangNM
function objectifyForm(formArray) {//serialize data function

    var returnArray = {};
    for (var i = 0; i < formArray.length; i++) {
        returnArray[formArray[i]['name']] = formArray[i]['value'];
    }
    return returnArray;
}

function guid() {
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
        s4() + '-' + s4() + s4() + s4();
}

function s4() {
    return Math.floor((1 + Math.random()) * 0x10000)
        .toString(16)
        .substring(1);
}


function GetSelect2Data(elementId) {
    var object = {
        id: -1,
        text: ""
    };
    var ddl = $("#" + elementId);
    var id = -1;
    var text = "";
    if (ddl) {
        if (ddl.select2("data") !== window.underfined) {
            id = parseInt(ddl.select2("data")[0].id);
            text = "";
            if (id !== -1) {
                text = ddl.select2("data")[0].text;
            }
        }
        object.id = id;
        object.text = text;
    }
    return object;
}
function GetSelectPickerData(elementId) {
    var object = {
        id: -1,
        text: ""
    };
    var ddl = $("#" + elementId);
    var id = -1;
    var text = "";
    if (ddl) {
        id = parseInt(ddl.val());
        text = ddl.find("option:selected").text();
    }

    object.id = id;
    object.text = text;
    return object;
}
// Files
function GetFileSize(files) {
    try {
        var totalSize = 0;
        var fileSize = 0;
        files = $("#" + files).get(0).files;
        for (var i = 0; i < files.length; i++) {
            fileSize = fileSize + files[i].size;
        }
        totalSize = fileSize / 1048576; //size in mb
        return totalSize;
    }
    catch (e) {
        return 0;
    }
};

function GetNameFromPath(strFilepath) {
    var objRe = new RegExp(/([^\/\\]+)$/);
    var strName = objRe.exec(strFilepath);
    if (strName == null) {
        return null;
    }
    else {
        return strName[0];
    }
};

function ValidateBlackistFileExtension(extension) {
    if (extension === "") return false;
    switch (extension.toLowerCase()) {
        case "asp":
        case "aspx":
        case "asa":
        case "aSP":
        case "aSpx":
        case "aSa":
        case "asp%20%20%20":
        case "aspx%20%20%20":
        case "asa%20%20%20":
        case "aSP%20%20%20":
        case "aSpx%20%20%20":
        case "aSa%20%20%20":
        case "asp......":
        case "aspx......":
        case "asa......":
        case "aSP......":
        case "aSpx......":
        case "aSa......":
        case "asp%20%20%20...%20.%20..":
        case "aspx%20%20%20...%20.%20..":
        case "asa%20%20%20...%20.%20..":
        case "aSP%20%20%20...%20.%20..":
        case "aSpx%20%20%20...%20.%20..":
        case "aSa%20%20%20...%20.%20..":
        case "asp%00":
        case "aspx%00":
        case "asa%00":
        case "aSp%00":
        case "aSpx%00":
        case "aSa%00":
        case "ani":
        case "asf":
        case "avi":
        case "bas":
        case "bat":
        case "bin":
        case "chm":
        case "cmd":
        case "com":
        case "cpl":
        case "cur":
        case "eml":
        case "exe":
        case "hta":
        case "ico":
        case "js":
        case "jse":
        case "midi":
        case "mp3":
        case "mpa":
        case "mpe":
        case "mpeg":
        case "mpg":
        case "msc":
        case "msp":
        case "pcd":
        case "pif":
        case "reg":
        case "scr":
        case "sct":
        case "vb":
        case "vbs":
        case "wma":
        case "wmf":
        case "wmv":
        case "wsc":
        case "wsf":
            return false;
        default:
            return true;
    }
}