using System;
using OfficeOpenXml;
using System.IO;

namespace ParserDecenaTerrestre
{
	public class Tabla : ITabla, IDisposable
	{
		ExcelWorksheet w;
		ExcelPackage p;

		public Tabla(string path, ref string r):this(path,1,ref r){}

		public Tabla(string path, int sheet, ref string r)
		{
			FileInfo f = new FileInfo(path);
			if (!f.Exists) r = String.Format("No existe el archivo {0}", path);
			else {
				p = new ExcelPackage(f);
				//epplus.codeplex.com
				//TODO ver que excepciones lanza
				int c = p.Workbook.Worksheets.Count;
				if (1 <= sheet && sheet <= c){
					w = p.Workbook.Worksheets[sheet];
				} else {
					r = string.Format("La hoja {0} no está en el rango 1-{1}", sheet, c);
				}
      
			}
		}

		public int Height
		{
			get { return w == null ? 0 : w.Dimension.Rows; }
		}

		public int Width
		{
			get { return w == null ? 0 : w.Dimension.Columns; }
		}

		public string Celda(int i, int j, ref string r)
		{
			//TODO ver que excepciones lanza
			string s = null;
			if (0 <= i && i < Height && 0 <= j && j < Width)
			{
				//+1 porque los indices
				//comienzan en uno para w (lamentablemente
				var o = w.GetValue(i + 1, j + 1);
				if (o != null)
					s = o.ToString().Trim();
			}
			else {
				r = string.Format("{0}-{1} fuera de rango {2}-{3}", i, j, Width, Height);
			}
			return s;
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
