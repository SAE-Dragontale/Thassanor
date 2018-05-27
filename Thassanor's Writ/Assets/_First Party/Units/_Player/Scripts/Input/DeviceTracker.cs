/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			DeviceTracker.cs
   Version:			0.0.0
   Description: 	An alternative to Unity's inbuilt keybindings system and provides monitoring for custom keybindings.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputTranslator))]
public abstract class DeviceTracker : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Player Input
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Tooltip("The number of Player-Axes we need to monitor. Each Axis is two buttons, or a joystick between two values.")]
	public int _itAxesCount;

	[Tooltip("The number of Player-Buttons we need to monitor. These are checked if they are pressed.")]
	public int _itButtonCount;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Tracker Specific Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	protected InputTranslator _inputTranslator;
	protected RawDataInput _inputData;
	protected bool _hasNewData;

}

/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
	Our custom Data Structs for use in all inherited scripts.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

public struct RawDataInput {

	public float[] _aflAxes;
	public bool[] _ablButtons;

	public RawDataInput(int itNumAxes, int itNumButtons) {
		_aflAxes = new float[itNumAxes];
		_ablButtons = new bool[itNumButtons];
	}

	public void Reset() {
		for (int it = 0; it < _aflAxes.Length; it++) {
			_aflAxes[it] = 0f;
		}
		for (int it = 0; it < _ablButtons.Length; it++) {
			_ablButtons[it] = false;
		}
	}

}