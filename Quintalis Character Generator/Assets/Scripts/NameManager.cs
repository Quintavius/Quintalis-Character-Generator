using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameManager : MonoBehaviour {

	public TextAsset vidurNameParts;
	public TextAsset nyrnNameParts;

	string[] vidurNameParts_List;
	string[] nyrnNameParts_List;

	void Start (){
		InitializeNames();
	}

//===========================================================================
	//Main name generation function

	public string GenerateName (CharacterClass.Species raceToName) {
		string tempName = "Johnny";
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
				;
				break;
			case CharacterClass.Species.Skjomadur:
				;
				break;
			case CharacterClass.Species.Troll:
				;
				break;
			case CharacterClass.Species.UrminnAdult:
				;
				break;
			case CharacterClass.Species.UrminnYoung:
				;
				break;
			case CharacterClass.Species.Vidur:
				;
				break;
			default:
			//Species doesn't match
				break;
		}
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

	}
}
