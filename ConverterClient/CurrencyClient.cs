using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConverterClient
{
    public static class CurrencyClient
    {
        public static void Connect(String server, String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 13000;
                TcpClient client = new TcpClient(server, port);
                // Translate the passed message into ASCII and store it as a Byte array
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                // Get a client stream for reading and writing
                NetworkStream stream = client.GetStream();
                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", message);
                // Receive the TcpServer.response.
                // Buffer to store the response bytes.
                data = new Byte[256];
                // String to store the response ASCII representation.
                String responseData = String.Empty;
                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);
                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }

        public static String ConvertXmlDataFromConsoleLine()
        {
            String units = "";
            String convertion = "";
            Console.WriteLine("Escriba el numero a convertir: ");
            while (String.IsNullOrWhiteSpace(units))
            { 
                units = Console.ReadLine();
            }
            Console.WriteLine("Escriba su opcion de conversion: ");
            PrintCurrencyNameAndValues();
            while (String.IsNullOrWhiteSpace(convertion))
            {
                convertion = Console.ReadLine();
                convertion = GetCurrencyNames(convertion); 
                if (String.IsNullOrWhiteSpace(convertion)) 
                {
                    Console.WriteLine("Ingrese un dato valido recuerde que es el numero de referencia");
                }
            }
            String[] convertionArrayFromAndTo = convertion.Split('-');
            XmlDocument newDocumentToSend = new XmlDocument();
            XmlNode documentCharset = newDocumentToSend.CreateXmlDeclaration("1.0", "utf-8", null);
            XmlNode requestNode = newDocumentToSend.CreateElement("ConvertRequest");
            newDocumentToSend.AppendChild(requestNode);
            XmlNode unitsNode = newDocumentToSend.CreateElement("units");
            unitsNode.InnerText = units;
            requestNode.AppendChild(unitsNode);
            XmlNode fromNode = newDocumentToSend.CreateElement("from");
            fromNode.InnerText = convertionArrayFromAndTo[0];
            requestNode.AppendChild(fromNode);
            XmlNode toNode = newDocumentToSend.CreateElement("to");
            toNode.InnerText = convertionArrayFromAndTo[1];
            requestNode.AppendChild(toNode);
            return newDocumentToSend.InnerXml;
        }
        /**
         * @param Dato  String This param will be transformed into double and is a string just to avoid problems with type
         */
        public static String GetCurrencyNames(String data)
        {
            int newIntegerData = -1;
            var newData = int.TryParse(data, out newIntegerData);
            String currency = "";
            if (newData) 
            { 
                switch (newIntegerData)
                {
                    case 1:
                        currency = "USD-EUR"; 
                        break;
                    default:
                        currency = "";
                        break;
                }
            }
            return (newData) ? currency : "";
        }
        public static void PrintCurrencyNameAndValues()
        {
            Console.WriteLine("*----------------------------------------------------------------------*");
            Console.WriteLine("* Escoga unas de las siguientes divisas disponibles para su conversion:");
            Console.WriteLine("* 1. USD -  EUR");
 
        }
    }
}
