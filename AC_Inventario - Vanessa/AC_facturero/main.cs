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

            //string comprobante = args[1];
            string rutaGenerados = args[0];
            string usuario = args[1];
            //string cliente = args[3];
           // string identificacion = args[4];
            //string fpago = args[5];
           // string pago = args[6];
           // string cambio = args[7];
            //string claveAcceso = args[8];
         

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
            //string impresora = "Microsoft XPS Document Writer";//ticket.ImprimirTicket("Microsoft XPS Document Writer");//Nombre de la impresora ticketera;

            string urlXMl = rutaGenerados;

            string producto = "", cantidad = "", fracciones = "";
          
            
    



            XmlDocument doc =new XmlDocument();
           
            using (FileStream fs = new FileStream(urlXMl, FileMode.Open, FileAccess.Read))
            {
                doc.Load(fs);
            }
            XmlNodeList elemList = doc.GetElementsByTagName("detalles");

            
         
                //Creamos una instancia d ela clase CrearTicket
                CrearTicket ticket = new CrearTicket();
                //Ya podemos usar todos sus metodos
                //ticket.AbreCajon();//Para abrir el cajon de dinero.

                //De aqui en adelante pueden formar su ticket a su gusto... Les muestro un ejemplo

                //Datos de la cabecera del Ticket.
                ticket.textoCentro("--FARMACIAS TU AHORRO--");
                ticket.textoCentro("ROMERO MONTALVAN ANTONIA VANESSA");
                
             

                //Sub cabecera.
                ticket.textoIzquierda("");
                ticket.textoIzquierda("USUARIO: " + usuario);
             
                ticket.textoExtremos("FECHA: " + DateTime.Now.ToShortDateString(), "HORA: " + DateTime.Now.ToShortTimeString());
                

                //Articulos a vender.
                ticket.EncabezadoArticulo();//NOMBRE DEL ARTICULO, CANT, PRECIO, IMPORTE
              
                foreach (XmlElement detalle in elemList)
                {
                    foreach (XmlElement det in detalle)
                    {


                        foreach (XmlElement descripcion in det.GetElementsByTagName("nombreComercial"))
                        {
                            producto = descripcion.InnerText.ToString();
                            //   Console.WriteLine(producto);
                        }
                        foreach (XmlElement cant in det.GetElementsByTagName("stock"))
                        {
                            cantidad = cant.InnerText.ToString();
                            //  Console.WriteLine(cantidad);

                        }
                        foreach (XmlElement fraccionesU in det.GetElementsByTagName("fraccionesTotales"))
                        {
                            fracciones = fraccionesU.InnerText.ToString();
                            // Console.WriteLine(precioU);

                        }

                      
                        ticket.AgregaArticulo(producto, int.Parse(cantidad), int.Parse(fracciones));

                    }

                }

              
               

                ticket.ImprimirTicket(impresora);//Nombre de la impresora ticketera
                
            
            
        }
    }
}
