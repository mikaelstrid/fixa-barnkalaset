using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Business;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;

namespace Pixel.FixaBarnkalaset.Infrastructure.Pdf
{
    public class PdfService : IPdfService
    {
        //public Stream GetInvitationCardsReviewStream(string reviewTemplateUrl, IEnumerable<Invitation> partyInvitations)
        //{
        //    var invitationDocuments = new List<PdfLoadedDocument>();
        //    foreach (var invitation in partyInvitations)
        //    {
        //        using (var stream = new FileStream(reviewTemplateUrl, FileMode.Open))
        //        {
        //            var loadedDocument = new PdfLoadedDocument(stream);
        //            invitationDocuments.Add(loadedDocument);
        //        }
        //    }
        //    var mergedDocument = new PdfLoadedDocument();
        //    PdfDocumentBase.Merge(mergedDocument, invitationDocuments);
        //    var outputStream = new MemoryStream();
        //    mergedDocument.Save(outputStream);
        //    return outputStream;
        //}

        public byte[] GenerateInvitations(byte[] template, int instancesInTemplate, IEnumerable<Invitation> invitations)
        {
            var invitationsArray = invitations as Invitation[] ?? invitations.ToArray();

            var pdfDocuments = new object[(int) Math.Ceiling((decimal) invitationsArray.Length / instancesInTemplate)];

            PdfLoadedDocument currentPdfDocument = null;
            for (var i=0; i<invitationsArray.Length; i++)
            {
                var instanceNumber = i % instancesInTemplate + 1;

                if (instanceNumber == 1)
                    currentPdfDocument = new PdfLoadedDocument(template);

                FillFormFields(ref currentPdfDocument, instanceNumber, invitationsArray[i]);

                if (instanceNumber == 1)
                    pdfDocuments[i / instancesInTemplate] = currentPdfDocument;
            }
            var mergedDocument = PdfDocumentBase.Merge(null, pdfDocuments);
            var outputStream = new MemoryStream();
            mergedDocument.Save(outputStream);
            return outputStream.ToArray();
        }


        private static void FillFormFields(ref PdfLoadedDocument document, int instanceNumber, Invitation invitation)
        {
            var form = document.Form;
            FillSingleField(ref form, "FodelsedagsbarnetsNamn", instanceNumber, invitation.Party.NameOfBirthdayChild);
            FillSingleField(ref form, "Starttid", instanceNumber, invitation.Party.StartTime?.ToString("HH:mm") ?? "");
            FillSingleField(ref form, "Sluttid", instanceNumber, invitation.Party.EndTime?.ToString("HH:mm") ?? "");
            FillSingleField(ref form, "PlatsNamn", instanceNumber, invitation.Party.LocationName);
            FillSingleField(ref form, "Gatuadress", instanceNumber, invitation.Party.StreetAddress);
            FillSingleField(ref form, "Postort", instanceNumber, invitation.Party.PostalCity);
            FillSingleField(ref form, "Postnummer", instanceNumber, invitation.Party.PostalCode);
            FillSingleField(ref form, "OsaDatum", instanceNumber, invitation.Party.RsvpDate?.ToString("yyyy-MM-dd") ?? "");
            FillSingleField(ref form, "OsaBeskrivning", instanceNumber, invitation.Party.RenderRsvpContactInformation());
        }

        private static void FillSingleField(ref PdfLoadedForm form, string fieldName, int instanceNumber, string value)
        {
            if (form.Fields[$"{fieldName}_{instanceNumber}"] is PdfLoadedTextBoxField field)
            {
                field.Text = value;
            }
        }


        internal static void FillFormFields()
        {
            using (var stream = new FileStream(@"c:\temp\invitation-card-template-1.pdf", FileMode.Open))
            {
                var loadedDocument = new PdfLoadedDocument(stream);
                var loadedForm = loadedDocument.Form;

                foreach (var field in loadedForm.Fields)
                {
                    (field as PdfLoadedTextBoxField).Text = (DateTime.Now.Ticks % 10000).ToString();
                }

                using (var stream2 = new FileStream(@"c:\temp\invitation-card-template-2.pdf", FileMode.OpenOrCreate))
                {
                    loadedDocument.Save(stream2);
                }
            }

            ////Get the loaded text box field and fill it.
            //PdfLoadedTextBoxField loadedTextBoxField = loadedForm.Fields[0] as PdfLoadedTextBoxField;
            //loadedTextBoxField.Text = "First Name";

            ////Save the modified document.
            //loadedDocument.Save("sample.pdf");

            ////Close the document
            //loadedDocument.Close(true);
        }
    }
}
