var appointments = {

    status: 'add',

    init: function () {
        appointments.defineListeners();
        appointments.loadGrid();
    },

    defineListeners: function () {
        $('#save').on("click", function () {
            appointments.save();
        });

        $('#cancel').on('click', function () {
            appointments.cancel();
        });
    },

    loadGrid: function () {
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                appointments.renderGrid(this);
            }
        };
        xmlhttp.open("GET", "/XML/Appt.xml", true);
        xmlhttp.send();

    },

    renderGrid: function (xml) {
        var xmlDoc = xml.responseXML;
        var table = "<tr><th>Title</th><th>Description</th><th>Actions</th></tr>";
        var x = xmlDoc.getElementsByTagName("Appointment");
        for (var i = 0; i < x.length; i++) {
            const t = x[i].getElementsByTagName("Title")[0].childNodes[0].nodeValue;
            table += "<tr><td>" +
                t +
                "</td><td>" +
                x[i].getElementsByTagName("Description")[0].childNodes[0].nodeValue +
                '</td><td><a href="javascript:appointments.remove(\'' + t + '\')">Delete</a>&nbsp;<a href="javascript:appointments.edit(\'' + t + '\')">Edit</a></td></tr>';
        }

        document.getElementById("dataGrid").innerHTML = table;
    },

    save: function () {

        const title = $('#title').val();
        const description = $('#description').val();
        const date = $('#date').val();
        const time = $('#time').val().replace(/:/, '|');
        const type = $('#type :selected').text();
        const workrelated = $('#workrelated').is(':checked');

        var endPoint = "";

        (appointments.status == "add") ? endPoint = "insert/" : endPoint = "update/";
        $.post(
            endPoint + title + "/" + description + "/" + date + "/" + time + "/" + type + "/" + workrelated)
                .done(function (data) {
                    appointments.showStatus(data);
            });

        appointments.status = "add";



    },

    edit: function (title) {
        appointments.status = 'edit';

        $.get("get/" + title, function (data, status) {
            
            var fields = data.split("|");
            $('#title').val(fields[0]);
            $('#description').val(fields[1]);
            $('#date').val(fields[2])
            $('#time').val(fields[3]);
            $('#type :selected').text(fields[4]);

            $("#title").attr("disabled", "disabled"); 

            var isWorkRelated = false;
            (fields[5] == 'true') ? isWorkRelated = true : isWorkRelated = false;

            $('#workrelated').prop('checked', isWorkRelated);

        });

    },

    cancel: function () {
        // reset values
        $('#title').val('');
        $('#description').val('');
        $('#date').val();
        $('#time').val();
        $('#type :selected').text();
        $('#workrelated').prop('checked', false);

        // enable title
        $("#title").attr("disabled", "enabled");
    },

    remove: function (title) {
        $.post(
            "delete/" + title)
            .done(function (data) {
                appointments.showStatus(data);
            });
    },

    showStatus: function (data) {
        if (data == "True") {
            alert('sucess');
            location.reload();
        } else {
            alert('fail')
        }
    }

};