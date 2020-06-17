using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
	static ConfigurationData _configData;

	#region Properties

	/// <summary>
	/// Gets the paddle move units per second
	/// </summary>
	/// <value>paddle move units per second</value>
	public static float PaddleMoveUnitsPerSecond => _configData.PaddleMoveUnitsPerSecond;

	/// <summary>
	/// Gets the impulse force to apply to move the ball
	/// </summary>
	/// <value>impulse force</value>
	public static float BallImpulseForce => _configData.BallImpulseForce;

	/// <summary>
	/// Gets the life time of a ball
	/// </summary>
	/// <value>ball life time</value>
	public static float BallLifeTime => _configData.BallLifeTime;

	/// <summary>
	/// Gets the wait time for a ball. After spawn it stays inactive for this long 
	/// </summary>
	public static float BallWaitTime => _configData.BallWaitTime;

	/// <summary>
	/// Minimum Time of ball spawn rate
	/// </summary>
	public static float MinBallSpawnTime => _configData.MinBallSpawnTime;

	/// <summary>
	/// Maximum Time of ball spawn rate
	/// </summary>
	public static float MaxBallSpawnTime => _configData.MaxBallSpawnTime;

	/// <summary>
	/// Ammount of balls in one Game Session
	/// </summary>
	public static int BallsPerGame => _configData.BallsPerGame;

	/// <summary>
	/// The Value of Block in score points. Standart
	/// </summary>
	public static int BlockValueStandart => _configData.BlockValueStandart;

	/// <summary>
	/// The Value of Block in score points. Bonus
	/// </summary>
	public static int BlockValueBonus => _configData.BlockValueBonus;

	/// <summary>
	/// The Value of Block in score points. Pickup
	/// </summary>
	public static int BlockValuePickup => _configData.BlockValuePickup;


	/// <summary>
	/// The Probability of spawning. Used in Level Builder. Block Standart
	/// </summary>
	public static float BlockProbStandart => _configData.BlockProbStandart;

	/// <summary>
	/// The Probability of spawning. Used in Level Builder. Block Bonus
	/// </summary>
	public static float BlockProbBonus => _configData.BlockProbBonus;

	/// <summary>
	/// The Probability of spawning. Used in Level Builder. Block Freezer
	/// </summary>
	public static float BlockProbFreezer => _configData.BlockProbFreezer;

	/// <summary>
	/// The Probability of spawning. Used in Level Builder. Block Speeder
	/// </summary>
	public static float BlockProbSpeeder => _configData.BlockProbSpeeder;


	/// <summary>
	/// Duration of Pickup Effect. Freezer 
	/// </summary>
	public static float PickupFreezerDuration => _configData.PickupFreezerDuration;

	/// <summary>
	/// Duration of Pickup Effect. Speeder
	/// </summary>
	public static float PickupSpeederDuration => _configData.PickupSpeederDuration;
	#endregion

	/// <summary>
	/// Initializes the configuration utils
	/// </summary>
	public static void Initialize()
    {
		_configData = new ConfigurationData();
    }

}
