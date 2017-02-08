using System;
using OfficeOpenXml;
using System.IO;

namespace ParserDecenaTerrestre
{
	public class TablaCSV:ITabla
	{
		ExcelWorksheet w;
		ExcelPackage p;

		public TablaCSV(string path, int sheet, ref string r)
		{
			FileInfo f = new FileInfo(path);
			if (!f.Exists) r = string.Format("El archivo {0} no existe", path);
			else {
				p = new ExcelPackage(f);
				int c = p.Workbook.Worksheets.Count;
				if (1 <= sheet && sheet <= c)
				{
					w = p.Workbook.Worksheets[sheet];
				}
				else {
					r = string.Format("La hoja {0} no está en el rango 1-{1}", sheet, c);
				}
			}
		}

		public int Height
		{
			get
			{
				return w.Dimension.Rows;
			}
		}

		public int Width
		{
			get
			{
				var s = w.GetValue(1,1).ToString();
				var n = s.Split(',').Length;
				return n;
			}
		}

		public string Celda(int i, int j, ref string r)
		{
			string v = null;
			if (0 <= i && i < Height)
			{
				var s = w.GetValue(i + 1, 1).ToString();
				var ss = s.Split(',');
				if (0 <= j && j < ss.Length)
				{
					v = ss[j].Trim('"');
				}
				else {
					r = string.Format("La columna {0} no esta en el rango {1}-{2}", j, 0, ss.Length);
				}
			}
			else {
				r = string.Format("La fila {0} no esta en el rango {1}-{2}", i, 0, Height);
			}

			return v;
		}

		public bool EsTotalCalculado(int i)
		{
			return false;
		}
	}
}
