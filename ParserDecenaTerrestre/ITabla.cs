using System;

namespace ParserDecenaTerrestre
{
	public interface ITabla
	{
		string Celda(int i, int j, ref string r);

        int Width { get; }

        int Height { get; }

        bool EsTotalCalculado(int i);
	}
}

