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
	public Dictionary<CharacterClass.Race,CoreValues.RaceClass> raceDefinition;
		//Contains age ranges and stat dice.


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
			character.currentYear = GetYearFromEra(character.era);
			
		}else{
			//Year is specified, match it with an era and pass it to the character
			character.currentYear = currentYear;
			character.era = GetEraFromYear(currentYear);
		}


		//++++++++++++++++++++
		//STEP 2
		//
		//Race
		//
		//Now that we know the current era, we can pick a race from the pool of the ones available, if race is set to random
		if (race == CharacterClass.Race.Random){
			//Load up all the predefined available races based on the era we selected above.
			List<CharacterClass.Race> availableRacesinSelectedEra = eraDefinition[character.era].availableRacesInEra;
			character.race = availableRacesinSelectedEra[Random.Range(1,availableRacesinSelectedEra.Count)];
		}else{
			//Nevermind, it's set. Just pass it on.
			character.race = race;
		}


		//++++++++++++++++++++
		//STEP 3
		//
		//Stats
		//
		//Knowing the race, we can now use the stats recipes to stat up this character according to selected power level
		


		//++++++++++++++++++++
		//STEP 4
		//
		//Name
		//
		//Always nice to have a name.



		//++++++++++++++++++++
		//STEP 5
		//
		//Gifts
		//
		//Same deal as choosing race, compare to era and pick a gift at random. This one however is weighted towards no gift based on powerlevel.



		//++++++++++++++++++++
		//STEP 5.1
		//
		//Gift details
		//
		//If any gifts have been given out, find out more about them.



		//++++++++++++++++++++
		//STEP 6
		//
		//Age
		//
		//We know current year and race. Let's chuck up a random age class, generate an age within that and subtract it from current year to get YoB.
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
		raceDefinition = new Dictionary<CharacterClass.Race,CoreValues.RaceClass>();

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



