const mainDiv = document.getElementById("sub-container");
const loginContainer = document.getElementById("login-container");
const loggedDiv = document.createElement("Div");
const loginDiv = document.createElement("div");
const mainDivForMyFilms = document.createElement("div");
mainDivForMyFilms.id = "mainDivForMyFilms";
loginDiv.id = "loginDiv";
loggedDiv.id = "logged-Div";
let apiToken = "";
const closeTogg = document.createElement("button");
closeTogg.id = "closeTogg";
closeTogg.innerHTML = "Close";


const GetFilms = async () => {
    const response = await fetch("/api/films");
    const data = await response.json();
    return data;
}


const GetMyFilms = async () => {

    const response = await fetch("/api/mystudio/rentals", {
        method: 'GET',
        headers: {
            'Content-type': 'application/json; charset=UTF-8',
            'Authorization': `Bearer ${localStorage.getItem("token")}`
        }
    });
    const data = await response.json();
    return data;
}



const ShowFilms = async () => {
    let data = await GetFilms();
    for (i = 0; i < data.length; i++) {
        const divForFilm = document.createElement("div");
        divForFilm.id = "divForFilm";
        const spanFilmId = document.createElement("span");
        const spanName = document.createElement("span");
        const spanReleaseDate = document.createElement("span");
        const spanCountry = document.createElement("span");
        const spanDirector = document.createElement("span");
        const Borrow = document.createElement("button");
        Borrow.id = "Borrow";
        spanFilmId.innerHTML = " Id : " + data[i].filmId;
        spanName.innerHTML = " Name : " + data[i].name;
        spanReleaseDate.innerHTML = " Release Year : " + data[i].releaseDate;
        spanCountry.innerHTML = " Country : " + data[i].country;
        spanDirector.innerHTML = " Director : " + data[i].director;
        Borrow.innerHTML = "Borrow this Film";
        let id =data[i].filmId;
        Borrow.addEventListener("click", async () => {
            if (localStorage.getItem("token") === null) {
                alert("Log in first!");
            }
            else {
              let studio = JSON.parse(localStorage.getItem("id"));
              let rentNow =  await fetch("/api/films/rent?id="+id+"&studioid="+studio, {
                method: 'POST',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8',
                    'Authorization': `Bearer ${localStorage.getItem("token")}`
                }
              });
                if (rentNow.status == 200) {
                    Borrow.innerHTML = " You Got It";
                    Borrow.style.backgroundColor = "black";
                    Borrow.style.color = "white";
                }
            }
        });

        divForFilm.append(spanFilmId, spanName, spanReleaseDate, spanCountry, spanDirector, Borrow);
        mainDiv.append(divForFilm);

    }

};

function login() {

    const inputUsername = document.createElement("input");
    inputUsername.placeholder = "User Name";
    inputUsername.id = "input-Username";
    const inputPassword = document.createElement("input");
    inputPassword.id = "input-Password"
    inputPassword.placeholder = "Password";
    inputPassword.type = "password";
    const loginButtuon = document.createElement("button");
    loginButtuon.id = "login-button";
    loginButtuon.innerHTML = "Login";
    loginDiv.append(inputUsername, inputPassword, loginButtuon);
    loginContainer.append(loginDiv);

    loginButtuon.addEventListener("click", async () => {
        let userName = document.getElementById('input-Username').value;
        let passWord = document.getElementById('input-Password').value;
        let response = await fetch("api/users/Authenticate", {
            method: 'POST',
            body: JSON.stringify({
                Username: userName,
                Password: passWord
            }),
            headers: {
                'Content-type': 'application/json; charset=UTF-8',
            },
        })
        let user = await response.json();
        apiToken = user.token;
        if (apiToken == "" || user.token === undefined) {
            alert("Username or Password Faild!");
        }
        else {
            localStorage.setItem("token", apiToken);
            localStorage.setItem("id", user.filmStudioId);
            loginDiv.style.display = "none";
            logged();
        }



    });

};

function logged() {
    loggedDiv.style.display = "";
    const logoutButton = document.createElement("button");
    logoutButton.id = "logout-button";
    logoutButton.innerHTML = "Logout";
    const yourFilm = document.createElement("button");
    yourFilm.id = "your-film"
    yourFilm.innerHTML = "Movies you borrowed";
    loggedDiv.append(yourFilm, logoutButton);
    loginContainer.append(loggedDiv);
    logoutButton.addEventListener("click", function () {
        loginDiv.style.display = "";
        loggedDiv.style.display = "none";
        loggedDiv.innerHTML = "";
        localStorage.clear();

    })

    yourFilm.addEventListener("click", async () => {
        mainDivForMyFilms.innerHTML = "";
        mainDivForMyFilms.style.display = "";
        var data = await GetMyFilms();
        var allfilms = await GetFilms();
        for (i = 0; i < data.length; i++) {
            for (j = 0; j < allfilms.length; j++) {
                if (data[i].filmId == allfilms[j].filmId) {
                    const divForMyFilms = document.createElement("div");
                    divForMyFilms.id = "divForMyFilms";
                    const spanfilmCopyId = document.createElement("span");
                    const spanfilmId = document.createElement("span");
                    const filmname = document.createElement("span");
                    const returnIt = document.createElement("button");
                    returnIt.id = "returnIt";
                    spanfilmCopyId.innerHTML = " CopyID: " + data[i].filmCopyId;
                    filmname.innerHTML = " Name: " + allfilms[j].name;
                    spanfilmId.innerHTML = " FilmId: " + data[i].filmId;
                    returnIt.innerHTML = "Return This film";
                    divForMyFilms.append(spanfilmCopyId, spanfilmId, filmname, returnIt);
                    mainDivForMyFilms.append(divForMyFilms, closeTogg);
                    loginContainer.append(mainDivForMyFilms);
                    let StudioId = localStorage.getItem("id");
                    let filmId = data[i].filmId;

                    returnIt.addEventListener("click", async () => {
                        let response2 = fetch("/api/films/return?id=" + filmId + "&studioid=" + JSON.parse(StudioId), {
                            method: 'POST',
                            headers: {
                                'Content-type': 'application/json; charset=UTF-8',
                                'Authorization': `Bearer ${localStorage.getItem("token")}`
                            }
                        });
                        const status = await response2;
                        if (status.status == 200) {
                            divForMyFilms.remove();
                        }
                    })

                    closeTogg.addEventListener("click", () => {
                        mainDivForMyFilms.style.display = "none";
                    })

                }
            }
        }

    })
}





function stayLogged() {
    if (localStorage.getItem("token") !== null && localStorage.getItem("id") !== null) {
        document.getElementById("loginDiv").style.display = "none";
        logged();
    }
}

window.onload = stayLogged;
login();
ShowFilms();
