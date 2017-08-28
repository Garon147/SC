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

	private float waitTimer = 0.5f;

	public Text warningText;

	public void alphabetFunction(string alphabet)
	{
		if (wordIndex < 9) 
		{
			showWarningWithAlpha (0.0f);

			wordIndex++;
			char[] keepChar = alphabet.ToCharArray ();
			nameChar [wordIndex] = keepChar [0];
			alpha = nameChar [wordIndex].ToString ();
			word += alpha;
			myPhone.text = word;
		}
	}

	public void backspace()
	{
		if (wordIndex >= 0) 
		{
			showWarningWithAlpha (0.0f);

			wordIndex--;
			alpha2 = null;
			for (int i = 0; i < wordIndex + 1; i++) 
			{
				alpha2 += nameChar [i].ToString ();
			}
			word = alpha2;
			myPhone.text = word;
		}
	}

	public void confirm()
	{
		if (myPhone.text.Length < 9) 
		{
			showWarningWithAlpha (1.0f);
		} 
		else 
		{
			PlayerPrefs.SetString ("CurrentPhoneNumber", myPhone.text);
			SceneManager.LoadScene ("Main");
		}

	}

	void Start()
	{
		isClickAllowed = false;
		cursorPoint = new KeyboardCursorController.MousePoint ();
		warningText.text = "Phone number must contain 9 characters";

		showWarningWithAlpha (0.0f);

		Cursor.visible = true;
	
	}

	void Update()
	{
		if (waitTimer > 0) 
		{
			waitTimer -= Time.deltaTime;
		} 
		else 
		{
			isClickAllowed = false;
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		cursorPoint = KeyboardCursorController.GetCursorPosition ();
		cursorPoint.X += 3 * (int)moveHorizontal;
		cursorPoint.Y -= 3 * (int)moveVertical;
		KeyboardCursorController.SetCursorPosition (cursorPoint);

		if (Input.GetButton ("Fire1") && !isClickAllowed) 
		{
			KeyboardCursorController.MouseEvent
			(
				KeyboardCursorController.MouseEventFlags.LeftDown | 
				KeyboardCursorController.MouseEventFlags.LeftUp
			);
			isClickAllowed = true;
			waitTimer = 0.5f;
		}
	}

	public void showWarningWithAlpha(float alpha)
	{
		Color temp = warningText.color;
		temp.a = alpha;
		warningText.color = temp;
	}
}
