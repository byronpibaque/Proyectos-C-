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

            string rutaXML = args[0]; 
            //string rutaXML = "A:\\0202104076056-EGr000000015.xml"; 

            string impresora = "IMPRESORA PV";//"Microsoft XPS Document Writer" Nombre de la impresora ticketera;

            string urlXMl = rutaXML;

            string producto = "", cantidad = "", codigoB = "",fracciones="";
            string farmaciaRecibe= "", farmaciaEmite = "", usuario = "", descripcion = "", numComprobante="";
            

            XmlDocument doc = new XmlDocument();
            doc.Load(urlXMl);

            XmlNodeList elemList = doc.GetElementsByTagName("detalles");

            XmlNodeList encabezado = doc.GetElementsByTagName("encabezado");
            
            foreach (XmlElement info in encabezado)
            {
               
                farmaciaEmite = info.SelectSingleNode("farmaciaEmite").InnerText;
                farmaciaRecibe = info.SelectSingleNode("farmaciaRecibe").InnerText;
                usuario = info.SelectSingleNode("usuario").InnerText;
                descripcion = info.SelectSingleNode("descripcion").InnerText;
                numComprobante = info.SelectSingleNode("numComprobante").InnerText;


            }
                //Creamos una instancia d ela clase CrearTicket
                CrearTicket ticket = new CrearTicket();
                //Ya podemos usar todos sus metodos
                //ticket.AbreCajon();//Para abrir el cajon de dinero.

                //De aqui en adelante pueden formar su ticket a su gusto... Les muestro un ejemplo

                //Datos de la cabecera del Ticket.
                ticket.textoCentro("--FARMACIAS TU AHORRO--");
                ticket.textoCentro("COMPROBANTE DE EGRESO");
                ticket.textoIzquierda("Farm. Em.: "+farmaciaEmite);
                ticket.textoIzquierda("Farm. Rec.: "+farmaciaRecibe);
                ticket.textoIzquierda("Usuario:"+usuario);
                ticket.textoIzquierda("Num. Comp:"+numComprobante);
                ticket.textoIzquierda("Descripcion:" + descripcion);
                ticket.textoExtremos("FECHA: " + DateTime.Now.ToShortDateString(), "HORA: " + DateTime.Now.ToShortTimeString());
                ticket.textoCentro("");

            ticket.lineasGuion();

               

                //Articulos a vender.
                ticket.EncabezadoArticulo();//NOMBRE DEL ARTICULO, CANT, PRECIO, IMPORTE
                ticket.lineasGuion();
                foreach (XmlElement detalle in elemList)
                {
                    foreach (XmlElement det in detalle)
                    {


                        foreach (XmlElement desc in det.GetElementsByTagName("producto"))
                        {
                            producto = desc.InnerText.ToString();
                            //   Console.WriteLine(producto);
                        }
                        foreach (XmlElement cant in det.GetElementsByTagName("cantidad"))
                        {
                            cantidad = cant.InnerText.ToString();
                            //  Console.WriteLine(cantidad);

                        }
                        foreach (XmlElement frac in det.GetElementsByTagName("fracciones"))
                        {
                            fracciones = frac.InnerText.ToString();
                            //  Console.WriteLine(cantidad);

                        }
                    foreach (XmlElement codigo in det.GetElementsByTagName("codigoBarras"))
                        {
                            codigoB = codigo.InnerText.ToString();
                            // Console.WriteLine(precioU);

                        }

                        
                    
                        ticket.AgregaArticulo(producto, int.Parse(cantidad),int.Parse(fracciones), codigoB);

                    }

                }

                ticket.lineasGuion();
                
                

                //Texto final del Ticket.
                
                ticket.textoIzquierda("");
                
                ticket.textoIzquierda("*Cada egreso debe ser autorizado y luego debe ser impreso.");
            ticket.textoIzquierda("");

            ticket.CortaTicket();
           // ticket.ImprimirTicket("Microsoft XPS Document Writer");
            ticket.ImprimirTicket(impresora);//Nombre de la impresora ticketera
               


        }
    }
}
