using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using IronOcr;

namespace ElationAutoImport
{ 
    class CaptureAdvText
    {
        private string textArray;
        public CaptureAdvText(string fileName)
        {
            IronOcr.Installation.LicenseKey = "IRONOCR.HILOFAMILYMEDICINE.IRO210901.8235.19124.109012-D5BFDC8A8D-B2ONIURAW5G4HT7-5GZSCFJBNMRH-P4H7AVCE4X2P-4L55LJ5TUTK2-NZBWJPRSE67C-3LYBHV-LPOT3EG5VOCHEA-PROFESSIONAL.SUB-2C7ORH.RENEW.SUPPORT.01.SEP.2022";
            var Ocr = new IronTesseract();
            StringBuilder text1 = new StringBuilder();
            
            if (File.Exists(fileName) && System.IO.Path.GetExtension(fileName) == ".pdf")
            {

                var pdfDocument = new PdfDocument(new PdfReader(fileName));
                var strategy = new LocationTextExtractionStrategy();
                //var strategy = new iText.Kernel.Pdf.Canvas.Parser.Listener.
                StringBuilder processed = new StringBuilder();
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
                {
                    var page = pdfDocument.GetPage(i);
                    //string text = PdfTextExtractor.GetTextFromPage(page, strategy);
                    LocationTextExtractionStrategy laxStrategy = new LocationTextExtractionStrategy(new LaxTextChunkLocationStrategy());
                    string text = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i), laxStrategy);
                    text1.Append(text);
                }
                /*for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {

                    //ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                    currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                    text1.Append(currentText);
                }
                pdfReader.Close();*/
                pdfDocument.Close();
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
            if (File.Exists(fileName) && System.IO.Path.GetExtension(fileName) == ".pdf")
            {
                StringBuilder text2 = new StringBuilder();
                var pdfDocument = new PdfDocument(new PdfReader(fileName));
                var strategy = new iText.Kernel.Pdf.Canvas.Parser.Listener.LocationTextExtractionStrategy();
                StringBuilder processed = new StringBuilder();
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
                {
                    var page = pdfDocument.GetPage(i);
                    string text = PdfTextExtractor.GetTextFromPage(page, strategy);
                    text1.Append(text);
                }
            }
            /*if (File.Exists(fileName))
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
            }*/
            textArray = text1.ToString();
        }

        public string ReturnInfo()
        {
            return textArray;
        }

        class LaxTextChunkLocationStrategy : LocationTextExtractionStrategy.ITextChunkLocationStrategy
        {
            public LaxTextChunkLocationStrategy()
            {
            }

            public virtual ITextChunkLocation CreateLocation(TextRenderInfo renderInfo, LineSegment baseline)
            {
                return new TextChunkLocationLaxImp(baseline.GetStartPoint(), baseline.GetEndPoint(), renderInfo.GetSingleSpaceWidth());
            }
        }

        class TextChunkLocationLaxImp : ITextChunkLocation
        {
            private const float DIACRITICAL_MARKS_ALLOWED_VERTICAL_DEVIATION = 2;
            private readonly Vector startLocation;
            private readonly Vector endLocation;
            private readonly Vector orientationVector;
            private readonly int orientationMagnitude;
            private readonly int distPerpendicular;
            private readonly float distParallelStart;
            private readonly float distParallelEnd;
            private readonly float charSpaceWidth;

            public TextChunkLocationLaxImp(Vector startLocation, Vector endLocation, float charSpaceWidth)
            {
                this.startLocation = startLocation;
                this.endLocation = endLocation;
                this.charSpaceWidth = charSpaceWidth;
                Vector oVector = endLocation.Subtract(startLocation);
                if (oVector.Length() == 0)
                {
                    oVector = new Vector(1, 0, 0);
                }
                orientationVector = oVector.Normalize();
                orientationMagnitude = (int)(Math.Atan2(orientationVector.Get(Vector.I2), orientationVector.Get(Vector.I1)) * 1000);
                Vector origin = new Vector(0, 0, 1);
                distPerpendicular = (int)(startLocation.Subtract(origin)).Cross(orientationVector).Get(Vector.I3);
                distParallelStart = orientationVector.Dot(startLocation);
                distParallelEnd = orientationVector.Dot(endLocation);
            }

            public virtual int OrientationMagnitude()
            {
                return orientationMagnitude;
            }

            public virtual int DistPerpendicular()
            {
                return distPerpendicular;
            }

            public virtual float DistParallelStart()
            {
                return distParallelStart;
            }

            public virtual float DistParallelEnd()
            {
                return distParallelEnd;
            }

            public virtual Vector GetStartLocation()
            {
                return startLocation;
            }

            public virtual Vector GetEndLocation()
            {
                return endLocation;
            }

            public virtual float GetCharSpaceWidth()
            {
                return charSpaceWidth;
            }

            public virtual bool SameLine(ITextChunkLocation @as)
            {
                if (OrientationMagnitude() != @as.OrientationMagnitude())
                {
                    return false;
                }
                int distPerpendicularDiff = DistPerpendicular() - @as.DistPerpendicular();
                if (Math.Abs(distPerpendicularDiff) < 2)
                {
                    return true;
                }
                LineSegment mySegment = new LineSegment(startLocation, endLocation);
                LineSegment otherSegment = new LineSegment(@as.GetStartLocation(), @as.GetEndLocation());
                return Math.Abs(distPerpendicularDiff) <= DIACRITICAL_MARKS_ALLOWED_VERTICAL_DEVIATION && (mySegment.GetLength() == 0 || otherSegment.GetLength() == 0);
            }

            public virtual float DistanceFromEndOf(ITextChunkLocation other)
            {
                return DistParallelStart() - other.DistParallelEnd();
            }

            public virtual bool IsAtWordBoundary(ITextChunkLocation previous)
            {
                if (startLocation.Equals(endLocation) || previous.GetEndLocation().Equals(previous.GetStartLocation()))
                {
                    return false;
                }
                float dist = DistanceFromEndOf(previous);
                if (dist < 0)
                {
                    dist = previous.DistanceFromEndOf(this);
                    //The situation when the chunks intersect. We don't need to add space in this case
                    if (dist < 0)
                    {
                        return false;
                    }
                }
                return dist > GetCharSpaceWidth() / 2.0f;
            }

            internal static bool ContainsMark(ITextChunkLocation baseLocation, ITextChunkLocation markLocation)
            {
                return baseLocation.GetStartLocation().Get(Vector.I1) <= markLocation.GetStartLocation().Get(Vector.I1) &&
                     baseLocation.GetEndLocation().Get(Vector.I1) >= markLocation.GetEndLocation().Get(Vector.I1) && Math.
                    Abs(baseLocation.DistPerpendicular() - markLocation.DistPerpendicular()) <= DIACRITICAL_MARKS_ALLOWED_VERTICAL_DEVIATION;
            }
        }

    }
}
