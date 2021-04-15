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

            string impresora = "IMPRESORA PV";//Microsoft XPS Document Writer

            string urlXMl = rutaXML;//"Z:\\NodeJs\\Proyecto en produccion\\BackEnd - F SRI\\archivos\\arqueos\\0202103223653-ARQUEOS.xml";

          
            string farmaciaEmite = "", usuario = "",totalCaja="",totalSistema="",positivos="",negativos="",tc="", td = "", ef = "",cr = "";
            

            XmlDocument doc = new XmlDocument();
            doc.Load(urlXMl);

            XmlNodeList elemList = doc.GetElementsByTagName("desgloce");

            XmlNodeList encabezado = doc.GetElementsByTagName("encabezado");
            
            foreach (XmlElement info in encabezado)
            {
               
                farmaciaEmite = info.SelectSingleNode("farmaciaEmite").InnerText;
                usuario = info.SelectSingleNode("usuario").InnerText;
                totalCaja = info.SelectSingleNode("totalCaja").InnerText;
                totalSistema = info.SelectSingleNode("totalSistema").InnerText;
                positivos = info.SelectSingleNode("positivos").InnerText;
                negativos = info.SelectSingleNode("negativos").InnerText;
}
            foreach (XmlElement detalle in elemList)
            {
                foreach (XmlElement det in detalle)
                {


                    foreach (XmlElement dtc in det.GetElementsByTagName("tc"))
                    {
                        tc = dtc.InnerText.ToString();
                        //   Console.WriteLine(producto);
                    }
                    foreach (XmlElement dtd in det.GetElementsByTagName("td"))
                    {
                        td = dtd.InnerText.ToString();
                        //  Console.WriteLine(cantidad);

                    }
                    foreach (XmlElement def in det.GetElementsByTagName("ef"))
                    {
                        ef = def.InnerText.ToString();
                        // Console.WriteLine(precioU);

                    }
                    foreach (XmlElement dcr in det.GetElementsByTagName("cr"))
                    {
                        cr = dcr.InnerText.ToString();
                        // Console.WriteLine(precioU);

                    }




                }
             

            }
            //Creamos una instancia d ela clase CrearTicket
            CrearTicket ticket = new CrearTicket();
               
                    ticket.textoCentro("--FARMACIAS TU AHORRO--");
                    ticket.textoCentro("BOLETA DE ARQUEOS");
                    ticket.textoIzquierda("Farmacia.: "+farmaciaEmite);
                    ticket.textoIzquierda("Usuario:"+usuario);
                    ticket.textoExtremos("FECHA: " + DateTime.Now.ToShortDateString(), "HORA: " + DateTime.Now.ToShortTimeString());
                    ticket.lineasGuion();
                    ticket.textoIzquierda("TOTAL CAJA:" + totalCaja);
                    ticket.textoIzquierda("TOTAL SISTEMA:" + totalSistema);
                    ticket.lineasGuion();
                    ticket.textoCentro("DESGLOCE");
                    ticket.textoIzquierda("EFECTIVO:" + ef);
                    ticket.textoIzquierda("CREDITO:" + cr);
                    ticket.textoIzquierda("TC:" + tc);
                    ticket.textoIzquierda("TD:" + td);
                    ticket.lineasGuion();
                    ticket.textoIzquierda("POSITIVOS:" + positivos);
                    ticket.textoIzquierda("NEGATIVOS:" + negativos);
                    ticket.lineasGuion();


            ticket.textoIzquierda("");
                
                ticket.textoIzquierda("*Este comprobante debe ir con los valores recaudados.");
            ticket.textoIzquierda("");

            ticket.CortaTicket();
           // ticket.ImprimirTicket("Microsoft XPS Document Writer");
            ticket.ImprimirTicket(impresora);//Nombre de la impresora ticketera
               


        }
    }
}
