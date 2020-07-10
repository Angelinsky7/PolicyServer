import './modal.js';

window.helper = {
    getClosest(elem, selector) {

        if (!Element.prototype.matches) {
            Element.prototype.matches =
                Element.prototype.matchesSelector ||
                Element.prototype.mozMatchesSelector ||
                Element.prototype.msMatchesSelector ||
                Element.prototype.oMatchesSelector ||
                Element.prototype.webkitMatchesSelector ||
                function (s) {
                    var matches = (this.document || this.ownerDocument).querySelectorAll(s),
                        i = matches.length;
                    while (--i >= 0 && matches.item(i) !== this) { }
                    return i > -1;
                };
        }

        for (; elem && elem !== document; elem = elem.parentNode) {
            if (elem.matches(selector)) return elem;
        }
        return null;
    }
};

Object.defineProperty(Array.prototype, "remove", {
    value: function (item) { return this.splice(this.indexOf(item), 1); }
});

//function showModal() {
//    alert("should show a modal");
//}

//function changeAllLinkIntoModal() {
//    var modal = document.querySelector("#modal");
//    var links = document.querySelectorAll("[data-remote][data-modal]");
//    links.forEach(function (link) {
//        Object.assign("html", link.dataset);
//        link.onclick = function (e) {
//            alert('go to ' + e.target);
//            fetch(e.target.href)
//                .then(response => response.text())
//                .then(html => { modal.innerHTML = html })
//                .then(p => showModal());
//            e.preventDefault();
//        }
//    });
//}

//changeAllLinkIntoModal();