using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Here we have global information about Quintalis and classes extending information
//held by the enums in CharacterClass.
//This one packs all the details.

public static class CoreValues {
	//Era divisions
	public class EraClass{
		public CharacterClass.Era eraIndex;
		public int eraStartYear;
		public int eraEndYear;
		public List<CharacterClass.Race> availableRacesInEra;
		public List<CharacterClass.Gift> availableGiftsInEra;
	}

	//Race info
	public class RaceClass{
		public CharacterClass.Race raceIndex;
		public int dieSize_Brains;
		public int dieSize_Brawn;
		public int dieSize_Skin;
		public int dieSize_Tongue;
		public Vector2 ageRange_Adolescent;
		public Vector2 ageRange_YoungAdult;
		public Vector2 ageRange_MiddleAge;
		public Vector2 ageRange_Old;
		public Vector2 ageRange_Ancient;
	}


}
