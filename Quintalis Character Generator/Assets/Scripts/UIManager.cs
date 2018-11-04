using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	Character currentCharacter;

	public Text txt_CharacterName;
	public Text txt_ageValue;
	public Text txt_YoBValue;
	public Text txt_species;

	void Start () {
		currentCharacter = GetComponent<Character>();
	}
	
	// Update is called once per frame
	public void UpdateUIValues () {
		txt_CharacterName.text = currentCharacter.characterName;
		txt_species.text = currentCharacter.species.ToString();
		txt_ageValue.text = currentCharacter.age.ToString() + "\u000A" + "(" + currentCharacter.ageGroup.ToString() + ")";
		txt_YoBValue.text = ConvertRawToYear(currentCharacter.yearOfBirth) + "\u000A" + "(" + currentCharacter.era.ToString() + ")";
	}

	string ConvertRawToYear(int rawYear){
		string returnString;
		if (rawYear > 2312){
			int mdyear = rawYear - 2312;
			returnString = "MD" + mdyear.ToString();
		}else{
			returnString = "AE" + rawYear.ToString();
		}
		return returnString;
	}

	void ConvertYearToRaw(){

	}
}
