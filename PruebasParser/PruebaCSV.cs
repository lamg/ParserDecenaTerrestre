using System;
using NUnit.Framework;
using ParserDecenaTerrestre;

namespace PruebasParser
{
	[TestFixture()]
	public class PruebaCSV
	{
		string book1CSV = @"Book1.csv", sep = ";";

		[Test()]
		public void TestParseCSV()
		{
			string r = null;
			string[][] p = { new string[] { "col1", "col2", "col3" } };
			Documento d = Parser.LoadParseCSV(book1CSV, sep, p, ref r);
			Assert.IsTrue(d != null && r == null && d.Tabla.GetLength(0) == 1
			              && d.Tabla.GetLength(1) == 3);
		}
	}
}
