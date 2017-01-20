using System;
using System.Collections.Generic;


namespace ParserDecenaTerrestre
{
	public static class Parser
	{
		/// <summary>
		/// Devuelve una instancia de Documento con
		/// los datos de las filas y columnas importantes
		/// del documento. En caso de fallar al reconocer el
		/// documento retorna null. Ejemplo:
		/// string path = @"C:\Users\lamg\Desktop\garbarino.xlsx";
		/// string[] pls = { new string[] { "X" },
		/// new string[] {"X", "Importe"},
		///        new string[] {"Importe","Moneda","Factura" } };
		/// string r = null;
		/// Documento d = Parse(path, pls, ref r);
		/// </summary>
		/// <param name="path">Camino al documento de Excel</param>
		/// <param name="plantillas">
		/// Arreglo de plantillas de las cuales una debe
		/// servir para reconocer el documento, en caso de
		/// que hayan mas la primera sera escogida.
		/// </param>
		/// <param name="r">
		/// Mensaje de error, debe ser null si
		/// no hubo error al reconocer el documento
		/// </param>
		/// <returns></returns>
		public static Documento Parse(string path, string[][] plantillas, ref string r)
		{
			//la plantilla es un subconjunto de las columnas del excel        
			Documento d = null;
			var t = new Tabla(path, ref r);
			if (r == null)
			{
				bool b = true;
				for (int i = 0; b && i != plantillas.Length; i++)
				{
					r = null;
					d = Parse(t, plantillas[i], ref r);
					b = r != null;
				}
			}
			if (d != null) r = null;
			return d;
		}

		public static Documento Parse(ITabla t, string[] perm, ref string r)
		{
			int[] ordEncabezado;
			string[,] tabla;
			Documento d = null;
			int n = 0;
			ordEncabezado = ObtEncabezado(t, perm, ref r, ref n);
			if (r == null)
			{
				//el encabezado es un subconjunto de perm
				//no es necesario ordenar el encabezado para
				//introducirlo en la base de datos porque 
				//sabemos que el resultado será un arreglo
				//igual a perm, por eso se usa este último
				//para construir el documento
				tabla = ObtTabla(t, ordEncabezado, ref r, ref n);
				//tabla es el cuerpo del documento
				//a ser ordenado segun el orden definido en
				//ordEncabezado, de esa forma queda
				//lista para ser introducida en la base 
				//de datos
				if (r == null)
				{
					d = new Documento(perm, tabla);
				}
			}
			return d;
		}

		public static int[] ObtEncabezado(ITabla t, string[] perm, ref string r, ref int n)
		{
			string[] fila = ObtFila(t, ref r, ref n);
			int[] ord = new int[perm.Length];
			//ord es la posicion de cada elemento
			//de perm en fila
			if (r == null)
			{
				bool b = EsSub(perm, fila, ref ord);
				//obtener el orden de la permutación
				if (!b)
					r = "El encabezado no es un subconjunto de las columnas predefinidas";
			}
			return ord;
		}

		public static bool EsSub(string[] a, string[] b, ref int[] ord)
		{
			//a no debe tener elementos repetidos
			//por eso debe usarse como parametro
			//en el que se pasa el encabezado modelo
			bool r = a.Length == ord.Length;
			int t = 0;//cantidad de elementos de a
					  //encontrados en b
			for (int i = 0; r && i != a.Length; i++)
			{
				for (int j = 0; j != b.Length; j++)
				{
					if (a[i] == b[j])
					{
						ord[i] = j;
						t++;
					}
				}
				r = t == i + 1;
				//r equivale a si se encontró el elemento
				//a[i] en b, si ese es el caso la posición
				//de a[i] en j queda en ord[i]
			}
			return r;
			//ord tiene en cada posición el indice
			//del cada elemento de a en b, si r.
		}

		public static string[] ObtFila(ITabla t, ref string r, ref int n)
		{
			//n es la fila a obtener
			string[] f = new string[t.Width];

			bool a = t.EsTotalCalculado(n);
			while (a && n != t.Height)
			{
				n++;
				a = t.EsTotalCalculado(n);
			}
			//las filas con totales calculados han sido saltadas
			//o se ha llegado al final
			if (n != t.Height)
			{
				for (int i = 0; r == null && i != t.Width; i++)
				{
					f[i] = t.Celda(n, i, ref r);
				}
				if (r == null) n++;
			}
			else if (a)
			{
				f = null;
			}

			return n <= t.Height && r == null ? f : null;
			//null significa que terminó el archivo
			//f es la fila sin total calculado más proxima
			//si no ha terminado el archivo
		}

		public static bool Vacía(ITabla t, int l, int n, ref string[] f, ref string r)
		{
			bool b = true;
			for (int j = 0; b && j != l; j++)
			{
				f[j] = t.Celda(n, j, ref r);
				b = f[j] == "" && r == null;
			}
			return b;
			//b = todas las celdas están vacías
		}

		public static string[,] ObtTabla(ITabla t, int[] ord, ref string r, ref int n)
		{
			bool b = true;
			LinkedList<string[]> nt = new LinkedList<string[]>();
			while (b)
			{
				string[] fila = ObtFila(t, ref r, ref n);
				if (fila != null)
				{
					var fn = ExtSubOrd(ord, fila, n, ref r);
					if (r == null)
					{
						nt.AddLast(fn);
					}
				}
				b = n != t.Height && fila != null && r == null;
			}

			//La tabla esta en nt y en el orden
			//correcto para introducirla en la base
			//de datos.
			//La cantidad de filas de la tabla es
			//la cantidad de elementos de nt.
			//La cantidad de columnas de la tabla
			//es la cantidad de elementos del orden
			//de las columnas.
			string[,] rt = new string[nt.Count, ord.Length];
			int c = 0;
			foreach (var x in nt)
			{
				for (int i = 0; i != x.Length; i++)
				{
					rt[c, i] = x[i];
				}
				c++;
			}
			//la tabla esta convertida a string[,]
			//con las dimensiones correctas
			return rt;
		}

		public static string[] ExtSubOrd(int[] ord, string[] f, int n, ref string e)
		{
			string[] r = new string[ord.Length];
			for (int i = 0; e == null && i != ord.Length; i++)
			{
				if (f[ord[i]] == "")
				{
					e = String.Format("La celda ({0},{1}) no debe estar vacia", n, ord[i]);
				}
				else {
					r[i] = f[ord[i]];
				}
			}
			return r;
			//en cada posición de r está el subconjunto de los elementos
			//de f, en la posición designada por ord
		}
	}
}
