using System;

namespace Smartd.Bibliotecas
{
    public class Configuracao
    {
        public static string NomeDatabase => "DB_SmartD.db3";
        public static string UrlApi => "https://nefrodee-client-api.herokuapp.com";
        public static TimeSpan TimeoutApi => 60d.ToTime();
        public static string Versao => "1.00";
    }
}
