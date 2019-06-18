$(document).ready(function () {

    // podaci od interesa
    var http = "http://";
    var host = window.location.host;
    var token = null;
    var headers = {};
    var nekretnineEndpoint = "/api/nekretnine/";
    var agentiEndpoint = "/api/agenti/";
    var editingId;
    var searchEndpoint = "/api/pretraga";
    var formAction = "Create";

    //// pripremanje dogadjaja za brisanje
    $("#tablebody").on("click", "#btnDelete", deleteObjekat);
   
    //// priprema dogadjaja za izmenu
    $("#tablebody").on("click", "#btnEdit", editObjekat);
    

    $("#info").append("Korisnik nije prijavljen na sistem.");
    $("#pocetna").css("display", "none");
    $("#registracija").css("display", "none");
    $("#pretraga").css("display", "none");
    $("#odjava").css("display", "none");
    $("#dodavanje").css("display", "none");
    $("#addDiv").css("display", "none");

    setHomeTable();

    $("#pocetnoDugme").click(function () {

        $("#pocetna").css("display", "block");       
        $("#pocetnoDugme").empty();
        $("#pocetnoDugme").append("Pocetak");
        $("#info").empty();
        $("#info").append("Registracija i prijava korisnika.");
       
    });

    $("#regDugme").click(function () {

        $("#registracija").css("display", "block");
        $("#prijava").css("display", "none");
        $("#regDugme").css("display", "none");

    });

    // //////////------------REGISTRACIJA------------------------------------------------
    $("#registracija").submit(function (e) {
        e.preventDefault();

        var email = $("#regEmail").val();
        var loz1 = $("#regLoz").val();
        var loz2 = $("#regLoz2").val();

        // objekat koji se salje
        var sendData = {
            "Email": email,
            "Password": loz1,
            "ConfirmPassword": loz2
        };

        $.ajax({
            type: "POST",
            url: 'http://' + host + "/api/Account/Register",
            data: sendData

        }).done(function (data) {
            $("#registracija").css("display", "none");
            $("#info").empty();
            $("#info").append("Uspešna registracija na sistem!.");
            $("#regEmail").val("");
            $("#regLoz").val("");
            $("#regLoz2").val("");
            $("#prijava").css("display", "block");


        }).fail(function (data) {
            alertalert("Greska prilikom registracije!");
        });
    });

    ///////////----------------------------------PRIJAVA-----------------------------------------/////////////////////////
    $("#prijavaDugme").click(function () {

        $("#skriveno").trigger("click");
    });
    $("#prijava").submit(function (e) {
        e.preventDefault();

        var email = $("#priEmail").val();
        var loz = $("#priLoz").val();

        // objekat koji se salje
        var sendData = {
            "grant_type": "password",
            "username": email,
            "password": loz
        };

        $.ajax({
            "type": "POST",
            "url": 'http://' + host + "/Token",
            "data": sendData

        }).done(function (data) {
            console.log(data);
            $("#info").empty().append("Prijavljen korisnik: " + data.userName);
            token = data.access_token;
            setHomeTable();
            $("#priEmail").val("");
            $("#priLoz").val("");
            $("#pocetnoDugme").css("display", "none");
            $("#prijava").css("display", "none");
            $("#odjava").css("display", "block");
            $("#pretraga").css("display", "block");
            $("#dodavanje").css("display", "block");

        }).fail(function (data) {
            alert("Greska prilikom prijavljivanja!");
        });
    });
    // /////-----------ODJAVA -------------------------
    $("#odjavise").click(function () {
        token = null;
        headers = {};

        $("#pretraga").css("display", "none");
        $("#kolDelete").hide();
        $("#kolEdit").hide();
        $("#dodavanje").css("display", "none");
        $("#pocetna").css("display", "none");
        $("#prijava").css("display", "none");
        $("#registracija").css("display", "none");
        $("#odjava").css("display", "none");
        $("#addDiv").css("display", "none");
        $("#pocetnoDugme").css("display", "block");
        $("#pocetnoDugme").empty();
        $("#pocetnoDugme").append("Prijava i registracija");
        setHomeTable();

        $("#info").empty();
        $("#sadrzaj").empty();

    });
    ////////++++++++++++++++++++++DODAVANJE ILI IZMENA++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    $("#dodavanje").click(function () {

        $("#addDiv").css("display", "block");

        var agenti = http + host + agentiEndpoint;
        console.log("URL zahteva: " + agenti);
        $.getJSON(agenti, showDropdown);

    });
    function showDropdown(data, status) {
        console.log("Status:" + status);
        var $container = $("#selectedId");
        $container.empty();
        if (status === "success") {
            console.log(data);

            for (i = 0; i < data.length; i++) {
                var id = data[i].Id.toString();
                var name = data[i].ImeIprezime;

                $container.append('<option value="' + id + '">' + name + '</option>');
                console.log(data);
            }
        }
        else {
            var h1 = $("<h1>Greška prilikom preuzimanja Drzave!</h1>");
        }
    }
    ///////////---------------------------------------dodavanje FORMA --------------------------------------------
    // dodavanje/izmena 
    $("#kreiraj").click(function (e) {
        e.preventDefault();
        $("#skriveniSubmit").trigger("click");
    });
    $("#createEdit").submit(function (e) {
        e.preventDefault();

        var oznaka = $("#polje1").val();
        var mesto = $("#polje2").val();
        var godIzgradnje = $("#polje3").val();
        var kvadratura = $("#polje4").val();
        var cena = $("#polje5").val();
        var agentId = $("#selectedId").val();

        if (token) {
            headers.Authorization = token;
        }
        if (formAction === "Create") {
            httpAction = "POST";
            url = http + host + nekretnineEndpoint;
            var sendData = {
                "Oznaka": oznaka,
                "Mesto": mesto,
                "GodinaIzgradnje": godIzgradnje,
                "Kvadratura": kvadratura,
                "Cena":cena,
                "AgentId": agentId
            };
        }
        else {
            httpAction = "PUT";
            url = http + host + nekretnineEndpoint + editingId.toString();
            sendData = {
                "Id": editingId,
                "Oznaka": oznaka,
                "Mesto": mesto,
                "GodinaIzgradnje": godIzgradnje,
                "Kvadratura": kvadratura,
                "Cena": cena,
                "AgentId": agentId
            };
        }
        console.log("Objekat za slanje");
        console.log(sendData);

        $.ajax({
            "url": url,
            "type": httpAction,
            "headers": headers,
            "data": sendData

        }).done(function (data, status) {
            formAction = "Create";
            setHomeTable();
            refreshTable();

        }).fail(function (data, status) {
            alert("Desila se greska!");
        });
    });
    ///////////---------------------------------------INICIJALIAZACIJA TABELE--------------------------------------------
    function setHomeTable() {

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        $.ajax({
            "type": "GET",
            "url": http + host + nekretnineEndpoint,
            "headers": headers

        }).done(function (data) {
            punjenjeTabela(data);

        }).fail(function (data) {
            alert(data.status + ": " + data.statusText);
        });
    }
    //////////////////////-------------------TABELA ZA PUNJENJE------------------------------////////

    function punjenjeTabela(data) {
        var $container = $("#tablebody");
        $container.empty();

        if (token) {
            $("#pretraga").css("width", "25%");
            $("#table").css("width", "75%");           
            $("#kolDelete").show();
            $("#kolEdit").show();

            for (i = 0; i < data.length; i++) {
                var row = '<tr>';
                var displayData = '<td>' + data[i].Mesto + '</td><td>' + data[i].Oznaka + '</td><td>' + data[i].GodinaIzgradnje + '</td><td>' + data[i].Kvadratura + '</td><td>' + data[i].Cena + '</td><td>' + data[i].Agent.ImeIprezime + '</td>';
                var stringId = data[i].Id.toString();
                var displayDelete = "<td><button id='btnDelete' style='background-color:red' class='btn btn-md glyphicon glyphicon-trash' name=" + stringId + "></button></td>";
              
                var displayEdit = "<td><button id='btnEdit' style='background-color:skyblue' class='btn btn-md glyphicon glyphicon-edit' name=" + stringId + "></button></td>";
               
                row += displayData + displayDelete + displayEdit + "</tr>";

                $container.append(row);
            }
        }
        else {
            $("#table").css("width", "100%");
            $("#kolDelete").hide();
            $("#kolEdit").hide();

            for (i = 0; i < data.length; i++) {

                row = "<tr>";
                displayData = '<td>' + data[i].Mesto + '</td><td>' + data[i].Oznaka + '</td><td>' + data[i].GodinaIzgradnje + '</td><td>' + data[i].Kvadratura + '</td><td>' + data[i].Cena + '</td><td>' + data[i].Agent.ImeIprezime + '</td>';
                row += displayData + "</tr>";
                $container.append(row);
            }
        }
    }
    ////////////////////---------------------PRETRAGA---------------------------------//////////////

    $("#btnSearch").submit(function (e) {

        e.preventDefault();

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        //var start = "?x=" + $("#donja").val();
        //var kraj = "&y=" + $("#gornja").val();

        var mini = document.getElementById("donja").value;
        var maksi = document.getElementById("gornja").value;


        var urlPretraga = http + host + "/api/pretraga";

        var sendData = {
            "x": mini,
            "y": maksi
        };
        $.ajax({
            "type": "POST",
            "url": urlPretraga,
            "headers": headers,
            "data": sendData

        }).done(function (data) {
            console.log(data);
            punjenjeTabela(data);

        }).fail(function (data) {
            alert("Greska prilikom pretrage");
        });
    });
    ////////-------------------------------------------BRISANJE OBJEKTA-------------------------------------////////
    function deleteObjekat() {

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        var deleteId = this.name;

        $.ajax({
            "type": "DELETE",
            "url": http + host + nekretnineEndpoint + deleteId.toString(),
            "headers": headers,
            "data": deleteId.toString()

        }).done(function (data, status) {
            setHomeTable();

        }).fail(function (data, status) {
            alert("Desila se greska pri brisanju!");
        });
    }
    ////////-------------------------------------------EDITOVANJE OBJEKTA-------------------------------------////////
    function editObjekat() {
          // izvlacimo {id}

        $("#dodavanje").trigger("click");

      
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        var editId = this.name;

        $.ajax({
            "type": "GET",
            "url": http + host + nekretnineEndpoint + editId.toString(),
            "headers": headers,
            "data": editId.toString()

        }).done(function (data, status) {

            $('#selectedId option[selected="selected"]').attr("selected", null);
            $('#selectedId option[value="' + data.AgentId + '"]').attr("selected", "selected");

            $("#polje1").val(data.Oznaka);
            $("#polje2").val(data.Mesto);
            $("#polje3").val(data.GodinaIzgradnje);
            $("#polje4").val(data.Kvadratura);
            $("#polje5").val(data.Cena);
            
            editingId = data.Id;
            formAction = "Update";

        }).fail(function (data, status) {
            alert("Desila se greska!");
        });
    }

    function refreshTable() {

        $("#polje1").val("");
        $("#polje2").val("");
        $("#polje3").val("");
        $("#polje4").val("");
        $("#polje5").val("");
    }
    $("#refreshbtn").click(function (e) {
        refreshTable();
        $("#addDiv").hide();
    });

    function refreshPage() {

        $("#polje1").val("");
        $("#polje2").val("");
        $("#polje3").val("");
        $("#polje4").val("");
        $("#polje5").val("");
    }

});
