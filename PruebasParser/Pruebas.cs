using System;
using NUnit.Framework;
using ParserDecenaTerrestre;

namespace PruebasParser
{
	[TestFixture()]
    public class Pruebas
    {
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
        public void TestExtSubOrd()
        {
            int[] ord = { 2, 0, 1, 3 };
            string[] f = { "bebe", "coco", "aca", "dedo" };
            string e = null;
            var r = Parser.ExtSubOrd(ord, f, 0, ref e);
            Array.Sort(f);
            int l = r.Length;//r.Length == f.Length
            bool b = AreEqual(r,f);            
            Assert.IsTrue(b);
        }

        [Test()]
        public void TestEsSub()
        {
            string[] a = { "aca", "bebe", "coco", "dedo" };
            string[] b = { "bebe", "coco", "aca", "dedo" };
            int l = a.Length;
            int[] ord = new int[l];
            bool r = Parser.EsSub(a, b, ref ord);
            Assert.IsTrue(r);
            int[] t = { 2, 0, 1, 3 };
            for (int i = 0; r && i != l; i++)
            {
                r = t[i] == ord[i];
            }
            Assert.IsTrue(r);

            a = new string[] { "aca", "coco", "dedo" };
            ord = new int[a.Length];
            r = Parser.EsSub(a, b, ref ord);
            Assert.IsTrue(r);
            t = new int[] { 2, 1, 3 };
            Assert.IsTrue(AreEqual(t, ord));            
        }

        bool AreEqual<T>(T[] a, T[] b) {
            bool r = a.Length == b.Length;
            int l = a.Length;
            for (int i = 0; r && i != l; i++) {
                r = a[i].Equals(b[i]);
            }
            return r;
        }
        

        [Test()]
        public void TestVacia()
        {
            string[,] doc = {{ "D", "C", "B", "A" },
                { "", "", "", "" }
            };
            var t = new TablaPrueba();
            int l = doc.GetLength(1);
            var f = new string[l];
            string r = null;
            bool b = Parser.Vacía(t, l, 0, ref f, ref r);
            Assert.IsFalse(b && r == null);

            b = Parser.Vacía(t, l, 1, ref f, ref r);
            Assert.IsFalse(b && r == null);
        }

        [Test()]
        public void TestObtFila()
        {
            var t = new TablaPrueba();
            int l = t.Width;
            string r = null;
            int n = 0;
            string[] f = Parser.ObtFila(t, ref r, ref n);
            Assert.IsTrue(f != null && f.Length == l && n == 1);

            n = 8;
            f = Parser.ObtFila(t, ref r, ref n);
            Assert.IsTrue(f != null && n == 11);
        }

        [Test()]
        public void TestObtEncabezado()
        {
            var t = new TablaPrueba();
            string r = null;
            int n = 0;
            var e = Parser.ObtEncabezado(t, perm, ref r, ref n);

            Assert.IsTrue(e != null && e.Length == perm.Length);
        }

        [Test()]
        public void TestObTabla()
        {
            int[] ordEncabezado;
            string[,] tb;
            int n = 0;
            var t = new TablaPrueba();
            string r = null;
            ordEncabezado = Parser.ObtEncabezado(t, perm, ref r, ref n);

            Assert.IsTrue(ordEncabezado != null && r == null && n == 1);

            tb = Parser.ObtTabla(t, ordEncabezado, ref r, ref n);

            Assert.IsTrue(tb != null && r == null && t.Height == tb.GetLength(0) + 3);

            //3 filas de total calculado
            //+ 15 filas con contenido correcto (encabezado sin "Booking")= 18
            //18 es la cantidad de filas de la tabla sin el encabezado
        }

        [Test()]
        public void TestParse()
        {
            var t = new TablaPrueba();
            string r = null;

            var d = Parser.Parse(t, perm, ref r);

            Assert.IsTrue(d.Encabezado != null && d.Tabla != null && r == null);
        }
    }
}
