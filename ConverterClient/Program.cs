using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterClient
{
    class Program
    {
        static void Main(string[] args)
        {
            String dato = "";
            while(true) 
            {
                dato = CurrencyClient.ConvertXmlDataFromConsoleLine();
                CurrencyClient.Connect("127.0.0.1", dato);
                dato = "";
            }
        }
    }
}
