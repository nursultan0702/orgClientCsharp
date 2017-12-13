$.get("/Home/UnitList", function (data) {
    $("#tree").append(data);
});
function positionTree(id) {
    var check = $("input[name=" + id + "]").val();
    if (check == 1) {
        console.log(check);
        $("#" + id).html("");
        $("#" + id + "icon").attr("class", "fa fa-fw fa-chevron-right")
        document.body.style.cursor = "default";
    } else {
        document.body.style.cursor = "progress";
        $.get("/Home/UnitListbyParent?unitId=" + id, function (data) {
            if (String(data) == "error") {
            } else {
                $("#" + id).append(data);
                $("#" + id).append("<input name=\"" + id + "\" style=\"display: none\" value=\"1\" />");
                $("#" + id + "icon").attr("class", "fa fa-fw fa-chevron-down")
            }
            personTree(id);
        });
    }
}
function personTree(id) {
    $.get("/Home/PersonList?unitId=" + id, function (data) {
        if (String(data) == "error") {
        } else {
            $("#" + id).append(data);
            $("#" + id).append("<input name=\"" + id + "\" style=\"display: none\" value=\"1\" />");
            document.body.style.cursor = "default";
        }
    });
}

function modalclick(modalid, parentid) {
    if (String(modalid) == "0") {
        window.location.reload();
    } else {
        window.open("/Home/PersonInfo?positionid=" + modalid + "&parentpositionid=" + parentid);
    }
}

function clearWindow() {
    $("#myModal").attr("style", "display:none");
}
window.onclick = function (event) {
    var modal = document.getElementById('myModal');
    if (event.target == modal) {
        clearWindow();
    }
}
function changeHead(personid) {
    $("#myModal").attr("style", "display:block");
}
function Search() {
    $("#searchresult").html("");
    var searchtext = $("#searchtext").val();
    $.get("/Home/Search?searchtext=" + searchtext, function (data) {
        if (String(data) == "error") {
        } else {
            $("#searchresult").append(data);
            $("#" + id).append("<input name=\"" + id + "\" style=\"display: none\" value=\"1\" />");
            document.body.style.cursor = "default";
        }
    });
}

