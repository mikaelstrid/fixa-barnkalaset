using System;
using System.IO;
using Syncfusion.Pdf.Parsing;

namespace Pixel.FixaBarnkalaset.Infrastructure.Pdf
{
    public class PdfService
    {
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
