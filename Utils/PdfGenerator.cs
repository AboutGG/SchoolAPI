using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Reflection;

namespace SchoolAPI.Utils
{
    public class PdfGenerator
    {
        public static byte[] GeneratePdf<T>(ICollection<T> value)
        {
            ///<summary>Create a new PDF document using iTextSharp</summary>
            using (MemoryStream ms = new MemoryStream())
            {
                #region Create new document

                iTextSharp.text.Document document = new iTextSharp.text.Document();
                try
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, ms);
                    document.Open();

                    #region Create and add Paragraph
                    ///<summary>Add the content on the document</summary> Aggiungi il contenuto al documento
                    Paragraph paragraph = new Paragraph("There are all user login data");

                    #region Paragraph style property
                    paragraph.Alignment = Element.ALIGN_CENTER; //align the paragraph to center
                    paragraph.SpacingAfter = 20f;
                    #endregion

                    document.Add(paragraph);
                    #endregion

                    #region Create Table Header
                    ///<summary>Create the table</summary>
                    PdfPTable table = new PdfPTable(value.GetType().GetProperties().Length); // this method get the number of the column

                    ///<summary> It retrieves the names of its public properties and adds them as cells to the table</summary>
                    var propertyNames = value.First().GetType().GetProperties().Select(p => p.Name);
                    foreach (string dummy in propertyNames)
                        table.AddCell(dummy);
                    #endregion

                    #region Popolate and add rows' table

                    ///<summary> Add the rows on the table </summary>
                    foreach (T item in value)
                    {
                        foreach (PropertyInfo property in item.GetType().GetProperties()) //PropertyInfo represents the information of a property of a class.
                        {
                            table.AddCell(property.GetValue(item).ToString()); //takes the property's value of a specific object in this case: property
                        }
                    }

                    /// <summary>Add the table on the document</summary>
                    document.Add(table);

                    #endregion
                }
                catch (DocumentException e)
                {
                    Console.Error.WriteLine(e.Message);
                }
                finally
                {
                    document.Close();
                }
                return ms.ToArray();

                #endregion
            }
        }
    }
}
