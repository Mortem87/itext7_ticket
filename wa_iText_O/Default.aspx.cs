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
        private Cell SetCeldaAlign(string text,iText.Layout.Properties.TextAlignment alignment)
        {
            var txt_empresa = new Text(text);
            //txt_empresa.SetFontColor(iText.Kernel.Colors.ColorConstants.BLUE);
            //txt_empresa.SetFont(PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA));
            txt_empresa.SetFontSize(8);
            var par_empresa = new Paragraph();
            par_empresa.SetMultipliedLeading(1);
            par_empresa.Add(txt_empresa);
            Cell celula_empresa = new Cell();
            celula_empresa.SetTextAlignment(alignment);
            //celula_empresa.SetHeight(36f);
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


                    iText.Kernel.Colors.DeviceCmyk magentaColor = new iText.Kernel.Colors.DeviceCmyk(63F, 0F, 97F, 0F);

                    


                    var table_folio = new iText.Layout.Element.Table(2);
                    table_folio.SetWidth(525);
                   
                    Cell celda = SetCelda("");
                    
                    table_folio.AddCell(celda);
                    Cell celda_folio = SetCeldaAlign("FOLIO: " + pdfI.folio, iText.Layout.Properties.TextAlignment.RIGHT);
                    celda_folio.SetBorder(Border.NO_BORDER);
                    table_folio.AddCell(celda_folio);
                    doc.Add(table_folio);

                    doc.Add(new Paragraph("\n"));

                    var table = new iText.Layout.Element.Table(new float[] { 40, 120, 30 });

                    table.SetWidth(525);

                    Cell celula_empresa = SetCelda("EMPRESA: " + pdfI.Empresa);
                    celula_empresa.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    table.AddCell(celula_empresa);

                    table.AddCell(SetCelda(" "));

                    Cell celula_caja = SetCelda("CAJA:" + pdfI.Punto_de_Venta);
                    celula_caja.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    table.AddCell(celula_caja);

                    Cell celula_rfc = SetCelda("RFC: " + pdfI.RFC);
                    celula_rfc.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    table.AddCell(celula_rfc);

                    table.AddCell(SetCelda(" "));
                    
                    Cell celula_cajero = SetCelda("CAJERO:" + pdfI.Cajero);
                    celula_cajero.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    table.AddCell(celula_cajero);

                    Cell celula_cliente = SetCelda("CLIENTE: " + pdfI.Cliente);
                    celula_cliente.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    table.AddCell(celula_cliente);

                    table.AddCell(SetCelda(" "));
                    
                    Cell celula_expedicion = SetCelda("FECHA DE EXPEDICION: " + pdfI.fechaExpedicion.Date.ToShortDateString());
                    celula_expedicion.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    table.AddCell(celula_expedicion);

                    Cell celula_fracc = SetCelda("FRACCIONAMIENTO: " + pdfI.Fraccionamiento);
                    celula_fracc.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    table.AddCell(celula_fracc);

                    table.AddCell(SetCelda(" "));

                    Cell celula_pago = SetCelda("FECHA DE PAGO: " + pdfI.fechaPago.Date.ToShortDateString());
                    celula_pago.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    table.AddCell(celula_pago);

                    Cell celula_apple = SetCelda("MANZANA Y LOTE: " + pdfI.ManzanaYLote);
                    celula_apple.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    table.AddCell(celula_apple);

                    table.AddCell(SetCelda(" "));

                    Cell celula_vence = SetCelda("FECHA DE VENCIMIENTO: " + pdfI.fechaVencimiento.Date.ToShortDateString());
                    celula_vence.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    table.AddCell(celula_vence);

                    Cell celula_ciudad = SetCelda("CIUDAD: " + pdfI.Municipio);
                    celula_ciudad.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    table.AddCell(celula_ciudad);

                    table.AddCell(SetCelda(" "));

                    Cell celula_contrato = SetCelda("CONTRATO: " + pdfI.identificador);
                    celula_contrato.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLACK, 1));
                    table.AddCell(celula_contrato);

                    doc.Add(table);

                    doc.Add(new Paragraph("\n"));

                    //INICIO LINEA
                    var table_bold = new iText.Layout.Element.Table(1);
                    table_bold.SetWidth(525);
                    var c1 = new Cell();
                    c1.Add(new Paragraph(" "));
                    c1.SetBorder(Border.NO_BORDER);
                    c1.SetBorderBottom(new SolidBorder(iText.Kernel.Colors.ColorConstants.BLUE, 4));//iText.Kernel.Colors.ColorConstants.RED, 3));
                    c1.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    table_bold.AddCell(c1);
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