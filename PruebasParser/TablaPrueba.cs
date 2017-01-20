using ParserDecenaTerrestre;

namespace PruebasParser
{
	public class TablaPrueba:ITabla
	{
		string[,] t = { {//0
				"Fecha de Documento",
                "Fecha Out",
                "Pax",
                "Importe",
                "Moneda",
                "Factura",
                "Hotelbeds Ref. #",
                "Client Ref. #",
                "Booking"
            }, {"14/07/2016", //1
				"15/07/2016",
                "MARIELA LAURA  COGLIATI",
                "49,38",
                "EUR",
                "8378495-0",
                "9RE662781",
                "(1) @OPERACIONESGARB",
                "77-662781"
            }, {
                "14/07/2016",//2
				"16/07/2016",
                "PIETRO  MARRA",
                "93,71",
                "EUR",
                "18302153-0",
                "703RE2849522",
                "(1) @WI4927",
                "207-3342111"
            }, {
                "14/07/2016",//3
				"16/07/2016",
                "PEDRO  MARRA",
                "321,88",
                "EUR",
                "18294501-0",
                "713RE3341994",
                "(1) @OPERACIONESGARB",
                "207-3341994"
            }, {
                "14/07/2016",//4
				"16/07/2016",
                "PIETRO  MARRA",
                "67,04",
                "EUR",
                "18302153-0",
                "713RE3342111",
                "(1) @WI4927",
                "207-3342111"
            }, {
                "17/07/2016",//5
				"17/07/2016",
                "OMAR RICARDO  BLANCO",
                "286,94",
                "EUR",
                "18349334-0",
                "725RE817932",
                "(1) @OPERACIONESGARB",
                "221-817932"
            }, {
                "17/07/2016",//6
				"17/07/2016",
                "MYRIAM  SANTILLAN",
                "97,71",
                "EUR",
                "18343513-0",
                "92RE6577902",
                "(1) @OPERACIONESGARB",
                "102-6577902"
            }, {
                "15/07/2016",//7
				"19/07/2016",
                "SEBASTIÁN DARÍO  SALVI FLUIXÁ",
                "710,27",
                "EUR",
                "18304893-0",
                "32RE2018732",
                "(1) @OPERACIONESGARB",
                "57-2018732"
            },
            { "", "", "", "1.626,93", "EUR", "", "", "", "" },//8
			{ "", "", "", "", "", "", "", "", "" },/*9*/ {//10
				"10/07/2016",
                "23/07/2016",
                "ALICIA GABRIELA  PRADO",
                "36,66",
                "USD",
                "1023347-0",
                "3RE1214054",
                "(1) @WI6171",
                "66-1214054"
            }, {//11
				"17/07/2016",
                "24/07/2016",
                "ALFIO ARTURO  VIARO",
                "1.996,65",
                "USD",
                "18340265-0",
                "1RE2028139",
                "(1) @OPERACIONESGARB",
                "69-2028139"
            }, {//12
				"17/07/2016",
                "24/07/2016",
                "ALFIO  VIARO",
                "57,96",
                "USD",
                "18340266-0",
                "1RE2030748",
                "(1) @WH6447",
                "69-2030748"
            }, {//13
				"15/07/2016",
                "24/07/2016",
                "SEBASTIAN CARLOS  MUNIAGURRY",
                "902,75",
                "USD",
                "5333998-0",
                "1RE3401099",
                "(1) @OPERACIONESGARB",
                "235-3401099"
            }, {//14
				"15/07/2016",
                "25/07/2016",
                "CHRISTIAN SEBASTIAN  GARY",
                "547,08",
                "USD",
                "5327522-0",
                "1RE3448097",
                "(1) @OPERACIONESGARB",
                "235-3448097"
            }, {//15 de aqui en adelante el parser
				//no reconoce las filas porque todas
				//tienen algun campo vacio
				"18/07/2016",
                "25/07/2016",
                "ANGEL  ALVAREZ GARCIA",
                "136,92",
                "USD",
                "1024197-0",
                "3RE1140455",
                "(1) @WH6105",
                ""
            }, {//16
				"18/07/2016",
                "25/07/2016",
                "SANDRA ADRIANA  VAZQUEZ",
                "159,74",
                "USD",
                "1024199-0",
                "3RE1151245",
                "(1) @WH7363",
                ""
            }, {//17
				"18/07/2016",
                "25/07/2016",
                "LEANDRO GABRIEL  FARFALLA",
                "27,52",
                "USD",
                "1024232-0",
                "3RE1220897",
                "(1) @WG3846",
                ""
            },
            { "", "", "", "3.865,28", "USD", "", "", "", "" },//18
		};

        public int Height
        {
            get
            {
                return t.GetLength(0);
            }
        }

        public int Width
        {
            get
            {
                return t.GetLength(1);
            }
        }

        public string Celda(int i, int j, ref string r){
			string c = null;
			if(B(i,t.GetLength(0)) && B(j, t.GetLength(1)))
				c = t [i, j];
			else
				r = "Celda no existente";
			return c;
		}

        public bool EsTotalCalculado(int i)
        {
            return i == 8 || i == 9 || i == 18;
        }

        bool B(int x, int b) {
			bool r = 0 <= x && x < b;
			return r;
		}
	}
}

