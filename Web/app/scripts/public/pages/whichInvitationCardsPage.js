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
        $('#addGuestButton').click(() => {
            var self = this;
            $('#removeErrorMessage').hide();
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
                .modal('show');
        });
        $('[data-remove-invitation-button]').click(event => {
            let invitationId = $(event.currentTarget).data('invitation-id');
            this.removeInvitation(invitationId);
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
    addGuest(partyId) {
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
            let invitation = {
                id: data,
                guest: guestModel
            };
            this.appendAddedInvitationToTable(invitation);
            this.clearAddGuestForm();
        })
            .fail(() => {
            $(".ui.modal .ui.error.message").show();
        });
    }
    removeInvitation(invitationId) {
        $('#removeErrorMessage').hide();
        $.ajax({
            'type': 'DELETE',
            'url': '/api/invitationcards/remove-guest-and-invitation/' + invitationId,
        })
            .done(data => {
            this.removeInvitationFromTable(invitationId);
        })
            .fail(() => {
            $('#removeErrorMessage').show();
        });
    }
    appendAddedInvitationToTable(invitation) {
        $('#invitationTable > tbody:last-child').append(`
            <tr data-invitation-id='${invitation.id}'>
                <td>${invitation.guest.firstName}</td>
                <td>${invitation.guest.lastName}</td>
                <td>${invitation.guest.streetAddress}</td>
                <td>${invitation.guest.postalCode}</td> 
                <td>${invitation.guest.postalCity}</td>
                <td><a id="removeInvitationButton" data-invitation-id="${invitation.id}"><i class="trash outline link icon"></i></a></td>
            </tr>
        `);
    }
    removeInvitationFromTable(invitationId) {
        $('#invitationTable tr[data-invitation-id="' + invitationId + '"]').remove();
    }
    clearAddGuestForm() {
        let $form = $(".ui.form");
        $("input[type='text']", $form).val("");
        $(".ui.modal .ui.error.message").hide();
    }
}
