/* TimeColor.cs
 * 
 * Author:  Joel Kuntz - github.com/Frozenfire92
 * Started: December 19 2014
 * Updated: December 20 2014
 * License: Public Domain
 * 
 * Attach this script to a Unity 4.6 UI/Text object.
 * It will update the text with the time, hex value, and rgb
 * afterwards it updates the main cameras background color to match it.
 * 
 * Inspired by http://whatcolourisit.scn9a.org/
 * HexToColor from http://wiki.unity3d.com/index.php?title=HexConverter
 */

using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeColor : MonoBehaviour {

	private DateTime time;
	private Camera cam;
	private Text textObj;

	private string doubleZero = "00";
	
	private string hex;
	private Color32 color;

	private bool toggleText = true;

	void Start() 
	{
		// Set references
		cam = Camera.main;
		textObj = gameObject.GetComponent<Text>();
	}

	void Update () 
	{
		// Get current time
		time = DateTime.Now;

		// Reset and build hex string
		hex = "";
			// Hour
		if (time.Hour == 0) hex += doubleZero;
		else if (time.Hour > 0 && time.Hour < 10) hex += ("0" + time.Hour);
		else hex += time.Hour;
			// Minute
		if (time.Minute == 0) hex += doubleZero;
		else if (time.Minute > 0 && time.Minute < 10) hex += ("0" + time.Minute);
		else hex += time.Minute;
			// Second
		if (time.Second == 0) hex += doubleZero;
		else if (time.Second > 0 && time.Second < 10) hex += ("0" + time.Second);
		else hex += time.Second;

		// Get color, update text & camera
		color = HexToColor(hex);
		textObj.text = (toggleText) ? hex.Substring(0,2) + ":" + hex.Substring(2,2) + ":" + hex.Substring(4,2) + "\n#" + hex;
		cam.backgroundColor = color;
	}

	// For UI to toggle the text on/off while maintaining camera color change
	public void ToggleText ()
	{
		toggleText = !toggleText;
	}

	// Converts a string hex color ("123456") to Unity Color
	private Color32 HexToColor(string HexVal)
	{
		// Convert each set of 2 digits to its corresponding byte value
		byte R = byte.Parse(HexVal.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte G = byte.Parse(HexVal.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte B = byte.Parse(HexVal.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(R, G, B, 255);
	}
}
