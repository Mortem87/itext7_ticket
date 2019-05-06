using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
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
            txt_head.SetFontSize(10);
            txt_head.SetFontColor(iText.Kernel.Colors.ColorConstants.BLUE);

            var txt_desc = new Text(desc);
            txt_desc.SetFontSize(10);
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
        private Cell SetCeldaColorBlack(string head, string desc)
        {

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            var txt_head = new Text(head);
            txt_head.SetFont(bold);
            txt_head.SetFontSize(10);
            txt_head.SetFontColor(iText.Kernel.Colors.ColorConstants.BLACK);

            var txt_desc = new Text(desc);
            txt_desc.SetFontSize(10);
            txt_desc.SetFontColor(iText.Kernel.Colors.ColorConstants.BLACK);

            var parrafo = new Paragraph();
            parrafo.Add(txt_head);
            parrafo.Add(txt_desc);
            parrafo.SetMultipliedLeading(0.7F);

            var celula = new Cell();
            celula.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            celula.Add(parrafo);
            celula.SetBorder(Border.NO_BORDER);
            //celula.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
            return celula;
        }
        private Cell SetCelda(string text)
        {
            var txt_empresa = new Text(text);
            txt_empresa.SetFontSize(10);
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
            txt_head.SetFontSize(10);
            txt_head.SetFontColor(iText.Kernel.Colors.ColorConstants.BLUE);

            var txt_desc = new Text(desc);
            txt_desc.SetFont(bold);
            txt_desc.SetFontSize(10);
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
            string exportDir = Server.MapPath("~/tmp/" );
            string exportFile = exportDir + "Test.pdf";
            
            if (!(Directory.Exists(exportDir)))
            {
                Directory.CreateDirectory(exportDir);
            }

            
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

                    //var c1 = new Cell();

                    //c1.Add(new Paragraph("HOLA MUNDO 01"));
                    //c1.SetBorder(Border.NO_BORDER);
                    //c1.SetBorderTop(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));

                    //c1.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    Cell c1 = SetCeldaColorBlack("TIPO DE OPERACION: ", pdfI.Tipo_de_Operacion);
                    c1.SetBorderTop(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));
                    table_bold.AddCell(c1);


                    var c2 = new Cell();
                    var txt2 = new Text("Mensualidad: ");
                    txt2.SetFontSize(10);
                    Paragraph parrafo2 = new Paragraph(txt2);
                    parrafo2.SetMultipliedLeading(0.7F);
                    c2.Add(parrafo2);
                    c2.SetBorder(Border.NO_BORDER);
                    c2.SetBorderTop (new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));
                    c2.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
                    table_bold.AddCell(c2);

                    var c3 = new Cell();
                    var txt3 = new Text(pdfI.mensualidad.ToString("C2"));
                    txt3.SetFontSize(10);
                    Paragraph parrafo3 = new Paragraph(txt3);
                    parrafo3.SetMultipliedLeading(0.7F);
                    c3.Add(parrafo3);
                    c3.SetBorder(Border.NO_BORDER);
                    c3.SetBorderLeft(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    c3.SetBorderTop(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));
                    c3.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
                    table_bold.AddCell(c3);

                    var c4 = new Cell(3, 1);
                    Paragraph parrafo4 = new Paragraph(pdfI.tipoOperacionDesc);
                    parrafo4.SetMultipliedLeading(0.7F);
                    c4.Add(parrafo4);
                    c4.SetBorder(Border.NO_BORDER);
                    c4.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
                    table_bold.AddCell(c4);

                    var c5 = new Cell();
                    var txt5 = new Text("Recargos: ");
                    txt5.SetFontSize(10);
                    Paragraph parrafo5 = new Paragraph(txt5);
                    parrafo5.SetMultipliedLeading(0.7F);
                    c5.Add(parrafo5);
                    c5.SetBorder(Border.NO_BORDER);
                    c5.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
                    table_bold.AddCell(c5);

                    var c6= new Cell();
                    var txt6 = new Text(pdfI.recargos.ToString("C2"));
                    txt6.SetFontSize(10);
                    Paragraph parrafo6 = new Paragraph(txt6);
                    parrafo6.SetMultipliedLeading(0.7F);
                    c6.Add(parrafo6);
                    c6.SetBorder(Border.NO_BORDER);
                    c6.SetBorderLeft(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    c6.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
                    table_bold.AddCell(c6);

                    var c8 = new Cell();
                    var txt8 = new Text("Otros: ");
                    txt8.SetFontSize(10);
                    Paragraph parrafo8 = new Paragraph(txt8);
                    parrafo8.SetMultipliedLeading(0.7F);
                    c8.Add(parrafo8);
                    c8.SetBorder(Border.NO_BORDER);
                    c8.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
                    table_bold.AddCell(c8);

                    var c9 = new Cell();
                    var txt9 = new Text(pdfI.otros.ToString("C2"));
                    txt9.SetFontSize(10);
                    Paragraph parrafo9 = new Paragraph(txt9);
                    parrafo9.SetMultipliedLeading(0.7F);
                    c9.Add(parrafo9);
                    c9.SetBorder(Border.NO_BORDER);
                    c9.SetBorderLeft(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    c9.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
                    table_bold.AddCell(c9);

                    var c11 = new Cell();
                    c11.Add(new Paragraph("\n"));
                    c11.SetBorder(Border.NO_BORDER);
                    c11.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    table_bold.AddCell(c11);

                    var c12 = new Cell();
                    c12.Add(new Paragraph("\n"));
                    c12.SetBorder(Border.NO_BORDER);
                    c12.SetBorderLeft(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    c12.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    table_bold.AddCell(c12);

                    var c13 = SetCeldaColorBlack("FORMA DE PAGO: ", pdfI.Forma_de_Pago);
                    c13.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));
                    table_bold.AddCell(c13);

                    var c14 = new Cell();
                    
                    PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                    var txt14 = new Text("Total: ");
                    txt14.SetFont(bold);
                    txt14.SetFontSize(10);
                    Paragraph parrafo14 = new Paragraph(txt14);
                    parrafo14.SetMultipliedLeading(0.7F);
                    c14.Add(parrafo14);
                    c14.SetBorder(Border.NO_BORDER);
                    c14.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));
                    c14.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
                    table_bold.AddCell(c14);

                    var c15 = new Cell();
                    var txt15 = new Text(pdfI.total.ToString("C2"));
                    txt15.SetFontSize(10);
                    Paragraph parrafo15 = new Paragraph(txt15);
                    parrafo15.SetMultipliedLeading(0.7F);
                    c15.Add(parrafo15);
                    c15.SetBorder(Border.NO_BORDER);
                    c15.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));
                    c15.SetBorderLeft(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    c15.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
                    table_bold.AddCell(c15);


                    var c16 = new Cell();
                    //PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                    var txt16 = new Text("Total: ");
                    //txt14.SetFont(bold);
                    txt16.SetFontSize(10);
                    Paragraph parrafo16 = new Paragraph(txt16);
                    parrafo16.SetMultipliedLeading(0.7F);
                    c16.Add(parrafo16);
                    c16.SetBorder(Border.NO_BORDER);
                    c16.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));
                    c16.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
                    table_bold.AddCell(c16);

                    var c17 = new Cell();
                    //PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                    var txt17 = new Text("Total: ");
                    //txt14.SetFont(bold);
                    txt17.SetFontSize(10);
                    Paragraph parrafo17 = new Paragraph(txt17);
                    parrafo17.SetMultipliedLeading(0.7F);
                    c17.Add(parrafo17);
                    c17.SetBorder(Border.NO_BORDER);
                    c17.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));
                    c17.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
                    table_bold.AddCell(c17);

                    var c18 = new Cell();
                    //PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                    var txt18 = new Text("Total: ");
                    //txt14.SetFont(bold);
                    txt18.SetFontSize(10);
                    Paragraph parrafo18 = new Paragraph(txt18);
                    parrafo18.SetMultipliedLeading(0.7F);
                    c18.Add(parrafo18);
                    c18.SetBorder(Border.NO_BORDER);
                    c18.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));
                    c18.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
                    table_bold.AddCell(c18);


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
            pdf.Tipo_de_Operacion = "APARTADO";
            pdf.mensualidad = 30.50d;
            //pdf.recargos = Convert.ToDouble(reader["recargos"]);
            //pdf.otros = Convert.ToDouble(reader["otros"]);
            pdf.Forma_de_Pago = "EFECTIVO";
            //pdf.total = Convert.ToDouble(reader["total"]);
            //pdf.estadoNombre = Convert.ToString(reader["estadoNombre"]);
            //pdf.creadoPor_Nombre = Convert.ToString(reader["creadoPor_Nombre"]);
            //pdf.modificadoPor_Nombre = Convert.ToString(reader["modificadoPor_Nombre"]);
            pdf.Fraccionamiento = "PUNTAZUL DIAMANTE";
            pdf.ManzanaYLote = "2 / 12";
            pdf.Municipio = "PLAYAS DE ROSARITO";
            pdf.identificador = "PD2016276";

            pdf.tipoOperacionDesc = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin vulputate tincidunt bibendum. Donec accumsan nibh sed nisi aliquet, euismod blandit nibh molestie. Morbi pretium egestas elit in pulvinar.";
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