//=======================================================
//||||||||||||||||||RACES||||||||||||||||||||||
//=======================================================


	//++++++++++++++++++++++++++++++++++++++++++
	//Askadur
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.RaceClass Askadur = new CoreValues.RaceClass();
		Askadur.raceIndex = CharacterClass.Race.Askadur;
		//Dice
		Askadur.dieSize_Brains = 3;
		Askadur.dieSize_Brawn = 4;
		Askadur.dieSize_Skin = 4;
		Askadur.dieSize_Tongue = 1;
		//Ages
		Askadur.ageRange_Adolescent = new Vector2(30,60);
		Askadur.ageRange_YoungAdult = new Vector2(60,100);
		Askadur.ageRange_MiddleAge = new Vector2(100,150);
		Askadur.ageRange_Old = new Vector2(150,175);
		Askadur.ageRange_Ancient = new Vector2(175,275);


		//Commit to class
		raceDefinition.Add(CharacterClass.Race.Askadur, Askadur);
		
	//++++++++++++++++++++++++++++++++++++++++++
	//Draugur
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.RaceClass Draugur = new CoreValues.RaceClass();
		Draugur.raceIndex = CharacterClass.Race.Draugur;
		//Dice
		Draugur.dieSize_Brains = 4;
		Draugur.dieSize_Brawn = 2;
		Draugur.dieSize_Skin = 3;
		Draugur.dieSize_Tongue = 1;
		//Ages
		Draugur.ageRange_Adolescent = new Vector2(100,250);
		Draugur.ageRange_YoungAdult = new Vector2(250,430);
		Draugur.ageRange_MiddleAge = new Vector2(430,750);
		Draugur.ageRange_Old = new Vector2(750,1200);
		Draugur.ageRange_Ancient = new Vector2(1200,11200);


		//Commit to class
		raceDefinition.Add(CharacterClass.Race.Draugur, Draugur);
				
	//++++++++++++++++++++++++++++++++++++++++++
	//Faeryn
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.RaceClass Faeryn = new CoreValues.RaceClass();
		Faeryn.raceIndex = CharacterClass.Race.Faeryn;
		//Dice
		Faeryn.dieSize_Brains = 3;
		Faeryn.dieSize_Brawn = 2;
		Faeryn.dieSize_Skin = 2;
		Faeryn.dieSize_Tongue = 4;
		//Ages
		Faeryn.ageRange_Adolescent = new Vector2(8,14);
		Faeryn.ageRange_YoungAdult = new Vector2(14,22);
		Faeryn.ageRange_MiddleAge = new Vector2(22,31);
		Faeryn.ageRange_Old = new Vector2(31,45);
		Faeryn.ageRange_Ancient = new Vector2(45,77);


		//Commit to class
		raceDefinition.Add(CharacterClass.Race.Faeryn, Faeryn);

	//++++++++++++++++++++++++++++++++++++++++++
	//Kanina
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.RaceClass Kanina = new CoreValues.Kanina();
		Kanina.raceIndex = CharacterClass.Race.Kanina;
		//Dice
		Kanina.dieSize_Brains = 1;
		Kanina.dieSize_Brawn = 3;
		Kanina.dieSize_Skin = 2;
		Kanina.dieSize_Tongue = 6;
		//Ages
		Kanina.ageRange_Adolescent = new Vector2(15,21);
		Kanina.ageRange_YoungAdult = new Vector2(21,55);
		Kanina.ageRange_MiddleAge = new Vector2(55,80);
		Kanina.ageRange_Old = new Vector2(80,105);
		Kanina.ageRange_Ancient = new Vector2(105,185);


		//Commit to class
		raceDefinition.Add(CharacterClass.Race.Kanina, Kanina);

	//++++++++++++++++++++++++++++++++++++++++++
	//Madur
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.RaceClass Madur = new CoreValues.Madur();
		Madur.raceIndex = CharacterClass.Race.Madur;
		//Dice
		Madur.dieSize_Brains = 4;
		Madur.dieSize_Brawn = 2;
		Madur.dieSize_Skin = 2;
		Madur.dieSize_Tongue = 4;
		//Ages
		Madur.ageRange_Adolescent = new Vector2(12,18);
		Madur.ageRange_YoungAdult = new Vector2(18,30);
		Madur.ageRange_MiddleAge = new Vector2(30,50);
		Madur.ageRange_Old = new Vector2(50,70);
		Madur.ageRange_Ancient = new Vector2(70,110);


		//Commit to class
		raceDefinition.Add(CharacterClass.Race.Madur, Madur);
		
	//++++++++++++++++++++++++++++++++++++++++++
	//Nyrn
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.RaceClass Nyrn = new CoreValues.Nyrn();
		Nyrn.raceIndex = CharacterClass.Race.Nyrn;
		//Dice
		Nyrn.dieSize_Brains = 6;
		Nyrn.dieSize_Brawn = 2;
		Nyrn.dieSize_Skin = 2;
		Nyrn.dieSize_Tongue = 2;
		//Ages
		Nyrn.ageRange_Adolescent = new Vector2(30,45);
		Nyrn.ageRange_YoungAdult = new Vector2(45,75);
		Nyrn.ageRange_MiddleAge = new Vector2(75,100);
		Nyrn.ageRange_Old = new Vector2(101,130);
		Nyrn.ageRange_Ancient = new Vector2(130,230);


		//Commit to class
		raceDefinition.Add(CharacterClass.Race.Nyrn, Nyrn);
				
	//++++++++++++++++++++++++++++++++++++++++++
	//Skjomadur
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.RaceClass Skjomadur = new CoreValues.Skjomadur();
		Skjomadur.raceIndex = CharacterClass.Race.Skjomadur;
		//Dice
		Skjomadur.dieSize_Brains = 3;
		Skjomadur.dieSize_Brawn = 3;
		Skjomadur.dieSize_Skin = 2;
		Skjomadur.dieSize_Tongue = 4;
		//Ages
		Skjomadur.ageRange_Adolescent = new Vector2(10,16);
		Skjomadur.ageRange_YoungAdult = new Vector2(16,25);
		Skjomadur.ageRange_MiddleAge = new Vector2(25,40);
		Skjomadur.ageRange_Old = new Vector2(40,65);
		Skjomadur.ageRange_Ancient = new Vector2(65,89);


		//Commit to class
		raceDefinition.Add(CharacterClass.Race.Skjomadur, Skjomadur);
						
	//++++++++++++++++++++++++++++++++++++++++++
	//Troll
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.RaceClass Troll = new CoreValues.Troll();
		Troll.raceIndex = CharacterClass.Race.Troll;
		//Dice
		Troll.dieSize_Brains = 1;
		Troll.dieSize_Brawn = 4;
		Troll.dieSize_Skin = 6;
		Troll.dieSize_Tongue = 1;
		//Ages
		Troll.ageRange_Adolescent = new Vector2(20,30);
		Troll.ageRange_YoungAdult = new Vector2(30,45);
		Troll.ageRange_MiddleAge = new Vector2(45,60);
		Troll.ageRange_Old = new Vector2(60,90);
		Troll.ageRange_Ancient = new Vector2(90,170);


		//Commit to class
		raceDefinition.Add(CharacterClass.Race.Troll, Troll);
								
	//++++++++++++++++++++++++++++++++++++++++++
	//Vidur
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.RaceClass Vidur = new CoreValues.Vidur();
		Vidur.raceIndex = CharacterClass.Race.Vidur;
		//Dice
		Vidur.dieSize_Brains = 4;
		Vidur.dieSize_Brawn = 1;
		Vidur.dieSize_Skin = 4;
		Vidur.dieSize_Tongue = 2;
		//Ages
		Vidur.ageRange_Adolescent = new Vector2(5,20);
		Vidur.ageRange_YoungAdult = new Vector2(20,50);
		Vidur.ageRange_MiddleAge = new Vector2(50,85);
		Vidur.ageRange_Old = new Vector2(85,120);
		Vidur.ageRange_Ancient = new Vector2(120,1120);


		//Commit to class
		raceDefinition.Add(CharacterClass.Race.Vidur, Vidur);
										
	//++++++++++++++++++++++++++++++++++++++++++
	//Lifindur
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.RaceClass Lifindur = new CoreValues.Lifindur();
		Vidur.raceIndex = CharacterClass.Race.Lifindur;
		//Dice
		Lifindur.dieSize_Brains = 6;
		Lifindur.dieSize_Brawn = 1;
		Lifindur.dieSize_Skin = 2;
		Lifindur.dieSize_Tongue = 4;
		//Ages
		Lifindur.ageRange_Adolescent = new Vector2(1,2);
		Lifindur.ageRange_YoungAdult = new Vector2(2,28);
		Lifindur.ageRange_MiddleAge = new Vector2(28,60);
		Lifindur.ageRange_Old = new Vector2(60,120);
		Lifindur.ageRange_Ancient = new Vector2(120,190);


		//Commit to class
		raceDefinition.Add(CharacterClass.Race.Lifindur, Lifindur);
												
	//++++++++++++++++++++++++++++++++++++++++++
	//UrminnYoung
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.RaceClass UrminnYoung = new CoreValues.UrminnYoung();
		UrminnYoung.raceIndex = CharacterClass.Race.UrminnYoung;
		//Dice
		UrminnYoung.dieSize_Brains = 6;
		Lifindur.dieSize_Brawn = 1;
		Lifindur.dieSize_Skin = 2;
		Lifindur.dieSize_Tongue = 4;
		//Ages
		Lifindur.ageRange_Adolescent = new Vector2(1,2);
		Lifindur.ageRange_YoungAdult = new Vector2(2,28);
		Lifindur.ageRange_MiddleAge = new Vector2(28,60);
		Lifindur.ageRange_Old = new Vector2(60,120);
		Lifindur.ageRange_Ancient = new Vector2(120,190);


		//Commit to class
		raceDefinition.Add(CharacterClass.Race.Lifindur, Lifindur);
	}
}
