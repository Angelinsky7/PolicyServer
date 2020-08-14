window.policyServer1.listController = function() {
    var checkboxes = document.querySelectorAll('input.js-list-check-box');

    return {
        selectedRows: [],
        deleteMultipleHref(base_href) {
            if (this.selectedRows.length === 0) { return ""; }
            return base_href + "?" + this.selectedRows.map(p => `guid=${p}`).join('&');
        },
        checkTick(src) {
            var checkedCount = document.querySelectorAll('input.js-list-check-box:checked').length;
            this.$refs.checkAll.checked = checkedCount === checkboxes.length;
            this.$refs.checkAll.indeterminate = checkedCount > 0 && checkedCount < checkboxes.length;
            var srcGuid = src.getAttribute("data-row-id");
            src.checked ? this.selectedRows.push(srcGuid) : this.selectedRows.remove(srcGuid);
        },
        checkAll(ref) {
            this.selectedRows = [];
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = ref.checked;
                ref.checked ? this.selectedRows.push(checkboxes[i].dataset.rowId) : this.selectedRows.remove(checkboxes[i].dataset.rowId);
            }
        },
    };
};