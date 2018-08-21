/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CharStats.cs
   Version:			0.2.0
   Description: 	Holds and executes functions based on the Player's Stats.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using UnityEngine;

public class CharStats : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private CharAudio _charAudio;
	private CharVisuals _charVisuals;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Space] [Header("Player Statistics")]
	[SerializeField] [Range(0,100)] private float _playerHealth;
	[SerializeField] private string _playerName;

	[Space] [Header("Opponent")]
	[SerializeField] private Transform _opposingPlayer;
	[SerializeField] private float _distanceToOpponent;

	[Space] [Header("Nearby Objects")]
	[SerializeField] private float _searchRadius;
	[SerializeField] private Collider[] _scanForPlayer;
	[SerializeField] private Collider[] _scanForVillagers;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Start().
	private void Awake() {

		_charAudio = GetComponent<CharAudio>();
		_charVisuals = GetComponent<CharVisuals>();

	}

	// Called before class calls or functions.
	private void Start() {

		StartCoroutine(PlayerScan());
		StartCoroutine(PeasantScan());

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
		PostProcessingUpdate();

		CheckForDeath();

	}

	// Are we dead?
	private void CheckForDeath() {

		if (_playerHealth > 0f)
			return;

		_charVisuals.Death();
		GetComponent<InputTranslator>().Death();

	}
	
	// Here we calculate the distance to the opposing player and send that information off to other functions.
	private void DistanceUpdate() {

		if (_opposingPlayer != null)
			_distanceToOpponent = Vector3.Distance(transform.position, _opposingPlayer.position);
		else
			_distanceToOpponent = 100f;

	}

	// Here we simply deliver any data that our audio component requires to function.
	private void AudioUpdate() {

		if (_charAudio._local)
			_charAudio.UpdateMusic(_playerHealth, _distanceToOpponent);

	}

	// Our health directly correlates to the PostProcessing weight here.
	private void PostProcessingUpdate() => _charVisuals.PostProcessDeath(_playerHealth);

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Scan Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private IEnumerator PlayerScan() {

		while (_opposingPlayer == null) {

			_scanForPlayer = Physics.OverlapSphere(transform.position, _searchRadius * 2f, 1 << 11);

			if (_scanForPlayer.Length > 1)
				_opposingPlayer =  _scanForPlayer?[0]?.transform ?? null;

			yield return new WaitForSeconds(2f);

		}

	}

	private IEnumerator PeasantScan() {

		while (true) {

			_scanForVillagers = Physics.OverlapSphere(transform.position, _searchRadius, 1 << 13, QueryTriggerInteraction.Collide);
			yield return new WaitForSeconds(1f);

		}

	}

	public PeasantGroup RetrievePeasants() {

		return _scanForVillagers[0].GetComponent<PeasantGroup>();

	}

	/* ----------------------------------------------------------------------------- */

}
