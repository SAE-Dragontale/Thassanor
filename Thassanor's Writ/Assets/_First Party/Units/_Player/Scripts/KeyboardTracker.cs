/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			KeyboardTracker.cs
   Version:			0.1.0
   Description: 	An alternative to Unity's inbuilt keybindings system and provides monitoring for custom keyboard keybindings.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputTranslator))]
public class KeyboardTracker : MonoBehaviour {

	[Tooltip("The buttons our Player currently has bound for toggle-able commands.")]
	public KeyCode[] _akcButtonsBoolean;

	[Tooltip("The buttons our Player currently has bound for axis-based movement.")]
	public AxisButtons[] _asuButtonsAxis;

	private InputTranslator _inputTranslator;
	private InputData _inputData;
	private bool _hasNewData;

	// Called before Awake() in Inspector
	private void Reset() {

		_inputTranslator = GetComponent<InputTranslator>();

        _akcButtonsBoolean = new KeyCode[_inputTranslator._itButtonCount];
        _asuButtonsAxis = new AxisButtons[_inputTranslator._itAxesCount];

        // Development Functionality: Assign 'baseline' keycodes for development purposes.
        DefaultKeybindings();

    }

    // Alternative to Reset() that doesn't clear away our keybindings.
    public void Refresh() {

        _inputTranslator = GetComponent<InputTranslator>();

        // Assign temporary Arrays to store data according to new length governance.
        KeyCode[] _akcButtonsBooleanRefresh = new KeyCode[_inputTranslator._itButtonCount];
        AxisButtons[] _asuButtonsAxisRefresh = new AxisButtons[_inputTranslator._itAxesCount];

        // Load our old data into the new array, either culling elements or expanding the maximum number of elements.
        if (_akcButtonsBoolean != null) {
            for (int it = 0; it < Mathf.Min(_akcButtonsBooleanRefresh.Length, _akcButtonsBoolean.Length); it++) {
                _akcButtonsBooleanRefresh[it] = _akcButtonsBoolean[it];
            }
        }

        if (_asuButtonsAxis != null) {
            for (int it = 0; it < Mathf.Min(_asuButtonsAxisRefresh.Length, _asuButtonsAxis.Length); it++) {
                _asuButtonsAxisRefresh[it] = _asuButtonsAxis[it];
            }
        }

        // Finally, assign our original variables with the new array lengths, containing our old array data.
        _akcButtonsBoolean = _akcButtonsBooleanRefresh;
        _asuButtonsAxis = _asuButtonsAxisRefresh;

    }

	// A quick hardcoded function to set the default keybindings for development purposes.
	public void DefaultKeybindings() {

		try {
			_asuButtonsAxis[0]._kcPositive = KeyCode.W;
			_asuButtonsAxis[0]._kcNegative = KeyCode.S;

			_asuButtonsAxis[1]._kcPositive = KeyCode.A;
			_asuButtonsAxis[1]._kcNegative = KeyCode.D;

			_akcButtonsBoolean[0] = KeyCode.Escape;
			_akcButtonsBoolean[1] = KeyCode.Return;
		} catch {
			Debug.LogError("There aren't enough kebindings assigned in the InputTranslator to properly reset.");
		}
		

	}

	// Called before Start()
	private void Awake() {
		_inputTranslator = GetComponent<InputTranslator>();
		_inputData = new InputData (_inputTranslator._itAxesCount, _inputTranslator._itAxesCount);
	}
	
	// Update is called once per frame
	private void Update () 
	{

		// Check each Boolean-Button to see if they have been pressed or not.
		for (int it = 0; it < _akcButtonsBoolean.Length; it++) {
			
			// if (_inputData._ablButtons[it] != Input.GetKey(_akcButtonsBoolean[it])) {
			// 	_hasNewData = true;
			// }
			// _inputData._ablButtons[it] = Input.GetKey(_akcButtonsBoolean[it]);
			
			if (Input.GetKey(_akcButtonsBoolean[it])) {
				_inputData._ablButtons[it] = true;
				_hasNewData = true;
			}
		}

		// Check each Axis-Button combination to see if either key in the axis has been pressed and return a float.
		for (int it = 0; it < _asuButtonsAxis.Length; it++) {
			
			float flAxisReturn = 0f;

			if (Input.GetKey (_asuButtonsAxis[it]._kcPositive)) {
				flAxisReturn += 1f;
				_hasNewData = true;
			}

			if (Input.GetKey (_asuButtonsAxis[it]._kcNegative)) {
				flAxisReturn -= 1f;
				_hasNewData = true;
			}

			// if (flAxisReturn != _inputData._aflAxes[it]) {
			// 	_hasNewData = true;
			// }

			_inputData._aflAxes[it] = flAxisReturn;

		}

		// If we have new Data, we need to act upon it, so we send it to the Input Translator to be processed.
		if (_hasNewData) {
			_inputTranslator.TranslateInput(_inputData);
			_hasNewData = false;
			_inputData.Reset();
		}

	}

}

[System.Serializable]
public struct AxisButtons {
	public KeyCode _kcPositive;
	public KeyCode _kcNegative;	
}

public struct InputData {

	public float[] _aflAxes;
	public bool[] _ablButtons;

	public InputData(int itNumAxes, int itNumButtons) {
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