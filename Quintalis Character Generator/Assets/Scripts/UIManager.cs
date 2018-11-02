using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	Character currentCharacter;

	public Text txt_CharacterName;
	public Text txt_ageValue;
	public Text txt_YoBValue;

	void Start () {
		currentCharacter = GetComponent<Character>();
	}
	
	// Update is called once per frame
	public void UpdateUIValues () {
		txt_CharacterName.text = currentCharacter.characterName;
		txt_ageValue.text = currentCharacter.age.ToString() + " " + "(" + currentCharacter.ageGroup.ToString() + ")";
		txt_YoBValue.text = currentCharacter.yearOfBirth.ToString();
	}

	void ConvertRawToYear(int rawYear){
		
	}

	void ConvertYearToRaw(){
		
	}
}
