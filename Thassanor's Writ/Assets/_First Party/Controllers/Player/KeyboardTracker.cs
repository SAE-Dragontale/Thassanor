using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class KeyboardTracker : MonoBehaviour {

	[Tooltip("The buttons our Player currently has bound for toggle-able commands.")]
	public KeyCode[] _kcBooleanButtons;

	[Tooltip("The buttons our Player currently has bound for axis-based movement.")]
	public AxisButtons[] _axisButtons;

	private InputManager _inputManager;
	private InputData _inputData;
	private bool _hasNewData;

	// Called before Awake() in Inspector
	void Reset() {
		_inputManager = GetComponent<InputManager>();
		_axisButtons = new AxisButtons[_inputManager._itAxesCount];
		_kcBooleanButtons = new KeyCode[_inputManager._itButtonCount];
	}

	// Called before Start()
	void Awake() {
		_inputManager = GetComponent<InputManager>();
		_inputData = new InputData (_inputManager._itAxesCount, _inputManager._itAxesCount);
	}
	
	// Update is called once per frame
	void Update () 
	{

		

		if (_hasNewData) {
			_inputManager.TranslateInput(_inputData);
			_hasNewData = false;
		}

	}
}

[System.Serializable]
public struct AxisButtons {
	public KeyCode _kcNegative;
	public KeyCode _kcPositive;
}

public struct InputData {

	public float[] _aflAxes;
	public bool[] _ablButtons;

	public InputData(int itNumAxes, int itNumButtons) {
		_aflAxes = new float[itNumAxes];
		_ablButtons = new bool[itNumButtons];
	}

}