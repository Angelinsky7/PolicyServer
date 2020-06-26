Object.defineProperty(Array.prototype, "remove", {
    value: function (item) { return this.splice(this.indexOf(item), 1); }
});