using System;
using System.Collections.Generic;
using System.IO;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;

namespace Pixel.FixaBarnkalaset.Infrastructure.Pdf
{
    public class PdfService : IPdfService
    {
        public Stream GetInvitationCardsReviewStream(string reviewTemplateUrl, IEnumerable<Invitation> partyInvitations)
        {
            var invitationDocuments = new List<PdfLoadedDocument>();
            foreach (var invitation in partyInvitations)
            {
                using (var stream = new FileStream(reviewTemplateUrl, FileMode.Open))
                {
                    var loadedDocument = new PdfLoadedDocument(stream);
                    invitationDocuments.Add(loadedDocument);
                }
            }
            var mergedDocument = new PdfLoadedDocument();
            PdfDocumentBase.Merge(mergedDocument, invitationDocuments);
            var outputStream = new MemoryStream();
            mergedDocument.Save(outputStream);
            return outputStream;
        }

        public byte[] GenerateInvitations(byte[] template, int instancesInTemplate, IEnumerable<Invitation> invitations)
        {
            var invitationDocuments = new List<PdfLoadedDocument>();
            foreach (var invitation in invitations)
            {
                var loadedDocument = new PdfLoadedDocument(template);
                invitationDocuments.Add(loadedDocument);
            }
            var mergedDocument = new PdfLoadedDocument();
            PdfDocumentBase.Merge(mergedDocument, invitationDocuments);
            var outputStream = new MemoryStream();
            mergedDocument.Save(outputStream);
            return outputStream.ToArray();
        }

        public static void FillFormFields()
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
