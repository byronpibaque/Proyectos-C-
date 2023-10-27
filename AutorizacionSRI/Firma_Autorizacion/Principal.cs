using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace Firma_Autorizacion
{
    public class Principal
    {
        Enviar enviar = new Enviar();
        Consultar cons = new Consultar();
        GenerarXML generar = new GenerarXML();

        public String TestPrueba(String rutafirma, String rutaxml, String rutaDevuelto, String rutaAutorizados, String rutaNoAutorizados, String cor, String clavefirma)
        {
            String retorno = null;

            if (GenerarXML.VerificarConexion())
            {
                Recepcion.respuestaSolicitud responseE = enviar.EnviarComprobante(rutafirma, rutaxml,clavefirma);
                if (responseE != null)
                {
                    String clave = Obtenerclaveacceso(rutaxml);
                    String estado = responseE.estado;

                    if (estado == "DEVUELTA")
                    {
                        String identificadorD = "";
                        String infomacionAdicionalD = "";
                        String tipoD = "";
                        String mensaje1D = "";
                        String estadD = responseE.estado;
                        var comprobantes = responseE.comprobantes.ToList();
                        foreach (Recepcion.comprobante compro in comprobantes)
                        {
                            var mesajes = compro.mensajes.ToList();
                            foreach (Recepcion.mensaje mesaj in mesajes)
                            {
                                identificadorD = mesaj.identificador;
                                infomacionAdicionalD = mesaj.informacionAdicional;
                                tipoD = mesaj.tipo;
                                mensaje1D = mesaj.mensaje1;
                            }
                        }
                        String rutaDevuel = rutaDevuelto + clave + ".xml";
                        Boolean generoXml = generar.getGenerarXmlDevuelto(rutaxml, estado, clave, identificadorD, mensaje1D, tipoD, rutaDevuel);
                        if (generoXml)
                        {
                            retorno = "El documento con la clave acceso " + clave + " fue DEVUELTO\nRevise la ruta de documentos devueltos";
                        }
                    }
                    else if (estado == "RECIBIDA")
                    {
                        String ambienteR = "";
                        String estadoR = "";
                        String fechaAutoR = "";
                        String numeroAutoR = "";
                        String comprobanteR = "";
                        String identificadorR = "";
                        String infomacionAdicionalR = "";
                        String tipoR = "";
                        String mensaje1R = "";
                        String rutaAutorizad = rutaAutorizados + clave + ".xml";
                        String rutaNoAutorizad = rutaNoAutorizados + clave + "NO_AUTORIZADO.xml";

                        Autorizacion.respuestaComprobante responseA = cons.ConsultarComprobante(clave);
                        var autorizaciones = responseA.autorizaciones.ToList();
                        foreach (Autorizacion.autorizacion autorizar in autorizaciones)
                        {
                            ambienteR = autorizar.ambiente;
                            estadoR = autorizar.estado;
                            fechaAutoR = autorizar.fechaAutorizacion.ToString();
                            numeroAutoR = autorizar.numeroAutorizacion;
                            comprobanteR = autorizar.comprobante;
                            var mesajesR = autorizar.mensajes.ToList();
                            foreach (Autorizacion.mensaje mesaj in mesajesR)
                            {
                                identificadorR = mesaj.identificador;
                                infomacionAdicionalR = mesaj.informacionAdicional;
                                tipoR = mesaj.tipo;
                                mensaje1R += mesaj.mensaje1;
                            }
                        }
                        if (estadoR == "AUTORIZADO")
                        {
                            Boolean generoxml = generar.getGenerarXmlAutorizado(estadoR, numeroAutoR, fechaAutoR, ambienteR, comprobanteR, mensaje1R, identificadorR, tipoR, infomacionAdicionalR, rutaAutorizad);
                            if (generoxml)
                            {
                                retorno = "OK";
                            }
                        }
                        else if (estadoR =="NO AUTORIZADO")
                        {
                            Boolean generoxml = generar.getGenerarXmlNoAutorizado(estadoR, numeroAutoR, fechaAutoR, ambienteR, comprobanteR, mensaje1R, identificadorR, tipoR, infomacionAdicionalR, rutaNoAutorizad);
                            if (generoxml)
                            {
                                retorno = "ERROR: COMPROBANTE NO AUTORIZADO";
                            }
                        }

                    }
                }
                else
                {
                    retorno = "Error: No se encontro respuesta desde el SRI.";
                }

            }
            else
            {
                retorno = "No tiene acceso al Internet";
            }



            return retorno;
        }

        private static String Obtenerclaveacceso(String xml)
        {
            String clave = null;
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xml);
            XElement doc = XElement.Parse(xmlDoc.InnerXml);
            var result = doc.Elements("infoTributaria").Elements("claveAcceso").ToArray();
            clave = result[0].Value;
            return clave;
        }
    }
}
