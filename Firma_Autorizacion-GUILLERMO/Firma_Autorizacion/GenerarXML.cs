using System;
using System.Net;
using System.Xml;

namespace Firma_Autorizacion
{
   public class GenerarXML
    {

        public static bool VerificarConexion()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public Boolean getGenerarXmlAutorizado(String estad, String numerAutorizacio,
            String fechAutorizacio, String ambient, String comprobant, String ruta)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.PreserveWhitespace = false;
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = doc.DocumentElement;

                doc.InsertBefore(xmlDeclaration, root);
                XmlElement autorizacion = doc.CreateElement(string.Empty, "autorizacion", string.Empty);
                doc.AppendChild(autorizacion);

                XmlElement estado = doc.CreateElement(string.Empty, "estado", string.Empty);
                estado.AppendChild(doc.CreateTextNode(estad));
                autorizacion.AppendChild(estado);

                XmlElement numeroAutorizacion = doc.CreateElement(string.Empty, "numeroAutorizacion", string.Empty);
                numeroAutorizacion.AppendChild(doc.CreateTextNode(numerAutorizacio));
                autorizacion.AppendChild(numeroAutorizacion);

                XmlElement fechaAutorizacion = doc.CreateElement(string.Empty, "fechaAutorizacion", string.Empty);
                fechaAutorizacion.AppendChild(doc.CreateTextNode(fechAutorizacio));
                autorizacion.AppendChild(fechaAutorizacion);

                XmlElement ambiente = doc.CreateElement(string.Empty, "ambiente", string.Empty);
                ambiente.AppendChild(doc.CreateTextNode(ambient));
                autorizacion.AppendChild(ambiente);

                XmlElement comprobante = doc.CreateElement(string.Empty, "comprobante", string.Empty);
                comprobante.AppendChild(doc.CreateCDataSection(comprobant));
                autorizacion.AppendChild(comprobante);

                doc.Save(ruta);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean getGenerarXmlDevuelto(String xml, String estad, String claveAcces, String identificado, String mensaje_1, String tip, String ruta)

        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.PreserveWhitespace = false;
                doc.Load(xml);
                XmlElement root = doc.DocumentElement;

                XmlElement autorizacion = doc.CreateElement("ns2", "respuestaSolicitud", "http://ec.gob.sri.ws.recepcion");
                root.InsertAfter(autorizacion, root.LastChild);

                XmlElement estado = doc.CreateElement(string.Empty, "estado", string.Empty);
                estado.AppendChild(doc.CreateTextNode(estad));
                autorizacion.AppendChild(estado);

                XmlElement comprobantes = doc.CreateElement(string.Empty, "comprobantes", string.Empty);
                autorizacion.AppendChild(comprobantes);

                XmlElement comprobante = doc.CreateElement(string.Empty, "comprobante", string.Empty);
                comprobantes.AppendChild(comprobante);

                XmlElement claveAcceso = doc.CreateElement(string.Empty, "claveAcceso", string.Empty);
                claveAcceso.AppendChild(doc.CreateTextNode(claveAcces));
                comprobante.AppendChild(claveAcceso);

                XmlElement mensajes = doc.CreateElement(string.Empty, "mensajes", string.Empty);
                comprobante.AppendChild(mensajes);

                XmlElement mensaje = doc.CreateElement(string.Empty, "mensaje", string.Empty);
                mensajes.AppendChild(mensaje);

                XmlElement identificador = doc.CreateElement(string.Empty, "identificador", string.Empty);
                identificador.AppendChild(doc.CreateTextNode(identificado));
                mensaje.AppendChild(identificador);

                XmlElement mensaje1 = doc.CreateElement(string.Empty, "mensaje", string.Empty);
                mensaje1.AppendChild(doc.CreateTextNode(mensaje_1));
                mensaje.AppendChild(mensaje1);

                XmlElement tipo = doc.CreateElement(string.Empty, "tipo", string.Empty);
                tipo.AppendChild(doc.CreateTextNode(tip));
                mensaje.AppendChild(tipo);

                doc.Save(ruta);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public Boolean getGenerarXmlTemporal(String xml, String fecha, String ruta)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.PreserveWhitespace = false;
                doc.Load(xml);
                XmlElement root = doc.DocumentElement;
                XmlElement estado = doc.CreateElement(string.Empty, "fechaAutorizacion", string.Empty);
                estado.AppendChild(doc.CreateTextNode(fecha));
                root.InsertAfter(estado, root.FirstChild);
                doc.Save(ruta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
