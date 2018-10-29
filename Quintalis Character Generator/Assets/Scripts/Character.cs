using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This bad boy holds all the character info to be displayed. 
//Is written to by CharacterGenerator and read by CharacterDisplay
//
//Let's try and keep things clean by not making this /do/ anything

public class Character : MonoBehaviour {
	public int brains;
	public int brawn;
	public int skin;
	public int tongue;
	public int age;
	public int yearOfBirth;
	public int currentYear;
	public CharacterClass.AgeGroup ageGroup;
	public CharacterClass.Era era;
	public CharacterClass.Race race;
	public CharacterClass.Gift gift;
	public CharacterClass.Esper esper;
	public CharacterClass.Pantheon pantheon;
}
