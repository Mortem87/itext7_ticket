using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
//using iText..itextpdf.text.Chunk;
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
        private Cell SetCeldaColor(string head, string desc)
        {

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            
            var txt_head = new Text(head);
            txt_head.SetFont(bold);
            txt_head.SetFontSize(8);
            txt_head.SetFontColor(iText.Kernel.Colors.ColorConstants.BLUE);

            var txt_desc = new Text(desc);
            txt_desc.SetFontSize(8);
            txt_desc.SetFontColor(iText.Kernel.Colors.ColorConstants.BLACK);

            var parrafo = new Paragraph();
            parrafo.Add(txt_head);
            parrafo.Add(txt_desc);
            parrafo.SetMultipliedLeading(0.5F);

            var celula = new Cell();
            celula.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            celula.Add(parrafo);
            celula.SetBorder(Border.NO_BORDER);
            celula.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
            return celula;
        }
        private Cell SetCelda(string text)
        {
            var txt_empresa = new Text(text);
            txt_empresa.SetFontSize(8);
            var par_empresa = new Paragraph();
            par_empresa.SetMultipliedLeading(0.5F);
            par_empresa.Add(txt_empresa);
            Cell celula_empresa = new Cell();
            celula_empresa.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            celula_empresa.Add(par_empresa);
            celula_empresa.SetBorder(Border.NO_BORDER);
            return celula_empresa;
        }
        private Cell SetCeldaAlign(string head, string desc)
        {
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            var txt_head = new Text(head);
            txt_head.SetFont(bold);
            txt_head.SetFontSize(8);
            txt_head.SetFontColor(iText.Kernel.Colors.ColorConstants.BLUE);

            var txt_desc = new Text(desc);
            txt_desc.SetFont(bold);
            txt_desc.SetFontSize(8);
            txt_desc.SetFontColor(iText.Kernel.Colors.ColorConstants.RED);

            var parrafo = new Paragraph();
            parrafo.Add(txt_head);
            parrafo.Add(txt_desc);
            parrafo.SetMultipliedLeading(0.5F);

            Cell celula = new Cell();
            celula.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
            celula.Add(parrafo);
            celula.SetBorder(Border.NO_BORDER);
            return celula;
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


                    iText.Kernel.Colors.DeviceCmyk magentaColor = new iText.Kernel.Colors.DeviceCmyk(63F, 0F, 97F, 0F);
                    
                    var table_folio = new iText.Layout.Element.Table(2);
                    table_folio.SetWidth(525);
                   
                    table_folio.AddCell(SetCelda(""));
                    table_folio.AddCell(SetCeldaAlign("FOLIO: ", pdfI.folio));

                    doc.Add(table_folio);
                    doc.Add(new Paragraph("\n"));

                    var table = new iText.Layout.Element.Table(new float[] { 40, 120, 30 });

                    table.SetWidth(525);
                    
                    table.AddCell(SetCeldaColor("EMPRESA: ", pdfI.Empresa));
                    table.AddCell(SetCelda(" "));
                    table.AddCell(SetCeldaColor("CAJA: ", pdfI.Punto_de_Venta));
                    table.AddCell(SetCeldaColor("RFC: ", pdfI.RFC));
                    table.AddCell(SetCelda(" "));
                    table.AddCell(SetCeldaColor("CAJERO:", pdfI.Cajero));
                    table.AddCell(SetCeldaColor("CLIENTE: ", pdfI.Cliente));
                    table.AddCell(SetCelda(" "));
                    table.AddCell(SetCeldaColor("FECHA DE EXPEDICION: ", pdfI.fechaExpedicion.Date.ToShortDateString()));
                    table.AddCell(SetCeldaColor("FRACCIONAMIENTO: ", pdfI.Fraccionamiento));
                    table.AddCell(SetCelda(" "));
                    table.AddCell(SetCeldaColor("FECHA DE PAGO: ", pdfI.fechaPago.Date.ToShortDateString()));
                    table.AddCell(SetCeldaColor("MANZANA Y LOTE: ", pdfI.ManzanaYLote));
                    table.AddCell(SetCelda(" "));
                    table.AddCell(SetCeldaColor("FECHA DE VENCIMIENTO: ", pdfI.fechaVencimiento.Date.ToShortDateString()));
                    table.AddCell(SetCeldaColor("CIUDAD: ", pdfI.Municipio));
                    table.AddCell(SetCelda(" "));
                    table.AddCell(SetCeldaColor("CONTRATO: ", pdfI.identificador));

                    doc.Add(table);
                    doc.Add(new Paragraph("\n"));

                    //INICIO LINEA
                    var table_bold = new iText.Layout.Element.Table(3);
                    table_bold.SetWidth(525);

                    var c1 = new Cell();
                    c1.Add(new Paragraph("HOLA MUNDO"));
                    c1.SetBorder(Border.NO_BORDER);
                    c1.SetBorderTop(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));
                    c1.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    table_bold.AddCell(c1);

                    var c2 = new Cell();
                    c2.Add(new Paragraph("HOLA MUNDO"));
                    c2.SetBorder(Border.NO_BORDER);
                    c2.SetBorderTop (new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));
                    c2.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    table_bold.AddCell(c2);

                    var c3 = new Cell();
                    c3.Add(new Paragraph("HOLA MUNDO"));
                    c3.SetBorder(Border.NO_BORDER);
                    c3.SetBorderTop(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));
                    c3.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    table_bold.AddCell(c3);

                    var c4 = new Cell();
                    c4.Add(new Paragraph("HOLA MUNDO"));
                    c4.SetBorder(Border.NO_BORDER);
                    c4.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));
                    c4.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    table_bold.AddCell(c4);

                    var c5 = new Cell();
                    c5.Add(new Paragraph("HOLA MUNDO"));
                    c5.SetBorder(Border.NO_BORDER);
                    c5.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));
                    c5.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    table_bold.AddCell(c5);

                    var c6= new Cell();
                    c6.Add(new Paragraph("HOLA MUNDO"));
                    c6.SetBorder(Border.NO_BORDER);
                    c6.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));
                    c6.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    table_bold.AddCell(c6);

                    doc.Add(table_bold);
                    //FIN LINEA

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