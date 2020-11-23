using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConverterServer
{
    public class XmlConverterTest
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        [Fact]
        public void TestXmlReadRequest()
        {
            TextReader tr = new StreamReader(basePath + "/" + "RequestFile.xml");
            string myText = tr.ReadToEnd();
            Assert.Contains("ConvertRequest", myText);
        }
        [Fact]
        public void TestProcesarXmlConvertRequest()
        {
            TextReader tr = new StreamReader(basePath + "/" + "RequestFile.xml");
            string myText = tr.ReadToEnd();
            var resultado = 0m;
            var operacion = "";
            operacion = XmlConverter.ProcesarXmlConvertRequest(myText, out resultado);
            Assert.Equal(10, resultado);
            Assert.Equal("USD-EUR", operacion);
        }
        [Fact]
        public void TestXmlReadResponse()
        {
            TextReader tr = new StreamReader(basePath + "/" + "ResponseFile.xml");
            string myText = tr.ReadToEnd();
            Assert.Contains("ConvertResponse", myText);
        }
        [Fact]
        public void TestXmlErrorMessage()
        {
            var xmlText = XmlConverter.GenerarPaqueteXmlConvertResponseError("ERROR: Divisa no reconocida MXN");
            Assert.Equal("<?xml version=\"1.0\" encoding=\"UTF-8\"?><ConvertResponse>ERROR: Divisa no reconocida MXN</ConvertResponse>", xmlText);
       }

    }
}
