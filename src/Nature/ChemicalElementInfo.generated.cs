

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

		/// <summary>
        /// Actinium (AC)
        /// </summary>
		public static ChemicalElementInfo AC => new ChemicalElementInfo("AC", "Actinium", 0.227);  

		/// <summary>
        /// Silver (AG)
        /// </summary>
		public static ChemicalElementInfo AG => new ChemicalElementInfo("AG", "Silver", 0.1078682);  

		/// <summary>
        /// Aluminum (AL)
        /// </summary>
		public static ChemicalElementInfo AL => new ChemicalElementInfo("AL", "Aluminum", 0.02698154);  

		/// <summary>
        /// Americium (AM)
        /// </summary>
		public static ChemicalElementInfo AM => new ChemicalElementInfo("AM", "Americium", 0.243);  

		/// <summary>
        /// Argon (AR)
        /// </summary>
		public static ChemicalElementInfo AR => new ChemicalElementInfo("AR", "Argon", 0.039948);  

		/// <summary>
        /// Arsenic (AS)
        /// </summary>
		public static ChemicalElementInfo AS => new ChemicalElementInfo("AS", "Arsenic", 0.07492159);  

		/// <summary>
        /// Astatine (AT)
        /// </summary>
		public static ChemicalElementInfo AT => new ChemicalElementInfo("AT", "Astatine", 0.21);  

		/// <summary>
        /// Gold (AU)
        /// </summary>
		public static ChemicalElementInfo AU => new ChemicalElementInfo("AU", "Gold", 0.1969665);  

		/// <summary>
        /// Boron (B)
        /// </summary>
		public static ChemicalElementInfo B => new ChemicalElementInfo("B", "Boron", 0.010811);  

		/// <summary>
        /// Barium (BA)
        /// </summary>
		public static ChemicalElementInfo BA => new ChemicalElementInfo("BA", "Barium", 0.137327);  

		/// <summary>
        /// Beryllium (BE)
        /// </summary>
		public static ChemicalElementInfo BE => new ChemicalElementInfo("BE", "Beryllium", 0.009012182);  

		/// <summary>
        /// Bismuth (BI)
        /// </summary>
		public static ChemicalElementInfo BI => new ChemicalElementInfo("BI", "Bismuth", 0.2089804);  

		/// <summary>
        /// Berkelium (BK)
        /// </summary>
		public static ChemicalElementInfo BK => new ChemicalElementInfo("BK", "Berkelium", 0.247);  

		/// <summary>
        /// Bromine (BR)
        /// </summary>
		public static ChemicalElementInfo BR => new ChemicalElementInfo("BR", "Bromine", 0.079904);  

		/// <summary>
        /// Carbon (C)
        /// </summary>
		public static ChemicalElementInfo C => new ChemicalElementInfo("C", "Carbon", 0.012011);  

		/// <summary>
        /// Calcium (CA)
        /// </summary>
		public static ChemicalElementInfo CA => new ChemicalElementInfo("CA", "Calcium", 0.040078);  

		/// <summary>
        /// Cadmium (CD)
        /// </summary>
		public static ChemicalElementInfo CD => new ChemicalElementInfo("CD", "Cadmium", 0.112411);  

		/// <summary>
        /// Cerium (CE)
        /// </summary>
		public static ChemicalElementInfo CE => new ChemicalElementInfo("CE", "Cerium", 0.140115);  

		/// <summary>
        /// Californium (CF)
        /// </summary>
		public static ChemicalElementInfo CF => new ChemicalElementInfo("CF", "Californium", 0.247);  

		/// <summary>
        /// Chlorine (CL)
        /// </summary>
		public static ChemicalElementInfo CL => new ChemicalElementInfo("CL", "Chlorine", 0.0354527);  

		/// <summary>
        /// Curium (CM)
        /// </summary>
		public static ChemicalElementInfo CM => new ChemicalElementInfo("CM", "Curium", 0.247);  

		/// <summary>
        /// Cobalt (CO)
        /// </summary>
		public static ChemicalElementInfo CO => new ChemicalElementInfo("CO", "Cobalt", 0.0589332);  

		/// <summary>
        /// Chromium (CR)
        /// </summary>
		public static ChemicalElementInfo CR => new ChemicalElementInfo("CR", "Chromium", 0.0519961);  

		/// <summary>
        /// Cesium (CS)
        /// </summary>
		public static ChemicalElementInfo CS => new ChemicalElementInfo("CS", "Cesium", 0.1329054);  

		/// <summary>
        /// Copper (CU)
        /// </summary>
		public static ChemicalElementInfo CU => new ChemicalElementInfo("CU", "Copper", 0.063546);  

		/// <summary>
        /// Deuterium (D)
        /// </summary>
		public static ChemicalElementInfo D => new ChemicalElementInfo("D", "Deuterium", 0.002014101778);  

		/// <summary>
        /// Dysprosium (DY)
        /// </summary>
		public static ChemicalElementInfo DY => new ChemicalElementInfo("DY", "Dysprosium", 0.1625);  

		/// <summary>
        /// Erbium (ER)
        /// </summary>
		public static ChemicalElementInfo ER => new ChemicalElementInfo("ER", "Erbium", 0.16726);  

		/// <summary>
        /// Einsteinium (ES)
        /// </summary>
		public static ChemicalElementInfo ES => new ChemicalElementInfo("ES", "Einsteinium", 0.251);  

		/// <summary>
        /// Europium (EU)
        /// </summary>
		public static ChemicalElementInfo EU => new ChemicalElementInfo("EU", "Europium", 0.151965);  

		/// <summary>
        /// Fluorine (F)
        /// </summary>
		public static ChemicalElementInfo F => new ChemicalElementInfo("F", "Fluorine", 0.0189984);  

		/// <summary>
        /// Iron (FE)
        /// </summary>
		public static ChemicalElementInfo FE => new ChemicalElementInfo("FE", "Iron", 0.055847);  

		/// <summary>
        /// Fermium (FM)
        /// </summary>
		public static ChemicalElementInfo FM => new ChemicalElementInfo("FM", "Fermium", 0.257);  

		/// <summary>
        /// Francium (FR)
        /// </summary>
		public static ChemicalElementInfo FR => new ChemicalElementInfo("FR", "Francium", 0.223);  

		/// <summary>
        /// Gallium (GA)
        /// </summary>
		public static ChemicalElementInfo GA => new ChemicalElementInfo("GA", "Gallium", 0.069723);  

		/// <summary>
        /// Gadolinium (GD)
        /// </summary>
		public static ChemicalElementInfo GD => new ChemicalElementInfo("GD", "Gadolinium", 0.15725);  

		/// <summary>
        /// Germanium (GE)
        /// </summary>
		public static ChemicalElementInfo GE => new ChemicalElementInfo("GE", "Germanium", 0.07261);  

		/// <summary>
        /// Hydrogen (H)
        /// </summary>
		public static ChemicalElementInfo H => new ChemicalElementInfo("H", "Hydrogen", 0.00100794);  

		/// <summary>
        /// Helium (HE)
        /// </summary>
		public static ChemicalElementInfo HE => new ChemicalElementInfo("HE", "Helium", 0.004002602);  

		/// <summary>
        /// Hafnium (HF)
        /// </summary>
		public static ChemicalElementInfo HF => new ChemicalElementInfo("HF", "Hafnium", 0.17849);  

		/// <summary>
        /// Mercury (HG)
        /// </summary>
		public static ChemicalElementInfo HG => new ChemicalElementInfo("HG", "Mercury", 0.20059);  

		/// <summary>
        /// Holmium (HO)
        /// </summary>
		public static ChemicalElementInfo HO => new ChemicalElementInfo("HO", "Holmium", 0.1649303);  

		/// <summary>
        /// Iodine (I)
        /// </summary>
		public static ChemicalElementInfo I => new ChemicalElementInfo("I", "Iodine", 0.12690447);  

		/// <summary>
        /// Indium (IN)
        /// </summary>
		public static ChemicalElementInfo IN => new ChemicalElementInfo("IN", "Indium", 0.11482);  

		/// <summary>
        /// Iridium (IR)
        /// </summary>
		public static ChemicalElementInfo IR => new ChemicalElementInfo("IR", "Iridium", 0.19222);  

		/// <summary>
        /// Potassium (K)
        /// </summary>
		public static ChemicalElementInfo K => new ChemicalElementInfo("K", "Potassium", 0.0390983);  

		/// <summary>
        /// Krypton (KR)
        /// </summary>
		public static ChemicalElementInfo KR => new ChemicalElementInfo("KR", "Krypton", 0.0838);  

		/// <summary>
        /// Lanthanum (LA)
        /// </summary>
		public static ChemicalElementInfo LA => new ChemicalElementInfo("LA", "Lanthanum", 0.1389055);  

		/// <summary>
        /// Lithium (LI)
        /// </summary>
		public static ChemicalElementInfo LI => new ChemicalElementInfo("LI", "Lithium", 0.006941);  

		/// <summary>
        /// Lutetium (LU)
        /// </summary>
		public static ChemicalElementInfo LU => new ChemicalElementInfo("LU", "Lutetium", 0.174967);  

		/// <summary>
        /// Magnesium (MG)
        /// </summary>
		public static ChemicalElementInfo MG => new ChemicalElementInfo("MG", "Magnesium", 0.024305);  

		/// <summary>
        /// Manganese (MN)
        /// </summary>
		public static ChemicalElementInfo MN => new ChemicalElementInfo("MN", "Manganese", 0.05493805);  

		/// <summary>
        /// Molybdenum (MO)
        /// </summary>
		public static ChemicalElementInfo MO => new ChemicalElementInfo("MO", "Molybdenum", 0.09594);  

		/// <summary>
        /// Nitrogen (N)
        /// </summary>
		public static ChemicalElementInfo N => new ChemicalElementInfo("N", "Nitrogen", 0.01400674);  

		/// <summary>
        /// Sodium (NA)
        /// </summary>
		public static ChemicalElementInfo NA => new ChemicalElementInfo("NA", "Sodium", 0.02298977);  

		/// <summary>
        /// Niobium (NB)
        /// </summary>
		public static ChemicalElementInfo NB => new ChemicalElementInfo("NB", "Niobium", 0.09290638);  

		/// <summary>
        /// Neodymium (ND)
        /// </summary>
		public static ChemicalElementInfo ND => new ChemicalElementInfo("ND", "Neodymium", 0.14424);  

		/// <summary>
        /// Neon (NE)
        /// </summary>
		public static ChemicalElementInfo NE => new ChemicalElementInfo("NE", "Neon", 0.0201797);  

		/// <summary>
        /// Nickel (NI)
        /// </summary>
		public static ChemicalElementInfo NI => new ChemicalElementInfo("NI", "Nickel", 0.0586934);  

		/// <summary>
        /// Neptunium (NP)
        /// </summary>
		public static ChemicalElementInfo NP => new ChemicalElementInfo("NP", "Neptunium", 0.2370482);  

		/// <summary>
        /// Oxygen (O)
        /// </summary>
		public static ChemicalElementInfo O => new ChemicalElementInfo("O", "Oxygen", 0.0159994);  

		/// <summary>
        /// Osmium (OS)
        /// </summary>
		public static ChemicalElementInfo OS => new ChemicalElementInfo("OS", "Osmium", 0.1902);  

		/// <summary>
        /// Phosphorus (P)
        /// </summary>
		public static ChemicalElementInfo P => new ChemicalElementInfo("P", "Phosphorus", 0.03097376);  

		/// <summary>
        /// Protactinium (PA)
        /// </summary>
		public static ChemicalElementInfo PA => new ChemicalElementInfo("PA", "Protactinium", 0.23103588);  

		/// <summary>
        /// Lead (PB)
        /// </summary>
		public static ChemicalElementInfo PB => new ChemicalElementInfo("PB", "Lead", 0.2072);  

		/// <summary>
        /// Palladium (PD)
        /// </summary>
		public static ChemicalElementInfo PD => new ChemicalElementInfo("PD", "Palladium", 0.10642);  

		/// <summary>
        /// Promethium (PM)
        /// </summary>
		public static ChemicalElementInfo PM => new ChemicalElementInfo("PM", "Promethium", 0.145);  

		/// <summary>
        /// Polonium (PO)
        /// </summary>
		public static ChemicalElementInfo PO => new ChemicalElementInfo("PO", "Polonium", 0.209);  

		/// <summary>
        /// Praseodymium (PR)
        /// </summary>
		public static ChemicalElementInfo PR => new ChemicalElementInfo("PR", "Praseodymium", 0.1409077);  

		/// <summary>
        /// Platinum (PT)
        /// </summary>
		public static ChemicalElementInfo PT => new ChemicalElementInfo("PT", "Platinum", 0.19508);  

		/// <summary>
        /// Plutonium (PU)
        /// </summary>
		public static ChemicalElementInfo PU => new ChemicalElementInfo("PU", "Plutonium", 0.244);  

		/// <summary>
        /// Radium (RA)
        /// </summary>
		public static ChemicalElementInfo RA => new ChemicalElementInfo("RA", "Radium", 0.2260254);  

		/// <summary>
        /// Rubidium (RB)
        /// </summary>
		public static ChemicalElementInfo RB => new ChemicalElementInfo("RB", "Rubidium", 0.0854678);  

		/// <summary>
        /// Rhenium (RE)
        /// </summary>
		public static ChemicalElementInfo RE => new ChemicalElementInfo("RE", "Rhenium", 0.186207);  

		/// <summary>
        /// Rhodium (RH)
        /// </summary>
		public static ChemicalElementInfo RH => new ChemicalElementInfo("RH", "Rhodium", 0.1029055);  

		/// <summary>
        /// Radon (RN)
        /// </summary>
		public static ChemicalElementInfo RN => new ChemicalElementInfo("RN", "Radon", 0.222);  

		/// <summary>
        /// Ruthenium (RU)
        /// </summary>
		public static ChemicalElementInfo RU => new ChemicalElementInfo("RU", "Ruthenium", 0.10107);  

		/// <summary>
        /// Sulfur (S)
        /// </summary>
		public static ChemicalElementInfo S => new ChemicalElementInfo("S", "Sulfur", 0.032066);  

		/// <summary>
        /// Antimony (SB)
        /// </summary>
		public static ChemicalElementInfo SB => new ChemicalElementInfo("SB", "Antimony", 0.121757);  

		/// <summary>
        /// Scandium (SC)
        /// </summary>
		public static ChemicalElementInfo SC => new ChemicalElementInfo("SC", "Scandium", 0.04495591);  

		/// <summary>
        /// Selenium (SE)
        /// </summary>
		public static ChemicalElementInfo SE => new ChemicalElementInfo("SE", "Selenium", 0.07896);  

		/// <summary>
        /// Silicon (SI)
        /// </summary>
		public static ChemicalElementInfo SI => new ChemicalElementInfo("SI", "Silicon", 0.0280855);  

		/// <summary>
        /// Samarium (SM)
        /// </summary>
		public static ChemicalElementInfo SM => new ChemicalElementInfo("SM", "Samarium", 0.15036);  

		/// <summary>
        /// Tin (SN)
        /// </summary>
		public static ChemicalElementInfo SN => new ChemicalElementInfo("SN", "Tin", 0.11871);  

		/// <summary>
        /// Strontium (SR)
        /// </summary>
		public static ChemicalElementInfo SR => new ChemicalElementInfo("SR", "Strontium", 0.08762);  

		/// <summary>
        /// Tantalum (TA)
        /// </summary>
		public static ChemicalElementInfo TA => new ChemicalElementInfo("TA", "Tantalum", 0.1809479);  

		/// <summary>
        /// Terbium (TB)
        /// </summary>
		public static ChemicalElementInfo TB => new ChemicalElementInfo("TB", "Terbium", 0.1589253);  

		/// <summary>
        /// Technetium (TC)
        /// </summary>
		public static ChemicalElementInfo TC => new ChemicalElementInfo("TC", "Technetium", 0.098);  

		/// <summary>
        /// Tellurium (TE)
        /// </summary>
		public static ChemicalElementInfo TE => new ChemicalElementInfo("TE", "Tellurium", 0.1276);  

		/// <summary>
        /// Thorium (TH)
        /// </summary>
		public static ChemicalElementInfo TH => new ChemicalElementInfo("TH", "Thorium", 0.2320381);  

		/// <summary>
        /// Titanium (TI)
        /// </summary>
		public static ChemicalElementInfo TI => new ChemicalElementInfo("TI", "Titanium", 0.04788);  

		/// <summary>
        /// Thallium (TL)
        /// </summary>
		public static ChemicalElementInfo TL => new ChemicalElementInfo("TL", "Thallium", 0.2043833);  

		/// <summary>
        /// Thulium (TM)
        /// </summary>
		public static ChemicalElementInfo TM => new ChemicalElementInfo("TM", "Thulium", 0.1689342);  

		/// <summary>
        /// Uranium (U)
        /// </summary>
		public static ChemicalElementInfo U => new ChemicalElementInfo("U", "Uranium", 0.2380289);  

		/// <summary>
        /// Vanadium (V)
        /// </summary>
		public static ChemicalElementInfo V => new ChemicalElementInfo("V", "Vanadium", 0.0509415);  

		/// <summary>
        /// Tungsten (W)
        /// </summary>
		public static ChemicalElementInfo W => new ChemicalElementInfo("W", "Tungsten", 0.18385);  

		/// <summary>
        /// Xenon (XE)
        /// </summary>
		public static ChemicalElementInfo XE => new ChemicalElementInfo("XE", "Xenon", 0.13129);  

		/// <summary>
        /// Yttrium (Y)
        /// </summary>
		public static ChemicalElementInfo Y => new ChemicalElementInfo("Y", "Yttrium", 0.08890585);  

		/// <summary>
        /// Ytterbium (YB)
        /// </summary>
		public static ChemicalElementInfo YB => new ChemicalElementInfo("YB", "Ytterbium", 0.17304);  

		/// <summary>
        /// Zinc (ZN)
        /// </summary>
		public static ChemicalElementInfo ZN => new ChemicalElementInfo("ZN", "Zinc", 0.06539);  

		/// <summary>
        /// Zirconium (ZR)
        /// </summary>
		public static ChemicalElementInfo ZR => new ChemicalElementInfo("ZR", "Zirconium", 0.091224);  
	}		
}
	