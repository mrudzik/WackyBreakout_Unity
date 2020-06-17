using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Ball Spawning Script
/// </summary>
public class BallSpawner : MonoBehaviour
{

	[SerializeField] Ball _ballPrefab;
	[SerializeField] Transform _spawnPoint;
	[SerializeField] Sprite[] _sprites;


	Timer _spawnTimer;
	int _ballsToSpawn;

	Vector2 _spawnPos1;
	Vector2 _spawnPos2;



	#region MonoBehave


	private void Start()
	{
		_spawnTimer = gameObject.AddComponent<Timer>();
		// This will spawn new ball when timer finnished
		_spawnTimer.AddListener_Timer(HandleSpawnTimerFinish);

		RefreshSpawnTimer();

		//Setup Listeners
		EventManager.AddListener(EventName.BallDiesEvent, AddBallToQueue);
		AddBallToQueue(0);



		{   // Setup Spawn Collision Check Corners
			Ball tempBall = _ballPrefab;
			var collider = tempBall.GetComponent<BoxCollider2D>();
			float ballColliderHalfWidth = collider.size.x / 2;
			float ballColliderHalfHeight = collider.size.y / 2;

			// Setting rectangle area diagonal positions
			_spawnPos1 = new Vector2(
				_spawnPoint.position.x - ballColliderHalfWidth, _spawnPoint.position.y - ballColliderHalfHeight);
			_spawnPos2 = new Vector2(
				_spawnPoint.position.x + ballColliderHalfWidth, _spawnPoint.position.y + ballColliderHalfHeight);
		}
	}

	private void Update()
	{
		TryToSpawnBall();

	}

	#endregion

	#region Methods


	private void AddBallToQueue(float notUsed)
	{
		_ballsToSpawn++;
	}


	private void TryToSpawnBall()
	{
		if (_ballsToSpawn <= 0)
			return;

		// Collision Check
		if (Physics2D.OverlapArea(_spawnPos1, _spawnPos2) == null)
		{
			// Spawning Ball
			SpawnNewBall();
			_ballsToSpawn--;
		}
	}

	/// <summary>
	/// Spawns Fresh Ball. Than gives it color
	/// </summary>
	void SpawnNewBall()
	{
		// Spawning Ball
		var spawnedBall = Instantiate(_ballPrefab, _spawnPoint);
		
		// Changing sprite color here
		if (_sprites.Length >= 2)
		{
			var spriteRend = spawnedBall.GetComponent<SpriteRenderer>();
			spriteRend.sprite = _sprites[Random.Range(0, _sprites.Length)];
		}
	}

	void RefreshSpawnTimer()
	{
		_spawnTimer.Duration = Random.Range(ConfigurationUtils.MinBallSpawnTime, ConfigurationUtils.MaxBallSpawnTime);
		_spawnTimer.Run();
	}


	private void HandleSpawnTimerFinish()
	{
		RefreshSpawnTimer();
		AddBallToQueue(0);
	}
	#endregion

}
