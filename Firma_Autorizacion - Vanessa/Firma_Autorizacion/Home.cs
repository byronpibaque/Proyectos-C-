using System;

namespace Firma_Autorizacion
{
    class Home
    {
        public static void Main(string[] args)
        {


            String rutaxml = args[0];
            String rutafirma = args[1];//"D:\\SRI\\Firma\\nelson_guillermo_morales_varas.p12";
            String rutadevu = args[2];//"D:\\SRI\\Devueltos\\";
            String rutaauto = args[3];//"D:\\SRI\\Autorizados\\";

            Principal principal = new Principal();
            String respuesta = principal.TestPrueba(rutafirma, rutaxml, rutadevu, rutaauto,null);
            if (respuesta == "OK")
            {
                Console.WriteLine(respuesta);  
            }
            else
            {
                Console.WriteLine("Hubo algun problema");
            }
        }
    }
}
