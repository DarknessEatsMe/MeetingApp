async function getJsonUserInfo() {
    const results = await fetch('/Meeting/GetJsonUser/');
    const data = await results.json();
    console.log(data);
    document.getElementById("UserId").value = data.idUser;
    document.getElementById("userName").innerHTML = data.name + " " + data.secName;
    document.getElementById("Sex").innerHTML = data.sex;
    document.getElementById("birth").innerHTML = data.birthday.substr(0, 10);

    const adrRes = await fetch('/Meeting/GetAdress/' + document.getElementById("UserId").value);
    const adr = await adrRes.json();
    console.log(adr);
    document.getElementById("city").innerHTML = adr.name;

    const descrRes = await fetch('/Meeting/GetDescr/' + document.getElementById("UserId").value);
    const des = await descrRes.json();
    console.log(des);
    if(des != null) document.getElementById("des").innerHTML = des.decsr;

    const imgRes = await fetch('/Meeting/GetImg/' + document.getElementById("UserId").value);
    const img = await imgRes.json();
    console.log(img);
    if (img != null) {
        document.getElementById("image").src = "data:image/jpeg;base64," + img.photoAdr;
    }
    else {
        document.getElementById("image").src = "/img/avatar.jpg";
    }

    document.getElementById("LikeBtn").style.display = "block";
    document.getElementById("RejectBtn").style.display = "block";
} 

let btns = document.querySelectorAll(".findBtn");

btns.forEach((btn) => {
    btn.addEventListener("click", getJsonUserInfo);
});

let form = document.querySelector("#meetingForm");

document.getElementById("LikeBtn").addEventListener("click", () => {
    form.submit();
});