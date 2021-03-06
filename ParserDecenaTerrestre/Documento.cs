﻿using System;
using System.Text;
using System.Security.Cryptography;

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

		public string CalculateChecksum()
		{
			string dataToCalculate = ToString();
			byte[] byteToCalculate = Encoding.UTF8.GetBytes(dataToCalculate);
			int checksum = 0;
			foreach (byte chData in byteToCalculate)
			{
				checksum += chData;
			}
			checksum &= 0xff;
			string r = checksum.ToString("X2");
			return r;
		}

		public string CalculateHash()
		{
			string r = "";
			var s = SHA256.Create();
			byte[] bs = Encoding.UTF8.GetBytes(ToString());
			byte[] rs = s.ComputeHash(bs);
			char[] cs = Encoding.ASCII.GetChars(rs);
			r = new string(cs);
			return r;
		}
	}
}

