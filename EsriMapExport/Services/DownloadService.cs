using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace EsriMapExport.Services
{
    class DownloadService
    {
        public static async Task DownloadImage(Uri requestUri, string filename)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
            using (
                Stream contentStream = await (await client.SendAsync(request)).Content.ReadAsStreamAsync(),
                stream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                    + "\\" + filename, FileMode.Create, FileAccess.Write, FileShare.None, 3145728, true))
            {
                await contentStream.CopyToAsync(stream);
            }
            
        }

        public static async Task DownloadPDF(string filename)
        {
            Document pdfDoc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Environment
                .GetFolderPath(Environment.SpecialFolder.Desktop)
                    + "\\" + "pdf.pdf", FileMode.Create));

            pdfDoc.Open();
            pdfDoc.Add(new Paragraph("Some data"));
            pdfDoc.Add(new Paragraph("Bla bla bla"));

            PdfContentByte cb = writer.DirectContent;
            cb.MoveTo(pdfDoc.PageSize.Width / 2, pdfDoc.PageSize.Height / 2);
            cb.LineTo(pdfDoc.PageSize.Width / 2, pdfDoc.PageSize.Height);
            cb.Stroke();
            pdfDoc.NewPage();
            var image = Image.GetInstance(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                    + "\\" + filename);
            image.SetAbsolutePosition(1, 1);

            cb.AddImage(image);


            pdfDoc.Close();
        }
    }
}
