using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Paddle class for moving platform that must be controlled by player
/// Controls made by arrow keys
/// </summary>
public class Paddle : MonoBehaviour
{

	Rigidbody2D _localRigid;
	Vector2 _velocity;
	float _halfColliderWidth;
	float _halfColliderHeight;

	// Degrees converted in Radians
	const float BounceAngleHalfRange = 60 * Mathf.Deg2Rad;

	// Freeze Timer
	Timer _freezeTimer;
	bool _isFrozen;

	#region MonoBehave
	// Start is called before the first frame update
	void Start()
    {
		// For Arrow Movement
		_localRigid = GetComponent<Rigidbody2D>();
		_velocity = new Vector2(ConfigurationUtils.PaddleMoveUnitsPerSecond, 0);

		// For Ball Precision
		BoxCollider2D localBoxColl = GetComponent<BoxCollider2D>();
		_halfColliderWidth = localBoxColl.size.x / 2;
		_halfColliderHeight = localBoxColl.size.y / 2;

		// For Freezer
		EventManager.AddListener(EventName.FreezerEvent, HandlePickupFreezerEvent);
		_freezeTimer = gameObject.AddComponent<Timer>();
		_freezeTimer.AddListener_Timer(HandleUnfreezeEvent);
		_isFrozen = false;
    }

	/// <summary>
	/// For Rigidbody interactions
	/// Main goal to be controlled by arrow input
	/// </summary>
	void FixedUpdate()
	{
		if (!_isFrozen)
		{
			MakeMovement();
		}
	}

	/// <summary>
	/// Detects collision with a ball to aim the ball
	/// </summary>
	/// <param name="coll">collision info</param>
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("Ball"))
		{
			//	if (IsTopCollision(coll))
			//	{
			AudioManager.Play(AudioClipName.PaddleHit);

				// calculate new ball direction
				float ballOffsetFromPaddleCenter = transform.position.x -
					coll.transform.position.x;
				float normalizedBallOffset = ballOffsetFromPaddleCenter /
					_halfColliderWidth;
				float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
				float angle = Mathf.PI / 2 + angleOffset;
				Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

				// tell ball to set direction to new direction
				Ball ballScript = coll.gameObject.GetComponent<Ball>();
				ballScript.SetDirection(direction);
		//	}
		}
	}

	#endregion

	private void HandlePickupFreezerEvent(float freezeTime)
	{
		Debug.Log("Hello I'm picked Freezer for " + freezeTime + " seconds");

		if (!_freezeTimer.Running)
		{// If freeze Timer not Running
			_freezeTimer.Duration = freezeTime;
			_freezeTimer.Run();
			_isFrozen = true;
		}
		else if (_freezeTimer.Running)
		{// If freeze Timer Working
			// Extend Duration
			_freezeTimer.Duration = _freezeTimer.Duration + freezeTime;
		}

		Debug.Log("Freeze time now: " + _freezeTimer.Duration);
	}

	private void HandleUnfreezeEvent()
	{
		_isFrozen = false;
		_freezeTimer.Duration = 0;
	}

	#region Movement

	void MakeMovement()
	{
		if (Input.GetKey(KeyCode.RightArrow))
		{
			_localRigid.MovePosition(
				CalculateClampedX(_localRigid.position + _velocity * Time.deltaTime, true));
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			_localRigid.MovePosition(
				CalculateClampedX(_localRigid.position - _velocity * Time.deltaTime, false));
		}
	}

	Vector2 CalculateClampedX(Vector2 potentialPos, bool isRight)
	{
		if (isRight)
		{ // Right Direction
			if (potentialPos.x + _halfColliderWidth > ScreenUtils.ScreenRight)
			{ // Right Border
				return new Vector2(ScreenUtils.ScreenRight - _halfColliderWidth, potentialPos.y);
			}
		}
		else
		{// Left Direction
			if (potentialPos.x - _halfColliderWidth < ScreenUtils.ScreenLeft)
			{// Left Border
				return new Vector2(ScreenUtils.ScreenLeft + _halfColliderWidth, potentialPos.y);
			}
		}

		return potentialPos;
	}

	#endregion


}
