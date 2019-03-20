using iTextSharp.text;
using iTextSharp.text.pdf;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Common.Infrastructure.Security
{
    public static class FormatPDF
    {

        public static void setFormatHeaderCell(PdfPCell cell)
        {
            cell.BackgroundColor = iTextSharp.text.html.WebColors.GetRgbColor("#364150");
            cell.PaddingTop = 5;
            cell.PaddingRight = 0;
            cell.PaddingBottom = 5;
            cell.PaddingLeft = 5;

           // cell.BorderColor = iTextSharp.text.html.WebColors.GetRgbColor("#E0E0E0");

        }

        public static void setFormatHeaderCellVertical(PdfPCell cell)
        {
            cell.BackgroundColor = iTextSharp.text.html.WebColors.GetRgbColor("#364150");
            cell.PaddingTop = 5;
            cell.PaddingRight = 0;
            cell.PaddingBottom = 5;
            cell.PaddingLeft = 5;

            // cell.BorderColor = iTextSharp.text.html.WebColors.GetRgbColor("#E0E0E0");

        }


        public static void setFormatItemCell(PdfPCell cell)
        {
            cell.BackgroundColor = iTextSharp.text.html.WebColors.GetRgbColor("#FFFFFF");
            cell.PaddingTop = 5;
            cell.PaddingRight = 1;
            cell.PaddingBottom = 5;
            cell.PaddingLeft = 1;
            //cell.BorderColor = iTextSharp.text.html.WebColors.GetRgbColor("#364150");
        }

    }
}
