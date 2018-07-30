using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillagerAI : MonoBehaviour {
	
	protected enum GameState {ProcessingState, IdleState, PatrolState, FleeState}
	protected GameState _currentState = GameState.ProcessingState;

    bool _canMove;
	public int _health = 3;

	float _directionChangeTimer;
	[Space]

	public GameObject _townRef;
	public float _outOfBoundsDistance;
	[Space]

	[SerializeField] bool _isIdling = false;
	[Space]

	[SerializeField] bool _isPatroling = false;
	[Space]

	[SerializeField] bool _isFleeing = false;
	[SerializeField] Vector3 _fleeDir;
	public GameObject _playerRef;
	[Space]

	float _actionTimer;
	bool _oneShotActionActive = false;
	
	Vector3 directionOfTown;
	bool outOfBounds = false;
	//patrol
	float patrolDirX;
	float patrolDirZ;
	public float distance;
	float randTimeValue;

	//update void which holds the state machine for the ally
	void Update (){
		
		if(_health <= 0)
		{
			Destroy(gameObject);
		}

		switch (_currentState) 
		{
			case GameState.ProcessingState:		
				StartUp();
				return;

			case GameState.IdleState:
				StartCoroutine(Idle(.5f));
				return;

			case GameState.PatrolState:
				StartCoroutine(Patrol(1.5f));
				return;

			case GameState.FleeState:	
				Flee();			
				return;

		}

		
		if(_isFleeing == true)
		{
			_canMove = true;
			_isPatroling = false;
			_isIdling = false;			
			
			transform.Translate(_fleeDir * 1f * Time.deltaTime, Space.World);

			_actionTimer = _actionTimer + Time.deltaTime;
			if(_actionTimer > 3f)
			{
				_actionTimer = 0f;
				_isIdling = _isPatroling = true;
				_oneShotActionActive = true;
			}
			
		}


	}

	protected void StartUp()
	{
		_canMove = true;

		_isIdling = true;
		_isPatroling = false;
		_isFleeing = false;
		
		_townRef = transform.parent.gameObject;

		_currentState = GameState.IdleState;

	}

	protected IEnumerator Idle(float timer)
	{
		//sets rotation to 0's
		transform.rotation = Quaternion.identity;
		//sets appropriate bools
		_canMove = false;
		_isFleeing = false;
		_isPatroling = false;
	
		//distance held here because in oneshot, it on has tthe value entered at idle start
		//causing the villager to walk endlessly in the direction of the town if out of bounds
		distance = Vector3.Distance(transform.position,_townRef.transform.position);
		if(_oneShotActionActive == true)
		{
			_oneShotActionActive = false;
			
			//get direction nd distance of the town 
			Vector3 heading = _townRef.transform.position - this.gameObject.transform.position;
			heading = heading.normalized;
			directionOfTown = heading / distance;				
		}

		//checks if im too far from the town, then sets out of bounds to true
		if(distance > _outOfBoundsDistance)
		{
			outOfBounds = true;
		}

		//time spent idling
		yield return new WaitForSeconds(timer);
			//if out of bounds is true, then move towards the town until im close enough.
			if(outOfBounds == true)
			{
				transform.Translate(directionOfTown * 5f* Time.deltaTime, Space.World);

				//if you're half the out of bounds distance, let npc leave idle state
				if(distance <= _outOfBoundsDistance / 2f)
				{
					outOfBounds = false;
					_actionTimer = 0f;
				}
				
			} // if out of bounds is false, then return to patrolling
			else
			{
				//resets action timer, sets one shot action to true because patrol requires it nd ctivates patrol state
				_actionTimer = 0f;
				_oneShotActionActive = true;
				_currentState = GameState.PatrolState;
			}
		

	}

	protected IEnumerator Patrol(float timer)
	{
			_canMove = true;
			_isFleeing = false;
			_isIdling = false;				
			
			//sets this to only occur once in patrol state
			if(_oneShotActionActive == true)
			{
				_oneShotActionActive = false;
				
				//chooses a random patrol direction out of 4 directions
				patrolDirX = Random.Range(-1f,1f);
				patrolDirZ = Random.Range(-1f,1f);

				//rand value for timer here so it only sets once per patrol state
				randTimeValue = Random.Range(1f,1.8f);				
			}

			//move in that patrol direction
			transform.Translate(new Vector3(patrolDirX, 0, patrolDirZ) * 1f * Time.deltaTime, Space.World);
			//add to timer to end patrol
			
		yield return new WaitForSeconds(timer);
				//reset action timer, set one shot bool to true because idle has 'play once' elements, and set back to idle 
				_actionTimer = 0f;
				_oneShotActionActive = true;
				_currentState = GameState.IdleState;
			

	}

	protected void Flee()
	{
		RaycastHit hit;
		if (Physics.SphereCast(transform.position, 3f, -transform.up, out hit))
		{
			if(hit.collider.tag == "Player")
			{
				Debug.Log(hit.collider.name);
				_isIdling = false;
				_isPatroling = false;

				Vector3 heading = hit.transform.position - this.gameObject.transform.position;
				heading = heading.normalized;
				float distance = heading.magnitude;
				//makes it the opposite direction of the player somehow
				distance = -distance;
				Vector3 direction = heading / distance;	
				_fleeDir = direction;

				_isFleeing = true;		
			}
		}	
	}

	public void TakeDamage(float getDamageAmount)
	{

	}

	void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 3f);
    }

}