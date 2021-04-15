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

            string rutaGenerados = args[0];
           string usuario = args[1];



            
            //string usuario = "asfs";
           
            //string rutaGenerados = "Z:\\C#\\c#\\AC_Inventario - GUILLERMO\\0202103299935.xml";
            
            string impresora = "IMPRESORA PV";
            //string impresora = "Microsoft XPS Document Writer";//ticket.ImprimirTicket("Microsoft XPS Document Writer");//Nombre de la impresora ticketera;

            string urlXMl = rutaGenerados;

            string producto = "", cantidad = "", fracciones = "", fraccionCaja="",lab="";
          
            
    



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
                        foreach (XmlElement fraccionesC in det.GetElementsByTagName("fraccionCaja"))
                        {
                            fraccionCaja = fraccionesC.InnerText.ToString();
                            // Console.WriteLine(precioU);

                        }
                        foreach (XmlElement codigoLaboratorio in det.GetElementsByTagName("codigoLaboratorio"))
                        {
                            foreach (XmlElement abreviatura in codigoLaboratorio.GetElementsByTagName("abreviatura"))
                            {
                            lab = abreviatura.InnerText.ToString();

                            }
                        }

                    decimal resultado = decimal.Parse(fracciones)/decimal.Parse(fraccionCaja);
                          decimal cajas = Math.Truncate(resultado);
                          decimal fraccion = Math.Round((resultado - cajas)*int.Parse(fraccionCaja));
                   // Console.WriteLine(resultado);
                   // Console.WriteLine(cajas);
                   ////Console.WriteLine(fraccion);
                    ticket.AgregaArticulo(producto, cajas, fraccion,lab);

                    }

                }



            //string impresora = "Microsoft XPS Document Writer";//ticket.ImprimirTicket("Microsoft XPS Document Writer")
            ticket.ImprimirTicket(impresora);//Nombre de la impresora ticketera
            //Console.ReadKey();


        }
    }
}
