using System;

namespace ParserDecenaTerrestre
{
	public class Documento
	{
		string[] encabezado;
		string[,] tabla;

		public Documento (string[] encabezado, string[,] tabla)
		{
			this.encabezado = encabezado;
			this.tabla = tabla;
		}

		public string[] Encabezado{
			get {return this.encabezado;}
		}

		public string[,] Tabla {
			get {return this.tabla;}
		}

		public override bool Equals(object obj)
		{
			bool r = false;
			Documento d = obj as Documento;
			r = d != null && encabezado.Length == d.encabezado.Length &&
									   tabla.GetLength(0) == d.tabla.GetLength(0) &&
									   tabla.GetLength(1) == d.tabla.GetLength(1);

			if (r)
			{
				int le = encabezado.Length;
				int t0 = tabla.GetLength(0);
				int t1 = tabla.GetLength(1);
				for (int i = 0; r && i != le; i++)
				{
					r = encabezado[i] == d.encabezado[i];
				}
				for (int i = 0; r && i != t0; i++)
				{
					for (int j = 0; r && j != t1; j++)
					{
						r = tabla[i, j] == d.tabla[i, j];
					}
				}
			}
			return r;
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		public override string ToString()
		{
			string s = "";
			for (int i = 0; i != encabezado.Length; i++)
			{
				s += encabezado[i];
			}
			for (int i = 0; i != tabla.GetLength(0); i++)
			{
				for (int j = 0; j != tabla.GetLength(1); j++)
				{
					s += tabla[i, j];
				}
			}
			return s;
		}
	}
}

