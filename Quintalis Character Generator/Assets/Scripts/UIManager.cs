using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class UIManager : MonoBehaviour {
	Character currentCharacter;

	public Text txt_CharacterName;
	public Text txt_ageValue;
	public Text txt_YoBValue;
	public Text txt_species;
	public Text txt_gifts;
	public Text txt_brains;
	public Text txt_brawn;
	public Text txt_skin;
	public Text txt_tongue;

	public Sprite madurSprite;
	public Sprite askadurSprite;
	public Sprite draugurSprite;
	public Sprite faerynSprite;
	public Sprite kaninaSprite;
	public Sprite lifindurSprite;
	public Sprite nyrnSprite;
	public Sprite skjomadurSprite;
	public Sprite trollSprite;
	public Sprite urminnAdultSprite;
	public Sprite urminnYoungSprite;
	public Sprite vidurSprite;

	public Image speciesPortrait;

	void Awake () {
		currentCharacter = GetComponent<Character>();
	}
	
	// Update is called once per frame
	public void UpdateUIValues () {
		txt_CharacterName.text = currentCharacter.characterName;
		txt_species.text = Regex.Replace(currentCharacter.species.ToString(), "(\\B[A-Z])"," $1");
		
		if (currentCharacter.species == CharacterClass.Species.Askadur){speciesPortrait.overrideSprite = askadurSprite;}
		else if (currentCharacter.species == CharacterClass.Species.Draugur){speciesPortrait.overrideSprite = draugurSprite;}
		else if (currentCharacter.species == CharacterClass.Species.Faeryn){speciesPortrait.overrideSprite = faerynSprite;}
		else if (currentCharacter.species == CharacterClass.Species.Kanina){speciesPortrait.overrideSprite = kaninaSprite;}
		else if (currentCharacter.species == CharacterClass.Species.Lifindur){speciesPortrait.overrideSprite = lifindurSprite;}
		else if (currentCharacter.species == CharacterClass.Species.Madur){speciesPortrait.overrideSprite = madurSprite;}
		else if (currentCharacter.species == CharacterClass.Species.Nyrn){speciesPortrait.overrideSprite = nyrnSprite;}
		else if (currentCharacter.species == CharacterClass.Species.Skjomadur){speciesPortrait.overrideSprite = skjomadurSprite;}
		else if (currentCharacter.species == CharacterClass.Species.Troll){speciesPortrait.overrideSprite = trollSprite;}
		else if (currentCharacter.species == CharacterClass.Species.UrminnAdult){speciesPortrait.overrideSprite = urminnAdultSprite;}
		else if (currentCharacter.species == CharacterClass.Species.UrminnYoung){speciesPortrait.overrideSprite = urminnYoungSprite;}
		else if (currentCharacter.species == CharacterClass.Species.Vidur){speciesPortrait.overrideSprite = vidurSprite;}

		txt_ageValue.text = currentCharacter.age.ToString() + "\u000A" + "(" + Regex.Replace(currentCharacter.ageGroup.ToString(), "(\\B[A-Z])"," $1") + ")";
		txt_YoBValue.text = ConvertRawToYear(currentCharacter.yearOfBirth) + "\u000A" + "(" + Regex.Replace(currentCharacter.era.ToString(), "(\\B[A-Z])"," $1") + ")";
		txt_gifts.text = currentCharacter.gift.ToString();
		if (currentCharacter.gift == CharacterClass.Gift.Esper){
			txt_gifts.text += "\u000A" + "(" + currentCharacter.esper.ToString() + ")";
		}else if (currentCharacter.gift == CharacterClass.Gift.Medium){
			txt_gifts.text += "\u000A" + "(" + Regex.Replace(currentCharacter.pantheon.ToString(), "(\\B[A-Z])"," $1") + ")";
		}
		txt_brains.text = currentCharacter.brains.ToString();
		txt_brawn.text = currentCharacter.brawn.ToString();
		txt_skin.text = currentCharacter.skin.ToString();
		txt_tongue.text = currentCharacter.tongue.ToString();
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
