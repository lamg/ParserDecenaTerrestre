using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ParserDecenaTerrestre
{
	public class TablaCSV:ITabla
	{
		string[,] p;

		public TablaCSV(string path, string sep, ref string r)
		{
			FileInfo f = new FileInfo(path);
			if (!f.Exists) r = string.Format("El archivo {0} no existe", path);
			else {
				StreamReader a = f.OpenText();
				var np = new List<string[]>();
				while (!a.EndOfStream)
				{
					var n = a.ReadLine().Split(sep.ToCharArray());
					np.Add(n);
				}
				p = new string[np.Count, np.Max(x => x.Length)];
				for (int i = 0; i != p.GetLength(0); i++)
				{
					for (int j = 0; j != p.GetLength(1); j++)
					{
						if (j >= np[i].Length)
						{
							p[i, j] = "";
						}
						else {
							p[i, j] = np[i][j];
						}
					}
				}
			}
		}

		public int Height
		{
			get
			{
				return p.GetLength(0);
			}
		}

		public int Width
		{
			get
			{
				return p.GetLength(1);
			}
		}

		public string Celda(int i, int j, ref string r)
		{
			string v = null;
			if (0 <= i && i < Height && 0 <= j && j < Width)
			{
				v = p[i, j];
			}
			else {
				r = string.Format("{0}-{1} no esta en el rango {2}-{3}", i, j, Height, Width);
			}

			return v;
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
