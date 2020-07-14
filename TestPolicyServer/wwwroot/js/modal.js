function initModal() {
    var modals = document.createElement("div");
    modals.id = "modals-container";
    document.body.append(modals);

    document.addEventListener("click", (e) => {
        var el = e.target, found;
        while (el && !(found = el.matches("[data-remote][data-modal]"))) {
            el = el.parentElement;
        }
        if (found) {
            e.preventDefault();

            var link_href = el.href;
            if (link_href.indexOf("?") == -1) {
                link_href += "?modal=true";
            } else {
                link_href += "&modal=true";
            }
            //link_href.searchParams.append('param_1', 'val_1'); 

            fetch(link_href)
                .then(response => response.text())
                .then(html => createModal(html, modals));

            return false;
        }
    });
};

function createModal(modal_content, container) {
    var modal = document.createElement("div");
    var id = (+container.getAttribute("modals-count")) + 1;
    container.setAttribute("modals-count", id);
    modal.id = "modal_" + id;
    modal.className = "modal";
    modal.innerHTML = modal_content;
    container.append(modal);

    Alpine.discoverUninitializedComponents((el) => {
        Alpine.initializeComponent(el)
    }, container)
}

window.modal = function() {
    var el;

    return {
        isOpen: false,
        openModal(el, $dispatch) {
            this.isOpen = true;
            this.el = el;
            $dispatch('custom-event', 'dropdown.close');
        },
        executeFormAction() {
            var modal = window.helper.getClosest(this.el, ".modal");
            if (modal != null) {
                var form = modal.querySelector("form#modalButton");
                if (form != null) {
                    form.submit();
                }
                this.closeModal();
            }
        },
        closeModal() {
            var modal = window.helper.getClosest(this.el, ".modal");
            var modals = document.querySelector("#modals-container");
            if (modals != null && modal != null) {
                modals.removeChild(modal);
            }
        }
    }
};

initModal();