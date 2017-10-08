export class WhichInvitationCardsPage {
    initPage() {
        $('.ui.form')
            .form({
            fields: {
                FirstName: 'empty',
                LastName: 'empty',
                StreetAddress: 'empty',
                PostalCode: 'regExp[/^\\d{3}\\s?\\d{2}$/]',
                PostalCity: 'empty'
            }
        });
        $("#addGuestButton").click(() => {
            var self = this;
            $('.ui.modal')
                .modal({
                onApprove: function () {
                    if (self.validateAddGuestForm()) {
                        self.addGuest();
                    }
                    return false;
                },
                onDeny: function () {
                    self.clearAddGuestForm();
                }
            })
                .modal('show');
        });
    }
    validateAddGuestForm() {
        if ($('.ui.form').form('is valid')) {
            return true;
        }
        else {
            $('.ui.form').form('validate form');
            return false;
        }
    }
    addGuest() {
        let $form = $(".ui.form");
        let guestModel = {
            firstName: $("input[name='FirstName']", $form).val().toString(),
            lastName: $("input[name='LastName']", $form).val().toString(),
            streetAddress: $("input[name='StreetAddress']", $form).val().toString(),
            postalCode: $("input[name='PostalCode']", $form).val().toString(),
            postalCity: $("input[name='PostalCity']", $form).val().toString()
        };
        $(".ui.modal .ui.error.message").hide();
        $.post("/api/invitationcards/add-guest", guestModel)
            .done(() => {
            this.appendAddedGuestToTable(guestModel);
            this.clearAddGuestForm();
        })
            .fail(() => {
            $(".ui.modal .ui.error.message").show();
        });
    }
    appendAddedGuestToTable(guest) {
        $('#guestTable > tbody:last-child').append(`
            <tr>
                <td>${guest.firstName}</td>
                <td>${guest.lastName}</td>
                <td>${guest.streetAddress}</td>
                <td>${guest.postalCode}</td> 
                <td>${guest.postalCity}</td>
            </tr>
        `);
    }
    clearAddGuestForm() {
        let $form = $(".ui.form");
        $("input[type='text']", $form).val("");
        $(".ui.modal .ui.error.message").hide();
    }
}
