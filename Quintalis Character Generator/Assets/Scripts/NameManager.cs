using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameManager : MonoBehaviour {

	public TextAsset vidurNameParts;
	public TextAsset nyrnNameParts;
	public TextAsset trollLetters_Consonant;
	public TextAsset trollLetters_Vowel;
	public TextAsset trollLetters_Double;
	public TextAsset askadurNameParts;
	public TextAsset faerynNameParts;
	public TextAsset faerynKarwhenSyls;
	public TextAsset faerynFaergoSyls;
	public TextAsset kaninaSimpleNames;
	public TextAsset kaninaFront;
	public TextAsset kaninaBack;
	public TextAsset madurFirst;
	public TextAsset madurLast;
	public TextAsset skjomadurFirstNames;
	public TextAsset skjomadurLastNames;
	public TextAsset urminnAdultNames;
	public TextAsset urminnAdultVowels;
	public TextAsset urminnYoungNames;
	public TextAsset urminnYoungVowels;
	public TextAsset lifindurSyls;
	public TextAsset draugurSyls;
	public TextAsset draugurEra;

	string[] vidurNameParts_List;
	string[] nyrnNameParts_List;
	string[] trollLetters_Consonant_List;
	string[] trollLetters_Vowel_List;
	string[] trollLetters_Double_List;
	string[] askadurNameParts_List;
	string[] faerynNameParts_List;
	string[] faerynKarwhenSyls_List;
	string[] faerynFaergoSyls_List;
	string[] kaninaSimple_List;
	string[] kaninaFront_List;
	string[] kaninaBack_List;
	string[] madurFirst_List;
	string[] madurLast_List;
	string[] skjomadurFirst_List;
	string[] skjomadurLast_List;
	string[] urminnAdultNames_List;
	string[] urminnAdultVowels_List;
	string[] urminnYoungNames_List;
	string[] urminnYoungVowels_List;
	string[] lifindurSyls_List;
	string[] draugurSyls_List;
	string[] draugurEra_List;

	string firstName;
	string lastName;
	string tempName;

	Character character;

	void Start (){
		character = GetComponent<Character>();
		InitializeNames();
	}

//===========================================================================
	//Main name generation function

	public string GenerateName (CharacterClass.Species raceToName) {
		ClearNames();
		switch (raceToName){
			case CharacterClass.Species.Askadur:
				GenerateAskadurName();
				break;
			case CharacterClass.Species.Draugur:
				GenerateDraugurName();;
				break;
			case CharacterClass.Species.Faeryn:
				GenerateFaerynName();
				break;
			case CharacterClass.Species.Kanina:
				GenerateKaninaName();
				break;
			case CharacterClass.Species.Lifindur:
				GenerateLifindurName();
				break;
			case CharacterClass.Species.Madur:
				GenerateMadurName();
				break;
			case CharacterClass.Species.Nyrn:
				GenerateNyrnName();
				break;
			case CharacterClass.Species.Skjomadur:				
				GenerateSkjomadurName();
				break;
			case CharacterClass.Species.Troll:
				GenerateTrollName();
				break;
			case CharacterClass.Species.UrminnAdult:
				GenerateUrminnName(false);
				break;
			case CharacterClass.Species.UrminnYoung:
				GenerateUrminnName(true);
				break;
			case CharacterClass.Species.Vidur:
				GenerateVidurName();
				break;
			default:
				tempName = "error";
			//Species doesn't match
				break;
		}
		tempName = char.ToUpper(tempName[0]) + tempName.Substring(1);
		return tempName;
	}

//===========================================================================
	//Name methods
	void GenerateDraugurName(){
		//First name
		int nameLength = Random.Range(1,3);
		for (var i = 0; i <= nameLength; i++){
			firstName += draugurSyls_List[Random.Range(0,draugurSyls_List.Length)];
		}

		//Last name
		lastName = draugurEra_List[Random.Range(0,draugurEra_List.Length)];
		lastName += draugurEra_List[Random.Range(0,draugurEra_List.Length)];

		//Combine names
		firstName = char.ToUpper(firstName[0]) + firstName.Substring(1);
		lastName = char.ToUpper(lastName[0]) + lastName.Substring(1);
		tempName = firstName + " of " + lastName;

	}
	void GenerateAskadurName(){
		tempName = askadurNameParts_List[Random.Range(0,askadurNameParts_List.Length)];
		tempName += askadurNameParts_List[Random.Range(0,askadurNameParts_List.Length)];
		tempName += askadurNameParts_List[Random.Range(0,askadurNameParts_List.Length)];
	}
	void GenerateFaerynName(){
	//50 50 chance between cultures
		if (Random.value >= 0.5){
		//Drifters (Faergo)
			int nameLength = Random.Range(1,4);
			for (var i = 0; i <= nameLength; i++){
				firstName += faerynFaergoSyls_List[Random.Range(0,faerynFaergoSyls_List.Length)];
			}
			firstName = char.ToUpper(firstName[0]) + firstName.Substring(1);

			if (Random.value >= 0.68){
			//Composite last name
				lastName += faerynNameParts_List[Random.Range(0,faerynNameParts_List.Length)];
				lastName += faerynNameParts_List[Random.Range(0,faerynNameParts_List.Length)];
			}else{
			//Syllabary last name
				nameLength = Random.Range(1,4);
				for (var i = 0; i <= nameLength; i++){
					lastName += faerynFaergoSyls_List[Random.Range(0,faerynFaergoSyls_List.Length)];
				}
			}
			lastName = char.ToUpper(lastName[0]) + lastName.Substring(1);
			tempName = firstName + '\u00A0' + lastName;
		}else{
		//Forbidden (Karwhen)
			int nameLength = Random.Range(1,3);
			for (var i = 0; i <= nameLength; i++){
				firstName += faerynKarwhenSyls_List[Random.Range(0,faerynKarwhenSyls_List.Length)];
			}
			firstName = char.ToUpper(firstName[0]) + firstName.Substring(1);		
			nameLength = Random.Range(1,3);
			for (var i = 0; i <= nameLength; i++){
				lastName += faerynKarwhenSyls_List[Random.Range(0,faerynKarwhenSyls_List.Length)];
			}
			lastName = char.ToUpper(lastName[0]) + lastName.Substring(1);
			tempName = firstName + '\u00A0' + lastName;
		}
	}
	void GenerateKaninaName(){
	//First name
		if (Random.value >= 0.4){
			//Long name
			firstName += kaninaFront_List[Random.Range(0,kaninaFront_List.Length)];
			firstName += kaninaBack_List[Random.Range(0,kaninaBack_List.Length)];
		}else{
			//Short name
			firstName = kaninaSimple_List[Random.Range(0,kaninaSimple_List.Length)];
		}
	//Last name
		if (Random.value >= 0.25){
		//Long name
			lastName += kaninaFront_List[Random.Range(0,kaninaFront_List.Length)];
			lastName += kaninaBack_List[Random.Range(0,kaninaBack_List.Length)];
		}else{
		//Short name
			lastName = kaninaSimple_List[Random.Range(0,kaninaSimple_List.Length)];
		}
		firstName = char.ToUpper(firstName[0]) + firstName.Substring(1);
		lastName = char.ToUpper(lastName[0]) + lastName.Substring(1);
		tempName = firstName + '\u00A0' + lastName;
	}
	void GenerateMadurName(){
		firstName = madurFirst_List[Random.Range(0,madurFirst_List.Length)];
		lastName = madurLast_List[Random.Range(0,madurLast_List.Length)];
		tempName = firstName + '\u00A0' + lastName;
	}
	void GenerateNyrnName(){
		int syllablesInNyrnsName = Mathf.RoundToInt(character.age / 5);
		for (var i = 0; i < syllablesInNyrnsName; i++){
			tempName += nyrnNameParts_List[Random.Range(0,nyrnNameParts_List.Length)];
		}
	}
	void GenerateSkjomadurName(){
		//First name
		if (Random.value <= 0.8){
		//80% chance of skjom name vs random name
		//Give us a skjom name
			firstName = skjomadurFirst_List[Random.Range(0,skjomadurFirst_List.Length)];
		}else{
		//Random race name
			int randomRaceName = Random.Range(0,5);
			
			if (randomRaceName == 0){GenerateTrollName(); firstName = tempName;}
			if (randomRaceName == 1){GenerateAskadurName(); firstName = tempName;}
			if (randomRaceName == 2){firstName = madurLast_List[Random.Range(0,madurLast_List.Length)];}
			if (randomRaceName == 3){
				if (Random.value >= 0.4){
					//Long name
					firstName += kaninaFront_List[Random.Range(0,kaninaFront_List.Length)];
					firstName += kaninaBack_List[Random.Range(0,kaninaBack_List.Length)];
				}else{
					//Short name
					firstName = kaninaSimple_List[Random.Range(0,kaninaSimple_List.Length)];
				}
			}
			if (randomRaceName == 4){GenerateVidurName(); firstName = tempName;}
		}

		//Last name, same deal
		if (Random.value <= 0.8){
			if (Random.value <= 0.5){
				lastName = skjomadurLast_List[Random.Range(0,skjomadurLast_List.Length)];
			}else{
				//Composite last name
				lastName = skjomadurLast_List[Random.Range(0,skjomadurLast_List.Length)];
				lastName += skjomadurLast_List[Random.Range(0,skjomadurLast_List.Length)];
			}
		}else{
		//Random race name
			int randomRaceName = Random.Range(0,5);
			
			if (randomRaceName == 0){GenerateTrollName(); lastName = tempName;}
			if (randomRaceName == 1){GenerateAskadurName(); lastName = tempName;}
			if (randomRaceName == 2){lastName = madurLast_List[Random.Range(0,madurLast_List.Length)];}
			if (randomRaceName == 3){
				if (Random.value >= 0.4){
					//Long name
					lastName += kaninaFront_List[Random.Range(0,kaninaFront_List.Length)];
					lastName += kaninaBack_List[Random.Range(0,kaninaBack_List.Length)];
				}else{
					//Short name
					lastName = kaninaSimple_List[Random.Range(0,kaninaSimple_List.Length)];
				}
			}
			if (randomRaceName == 4){GenerateVidurName(); lastName = tempName;}
		}

		if (firstName != ""){firstName = char.ToUpper(firstName[0]) + firstName.Substring(1);}
		if (lastName != ""){lastName = char.ToUpper(lastName[0]) + lastName.Substring(1);}
		tempName = firstName + '\u00A0' + lastName;
	}
	void GenerateTrollName(){
		int rulePick = Random.Range(0,3);

		if (rulePick == 0){
			//WOG configuration
			tempName += trollLetters_Consonant_List[Random.Range(0,trollLetters_Consonant_List.Length)];					tempName += trollLetters_Vowel_List[Random.Range(0,trollLetters_Vowel_List.Length)];
			tempName += trollLetters_Consonant_List[Random.Range(0,trollLetters_Consonant_List.Length)];
		}
		if (rulePick == 1){
			//UWU configuration
			tempName += trollLetters_Vowel_List[Random.Range(0,trollLetters_Vowel_List.Length)];
			tempName += trollLetters_Consonant_List[Random.Range(0,trollLetters_Consonant_List.Length)];
			tempName += trollLetters_Vowel_List[Random.Range(0,trollLetters_Vowel_List.Length)];
		}
		if (rulePick == 2){
			tempName += trollLetters_Vowel_List[Random.Range(0,trollLetters_Vowel_List.Length)];
			tempName += trollLetters_Double_List[Random.Range(0,trollLetters_Double_List.Length)];
		}
	}
	void GenerateUrminnName(bool young){
		string vowel1;
		string vowel2;
		if (young){
			vowel1 = urminnYoungVowels_List[Random.Range(0,urminnYoungVowels_List.Length)];
			vowel2 = urminnYoungVowels_List[Random.Range(0,urminnYoungVowels_List.Length)];

			tempName = urminnYoungNames_List[Random.Range(0,urminnYoungNames_List.Length)];

			tempName = tempName.Replace("*", vowel1);
			tempName = tempName.Replace("$", vowel2);
		}else{
			vowel1 = urminnAdultVowels_List[Random.Range(0,urminnAdultVowels_List.Length)];
			vowel2 = urminnAdultVowels_List[Random.Range(0,urminnAdultVowels_List.Length)];

			tempName = urminnAdultNames_List[Random.Range(0,urminnAdultNames_List.Length)];

			tempName = tempName.Replace("*", vowel1);
			tempName = tempName.Replace("$", vowel2);
		}
	}
	void GenerateVidurName(){
		string part1 = vidurNameParts_List[Random.Range(0,vidurNameParts_List.Length)];
		string part2 = vidurNameParts_List[Random.Range(0,vidurNameParts_List.Length)];
		tempName = part1 + part2;
	}
	void GenerateLifindurName(){
		int nameLength = Random.Range(1,3);
		for (var i = 0; i <= nameLength; i++){
			tempName += lifindurSyls_List[Random.Range(0,lifindurSyls_List.Length)];
		}
	}

//===========================================================================
	//Utilities
	void ClearNames(){
		firstName = "";
		lastName = "";
		tempName = "";
	}

//===========================================================================
	//Creating all the lists so shit can generate

	void InitializeNames(){
	//++++++++++++++++++++++++++++++++++++++++++
	//Draugur list creation
	//++++++++++++++++++++++++++++++++++++++++++
		draugurSyls_List = draugurSyls.text.Split("\n"[0]);
		draugurEra_List = draugurEra.text.Split("\n"[0]);

	//++++++++++++++++++++++++++++++++++++++++++
	//Vidur list creation
	//++++++++++++++++++++++++++++++++++++++++++
		vidurNameParts_List = vidurNameParts.text.Split("\n"[0]);

	//++++++++++++++++++++++++++++++++++++++++++
	//Nyrn list creation
	//++++++++++++++++++++++++++++++++++++++++++
		nyrnNameParts_List = nyrnNameParts.text.Split("\n"[0]);

	//++++++++++++++++++++++++++++++++++++++++++
	//Lifindur list creation
	//++++++++++++++++++++++++++++++++++++++++++
		lifindurSyls_List = lifindurSyls.text.Split("\n"[0]);

	//++++++++++++++++++++++++++++++++++++++++++
	//Kanina list creation
	//++++++++++++++++++++++++++++++++++++++++++
		kaninaBack_List = kaninaBack.text.Split("\n"[0]);
		kaninaFront_List = kaninaFront.text.Split("\n"[0]);
		kaninaSimple_List = kaninaSimpleNames.text.Split("\n"[0]);

	//++++++++++++++++++++++++++++++++++++++++++
	//Madur list creation
	//++++++++++++++++++++++++++++++++++++++++++
		madurFirst_List = madurFirst.text.Split("\n"[0]);
		madurLast_List = madurLast.text.Split("\n"[0]);

	//++++++++++++++++++++++++++++++++++++++++++
	//Askadur list creation
	//++++++++++++++++++++++++++++++++++++++++++
		skjomadurFirst_List = skjomadurFirstNames.text.Split("\n"[0]);
		skjomadurLast_List = skjomadurLastNames.text.Split("\n"[0]);

	//++++++++++++++++++++++++++++++++++++++++++
	//Askadur list creation
	//++++++++++++++++++++++++++++++++++++++++++
		askadurNameParts_List = askadurNameParts.text.Split("\n"[0]);

	//++++++++++++++++++++++++++++++++++++++++++
	//Urminn list creation
	//++++++++++++++++++++++++++++++++++++++++++
		urminnAdultNames_List = urminnAdultNames.text.Split("\n"[0]);
		urminnAdultVowels_List = urminnAdultVowels.text.Split("\n"[0]);
		urminnYoungNames_List = urminnYoungNames.text.Split("\n"[0]);
		urminnYoungVowels_List = urminnYoungVowels.text.Split("\n"[0]);

	//++++++++++++++++++++++++++++++++++++++++++
	//Faeryn list creation
	//++++++++++++++++++++++++++++++++++++++++++
		faerynNameParts_List = faerynNameParts.text.Split("\n"[0]);
		faerynFaergoSyls_List = faerynFaergoSyls.text.Split("\n"[0]);
		faerynKarwhenSyls_List = faerynKarwhenSyls.text.Split("\n"[0]);

	//++++++++++++++++++++++++++++++++++++++++++
	//Troll list creation
	//++++++++++++++++++++++++++++++++++++++++++
		trollLetters_Consonant_List = trollLetters_Consonant.text.Split("\n"[0]);
		trollLetters_Vowel_List = trollLetters_Vowel.text.Split("\n"[0]);
		trollLetters_Double_List = trollLetters_Double.text.Split("\n"[0]);

	}
}
