using System;
using System.Collections.Generic;
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
            string razonsocial = args[10];
            string ruc = args[10];
            string impresora = "IMPRESORA PV";

            /*string farmacia ="PRUEBA";
            string comprobante = "PRUEBA";
            string usuario = "PRUEBA";
            string cliente = "PRUEBA";
            string identificacion = "PRUEBA";
            string fpago = "PRUEBA";
            string pago = "PRUEBA";
            string cambio = "PRUEBA";
            string claveAcceso = "1703202101120549054100120010010000109121703912615G";
            string rutaGenerados = "C:\\Users\\Administrador.WIN-F9ITAC4UFGK\\Documents\\1703202101120549054100120010010000109121703912615G.xml";
            string razonsocial = "PRUEBA"; ;
            string ruc = "PRUEBA";
            string impresora = "Microsoft XPS Document Writer";*/

            
            string urlXMl = rutaGenerados;

            string producto = "", cantidad = "", precioU = "", precioTotalSinImpuesto="";
            string TotalSinImpuesto = "", totalDescuento = "", totaliva = "", valorIva = "",importeTotal="";
            decimal resultado = 0;
            decimal suma = 0;
            XmlDocument doc = new XmlDocument();
            doc.Load(urlXMl);
            XmlNodeList elemList = doc.GetElementsByTagName("detalles");

            XmlNodeList infoFactura = doc.GetElementsByTagName("infoFactura");
            foreach (XmlElement info in infoFactura)
            {
                // TotalSinImpuesto = info.SelectSingleNode("totalSinImpuestos").InnerText;
                totalDescuento = info.SelectSingleNode("totalDescuento").InnerText;
                TotalSinImpuesto = info.SelectSingleNode("totalSinImpuestos").InnerText;
                importeTotal = info.SelectSingleNode("importeTotal").InnerText;
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
               //De aqui en adelante pueden formar su ticket a su gusto... Les muestro un ejemplo

                //Datos de la cabecera del Ticket.
                ticket.textoCentro("--FARMACIAS TU AHORRO--");
                ticket.textoCentro(razonsocial);
                ticket.textoIzquierda("Obligado contabilidad: SI");
                ticket.textoIzquierda("RUC: "+ruc);
                ticket.textoIzquierda("");
                ticket.textoCentro("CA / NA");
                ticket.textoCentro(claveAcceso);
                ticket.textoIzquierda("");
                ticket.textoExtremos(farmacia, "F:" + comprobante);
                ticket.lineasGuion();

                //Sub cabecera.
                ticket.textoIzquierda("USUARIO: " + usuario);
                ticket.textoIzquierda("");
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
                    XmlNodeList elemento = det.GetElementsByTagName("impuestos");
                    
                    foreach (XmlElement impuesto in elemento)
                    {
                        foreach (XmlElement valor in impuesto.GetElementsByTagName("valor"))
                        {
                            valorIva = valor.InnerText.ToString();
                            // Console.WriteLine(precioU);

                        }

                    }

                    resultado = (decimal.Parse(precioU) * int.Parse(cantidad)) - decimal.Parse(valorIva);



                    ticket.AgregaArticulo(producto, int.Parse(cantidad), decimal.Parse(precioU), resultado);

                    }

                }

                ticket.lineasGuion();
             
                //Resumen de la venta. Sólo son ejemplos
                ticket.AgregarTotales("    SUBTOTAL......$", decimal.Parse(TotalSinImpuesto));
                ticket.AgregarTotales("    DESCUENTO.....$", decimal.Parse(totalDescuento));
                ticket.AgregarTotales("    IVA...........$", decimal.Parse(totaliva));//La M indica que es un decimal en C#
                ticket.AgregarTotales("    TOTAL.........$", decimal.Parse(importeTotal));
                ticket.textoIzquierda("");
                ticket.textoDerecha("Efectivo:$"+pago);
                ticket.textoDerecha("Cambio:$"+cambio);
                ticket.textoIzquierda("");
            ticket.textoCentro("F. TU AHORRO AGRADECE SU COMPRA!");
                
                ticket.CortaTicket();
                //ticket.ImprimirTicket("Microsoft XPS Document Writer");
                ticket.ImprimirTicket(impresora);//Nombre de la impresora ticketera

            
            
        }
    }
}
