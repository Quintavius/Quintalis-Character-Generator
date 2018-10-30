using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameManager : MonoBehaviour {

	public TextAsset vidurNameParts;
	public TextAsset nyrnNameParts;
	public TextAsset trollLetters_Consonant;
	public TextAsset trollLetters_Vowel;
	public TextAsset trollLetters_Double;

	string[] vidurNameParts_List;
	string[] nyrnNameParts_List;
	string[] trollLetters_Consonant_List;
	string[] trollLetters_Vowel_List;
	string[] trollLetters_Double_List;

	Character character;

	void Start (){
		character = GetComponent<Character>();
		InitializeNames();
	}

//===========================================================================
	//Main name generation function

	public string GenerateName (CharacterClass.Species raceToName) {
		string tempName = "";
		switch (raceToName){
			case CharacterClass.Species.Askadur:
				;
				break;
			case CharacterClass.Species.Draugur:
				;
				break;
			case CharacterClass.Species.Faeryn:
				;
				break;
			case CharacterClass.Species.Kanina:
				;
				break;
			case CharacterClass.Species.Lifindur:
				;
				break;
			case CharacterClass.Species.Madur:
				;
				break;
			case CharacterClass.Species.Nyrn:
				int syllablesInNyrnsName = Mathf.RoundToInt(character.age / 5);
				for (var i = 0; i < syllablesInNyrnsName; i++){
					tempName += nyrnNameParts_List[Random.Range(0,nyrnNameParts_List.Length)];
				}
				break;
			case CharacterClass.Species.Skjomadur:
				;
				break;
			case CharacterClass.Species.Troll:
				int rulePick = Random.Range(0,3);

				if (rulePick == 0){
					//WOG configuration
					tempName += trollLetters_Consonant_List[Random.Range(0,trollLetters_Consonant_List.Length)];
					tempName += trollLetters_Vowel_List[Random.Range(0,trollLetters_Vowel_List.Length)];
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
				;
				break;
			case CharacterClass.Species.UrminnAdult:
				;
				break;
			case CharacterClass.Species.UrminnYoung:
				;
				break;
			case CharacterClass.Species.Vidur:
				string part1 = vidurNameParts_List[Random.Range(0,vidurNameParts_List.Length)];
				string part2 = vidurNameParts_List[Random.Range(0,vidurNameParts_List.Length)];
				tempName = part1 + part2;
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
	//Creating all the lists so shit can generate

	void InitializeNames(){

	//++++++++++++++++++++++++++++++++++++++++++
	//Vidur list creation
	//++++++++++++++++++++++++++++++++++++++++++
		vidurNameParts_List = vidurNameParts.text.Split("\n"[0]);

	//++++++++++++++++++++++++++++++++++++++++++
	//Nyrn list creation
	//++++++++++++++++++++++++++++++++++++++++++
		nyrnNameParts_List = nyrnNameParts.text.Split("\n"[0]);

	//++++++++++++++++++++++++++++++++++++++++++
	//Troll list creation
	//++++++++++++++++++++++++++++++++++++++++++
		trollLetters_Consonant_List = trollLetters_Consonant.text.Split("\n"[0]);
		trollLetters_Vowel_List = trollLetters_Vowel.text.Split("\n"[0]);
		trollLetters_Double_List = trollLetters_Double.text.Split("\n"[0]);

	}
}
