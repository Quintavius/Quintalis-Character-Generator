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

	//Age settings by race


	//Base rolls for stat by race
}
