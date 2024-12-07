let form = document.getElementById("msgForm");

form.addEventListener("submit", (e) => {
    e.target.elements.message.value = document.getElementById("textarea").value;
})

var textarea = document.getElementsByTagName('textarea')[0];

textarea.addEventListener('keydown', resize);

function resize() {
    var el = this;
    setTimeout(function () {
        el.style.cssText = 'height:auto; padding:0';
        el.style.cssText = 'height:' + el.scrollHeight + 'px';
    }, 1);
}