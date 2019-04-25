using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wa_iText_O
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


           // CrearPDF();


        }
        private Cell SetCelda(string text)
        {
            var txt_empresa = new Text(text);
            //txt_empresa.SetFontColor(iText.Kernel.Colors.ColorConstants.BLUE);
            //txt_empresa.SetFont(PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA));
            txt_empresa.SetFontSize(10);
            var par_empresa = new Paragraph();
            par_empresa.SetMultipliedLeading(1);
            par_empresa.Add(txt_empresa);
            Cell celula_empresa = new Cell();
            celula_empresa.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            celula_empresa.Add(par_empresa);

            return celula_empresa;
        }
        private Cell SetCeldaAlign(string text,iText.Layout.Properties.TextAlignment alignment)
        {
            var txt_empresa = new Text(text);
            //txt_empresa.SetFontColor(iText.Kernel.Colors.ColorConstants.BLUE);
            //txt_empresa.SetFont(PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA));
            txt_empresa.SetFontSize(10);
            var par_empresa = new Paragraph();
            par_empresa.SetMultipliedLeading(1);
            par_empresa.Add(txt_empresa);
            Cell celula_empresa = new Cell();
            celula_empresa.SetTextAlignment(alignment);
            celula_empresa.Add(par_empresa);

            return celula_empresa;
        }
        private void CrearPDF()
        {
            var exportFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var exportFile = System.IO.Path.Combine(exportFolder, "Test.pdf");
            using (var writer = new PdfWriter(exportFile))
            {
                using (var pdf = new PdfDocument(writer))
                {

                    var doc = new Document(pdf);
                    //doc.Add(new Paragraph("Recibos Provisionales"));



                    
                    //INICIA ENCABEZADO
                    ReciboProvisionalToPDF_CR pdfI = ObtenerRecibo();
                    var txt_encabezado = new Text("RECIBOS PROVISIONALES");
                    txt_encabezado.SetFontColor(iText.Kernel.Colors.ColorConstants.BLUE);
                    txt_encabezado.SetFont(PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA));
                    txt_encabezado.SetFontSize(16);
                    var par_encabezado = new Paragraph();
                    par_encabezado.SetMultipliedLeading(4);//(0.05F);
                    par_encabezado.Add(txt_encabezado);
                    doc.Add(par_encabezado);
                    //TERMINA ENCABEZADO


                    iText.Kernel.Colors.DeviceCmyk magentaColor = new iText.Kernel.Colors.DeviceCmyk(100F, 10F, 30F, 0F);

                    var table_bold = new iText.Layout.Element.Table(1);
                    table_bold.SetWidth(525);
                    // Adding row 1 to the table
                    Cell c1 = new Cell();

                    // Adding the contents of the cell
                    c1.Add(new Paragraph("Name"));
                    // Setting the back ground color of the cell
                    c1.SetBorder(Border.NO_BORDER);
                    // Setting the border of the cell
                    c1.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.RED, 3));
                    // Setting the text alignment       
                    c1.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

                    table_bold.AddCell(c1);
                    doc.Add(table_bold);
                    
                    var table_folio = new iText.Layout.Element.Table(2);
                    table_folio.SetWidth(525);
                    

                    //var cell = new Cell();
                    //cell.Add(par_encabezado);
                    //cell.SetBorder(Border.NO_BORDER);
                    //cell.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.RED, 3));
                    //table_folio.AddCell(cell);

                    Cell celda = SetCelda("");
                    celda.SetBorder(Border.NO_BORDER);
                    table_folio.AddCell(celda);
                    Cell celda_folio = SetCeldaAlign("Folio: " + pdfI.folio, iText.Layout.Properties.TextAlignment.RIGHT);
                    celda_folio.SetBorder(Border.NO_BORDER);
                    //celda_folio.SetBorderTop()etBorderWidthTop(2f);
                    table_folio.AddCell(celda_folio);
                    doc.Add(table_folio);

                    doc.Add(new Paragraph("\n"));
                    //doc.Add(new Paragraph("\n"));

                    var table = new iText.Layout.Element.Table(2);
                    table.SetWidth(525);

                    
                    table.AddCell(SetCelda("Empresa: " + pdfI.Empresa));
                    
                    table.AddCell(SetCelda("Caja:" + pdfI.Punto_de_Venta));

                    table.AddCell(SetCelda("RFC: " + pdfI.RFC));

                    table.AddCell(SetCelda("Cajero:" + pdfI.Cajero));

                    table.AddCell(SetCelda("Cliente: " + pdfI.Cliente));

                    table.AddCell(SetCelda("Fecha de Expedición: " + pdfI.fechaExpedicion.Date.ToShortDateString()));

                    table.AddCell(SetCelda("Fraccionamiento: " + pdfI.Fraccionamiento));

                    table.AddCell(SetCelda("Fecha de Pago: " + pdfI.fechaPago.Date.ToShortDateString()));

                    table.AddCell(SetCelda("Manzana y Lote: " + pdfI.ManzanaYLote));

                    table.AddCell(SetCelda("Fecha de Vencimiento: " + pdfI.fechaVencimiento.Date.ToShortDateString()));

                    table.AddCell(SetCelda("Ciudad: " + pdfI.Municipio));

                    table.AddCell(SetCelda("Contrato: " + pdfI.identificador));
                    //table.AddCell("hi");

                    doc.Add(table);
                    //doc.Add();
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("Fraccionamiento: " + pdfI.Fraccionamiento));
                    doc.Add(new Paragraph("Empresa: " + pdfI.Empresa));
                    doc.Add(new Paragraph("Manzana y Lote: " + pdfI.ManzanaYLote));
                    doc.Add(new Paragraph("Ciudad: " + pdfI.Municipio));
                }
            }
        }
        private ReciboProvisionalToPDF_CR ObtenerRecibo()
        {
            var pdf = new ReciboProvisionalToPDF_CR();
            pdf.idRecibo = 154;
            pdf.Punto_de_Venta = "PLAZA RIO 1";
            pdf.Empresa = "ONIX LUXURY CONDOS SA DE CV";
            pdf.Producto = "CONDOMINIOS";
            pdf.Cliente = "ANA RAMIREZ RAMIREZ";
            pdf.Cajero = "MAC";

            //pdf.Divisa = Convert.ToString(reader["Divisa"]);
            pdf.folio = "10";
            //pdf.Tipo_de_Operacion = Convert.ToString(reader["Tipo_de_Operacion"]);
            //pdf.mensualidad = Convert.ToDouble(reader["mensualidad"]);
            //pdf.recargos = Convert.ToDouble(reader["recargos"]);
            ////pdf.abono_a_cuenta = Convert.ToDouble(reader["abono_a_cuenta"]);
            //pdf.otros = Convert.ToDouble(reader["otros"]);
            //pdf.Forma_de_Pago = Convert.ToString(reader["Forma_de_Pago"]);
            //pdf.total = Convert.ToDouble(reader["total"]);
            //pdf.estadoNombre = Convert.ToString(reader["estadoNombre"]);
            //pdf.creadoPor_Nombre = Convert.ToString(reader["creadoPor_Nombre"]);
            //pdf.modificadoPor_Nombre = Convert.ToString(reader["modificadoPor_Nombre"]);
            pdf.Fraccionamiento = "PUNTAZUL DIAMANTE";
            pdf.ManzanaYLote = "2 / 12";
            pdf.Municipio = "PLAYAS DE ROSARITO";
            pdf.identificador = "PD2016276";

            //pdf.tipoOperacionDesc = Convert.ToString(reader["tipoOperacionDesc"]);
            pdf.RFC = "DSFD543423FDS";
            //try
            //{
            pdf.fechaExpedicion = DateTime.Now;
           //pdf.fechaCreacion = DateTime.Parse(reader["fechaCreacion"].ToString());

            //    string fechaModificacion = reader["fechaModificacion"].ToString();
            //    if (fechaModificacion != "") pdf.fechaModificacion = DateTime.Parse(fechaModificacion);

            pdf.fechaPago = DateTime.Now;
            pdf.fechaVencimiento = DateTime.Now;

            //}
            //catch (Exception e)
            //{
            //    //Response.Write("<script>console.log('" + e.Message + "');</script>");
            //}
            return pdf;
        }
        public class ReciboProvisionalToPDF_CR
        {
            public long idRecibo { get; set; }
            public string Punto_de_Venta { get; set; }
            public string Empresa { get; set; }
            public string Producto { get; set; }
            public string Cliente { get; set; }
            public string Cajero { get; set; }
            public string Divisa { get; set; }
            public string folio { get; set; }
            public string Tipo_de_Operacion { get; set; }
            public double mensualidad { get; set; }
            public double recargos { get; set; }
            public double abono_a_cuenta { get; set; }
            public double otros { get; set; }
            public string Forma_de_Pago { get; set; }
            public double total { get; set; }
            public string estadoNombre { get; set; }
            public string creadoPor_Nombre { get; set; }
            public string modificadoPor_Nombre { get; set; }
            public DateTime fechaExpedicion { get; set; }
            public DateTime fechaCreacion { get; set; }
            public DateTime fechaModificacion { get; set; }
            public string PagoTotalInLetter { get; set; }
            public string ManzanaYLote { get; set; }
            public string Fraccionamiento { get; set; }
            public string Municipio { get; set; }

            public int idPV { get; set; }
            public string identificador { get; set; }
            public int idEmpresa { get; set; }
            public int idProducto { get; set; }
            public int idCliente { get; set; }
            public int idCajero { get; set; }
            public int idDivisa { get; set; }

            public string tipoOperacion { get; set; }
            public string tipoOperacionDesc { get; set; }

            public string formaPago { get; set; }
            public string formaPagoNombre { get; set; }
            public string status { get; set; }
            public DateTime fechaPago { get; set; }
            public DateTime fechaVencimiento { get; set; }
            public int creadoPor { get; set; }
            public int modificadoPor { get; set; }
            public string RFC { get; set; }

        }

        protected void pdf_Click(object sender, EventArgs e)
        {
            CrearPDF();
        }
    }
}