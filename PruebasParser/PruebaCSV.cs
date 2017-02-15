using System;
using NUnit.Framework;
using ParserDecenaTerrestre;

namespace PruebasParser
{
	[TestFixture()]
	public class PruebaCSV
	{
		string book1CSV = @"Book1.csv", sep = ";",
		garbarinoCSV = @"garbarino.csv", decena = "decena__01985.csv";

		[Test()]
		public void TestParseCSV()
		{
			string r = null;
			string[][] p = { new string[] { "col1", "col2", "col3" } };
			Documento d = Parser.LoadParseCSV(book1CSV, sep, p, ref r);
			Assert.IsTrue(d != null && r == null && d.Tabla.GetLength(0) == 1
						  && d.Tabla.GetLength(1) == 3);

			Documento dn;
			p = new string[][]{new string[] { "X" },
				new string[] { "X", "Importe" },
				new string[] { "Importe", "Moneda", "Factura" } };
			dn = Parser.LoadParseCSV(garbarinoCSV, sep, p, ref r);
			Assert.IsTrue(d != null && r == null && dn.Tabla.GetLength(0) == 15 &&
						  dn.Tabla.GetLength(1) == 3);

			Documento dec;
			p = new string[][]{
				new string[]{"operador","fecha","out","pax","importe","iva","MONEDA","NRO_CONF"}};
			dec = Parser.LoadParseCSV(decena, sep, p, ref r);
			Assert.IsTrue(d != null && r == null &&
						  dec.Tabla.GetLength(1) == p[0].Length &&
						  dec.Tabla.GetLength(0) == 26);
		}
	}
}