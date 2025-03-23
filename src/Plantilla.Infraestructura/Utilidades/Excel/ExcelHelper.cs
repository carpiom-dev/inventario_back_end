using OfficeOpenXml;
using Plantilla.Infraestructura.Modelo.Excel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Plantilla.Infraestructura.Utilidades.Excel
{

    public static class ExcelHelper
    {
        public static (ExcelDto? excel, string error) ExportToExcel<T>(IEnumerable<T> data, string nombreArchivo = "excel.xlsx")
        {
            if (data == null || !data.Any())
                return (null, "La lista de datos no puede estar vacía.");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Hoja1");

            // Obtener propiedades del tipo genérico T
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Escribir encabezados
            // Obtener nombres personalizados de las columnas (usando Display o el nombre de la propiedad)
            for (int col = 0; col < properties.Length; col++)
            {
                var displayAttr = properties[col].GetCustomAttribute<DisplayAttribute>();
                string columnName = displayAttr?.Name ?? properties[col].Name;

                worksheet.Cells[1, col + 1].Value = columnName;
                worksheet.Cells[1, col + 1].Style.Font.Bold = true;
            }

            // Escribir datos
            for (int row = 0; row < data.Count(); row++)
            {
                for (int col = 0; col < properties.Length; col++)
                {
                    worksheet.Cells[row + 2, col + 1].Value = properties[col].GetValue(data.ElementAt(row));
                }
            }

            var byteArray = package.GetAsByteArray();

            var excel = new ExcelDto()
            {
                Base64 = Convert.ToBase64String(byteArray),
                NombreArchivo = nombreArchivo,
            };

            return (excel, string.Empty);
        }
    }


}
