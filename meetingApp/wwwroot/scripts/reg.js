let form = document.querySelector(".form");
let inputs = document.querySelectorAll(".req");

let city = document.getElementById("city");
let cityInp = document.getElementById("cityInp");
let sexInp = document.getElementById("sexInp");
let descr = document.getElementById("des");
let textarea = document.getElementById("textarea");

let pass = document.getElementById("Pass");
let passConf = document.getElementById("PassConf");

sexInp.value = gender.value;
gender.addEventListener("change", (e) => {
    sexInp.value = e.target.value;
});

cityInp.value = city.value;
city.addEventListener("change", (e) => {
    cityInp.value = e.target.value;
});

document.querySelector("#btn").addEventListener("click", (e) => {
    for (let i = 0; i < inputs.length; i++) {
        if (inputs[i].value == "") {
            inputs[i].style.background = " #ffdddd";
            alert("Не заполнено " + inputs[i].name);
            e.preventDefault();
            break;
        }
        else
        if (pass.value != passConf.value) {
            passConf.style.background = " #ffdddd";
            alert("Пароли не совпали!");
            e.preventDefault();
            break;
        }
        else {
            if (i == inputs.length - 1) {
                descr.value = textarea.value;
                form.submit();
            }
            continue;
        }
    }
})

inputs.forEach(inp => {
    inp.addEventListener("input", (e) => {
        e.target.style.background = "white";
    })
})
