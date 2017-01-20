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
	}
}

