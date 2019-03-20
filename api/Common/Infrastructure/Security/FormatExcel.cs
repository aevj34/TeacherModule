using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Common.Infrastructure.Security
{
    public static class FormatExcel
    {

        public static void setFormatItemCell(IWorkbook workbook, ICell cell)
        {
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.BorderBottom = BorderStyle.Thin;
            boldStyle.BorderTop = BorderStyle.Thin;
            boldStyle.BorderLeft = BorderStyle.Thin;
            boldStyle.BorderRight = BorderStyle.Thin;
            cell.CellStyle = boldStyle;
        }

        public static void setFormatColumnCell(IWorkbook workbook, ICell cell)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)11;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            h1Font.Boldweight = (short)FontBoldWeight.Bold;
            boldStyle.BorderBottom = BorderStyle.Thin;
            boldStyle.BorderTop = BorderStyle.Thin;
            boldStyle.BorderLeft = BorderStyle.Thin;
            boldStyle.BorderRight = BorderStyle.Thin;
            boldStyle.Alignment = HorizontalAlignment.Center;
            boldStyle.VerticalAlignment = VerticalAlignment.Center;
            boldStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            boldStyle.SetFont(h1Font);
            cell.CellStyle = boldStyle;
        }

        public static void setFormatColumnCellVertical(IWorkbook workbook, ICell cell)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)11;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            h1Font.Boldweight = (short)FontBoldWeight.Bold;
            boldStyle.BorderBottom = BorderStyle.Thin;
            boldStyle.BorderTop = BorderStyle.Thin;
            boldStyle.BorderLeft = BorderStyle.Thin;
            boldStyle.BorderRight = BorderStyle.Thin;
            boldStyle.Alignment = HorizontalAlignment.Center;
            boldStyle.VerticalAlignment = VerticalAlignment.Bottom;
            boldStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            boldStyle.Rotation = 90;
            boldStyle.SetFont(h1Font);
            cell.CellStyle = boldStyle;
        }


        public static void setFormatTitleCellBold(IWorkbook workbook, ICell cell)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)13;
            h1Font.Boldweight = (short)FontBoldWeight.Bold;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            h1Font.Boldweight = (short)FontBoldWeight.Bold;
            boldStyle.SetFont(h1Font);
            cell.CellStyle = boldStyle;
        }

        public static void setFormatSubTitleCell(IWorkbook workbook, ICell cell)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)12;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.SetFont(h1Font);
            cell.CellStyle = boldStyle;
        }

        public static void setFormatHeaderCellBold(IWorkbook workbook, ICell cell)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)12;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            h1Font.Boldweight = (short)FontBoldWeight.Bold;
            boldStyle.SetFont(h1Font);
            cell.CellStyle = boldStyle;
        }

        public static void setFormatHeaderCell(IWorkbook workbook, ICell cell)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)12;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.SetFont(h1Font);
            cell.CellStyle = boldStyle;
        }

        public static void setFormatFooterCell(IWorkbook workbook, ICell cell)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)11;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.SetFont(h1Font);
            cell.CellStyle = boldStyle;
        }

        public static void setFormatFooterCellBold(IWorkbook workbook, ICell cell)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)11;
            h1Font.Boldweight = (short)FontBoldWeight.Bold;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.SetFont(h1Font);
            cell.CellStyle = boldStyle;
        }

    }
}
