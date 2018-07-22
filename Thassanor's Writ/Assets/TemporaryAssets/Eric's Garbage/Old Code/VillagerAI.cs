using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillagerAI : MonoBehaviour {
	
	public enum GameState {ProcessingState, IdleState, PatrolState, FleeState, DeadState}
	public GameState _currentState = GameState.ProcessingState;

    bool _canMove;
	public int _health = 3;
	bool alive = true;

	void Start (){
		_currentState = GameState.ProcessingState;
	}

	//update void which holds the state machine for the ally
	void Update (){
		
		switch (_currentState) {
		case GameState.ProcessingState:		
			_canMove = true;
			alive = true;
			_currentState = GameState.PatrolState;
			break;

		case GameState.IdleState:
			//if there is something i should do, change state
			_currentState = GameState.PatrolState;
			break;

		case GameState.PatrolState:
			if (_canMove = true)
			{
				StartCoroutine(MoveDir(Random.Range(2f,4f)));
			}
			break;

		case GameState.FleeState:		
		
			break;

		case GameState.DeadState:
			if (!alive){
				return;
			} 
			else{

				//removes the villager from any lists

				//can run additional code to say do x or y when dead

				alive = false;
			}
			break;

		}

	}

	public IEnumerator MoveDir(float stopTimer)
	{		
		_canMove = false;

		transform.Translate(-Vector3.forward * 1f * Time.deltaTime);
		
		yield return new WaitForSeconds(stopTimer);		
		_currentState = GameState.IdleState;
		
	}

	public void TakeDamage(float getDamageAmount)
	{

	}
}