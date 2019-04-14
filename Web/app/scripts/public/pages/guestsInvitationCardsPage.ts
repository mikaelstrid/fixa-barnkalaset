interface GuestModel {
    firstName: string;
    lastName: string;
    streetAddress: string;
    postalCode: string;
    postalCity: string;
}

interface InvitationModel {
    id: string;
    guest: GuestModel;
}

export class GuestsInvitationCardsPage {
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
                .modal('show')
        });

        $('[data-remove-invitation-button]').click(event => {
            let invitationId = $(event.currentTarget).data('invitation-id');
            this.removeInvitation(invitationId);
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
                let invitation = {
                    id: data,
                    guest: guestModel
                };
                this.appendAddedInvitationToList(invitation);
                this.clearAddGuestForm();
            })
            .fail(() => {
                $(".ui.modal .ui.error.message").show();
            });
    }

    private removeInvitation(invitationId: string): void {
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


    private appendAddedInvitationToList(invitation: InvitationModel): void {
        $('[data-invitations-list]').append(`
            <div class="item" data-invitation-id='${invitation.id}'>
                <div class="right floated content">
                    <a data-remove-invitation-button data-invitation-id="${invitation.id}"><i class="large remove icon"></i></a>
                </div>
                <div class="content">
                    <div class="header">${invitation.guest.firstName} ${invitation.guest.lastName}</div>
                    <div class="description">${invitation.guest.streetAddress}, ${invitation.guest.postalCode} ${invitation.guest.postalCity}</div>
                </div>
            </div>
        `);
        $(`[data-remove-invitation-button][data-invitation-id="${invitation.id}"]`).click(event => {
            let invitationId = $(event.currentTarget).data('invitation-id');
            this.removeInvitation(invitationId);
        });
    }

    private removeInvitationFromTable(invitationId: string): void {
        $('[data-invitations-list] .item[data-invitation-id="' + invitationId + '"]').remove();
    }

    private clearAddGuestForm(): void {
        let $form = $(".ui.form");
        $("input[type='text']", $form).val("");
        $(".ui.modal .ui.error.message").hide();
    }
}