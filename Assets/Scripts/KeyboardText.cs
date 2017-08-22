﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KeyboardText : MonoBehaviour 
{
	string word = null;
	int wordIndex = -1;
	string alpha = null;
	string alpha2 = null;
	public Text myName = null;
	char[] nameChar = new char[30];

	public void alphabetFunction(string alphabet)
	{
		wordIndex++;
		char[] keepChar = alphabet.ToCharArray ();
		nameChar [wordIndex] = keepChar [0];
		alpha = nameChar [wordIndex].ToString ();
		word += alpha;
		myName.text = word;
	}

	public void backspace()
	{
		if (wordIndex >= 0) 
		{
			wordIndex--;
			alpha2 = null;
			for (int i = 0; i < wordIndex + 1; i++) 
			{
				alpha2 += nameChar [i].ToString ();
			}
			word = alpha2;
			myName.text = word;
		}
	}

	public void confirm()
	{
		PlayerPrefs.SetString ("PhoneNumber", myName.text);
		SceneManager.LoadScene ("Main");
	}
}
