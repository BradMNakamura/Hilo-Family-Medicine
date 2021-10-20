using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using IronOcr;

namespace ElationAutoImport
{
    class CaptureSimpleText
    {
        private string textArray;
        public CaptureSimpleText(string fileName)
        {
            IronOcr.Installation.LicenseKey = "IRONOCR.HILOFAMILYMEDICINE.IRO210901.8235.19124.109012-D5BFDC8A8D-B2ONIURAW5G4HT7-5GZSCFJBNMRH-P4H7AVCE4X2P-4L55LJ5TUTK2-NZBWJPRSE67C-3LYBHV-LPOT3EG5VOCHEA-PROFESSIONAL.SUB-2C7ORH.RENEW.SUPPORT.01.SEP.2022";
            var Ocr = new IronTesseract();
            StringBuilder text1 = new StringBuilder();
            if (File.Exists(fileName) && System.IO.Path.GetExtension(fileName) == ".pdf")
            {
                PdfReader pdfReader = new PdfReader(fileName);

                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                    currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                    text1.Append(currentText);
                }
                pdfReader.Close();

                if (text1.Length == 0)
                {
                    using (var Input = new OcrInput())
                    {
                        Input.AddPdf(fileName);

                        // clean up twisted pages
                        Input.Deskew();

                        var Result = Ocr.Read(Input);
                        Result.SaveAsSearchablePdf(fileName);
                    }
                }

            }
            StringBuilder text = new StringBuilder();
            if (File.Exists(fileName) && System.IO.Path.GetExtension(fileName) == ".pdf")
            {
                PdfReader pdfReader = new PdfReader(fileName);

                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                    currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                    text.Append(currentText);
                }
                pdfReader.Close();
            }
            textArray = text.ToString();
        }

        public string ReturnInfo()
        {
            return textArray;
        }
    }
}
