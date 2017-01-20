using System;
using OfficeOpenXml;
using System.IO;

namespace ParserDecenaTerrestre
{
	public class Tabla : ITabla, IDisposable
	{
		ExcelWorksheet w;
		ExcelPackage p;

		public Tabla(string path, ref string r)
		{
			FileInfo f = new FileInfo(path);
			if (!f.Exists) r = String.Format("No existe el archivo {0}", path);
			else {
				p = new ExcelPackage(f);
				//epplus.codeplex.com
				//TODO ver que excepciones lanza                
				w = p.Workbook.Worksheets[1];//que payasada contar desde 1                

			}
		}

		public int Height
		{
			get { return w.Dimension.Rows; }
		}

		public int Width
		{
			get { return w.Dimension.Columns; }
		}

		public string Celda(int i, int j, ref string r)
		{
			//TODO ver que excepciones lanza
			var s = w.GetValue(i + 1, j + 1);//+1 porque los indices
											 //comienzan en uno para w (lamentablemente)
			string a = null;
			if (s != null) a = s.ToString().Trim();
			return a;
		}

		public void Dispose()
		{
			p.Dispose();
		}

		public bool EsTotalCalculado(int i)
		{
			int c = 0;
			string r = null;
			for (int j = 0; r == null && j != Width; j++)
			{
				var s = Celda(i, j, ref r);
				c += string.IsNullOrEmpty(s) ? 1 : 0;
			}
			return c == Width || c == Width - 2;
		}
	}
}
