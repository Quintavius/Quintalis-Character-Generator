using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour {
	//Reference slots
	private Character character;
	private NameManager nameManager;
	
	//Dictionaries containing all expanded information based on enum lookups. 
	//Works by doing Dictionary<[Enum Reference],[Class container that has all the deetz]>
	public Dictionary<CharacterClass.Era,CoreValues.EraClass> eraDefinition;
		//Contains era start & end, available speciess and gifts per era.
	public Dictionary<CharacterClass.Species,CoreValues.SpeciesClass> speciesDefinition;
		//Contains age ranges and stat dice.


//===========================================================================
	//Public generation settings, changed by user input. These are overrides, basically.

	public string characterName;
	public int age;
	public int currentYear;
	public CharacterClass.PowerLevel powerLevel;
	public CharacterClass.Era era;
	public CharacterClass.Species species;
	public CharacterClass.Gift gift;
	public CharacterClass.AgeGroup ageGroup;

//===========================================================================
	//References

	private void Start(){
		character = GetComponent<Character>();
		nameManager = GetComponent<NameManager>();
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
		//Species
		//
		//Now that we know the current era, we can pick a species from the pool of the ones available, if species is set to random
		if (species == CharacterClass.Species.Random){
			//Load up all the predefined available speciess based on the era we selected above.
			List<CharacterClass.Species> availableSpeciesinSelectedEra = eraDefinition[character.era].availableSpeciesInEra;
			character.species = availableSpeciesinSelectedEra[Random.Range(0,availableSpeciesinSelectedEra.Count)];
		}else{
			//Nevermind, it's set. Just pass it on.
			character.species = species;
		}


		//++++++++++++++++++++
		//STEP 3
		//
		//Stats
		//
		//Knowing the species, we can now use the stats recipes to stat up this character according to selected power level
		RollStats(character.species);


		//++++++++++++++++++++
		//STEP 4
		//
		//Age
		//
		//We know current year and species. Let's chuck up a random age class, generate an age within that and subtract it from current year to get YoB.
		if (age == 0){											//Age isn't specified at all. Is age group specified?
			if (ageGroup == CharacterClass.AgeGroup.Random){	//Age group is random, pick one of those first
				character.ageGroup = (CharacterClass.AgeGroup)Random.Range(1,System.Enum.GetValues(typeof(CharacterClass.AgeGroup)).Length);
			}else{												//Age group is set. Just pass it on.
				character.ageGroup = ageGroup;
			}
			//Now we have our age group, time to slam out an age inside that age group for the chosen species.
			character.age = GetAgeFromAgeGroup(character.ageGroup,character.species);
		}else{													//Age is already defined, just assign it and match it with an age class.
		character.age = age;
		character.ageGroup = GetAgeGroupFromAge(character.age, character.species);	
		}
		//Almost done, now get year of birth
		character.yearOfBirth = character.currentYear - character.age;


		//++++++++++++++++++++
		//STEP 5
		//
		//Name
		//
		//Always nice to have a name. This one's easy.
		if (characterName == ""){
			character.characterName = nameManager.GenerateName(character.species);
		}else{
			character.characterName = characterName;
		}


		//++++++++++++++++++++
		//STEP 6
		//
		//Gifts
		//
		//Same deal as choosing species, compare to era and pick a gift at random. This one however is weighted towards no gift based on powerlevel.



		//++++++++++++++++++++
		//STEP 6.1
		//
		//Gift details
		//
		//If any gifts have been given out, find out more about them.



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


	//Some age tools
	int GetAgeFromAgeGroup(CharacterClass.AgeGroup ageGroupToProcess, CharacterClass.Species speciesToAge){
		int ageToReturn;
		switch (ageGroupToProcess){
			case CharacterClass.AgeGroup.Adolescent: ageToReturn = Random.Range((int)speciesDefinition[speciesToAge].ageRange_Adolescent.x,(int)speciesDefinition[speciesToAge].ageRange_Adolescent.y); break;
			case CharacterClass.AgeGroup.YoungAdult: ageToReturn = Random.Range((int)speciesDefinition[speciesToAge].ageRange_YoungAdult.x,(int)speciesDefinition[speciesToAge].ageRange_YoungAdult.y); break;
			case CharacterClass.AgeGroup.MiddleAge: ageToReturn = Random.Range((int)speciesDefinition[speciesToAge].ageRange_MiddleAge.x,(int)speciesDefinition[speciesToAge].ageRange_MiddleAge.y); break;
			case CharacterClass.AgeGroup.Old: ageToReturn = Random.Range((int)speciesDefinition[speciesToAge].ageRange_Old.x,(int)speciesDefinition[speciesToAge].ageRange_Old.y); break;
			case CharacterClass.AgeGroup.Ancient: ageToReturn = Random.Range((int)speciesDefinition[speciesToAge].ageRange_Ancient.x,(int)speciesDefinition[speciesToAge].ageRange_Ancient.y); break;
			default: ageToReturn = 0; break;
		}
		return ageToReturn;
	}

	CharacterClass.AgeGroup GetAgeGroupFromAge(int agetoProcess, CharacterClass.Species speciesToAge){
		CharacterClass.AgeGroup ageGroupToReturn = CharacterClass.AgeGroup.Adolescent;
		var s = speciesDefinition[speciesToAge];
		if (agetoProcess >= s.ageRange_Adolescent.x && agetoProcess < s.ageRange_Adolescent.y) {ageGroupToReturn = CharacterClass.AgeGroup.Adolescent;}
		else if (agetoProcess >= s.ageRange_YoungAdult.x && agetoProcess < s.ageRange_YoungAdult.y) {ageGroupToReturn = CharacterClass.AgeGroup.YoungAdult;}
		else if (agetoProcess >= s.ageRange_MiddleAge.x && agetoProcess < s.ageRange_MiddleAge.y) {ageGroupToReturn = CharacterClass.AgeGroup.MiddleAge;}
		else if (agetoProcess >= s.ageRange_Old.x && agetoProcess < s.ageRange_Old.y) {ageGroupToReturn = CharacterClass.AgeGroup.Old;}
		else if (agetoProcess >= s.ageRange_Ancient.x && agetoProcess < s.ageRange_Ancient.y) {ageGroupToReturn = CharacterClass.AgeGroup.Ancient;}
		return ageGroupToReturn;
	}

	void RollStats(CharacterClass.Species speciesToRollFor){
		//Generate stats for any species depending on powerlevel set. This will only work locally inside this script and only has character generation use!
		var tempBrains = 0;
		var tempBrawn = 0;
		var tempSkin = 0;
		var tempTongue = 0;
		int tempPowerLevel;
		if (powerLevel == 0){
			//Looks like bullshit. Count entries in powerlevel enum, generate random then bump it by 1 to include max and exclude "random".
			tempPowerLevel = Random.Range(1, System.Enum.GetValues(typeof(CharacterClass.PowerLevel)).Length);
		}else{
			tempPowerLevel = (int)powerLevel;
		}
		//iterate through each power level granted. Each time roll a dice between 1 and dice size of that species.
		for (int i = 0; i < tempPowerLevel; i++){
			tempBrains += Random.Range(0,speciesDefinition[speciesToRollFor].dieSize_Brains) + 1;
			tempBrawn += Random.Range(0,speciesDefinition[speciesToRollFor].dieSize_Brawn) + 1;
			tempSkin += Random.Range(0,speciesDefinition[speciesToRollFor].dieSize_Skin) + 1;
			tempTongue += Random.Range(0,speciesDefinition[speciesToRollFor].dieSize_Tongue) + 1;
		}
		//finally, apply and send the form off.
		character.brains = tempBrains;
		character.brawn = tempBrawn;	
		character.skin = tempSkin;	
		character.tongue = tempTongue;			
	}



//===========================================================================
	//Dictionary definition setup

	void PopulateDictionaries(){

		//Era dicitionary
		eraDefinition = new Dictionary<CharacterClass.Era,CoreValues.EraClass>();
		speciesDefinition = new Dictionary<CharacterClass.Species,CoreValues.SpeciesClass>();

		//Prebuild species libraries
		List<CharacterClass.Species> preMagusdawnSpeciesList = new List<CharacterClass.Species>();
		preMagusdawnSpeciesList.Add(CharacterClass.Species.Askadur);
		preMagusdawnSpeciesList.Add(CharacterClass.Species.Faeryn);
		preMagusdawnSpeciesList.Add(CharacterClass.Species.Kanina);
		preMagusdawnSpeciesList.Add(CharacterClass.Species.Lifindur);
		preMagusdawnSpeciesList.Add(CharacterClass.Species.Madur);
		preMagusdawnSpeciesList.Add(CharacterClass.Species.Nyrn);
		preMagusdawnSpeciesList.Add(CharacterClass.Species.Skjomadur);
		preMagusdawnSpeciesList.Add(CharacterClass.Species.Troll);
		preMagusdawnSpeciesList.Add(CharacterClass.Species.Vidur);
		preMagusdawnSpeciesList.Add(CharacterClass.Species.UrminnAdult);
		preMagusdawnSpeciesList.Add(CharacterClass.Species.UrminnYoung);

		List<CharacterClass.Species> postMagusdawnSpeciesList = new List<CharacterClass.Species>();
		postMagusdawnSpeciesList.Add(CharacterClass.Species.Askadur);
		postMagusdawnSpeciesList.Add(CharacterClass.Species.Faeryn);
		postMagusdawnSpeciesList.Add(CharacterClass.Species.Kanina);
		postMagusdawnSpeciesList.Add(CharacterClass.Species.Draugur);
		postMagusdawnSpeciesList.Add(CharacterClass.Species.Madur);
		postMagusdawnSpeciesList.Add(CharacterClass.Species.Nyrn);
		postMagusdawnSpeciesList.Add(CharacterClass.Species.Skjomadur);
		postMagusdawnSpeciesList.Add(CharacterClass.Species.Troll);
		postMagusdawnSpeciesList.Add(CharacterClass.Species.Vidur);
		postMagusdawnSpeciesList.Add(CharacterClass.Species.UrminnAdult);
		postMagusdawnSpeciesList.Add(CharacterClass.Species.UrminnYoung);

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
		AstralEra.availableSpeciesInEra = preMagusdawnSpeciesList;
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
		Magusdawn.availableSpeciesInEra = preMagusdawnSpeciesList;
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
		FirstAgeOfEsper.availableSpeciesInEra = postMagusdawnSpeciesList;
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
		SecondAgeOfEsper.availableSpeciesInEra = postMagusdawnSpeciesList;
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
		ThirdAgeOfEsper.availableSpeciesInEra = postMagusdawnSpeciesList;
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
		SynsteelAge.availableSpeciesInEra = postMagusdawnSpeciesList;
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
		AgeOfUpheaval.availableSpeciesInEra = postMagusdawnSpeciesList;
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
		Astralnight.availableSpeciesInEra = postMagusdawnSpeciesList;
		Astralnight.availableGiftsInEra = allGiftsList;

		//Commit to class
		eraDefinition.Add(CharacterClass.Era.Astralnight, Astralnight);



//=======================================================
//||||||||||||||||||RACES||||||||||||||||||||||
//=======================================================


	//++++++++++++++++++++++++++++++++++++++++++
	//Askadur
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.SpeciesClass Askadur = new CoreValues.SpeciesClass();
		Askadur.speciesIndex = CharacterClass.Species.Askadur;
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
		speciesDefinition.Add(CharacterClass.Species.Askadur, Askadur);
		
	//++++++++++++++++++++++++++++++++++++++++++
	//Draugur
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.SpeciesClass Draugur = new CoreValues.SpeciesClass();
		Draugur.speciesIndex = CharacterClass.Species.Draugur;
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
		speciesDefinition.Add(CharacterClass.Species.Draugur, Draugur);
				
	//++++++++++++++++++++++++++++++++++++++++++
	//Faeryn
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.SpeciesClass Faeryn = new CoreValues.SpeciesClass();
		Faeryn.speciesIndex = CharacterClass.Species.Faeryn;
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
		speciesDefinition.Add(CharacterClass.Species.Faeryn, Faeryn);

	//++++++++++++++++++++++++++++++++++++++++++
	//Kanina
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.SpeciesClass Kanina = new CoreValues.SpeciesClass();
		Kanina.speciesIndex = CharacterClass.Species.Kanina;
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
		speciesDefinition.Add(CharacterClass.Species.Kanina, Kanina);

	//++++++++++++++++++++++++++++++++++++++++++
	//Madur
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.SpeciesClass Madur = new CoreValues.SpeciesClass();
		Madur.speciesIndex = CharacterClass.Species.Madur;
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
		speciesDefinition.Add(CharacterClass.Species.Madur, Madur);
		
	//++++++++++++++++++++++++++++++++++++++++++
	//Nyrn
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.SpeciesClass Nyrn = new CoreValues.SpeciesClass();
		Nyrn.speciesIndex = CharacterClass.Species.Nyrn;
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
		speciesDefinition.Add(CharacterClass.Species.Nyrn, Nyrn);
				
	//++++++++++++++++++++++++++++++++++++++++++
	//Skjomadur
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.SpeciesClass Skjomadur = new CoreValues.SpeciesClass();
		Skjomadur.speciesIndex = CharacterClass.Species.Skjomadur;
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
		speciesDefinition.Add(CharacterClass.Species.Skjomadur, Skjomadur);
						
	//++++++++++++++++++++++++++++++++++++++++++
	//Troll
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.SpeciesClass Troll = new CoreValues.SpeciesClass();
		Troll.speciesIndex = CharacterClass.Species.Troll;
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
		speciesDefinition.Add(CharacterClass.Species.Troll, Troll);
								
	//++++++++++++++++++++++++++++++++++++++++++
	//Vidur
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.SpeciesClass Vidur = new CoreValues.SpeciesClass();
		Vidur.speciesIndex = CharacterClass.Species.Vidur;
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
		speciesDefinition.Add(CharacterClass.Species.Vidur, Vidur);
										
	//++++++++++++++++++++++++++++++++++++++++++
	//Lifindur
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.SpeciesClass Lifindur = new CoreValues.SpeciesClass();
		Vidur.speciesIndex = CharacterClass.Species.Lifindur;
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
		speciesDefinition.Add(CharacterClass.Species.Lifindur, Lifindur);
												
	//++++++++++++++++++++++++++++++++++++++++++
	//UrminnYoung
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.SpeciesClass UrminnYoung = new CoreValues.SpeciesClass();
		UrminnYoung.speciesIndex = CharacterClass.Species.UrminnYoung;
		//Dice
		UrminnYoung.dieSize_Brains = 4;
		UrminnYoung.dieSize_Brawn = 2;
		UrminnYoung.dieSize_Skin = 1;
		UrminnYoung.dieSize_Tongue = 4;
		//Ages
		UrminnYoung.ageRange_Adolescent = new Vector2(15,24);
		UrminnYoung.ageRange_YoungAdult = new Vector2(24,40);
		UrminnYoung.ageRange_MiddleAge = new Vector2(40,60);
		UrminnYoung.ageRange_Old = new Vector2(60,85);
		UrminnYoung.ageRange_Ancient = new Vector2(85,125);


		//Commit to class
		speciesDefinition.Add(CharacterClass.Species.UrminnYoung, UrminnYoung);
														
	//++++++++++++++++++++++++++++++++++++++++++
	//UrminnAdult
	//++++++++++++++++++++++++++++++++++++++++++

		CoreValues.SpeciesClass UrminnAdult = new CoreValues.SpeciesClass();
		UrminnAdult.speciesIndex = CharacterClass.Species.UrminnAdult;
		//Dice
		UrminnAdult.dieSize_Brains = 1;
		UrminnAdult.dieSize_Brawn = 6;
		UrminnAdult.dieSize_Skin = 3;
		UrminnAdult.dieSize_Tongue = 3;
		//Ages
		UrminnAdult.ageRange_Adolescent = new Vector2(15,24);
		UrminnAdult.ageRange_YoungAdult = new Vector2(24,40);
		UrminnAdult.ageRange_MiddleAge = new Vector2(40,60);
		UrminnAdult.ageRange_Old = new Vector2(60,85);
		UrminnAdult.ageRange_Ancient = new Vector2(85,125);


		//Commit to class
		speciesDefinition.Add(CharacterClass.Species.UrminnAdult, UrminnAdult);
	}
}
