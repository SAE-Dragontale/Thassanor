/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CharStats.cs
   Version:			0.0.0
   Description: 	Holds and executes functions based on the Player's Stats.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

public class CharStats : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private CharAudio _charAudio;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Space] [Header("Player Statistics")]
	[SerializeField] [Range(0,100)] private float _playerHealth;
	[SerializeField] private string _playerName;

	[Space]
	[SerializeField] private Transform _opposingPlayer;
	[SerializeField] private float _distanceToOpponent;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Start().
	private void Awake() {

		_charAudio = GetComponent<CharAudio>();

	}

	// Called before class calls or functions.
	private void Start () {
		
	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Functions
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	
	// Update is called once per frame.
	private void FixedUpdate () {

		DistanceUpdate();
		AudioUpdate();

	}
	
	// Here we calculate the distance to the opposing player and send that information off to other functions.
	private void DistanceUpdate() {

		//_distanceToOpponent = Vector3.Distance(transform.position, _opposingPlayer.position);

	}

	// Here we simply deliver any data that our audio component requires to function.
	private void AudioUpdate() {

		if (_charAudio._local)
			_charAudio.UpdateMusic(_playerHealth, _distanceToOpponent);

	}

	/* ----------------------------------------------------------------------------- */
	
}
