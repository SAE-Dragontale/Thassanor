using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerBehaviour : MonoBehaviour {

    bool _canMove;
	float _directionChangeTimer;
	public float _health = 3f;
	bool _isAlive = true;
	[Space]

	public GameObject _townRef;
	public float _outOfBoundsDistance;
	[Space]

	[SerializeField] bool _isIdling = false;
	[Space]

	[SerializeField] bool _isPatroling = false;
	[SerializeField] Vector3 _patrolDir;
	[Space]

	[SerializeField] bool _isFleeing = false;
	[SerializeField] Vector3 _fleeDir;
	[Space]


	float _actionTimer;
	bool _oneShotActionActive = false;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	void Awake()
	{
		_canMove = true;
		_isAlive = true;

		_isIdling = true;
		_isPatroling = false;
		_isFleeing = false;
		
		_townRef = transform.parent.gameObject;

	}
	
	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	bool outOfBounds = false;
	public float distance;
	// Update is called once per frame
	void Update () 
	{
		
	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	//	IDLE
		if(_isIdling == true)
		{
			_canMove = false;
			_isFleeing = false;
			_isPatroling = false;

			//get direction nd distance of the town 
			Vector3 heading = _townRef.transform.position - this.gameObject.transform.position;
			heading = heading.normalized;
			distance = Vector3.Distance(transform.position,_townRef.transform.position);
			Vector3 directionOfTown = heading / distance;	

			//checks if im too far from the town, then sets out of bounds to true
			if(distance > _outOfBoundsDistance)
			{
				outOfBounds = true;
			}

			//time spent idling
			_actionTimer = _actionTimer + Time.deltaTime;
			if(_actionTimer >= Random.Range(.5f,2f))
			{
				//if out of bounds is true, then move towards the town until im close enough.
				if(outOfBounds == true)
				{
					transform.Translate(directionOfTown * 5f* Time.deltaTime, Space.World);

					if(distance <= _outOfBoundsDistance / 4f)
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
					_isPatroling = true;
				}
			}


		}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	// PATROL
		if(_isPatroling == true)
		{
			_canMove = true;
			_isFleeing = false;
			_isIdling = false;				
			
			//sets this to only occur once in patrol state
			if(_oneShotActionActive == true)
			{
				_oneShotActionActive = false;
				
				//chooses a random patrol direction out of 4 directions
				_patrolDir = new Vector3(0f,0f,0f);
				int dir = Random.Range(0,4);

				if(dir == 0){_patrolDir = Vector3.forward;}
				if(dir == 1){_patrolDir = -Vector3.forward;}
				if(dir == 2){_patrolDir = Vector3.right;}
				if(dir == 3){_patrolDir = -Vector3.right;}					
			}

			//move in that patrol direction
			transform.Translate(_patrolDir * 1f * Time.deltaTime, Space.World);
			//add to timer to end patrol
			_actionTimer = _actionTimer + Time.deltaTime;
			if(_actionTimer >= Random.Range(1.5f,3f))
			{
				//reset action timer, and set back to idle 
				_actionTimer = 0f;
				_isIdling = true;	
			}
		}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	// FLEE

		RaycastHit hit;
		if (Physics.SphereCast(transform.position, 15, transform.forward, out hit))
		{
			if(hit.transform.tag == "Player")
			{
				Vector3 heading = hit.transform.position - this.gameObject.transform.position;
				heading = heading.normalized;
				float distance = heading.magnitude;
				Vector3 direction = heading / distance;	
				_fleeDir = direction;
				_isFleeing = true;		
			}
		}

		if(_isFleeing == true)
		{
			_canMove = true;
			_isPatroling = false;
			_isIdling = false;
			
			
			transform.Translate(_fleeDir * 1f * Time.deltaTime, Space.World);
			
		}

		if(Input.GetKeyDown(KeyCode.E))
		{
			_health--;
		}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	// DEATH

		if(_health <= 0f)
		{
			Debug.Log("time to die!");
			_townRef.GetComponent<VillagerSpawn>().AddPower(1f);
			Destroy(gameObject);
		}
		
	}
	
	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	

}
