interface GuestModel {
    firstName: string;
    lastName: string;
    streetAddress: string;
    postalCode: string;
    postalCity: string;
}

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
                            self.addGuest($('.ui.modal').data('party-id'));
                        }
                        return false;
                    },
                    onDeny: function () {
                        self.clearAddGuestForm();
                    }
                })
                .modal('show')
        });
    }


    private validateAddGuestForm(): boolean {
        if ($('.ui.form').form('is valid')) {
            return true;
        } else {
            $('.ui.form').form('validate form');
            return false;
        }
    }

    private addGuest(partyId: string): void {
        let $form = $(".ui.form");
        let guestModel = {
            partyId: partyId,
            firstName: $("input[name='FirstName']", $form).val().toString(),
            lastName: $("input[name='LastName']", $form).val().toString(),
            streetAddress: $("input[name='StreetAddress']", $form).val().toString(),
            postalCode: $("input[name='PostalCode']", $form).val().toString(),
            postalCity: $("input[name='PostalCity']", $form).val().toString()
        };

        $(".ui.modal .ui.error.message").hide();
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'type': 'POST',
            'url': "/api/invitationcards/add-guest-and-invitation",
            'data': JSON.stringify(guestModel),
            'dataType': 'json',
        })
            .done(data => {
                this.appendAddedGuestToTable(guestModel);
                this.clearAddGuestForm();
            })
            .fail(() => {
                $(".ui.modal .ui.error.message").show();
            });
    }

    private appendAddedGuestToTable(guest: GuestModel): void {
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

    private clearAddGuestForm(): void {
        let $form = $(".ui.form");
        $("input[type='text']", $form).val("");
        $(".ui.modal .ui.error.message").hide();
    }
}