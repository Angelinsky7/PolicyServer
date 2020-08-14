window.policyServer1.dropdown = function () {
    return {
        show: false,
        toggle() { this.show = !this.show; },
        open() { this.show = true },
        close() { this.show = false },
        isOpen() { return this.show === true },
    }
};