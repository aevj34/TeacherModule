using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Common.Infrastructure.Security
{

    public class HeadeFooter : PdfPageEventHelper
    {

        public string title { get; set; }
        public string subTitle { get; set; }
        public string schoolName { get; set; }
        public string header1 { get; set; }
        public string header2 { get; set; }
        public string header3 { get; set; }

        public HeadeFooter(string title, string subTitle, string schoolName, string header1, string header2, string header3)
        {
            this.title = title;
            this.subTitle = subTitle;
            this.schoolName = schoolName;
            this.header1 = header1;
            this.header2 = header2;
            this.header3 = header3;
        }

       
        public override void OnEndPage(PdfWriter writer, Document document)
        {

            PdfPTable header = new PdfPTable(4);
            header = GetHeader(document);
            header.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 150, writer.DirectContent);

            PdfPTable footer = new PdfPTable(4);
            footer = GetFooter(writer,document);
            footer.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetBottom(document.BottomMargin) - 5, writer.DirectContent);

        }

         PdfPTable GetHeader(Document document)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);
            Font headerfontText = new Font(bf, 18, 0, BaseColor.Black);
            Font subHeaderfontText = new Font(bf, 13, 0, BaseColor.DarkGray);
            Font footerfontText = new Font(bf, 8, 0, BaseColor.DarkGray);

            Font subHeader1fontText = new Font(bf, 13, Font.BOLD, BaseColor.Black);
            Font subHeader2fontText = new Font(bf, 13, Font.NORMAL, BaseColor.Black);
            Font subHeader3fontText = new Font(bf, 13, Font.NORMAL, BaseColor.Black);

            PdfPTable header = new PdfPTable(4);
            header.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            header.DefaultCell.Border = 0;

            PdfPCell cell = new PdfPCell(new Paragraph("Fecha: " + String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Now), footerfontText));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            header.AddCell(cell);

            cell = new PdfPCell(new Paragraph(this.schoolName, footerfontText));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            cell.Colspan = 2;
            header.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Hora: " + string.Format("{0:t}", DateTime.Now), footerfontText));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            header.AddCell(cell);

            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.Gray, Element.ALIGN_LEFT, 1)));
            cell = new PdfPCell(p);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            cell.Colspan = 4;
            header.AddCell(cell);

            cell = new PdfPCell(new Paragraph(title.ToUpper(), headerfontText));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.PaddingTop = 15;
            cell.Border = 0;
            cell.Colspan = 4;
            header.AddCell(cell);

            cell = new PdfPCell(new Paragraph(subTitle.ToUpper(), subHeaderfontText));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            cell.Colspan = 4;
            header.AddCell(cell);

            cell = new PdfPCell(new Paragraph(Chunk.Newline));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            cell.Colspan = 4;
            cell.PaddingBottom = 10;
            header.AddCell(cell);


            cell = new PdfPCell(new Paragraph(this.header1, subHeader1fontText));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            cell.Colspan = 4;
            header.AddCell(cell);

            cell = new PdfPCell(new Paragraph(this.header2, subHeader2fontText));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            cell.Colspan = 2;
            header.AddCell(cell);

            cell = new PdfPCell(new Paragraph(this.header3, subHeader3fontText));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            cell.Colspan = 2;
            header.AddCell(cell);

            return header;
        }

        PdfPTable GetFooter(PdfWriter writer, Document document)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);
            Font footerfontText = new Font(bf, 8, 0, BaseColor.DarkGray);

            PdfPTable footer = new PdfPTable(4);
            footer.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            footer.DefaultCell.Border = 0;

            PdfPCell cell = new PdfPCell(new Paragraph("Fecha: " + String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Now), footerfontText));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            footer.AddCell(cell);

            cell = new PdfPCell(new Paragraph(this.schoolName, footerfontText));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            cell.Colspan = 2;
            footer.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Pág. " + writer.PageNumber.ToString(), footerfontText));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            footer.AddCell(cell);

            return footer;
        }




        }
}
