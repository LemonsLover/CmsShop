$(function () {
    if ($("a.confirmDeletion").length) {
        $("a.confirmDeletion").click(() => {
            if (!confirm("Confirm deletion")) return false;
        });
    }
    if ($("div.notification").length) {
        setTimeout(() => {
            $("div.notification").fadeOut();
        }, 2000)
    }
})
