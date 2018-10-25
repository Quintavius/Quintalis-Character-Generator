using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameManager : MonoBehaviour {

	public TextAsset vidurNameParts;	

	string[] vidurNameParts_List;

	void Start (){
		InitializeNames();
	}

//===========================================================================
	//Main name generation function

	void GenerateName (CharacterClass.Race raceToName) {
		switch (raceToName){
			case CharacterClass.Race.Askadur:
				;
				break;
			case CharacterClass.Race.Draugur:
				;
				break;
			case CharacterClass.Race.Faeryn:
				;
				break;
			case CharacterClass.Race.Kanina:
				;
				break;
			case CharacterClass.Race.Lifindur:
				;
				break;
			case CharacterClass.Race.Madur:
				;
				break;
			case CharacterClass.Race.Nyrn:
				;
				break;
			case CharacterClass.Race.Skjomadur:
				;
				break;
			case CharacterClass.Race.Troll:
				;
				break;
			case CharacterClass.Race.UrminnAdult:
				;
				break;
			case CharacterClass.Race.UrminnYoung:
				;
				break;
			case CharacterClass.Race.Vidur:
				;
				break;
			default:
			//Race doesn't match
				break;
		}
	}

//===========================================================================
	//Creating all the lists so shit can generate

	void InitializeNames(){

	//++++++++++++++++++++++++++++++++++++++++++
	//Vidur list creation
	//++++++++++++++++++++++++++++++++++++++++++
		vidurNameParts_List = vidurNameParts.text.Split("\n"[0]);

	}
}
