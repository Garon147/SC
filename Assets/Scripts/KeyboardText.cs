using System.Collections;
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
	public Text myPhone = null;
	char[] nameChar = new char[9];

	bool isClickAllowed;
	KeyboardCursorController.MousePoint cursorPoint;

	public void alphabetFunction(string alphabet)
	{
		if (wordIndex < 9) 
		{
			wordIndex++;
			char[] keepChar = alphabet.ToCharArray ();
			nameChar [wordIndex] = keepChar [0];
			alpha = nameChar [wordIndex].ToString ();
			word += alpha;
			myPhone.text = word;
		}

		isClickAllowed = false;
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
			myPhone.text = word;
		}

		isClickAllowed = false;
	}

	public void confirm()
	{
		PlayerPrefs.SetString ("CurrentPhoneNumber", myPhone.text);
		SceneManager.LoadScene ("Main");
	}

	void Start()
	{
		isClickAllowed = false;
		cursorPoint = new KeyboardCursorController.MousePoint ();
	}

	void Update()
	{
		if (Input.GetButton ("Fire1") && !isClickAllowed) 
		{
			KeyboardCursorController.MouseEvent
			(
				KeyboardCursorController.MouseEventFlags.LeftDown | 
				KeyboardCursorController.MouseEventFlags.LeftUp
			);
			isClickAllowed = true;
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		cursorPoint = KeyboardCursorController.GetCursorPosition ();
		cursorPoint.X += (int)moveHorizontal;
		cursorPoint.Y += (int)moveVertical;
		KeyboardCursorController.SetCursorPosition (cursorPoint);
	}
}
