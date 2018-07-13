/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			DeviceTracker.cs
   Version:			0.2.0
   Description: 	An alternative to Unity's inbuilt keybindings system and provides monitoring for custom keybindings.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

// We send information to:
[RequireComponent(typeof(InputTranslator))]

public abstract class DeviceTracker : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	protected InputTranslator _inputTranslator;	// Where we are passing all of our input information once it's collected.

	protected RawDataInput _inputData; // The data that we are passing through.
	protected bool _hasNewData; // Whether that data has changed since the last Sync.

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Main program
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Run before Start()
	private void Awake() {

		_inputTranslator = GetComponent<InputTranslator>();

	}

	// New data should be collected, packaged, and then sent over the network so that our inputs are processed on a network-wide scale.
	protected void SendNewInput() {

		_inputTranslator.CmdSyncStruct(_inputData);
		_hasNewData = false;

	}

}

/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
	Our custom Data Structs for use in all inherited scripts.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

public struct RawDataInput {

	public float[] _aflAxis;
	public bool[] _ablKeys;

	public RawDataInput(int itAxesNum, int itButtonNum) {

		_aflAxis = new float[itAxesNum];
		_ablKeys = new bool[itButtonNum];

	}

	public void Reset() {

		for (int it = 0; it < _aflAxis.Length; it++) {
			_aflAxis[it] = 0f;
		}

		for (int it = 0; it < _ablKeys.Length; it++) {
			_ablKeys[it] = false;
		}

	}

}