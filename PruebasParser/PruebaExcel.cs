using System;
using NUnit.Framework;
using ParserDecenaTerrestre;

namespace PruebasParser {
	[TestFixture()]
    public class PruebaExcel {
        string garbarino = @"garbarino.xlsx";
        string book1 = @"Book1.xlsx";
		string tourico = @"Tourico.xlsx";
        string[] perm = {
            "Fecha de Documento",
            "Fecha Out",
            "Pax",
            "Importe",
            "Moneda",
            "Factura",
            "Hotelbeds Ref. #",
            "Client Ref. #",
            //"Booking"
        };

        [Test()]
        public void TestTablaNueva() {
            string r = null;
            Tabla t = new Tabla(garbarino, ref r);
            Assert.IsTrue(r == null && t != null);
        }

        [Test()]
        public void TestHeighWidth() {
            string r = null;
            var t = new Tabla(garbarino, ref r);
            Assert.IsTrue(r == null && t.Height == 19 && t.Width == 9);
        }

        [Test()]
        public void TestCelda() {
            string r = null;
            var t = new Tabla(garbarino, ref r);
            string x = t.Celda(0, 0, ref r);
            Assert.IsTrue(x != null && r == null);

            x = t.Celda(8, 0, ref r);
            Assert.IsTrue(x == null && r == null);
        }

        [Test()]
        public void EsTotalCalculadoExcel() {
            string r = null;
            var t = new Tabla(garbarino, ref r);
            bool b = t.EsTotalCalculado(8);
            Assert.IsTrue(b);
        }

        [Test()]
        public void Test_ObtFilaExcel() {
            string r = null;
            var t = new Tabla(garbarino, ref r);
            int l = t.Width;      
            int n = 0;
            string[] f = Parser._ObtFila(t, ref r, ref n);
            Assert.IsTrue(f != null && f.Length == l && n == 1);

            n = 8;
            f = Parser._ObtFila(t, ref r, ref n);
            Assert.IsTrue(f != null && n == 11);

            n = 18;
            f = Parser._ObtFila(t, ref r, ref n);
            Assert.IsTrue(f == null && n == 19);
        }

        [Test()]
        public void TestParse() {
            string r = null;
            var t = new Tabla(garbarino, ref r);
            Documento d = Parser.Parse(t, perm, ref r);

            Assert.IsTrue(d.Tabla.GetLength(0) == 15 && d.Tabla.GetLength(1) == 8);

            string[] p = { "Importe", "Moneda", "Booking" };
            d = Parser.Parse(t, p, ref r);
            Assert.IsTrue(r != null);

            p = new string[]{ "X" };
            d = Parser.Parse(t, p, ref r);
            Assert.IsTrue(r != null);
        }

        [Test()]
        public void TestParse2() {
            string[][] p = { new string[] { "X" },
                new string[] {"X", "Importe"},
                new string[] {"Importe","Moneda","Factura" } };
            //p es el arreglo de plantillas de la cual una debe
            //servir para parsear el documento
            string r = null;
            Documento d = Parser.LoadParse(garbarino, p, ref r);
			var cs = d.CalculateChecksum();
            Assert.IsTrue(r == null && d != null && d.Tabla.GetLength(1) == 3 && cs != null);
        }

		[Test()]
		public void TestBook1()
		{
			string[][] p = { new string[] { "col1", "col2", "col3" } };
			string r = null;
			Tabla t = new Tabla(book1, ref r);
			int n = 3;
			var f = Parser._ObtFila(t, ref r, ref n);
			Assert.IsTrue(f != null);

			Documento d = Parser.LoadParse(book1, p, ref r);
			Assert.IsTrue(d != null && d.Tabla.GetLength(0) == 3);

			d = Parser.LoadParse(book1, p, ref r,2);
			Assert.IsTrue(d != null && r == null);
			Assert.IsTrue(d.Equals(d));
			Assert.IsTrue(d.ToString() != null);
			var cs = d.CalculateChecksum();
			Assert.IsTrue(cs != null);
			var s = d.CalculateHash();
			Assert.IsNotNullOrEmpty(s);

			d = Parser.LoadParse(book1, p, ref r, 3);
			Assert.IsTrue(d == null && r != null);
		}

		[Test()]
		public void TestTourico()
		{
			string r = null;
			var p = new string[] { "Doc#", "RL#", "Guest Name", "System Amount", "System Currency", "Local Amount", "Local Currency", "Commission", "Commission Currency", "Commission Local Currency ", "Supplier", "Reservation #", "No. of guests", "Service", "Service Status", "Invoice Type", "Invoice State", "Check In", "Check Out", "Booking Agency", "Booking Agent", "Agent ref #", "Reservation Date", "Inv" };

			TablaCSV ts = new TablaCSV(tourico, 1, ref r);
			Assert.IsTrue(r == null && ts.Width == p.Length);

			int n = 1;
			var ss = Parser._ObtFila(ts, ref r, ref n);
			Assert.IsTrue(ss.Length == p.Length);

			var d = Parser.Parse(ts, p, ref r);
			Assert.IsTrue(r == null);

			d = Parser.LoadParse(tourico, new string[][] { p }, ref r);
			Assert.IsTrue(d != null && r == null);
		}
    }
}
