using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AC_facturero
{
    class main
    {
        public static void Main(string[] args)
        {
            GC.Collect();
            string farmacia = args[0];
            string comprobante = args[1];
            string usuario = args[2];
            string cliente = args[3];
            string identificacion = args[4];
            string fpago = args[5];
            string pago = args[6];
            string cambio = args[7];
            string claveAcceso = args[8];
            string rutaGenerados = args[9];

            /*string farmacia ="Tu ahorro";
            string comprobante = "0001";
            string usuario = "asfs";
            string cliente = "fas";
            string identificacion = "1023465";
            string fpago = "efe";
            string pago ="20";
            string cambio = "5";
            string claveAcceso = "456879879874965465465465";
            string rutaGenerados = "D:\\Desarrollo\\Nodejs\\Proyectos - NodeJs\\Proyecto en produccion\\BackEnd - F SRI\\archivos\\sri\\Generados\\0611202001120549054100120010010000002960611296119.xml";
            */
            string impresora = "IMPRESORA PV";
           // string impresora = "Microsoft XPS Document Writer";//ticket.ImprimirTicket("Microsoft XPS Document Writer");//Nombre de la impresora ticketera;

            string urlXMl = rutaGenerados;

            string producto = "", cantidad = "", precioU = "";
            string TotalSinImpuesto = "", totalDescuento = "", totaliva = "", total = "";
            decimal resultado = 0;
            decimal suma = 0;



            XmlDocument doc =new XmlDocument();
           
            using (FileStream fs = new FileStream(urlXMl, FileMode.Open, FileAccess.Read))
            {
                doc.Load(fs);
            }
            XmlNodeList elemList = doc.GetElementsByTagName("detalles");

            XmlNodeList infoFactura = doc.GetElementsByTagName("infoFactura");
            foreach (XmlElement info in infoFactura)
            {
                // TotalSinImpuesto = info.SelectSingleNode("totalSinImpuestos").InnerText;
                totalDescuento = info.SelectSingleNode("totalDescuento").InnerText;
                foreach (XmlElement tconimp in info.GetElementsByTagName("totalConImpuestos"))
                {
                    foreach (XmlElement totalImpuesto in tconimp)
                    {
                        totaliva = totalImpuesto.SelectSingleNode("valor").InnerText;

                    }


                }

            }
                //Creamos una instancia d ela clase CrearTicket
                CrearTicket ticket = new CrearTicket();
                //Ya podemos usar todos sus metodos
                //ticket.AbreCajon();//Para abrir el cajon de dinero.

                //De aqui en adelante pueden formar su ticket a su gusto... Les muestro un ejemplo

                //Datos de la cabecera del Ticket.
                ticket.textoCentro("--FARMACIAS TU AHORRO--");
                ticket.textoCentro("Morales Varas Diego Armando");
                ticket.textoIzquierda("Obligado a llevar contabilidad: NO");
                ticket.textoIzquierda("RUC: 1205490541001");
               
                ticket.textoIzquierda("EMAIL: tuahorrogi@gmail.com");
               
                ticket.textoCentro("CA / NA");
                ticket.textoCentro(claveAcceso);
                
                ticket.textoExtremos(farmacia, "F:" + comprobante);
             

                //Sub cabecera.
                ticket.textoIzquierda("");
                ticket.textoIzquierda("USUARIO: " + usuario);
                
                ticket.textoIzquierda("CLIENTE: " + cliente);
                ticket.textoIzquierda("RUC/CI: " + identificacion);
                ticket.textoIzquierda("F PAGO: " + fpago);
                ticket.textoExtremos("FECHA: " + DateTime.Now.ToShortDateString(), "HORA: " + DateTime.Now.ToShortTimeString());
                

                //Articulos a vender.
                ticket.EncabezadoArticulo();//NOMBRE DEL ARTICULO, CANT, PRECIO, IMPORTE
              
                foreach (XmlElement detalle in elemList)
                {
                    foreach (XmlElement det in detalle)
                    {


                        foreach (XmlElement descripcion in det.GetElementsByTagName("descripcion"))
                        {
                            producto = descripcion.InnerText.ToString();
                            //   Console.WriteLine(producto);
                        }
                        foreach (XmlElement cant in det.GetElementsByTagName("cantidad"))
                        {
                            cantidad = cant.InnerText.ToString();
                            //  Console.WriteLine(cantidad);

                        }
                        foreach (XmlElement precio in det.GetElementsByTagName("precioUnitario"))
                        {
                            precioU = precio.InnerText.ToString();
                            // Console.WriteLine(precioU);

                        }

                        resultado += int.Parse(cantidad) * decimal.Parse(precioU);
                        TotalSinImpuesto = Convert.ToString(resultado);
                        ticket.AgregaArticulo(producto, int.Parse(cantidad), decimal.Parse(precioU), int.Parse(cantidad) * decimal.Parse(precioU));

                    }

                }

              
                suma += (decimal.Parse(TotalSinImpuesto) - decimal.Parse(totalDescuento)) + decimal.Parse(totaliva);
                //Resumen de la venta. Sólo son ejemplos
                ticket.AgregarTotales("         SUBTOTAL......$", decimal.Parse(TotalSinImpuesto));
                ticket.AgregarTotales("         DESCUENTO......$", decimal.Parse(totalDescuento));
                ticket.AgregarTotales("         IVA...........$", decimal.Parse(totaliva));//La M indica que es un decimal en C#
                ticket.AgregarTotales("         TOTAL.........$", suma);
                ticket.textoIzquierda("");
                ticket.AgregarTotales("         PAGO.........$", decimal.Parse(pago));
                ticket.AgregarTotales("         CAMBIO.........$", decimal.Parse(cambio));

            //Texto final del Ticket.

                ticket.textoCentro("FARMACIAS TU AHORRO AGRADECE SU COMPRA!");
                ticket.textoIzquierda("");
             
                ticket.textoIzquierda("*No se acepta devolucion de medicina luego del 3er dia de la presente fecha.");
                ticket.textoIzquierda("*Para devolucion es indispensable este documento.");
                ticket.AbreCajon();

                ticket.ImprimirTicket(impresora);//Nombre de la impresora ticketera
                
            
            
        }
    }
}
