let form = document.getElementById("form");
let gender = document.getElementById("gender");


let city = document.getElementById("city");
let cityInp = document.getElementById("cityInp");
let sexInp = document.getElementById("sexInp");
let descr = document.getElementById("des");
let textarea = document.getElementById("textarea");

city.options[cityInp.value - 1].selected = true;

gender.addEventListener("change", (e) => {
    sexInp.value = e.target.value;
});

city.addEventListener("change", (e) => {
    cityInp.value = e.target.value;
});

form.addEventListener("submit", (e) => {
    descr.value = textarea.value;
})