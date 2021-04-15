using System;

namespace Firma_Autorizacion
{
    class Home
    {
        public static void Main(string[] args)
        {


             String rutaxml = args[0];
             String rutafirma = args[1];//"D:\\SRI\\Firma\\.p12";
             String rutadevu = args[2];//"D:\\SRI\\Devueltos\\";
             String rutaauto = args[3];//"D:\\SRI\\Autorizados\\";

           /* String rutaxml = "A:\\0603202101120549054100120010010000101660603166518.xml";
            String rutafirma = "Z:\\NodeJs\\Proyecto en produccion\\Firmas\\DiegoMorales\\diego_armando_morales_varas.p12";
            String rutadevu = "A:\\";
            String rutaauto = "A:\\";*/

            Principal principal = new Principal();
            String respuesta = principal.TestPrueba(rutafirma, rutaxml, rutadevu, rutaauto,null);
            if (respuesta == "OK")
            {
                Console.WriteLine(respuesta);  
            }
            else
            {
                Console.WriteLine("Ocurrió un error..");
            }
        }
    }
}
