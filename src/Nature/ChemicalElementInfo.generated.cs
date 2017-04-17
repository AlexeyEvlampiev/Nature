

namespace Nature
{
	using System.Diagnostics;

	[DebuggerDisplay("{Name} ({Code}): {Mass}")]
	public struct ChemicalElementInfo
	{
		public readonly string Code;
		public readonly string Name;
		public readonly double Mass;

		public ChemicalElementInfo(string code, string name, double mass) : this() 
		{
			Code = code;
			Name = name;
			Mass = mass;
		}

		public override int GetHashCode() => Code.GetHashCode();

		public static ChemicalElementInfo[] GetChemicalElements()
		{
			return new ChemicalElementInfo[]
			{ 
				new ChemicalElementInfo("AC", "Actinium", 0.227),   
				new ChemicalElementInfo("AG", "Silver", 0.1078682),   
				new ChemicalElementInfo("AL", "Aluminum", 0.02698154),   
				new ChemicalElementInfo("AM", "Americium", 0.243),   
				new ChemicalElementInfo("AR", "Argon", 0.039948),   
				new ChemicalElementInfo("AS", "Arsenic", 0.07492159),   
				new ChemicalElementInfo("AT", "Astatine", 0.21),   
				new ChemicalElementInfo("AU", "Gold", 0.1969665),   
				new ChemicalElementInfo("B", "Boron", 0.010811),   
				new ChemicalElementInfo("BA", "Barium", 0.137327),   
				new ChemicalElementInfo("BE", "Beryllium", 0.009012182),   
				new ChemicalElementInfo("BI", "Bismuth", 0.2089804),   
				new ChemicalElementInfo("BK", "Berkelium", 0.247),   
				new ChemicalElementInfo("BR", "Bromine", 0.079904),   
				new ChemicalElementInfo("C", "Carbon", 0.012011),   
				new ChemicalElementInfo("CA", "Calcium", 0.040078),   
				new ChemicalElementInfo("CD", "Cadmium", 0.112411),   
				new ChemicalElementInfo("CE", "Cerium", 0.140115),   
				new ChemicalElementInfo("CF", "Californium", 0.247),   
				new ChemicalElementInfo("CL", "Chlorine", 0.0354527),   
				new ChemicalElementInfo("CM", "Curium", 0.247),   
				new ChemicalElementInfo("CO", "Cobalt", 0.0589332),   
				new ChemicalElementInfo("CR", "Chromium", 0.0519961),   
				new ChemicalElementInfo("CS", "Cesium", 0.1329054),   
				new ChemicalElementInfo("CU", "Copper", 0.063546),   
				new ChemicalElementInfo("D", "Deuterium", 0.002014101778),   
				new ChemicalElementInfo("DY", "Dysprosium", 0.1625),   
				new ChemicalElementInfo("ER", "Erbium", 0.16726),   
				new ChemicalElementInfo("ES", "Einsteinium", 0.251),   
				new ChemicalElementInfo("EU", "Europium", 0.151965),   
				new ChemicalElementInfo("F", "Fluorine", 0.0189984),   
				new ChemicalElementInfo("FE", "Iron", 0.055847),   
				new ChemicalElementInfo("FM", "Fermium", 0.257),   
				new ChemicalElementInfo("FR", "Francium", 0.223),   
				new ChemicalElementInfo("GA", "Gallium", 0.069723),   
				new ChemicalElementInfo("GD", "Gadolinium", 0.15725),   
				new ChemicalElementInfo("GE", "Germanium", 0.07261),   
				new ChemicalElementInfo("H", "Hydrogen", 0.00100794),   
				new ChemicalElementInfo("HE", "Helium", 0.004002602),   
				new ChemicalElementInfo("HF", "Hafnium", 0.17849),   
				new ChemicalElementInfo("HG", "Mercury", 0.20059),   
				new ChemicalElementInfo("HO", "Holmium", 0.1649303),   
				new ChemicalElementInfo("I", "Iodine", 0.12690447),   
				new ChemicalElementInfo("IN", "Indium", 0.11482),   
				new ChemicalElementInfo("IR", "Iridium", 0.19222),   
				new ChemicalElementInfo("K", "Potassium", 0.0390983),   
				new ChemicalElementInfo("KR", "Krypton", 0.0838),   
				new ChemicalElementInfo("LA", "Lanthanum", 0.1389055),   
				new ChemicalElementInfo("LI", "Lithium", 0.006941),   
				new ChemicalElementInfo("LU", "Lutetium", 0.174967),   
				new ChemicalElementInfo("MG", "Magnesium", 0.024305),   
				new ChemicalElementInfo("MN", "Manganese", 0.05493805),   
				new ChemicalElementInfo("MO", "Molybdenum", 0.09594),   
				new ChemicalElementInfo("N", "Nitrogen", 0.01400674),   
				new ChemicalElementInfo("NA", "Sodium", 0.02298977),   
				new ChemicalElementInfo("NB", "Niobium", 0.09290638),   
				new ChemicalElementInfo("ND", "Neodymium", 0.14424),   
				new ChemicalElementInfo("NE", "Neon", 0.0201797),   
				new ChemicalElementInfo("NI", "Nickel", 0.0586934),   
				new ChemicalElementInfo("NP", "Neptunium", 0.2370482),   
				new ChemicalElementInfo("O", "Oxygen", 0.0159994),   
				new ChemicalElementInfo("OS", "Osmium", 0.1902),   
				new ChemicalElementInfo("P", "Phosphorus", 0.03097376),   
				new ChemicalElementInfo("PA", "Protactinium", 0.23103588),   
				new ChemicalElementInfo("PB", "Lead", 0.2072),   
				new ChemicalElementInfo("PD", "Palladium", 0.10642),   
				new ChemicalElementInfo("PM", "Promethium", 0.145),   
				new ChemicalElementInfo("PO", "Polonium", 0.209),   
				new ChemicalElementInfo("PR", "Praseodymium", 0.1409077),   
				new ChemicalElementInfo("PT", "Platinum", 0.19508),   
				new ChemicalElementInfo("PU", "Plutonium", 0.244),   
				new ChemicalElementInfo("RA", "Radium", 0.2260254),   
				new ChemicalElementInfo("RB", "Rubidium", 0.0854678),   
				new ChemicalElementInfo("RE", "Rhenium", 0.186207),   
				new ChemicalElementInfo("RH", "Rhodium", 0.1029055),   
				new ChemicalElementInfo("RN", "Radon", 0.222),   
				new ChemicalElementInfo("RU", "Ruthenium", 0.10107),   
				new ChemicalElementInfo("S", "Sulfur", 0.032066),   
				new ChemicalElementInfo("SB", "Antimony", 0.121757),   
				new ChemicalElementInfo("SC", "Scandium", 0.04495591),   
				new ChemicalElementInfo("SE", "Selenium", 0.07896),   
				new ChemicalElementInfo("SI", "Silicon", 0.0280855),   
				new ChemicalElementInfo("SM", "Samarium", 0.15036),   
				new ChemicalElementInfo("SN", "Tin", 0.11871),   
				new ChemicalElementInfo("SR", "Strontium", 0.08762),   
				new ChemicalElementInfo("TA", "Tantalum", 0.1809479),   
				new ChemicalElementInfo("TB", "Terbium", 0.1589253),   
				new ChemicalElementInfo("TC", "Technetium", 0.098),   
				new ChemicalElementInfo("TE", "Tellurium", 0.1276),   
				new ChemicalElementInfo("TH", "Thorium", 0.2320381),   
				new ChemicalElementInfo("TI", "Titanium", 0.04788),   
				new ChemicalElementInfo("TL", "Thallium", 0.2043833),   
				new ChemicalElementInfo("TM", "Thulium", 0.1689342),   
				new ChemicalElementInfo("U", "Uranium", 0.2380289),   
				new ChemicalElementInfo("V", "Vanadium", 0.0509415),   
				new ChemicalElementInfo("W", "Tungsten", 0.18385),   
				new ChemicalElementInfo("XE", "Xenon", 0.13129),   
				new ChemicalElementInfo("Y", "Yttrium", 0.08890585),   
				new ChemicalElementInfo("YB", "Ytterbium", 0.17304),   
				new ChemicalElementInfo("ZN", "Zinc", 0.06539),   
				new ChemicalElementInfo("ZR", "Zirconium", 0.091224),   
			};
		}
	}		
}
	