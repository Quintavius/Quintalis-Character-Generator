using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour {
	//Reference slots
	private Character character;
	
	//Dictionaries containing all expanded information based on enum lookups. 
	//Works by doing Dictionary<[Enum Reference],[Class container that has all the deetz]>
	public Dictionary<CharacterClass.Era,CoreValues.EraClass> eraDefinition;
		//Contains era start & end, available races and gifts per era.


//===========================================================================
	//Public generation settings, changed by user input

	public int currentYear;
	public CharacterClass.PowerLevel powerLevel;
	public CharacterClass.Era era;
	public CharacterClass.Race race;
	public CharacterClass.Gift gift;

//===========================================================================
	//References

	private void Start(){
		character = GetComponent<Character>();
		PopulateDictionaries();
	}

//===========================================================================
	//Character generation, aka why we're all here today.

	public void GenerateNewCharacter(){

		//++++++++++++++++++++
		//STEP 1
		//
		//Decide on era
		//
		//- If era is set to random, pick era at random. Skip this step if a current year is specified.
		if (currentYear == 0){
			if (era == CharacterClass.Era.Random){
				//Pick a random era and pass it to the character
				var values = System.Enum.GetValues(typeof(CharacterClass.Era));
				character.era = (CharacterClass.Era)Random.Range(1,values.Length);
			}else{
				//Just pass the selected era to the Character
				character.era = era;
			}
			//Era is now set, now generate a current year.
			currentYear = GetYearFromEra(character.era);
			
		}else{
			//Year is specified, match it with an era and pass it to the character
			era = GetEraFromYear(currentYear);
			character.era = era;
		}


		//++++++++++++++++++++
		//STEP 2
		//
		//Race
		//
		//Now that we know the current era, we can pick a race from the pool of the ones available, if race is set to random
		if (race == CharacterClass.Race.Random){
			//character.race = 
		}else{
			//Nevermind, it's set. Just pass it on.
			character.race = race;
		}
	}

//===========================================================================
	//Utility functions

	CharacterClass.Era GetEraFromYear(int yearToProcess){
		//Iterate through era dictionary, compare current year to era and return if a match is found.
		CharacterClass.Era eraToReturn;
		foreach(KeyValuePair<CharacterClass.Era,CoreValues.EraClass> eraClass in eraDefinition){
			if (currentYear >= eraClass.Value.eraStartYear && currentYear < eraClass.Value.eraEndYear){
				//Found a matching era
				eraToReturn = eraClass.Value.eraIndex;
				return eraToReturn;
			}
		}
		//Year specified doesn't match, return a default.
		eraToReturn = CharacterClass.Era.SynsteelAge;
		return eraToReturn;
	}

	int GetYearFromEra(CharacterClass.Era eraToProcess){
		//Take specified era, compare with dictionary to get era range, generate random number between those years.
		int yearToReturn;
		yearToReturn = Random.Range(eraDefinition[(CharacterClass.Era)eraToProcess].eraStartYear,eraDefinition[(CharacterClass.Era)eraToProcess].eraEndYear);
		return yearToReturn;
	}


//===========================================================================
	//Dictionary definition setup

	void PopulateDictionaries(){

		//Era dicitionary
		eraDefinition = new Dictionary<CharacterClass.Era,CoreValues.EraClass>();

		//Prebuild race libraries
		List<CharacterClass.Race> preMagusdawnRaceList = new List<CharacterClass.Race>();
		preMagusdawnRaceList.Add(CharacterClass.Race.Askadur);
		preMagusdawnRaceList.Add(CharacterClass.Race.Faeryn);
		preMagusdawnRaceList.Add(CharacterClass.Race.Kanina);
		preMagusdawnRaceList.Add(CharacterClass.Race.Lifindur);
		preMagusdawnRaceList.Add(CharacterClass.Race.Madur);
		preMagusdawnRaceList.Add(CharacterClass.Race.Nyrn);
		preMagusdawnRaceList.Add(CharacterClass.Race.Skjomadur);
		preMagusdawnRaceList.Add(CharacterClass.Race.Troll);
		preMagusdawnRaceList.Add(CharacterClass.Race.Vidur);
		preMagusdawnRaceList.Add(CharacterClass.Race.UrminnAdult);
		preMagusdawnRaceList.Add(CharacterClass.Race.UrminnYoung);

		List<CharacterClass.Race> postMagusdawnRaceList = new List<CharacterClass.Race>();
		postMagusdawnRaceList.Add(CharacterClass.Race.Askadur);
		postMagusdawnRaceList.Add(CharacterClass.Race.Faeryn);
		postMagusdawnRaceList.Add(CharacterClass.Race.Kanina);
		postMagusdawnRaceList.Add(CharacterClass.Race.Draugur);
		postMagusdawnRaceList.Add(CharacterClass.Race.Madur);
		postMagusdawnRaceList.Add(CharacterClass.Race.Nyrn);
		postMagusdawnRaceList.Add(CharacterClass.Race.Skjomadur);
		postMagusdawnRaceList.Add(CharacterClass.Race.Troll);
		postMagusdawnRaceList.Add(CharacterClass.Race.Vidur);
		postMagusdawnRaceList.Add(CharacterClass.Race.UrminnAdult);
		postMagusdawnRaceList.Add(CharacterClass.Race.UrminnYoung);

		//Prebuild gift libraries
		List<CharacterClass.Gift> noGiftList = new List<CharacterClass.Gift>();
		noGiftList.Add(CharacterClass.Gift.None);

		List<CharacterClass.Gift> mediumGiftList = new List<CharacterClass.Gift>();
		mediumGiftList.Add(CharacterClass.Gift.None);
		mediumGiftList.Add(CharacterClass.Gift.Medium);

		List<CharacterClass.Gift> esperGiftList = new List<CharacterClass.Gift>();
		esperGiftList.Add(CharacterClass.Gift.None);
		esperGiftList.Add(CharacterClass.Gift.Medium);
		esperGiftList.Add(CharacterClass.Gift.Esper);

		List<CharacterClass.Gift> allGiftsList = new List<CharacterClass.Gift>();
		allGiftsList.Add(CharacterClass.Gift.None);
		allGiftsList.Add(CharacterClass.Gift.Medium);
		allGiftsList.Add(CharacterClass.Gift.Esper);
		allGiftsList.Add(CharacterClass.Gift.Syn);

	//++++++++++++++++++++++++++++++++++++++++++
	//Astral Era
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.EraClass AstralEra = new CoreValues.EraClass();
		AstralEra.eraIndex = CharacterClass.Era.AstralEra;
		AstralEra.eraStartYear = 1;
		AstralEra.eraEndYear = 2293;
		AstralEra.availableRacesInEra = preMagusdawnRaceList;
		AstralEra.availableGiftsInEra = mediumGiftList;

		//Commit to class
		eraDefinition.Add(CharacterClass.Era.AstralEra, AstralEra);
		
	//++++++++++++++++++++++++++++++++++++++++++
	//Magusdawn
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.EraClass Magusdawn = new CoreValues.EraClass();
		Magusdawn.eraIndex = CharacterClass.Era.Magusdawn;
		Magusdawn.eraStartYear = 2293;
		Magusdawn.eraEndYear = 2312;
		Magusdawn.availableRacesInEra = preMagusdawnRaceList;
		Magusdawn.availableGiftsInEra = mediumGiftList;

		//Commit to class
		eraDefinition.Add(CharacterClass.Era.Magusdawn, Magusdawn);
				
	//++++++++++++++++++++++++++++++++++++++++++
	//FirstAgeOfEsper
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.EraClass FirstAgeOfEsper = new CoreValues.EraClass();
		FirstAgeOfEsper.eraIndex = CharacterClass.Era.FirstAgeOfEsper;
		FirstAgeOfEsper.eraStartYear = 2312;
		FirstAgeOfEsper.eraEndYear = 2312+562;
		FirstAgeOfEsper.availableRacesInEra = postMagusdawnRaceList;
		FirstAgeOfEsper.availableGiftsInEra = esperGiftList;

		//Commit to class
		eraDefinition.Add(CharacterClass.Era.FirstAgeOfEsper, FirstAgeOfEsper);		

	//++++++++++++++++++++++++++++++++++++++++++
	//SecondAgeOfEsper
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.EraClass SecondAgeOfEsper = new CoreValues.EraClass();
		SecondAgeOfEsper.eraIndex = CharacterClass.Era.SecondAgeOfEsper;
		SecondAgeOfEsper.eraStartYear = 2312+562;
		SecondAgeOfEsper.eraEndYear = 2312+700;
		SecondAgeOfEsper.availableRacesInEra = postMagusdawnRaceList;
		SecondAgeOfEsper.availableGiftsInEra = esperGiftList;

		//Commit to class
		eraDefinition.Add(CharacterClass.Era.SecondAgeOfEsper, SecondAgeOfEsper);	

	//++++++++++++++++++++++++++++++++++++++++++
	//ThirdAgeOfEsper
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.EraClass ThirdAgeOfEsper = new CoreValues.EraClass();
		ThirdAgeOfEsper.eraIndex = CharacterClass.Era.ThirdAgeOfEsper;
		ThirdAgeOfEsper.eraStartYear = 2312+700;
		ThirdAgeOfEsper.eraEndYear = 2312+973;
		ThirdAgeOfEsper.availableRacesInEra = postMagusdawnRaceList;
		ThirdAgeOfEsper.availableGiftsInEra = esperGiftList;

		//Commit to class
		eraDefinition.Add(CharacterClass.Era.ThirdAgeOfEsper, ThirdAgeOfEsper);		

	//++++++++++++++++++++++++++++++++++++++++++
	//SynsteelAge
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.EraClass SynsteelAge = new CoreValues.EraClass();
		SynsteelAge.eraIndex = CharacterClass.Era.SynsteelAge;
		SynsteelAge.eraStartYear = 2312+973;
		SynsteelAge.eraEndYear = 2312+1217;
		SynsteelAge.availableRacesInEra = postMagusdawnRaceList;
		SynsteelAge.availableGiftsInEra = allGiftsList;

		//Commit to class
		eraDefinition.Add(CharacterClass.Era.SynsteelAge, SynsteelAge);		

	//++++++++++++++++++++++++++++++++++++++++++
	//AgeOfUpheaval
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.EraClass AgeOfUpheaval = new CoreValues.EraClass();
		AgeOfUpheaval.eraIndex = CharacterClass.Era.AgeOfUpheaval;
		AgeOfUpheaval.eraStartYear = 2312+1217;
		AgeOfUpheaval.eraEndYear = 2312+2312;
		AgeOfUpheaval.availableRacesInEra = postMagusdawnRaceList;
		AgeOfUpheaval.availableGiftsInEra = allGiftsList;

		//Commit to class
		eraDefinition.Add(CharacterClass.Era.AgeOfUpheaval, AgeOfUpheaval);		

	//++++++++++++++++++++++++++++++++++++++++++
	//Astralnight
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.EraClass Astralnight = new CoreValues.EraClass();
		Astralnight.eraIndex = CharacterClass.Era.Astralnight;
		Astralnight.eraStartYear = 2312+2312;
		Astralnight.eraEndYear = 9999;
		Astralnight.availableRacesInEra = postMagusdawnRaceList;
		Astralnight.availableGiftsInEra = allGiftsList;

		//Commit to class
		eraDefinition.Add(CharacterClass.Era.Astralnight, Astralnight);
	}
}
