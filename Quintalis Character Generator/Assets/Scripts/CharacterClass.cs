using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A ton of lists and enums to hold overall options.
//These are expanded with details inside CharacterGenerator's fancy dictionaries
//using the classes defined in CoreValues.

public static class CharacterClass {
	public enum Race {
		Random,
		Madur,
		Askadur,
		Vidur,
		Kanina,
		Skjomadur,
		Troll,
		Nyrn,
		Draugur,
		UrminnYoung,
		UrminnAdult,
		Faeryn,
		Lifindur
	}
	public enum Gift {
		Random,
		None,
		Medium,
		Esper,
		Syn
	}
	public enum Era{
		Random,
		AstralEra,
		Magusdawn,
		FirstAgeOfEsper,
		SecondAgeOfEsper,
		ThirdAgeOfEsper,
		SynsteelAge,
		AgeOfUpheaval,
		Astralnight
	}
	public enum Esper{
		Random,
		None,
		Drividra,
		Enndra,
		Nyardra,
		Quibhdra,
		Tvardra,
		Vesdra,
		Zeshdra	
	}
	public enum Pantheon{
		Random,
		None,
		Magi,
		TheTen,
		AstralGods
	}
	public enum AgeGroup{
		Adolescent,
		YoungAdult,
		MiddleAge,
		Old,
		Ancient
	}
	public enum PowerLevel{
		Random,
		Fresh,
		AverageJoe,
		Talented,
		Blake,
		Unseduceable,
		TooMuchPower
	}
}
