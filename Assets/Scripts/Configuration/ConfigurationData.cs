using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{
    #region Fields

    const string ConfigurationDataFileName = "ConfigurationData.csv";

    // configuration data
    static float paddleMoveUnitsPerSecond = 6;

    static float ballImpulseForce = 150;
	static float ballLifeTime = 20;
	static float ballWaitTime = 3;

	static float minBallSpawnTime = 25;
	static float maxBallSpawnTime = 45;

	static int ballsPerGame = 10;

	static int blockValueStandart = 10;
	static int blockValueBonus = 50;
	static int blockValuePickup = 100;

	static float blockProbStandart = 40;
	static float blockProbBonus = 20;
	static float blockProbFreezer = 10;
	static float blockProbSpeeder = 5;

	static float pickupFreezerDuration = 2;
	static float pickupSpeederDuration = 2;

	#endregion

	#region Properties

	#region Paddle_Prop
	public float PaddleMoveUnitsPerSecond => paddleMoveUnitsPerSecond;

	#endregion

	#region Ball_Prop

	public float BallImpulseForce => ballImpulseForce;
	public float BallLifeTime => ballLifeTime;
	public float BallWaitTime => ballWaitTime;
	
	public float MinBallSpawnTime => minBallSpawnTime;
	public float MaxBallSpawnTime => maxBallSpawnTime;

	public int BallsPerGame => ballsPerGame;

	public int BlockValueStandart => blockValueStandart;
	public int BlockValueBonus => blockValueBonus;
	public int BlockValuePickup => blockValuePickup;

	public float BlockProbStandart => blockProbStandart;
	public float BlockProbBonus => blockProbBonus;
	public float BlockProbFreezer => blockProbFreezer;
	public float BlockProbSpeeder => blockProbSpeeder;


	public float PickupFreezerDuration => pickupFreezerDuration;
	public float PickupSpeederDuration => pickupSpeederDuration;

	#endregion

	#endregion






	#region Constructor


	/// <summary>
	/// Sets the configuration data fields from the provided
	/// csv string
	/// </summary>
	/// <param name="csvValues">csv string of values</param>
	static void SetConfigurationDataFields(StreamReader input)
	{
		string line;

		while ((line = input.ReadLine()) != null)
		{
			string[] values = line.Split(',');
		
			switch (values[0])
			{
				case "paddleMoveUnitsPerSecond":
					paddleMoveUnitsPerSecond = float.Parse(values[1]);
					break;
				case "ballImpulseForce":
					ballImpulseForce = float.Parse(values[1]);
					break;
				case "ballLifeTime":
					ballLifeTime = float.Parse(values[1]);
					break;
				case "ballWaitTime":
					ballWaitTime = float.Parse(values[1]);
					break;
				// Ball Spawn Time
				case "minBallSpawnTime":
					minBallSpawnTime = float.Parse(values[1]);
					break;
				case "maxBallSpawnTime":
					maxBallSpawnTime = float.Parse(values[1]);
					break;

				// Balls Per Game
				case "ballsPerGame":
					ballsPerGame = int.Parse(values[1]);
					break;

				// Block Values
				case "blockValueStandart":
					blockValueStandart = int.Parse(values[1]);
					break;
				case "blockValueBonus":
					blockValueBonus = int.Parse(values[1]);
					break;
				case "blockValuePickup":
					blockValuePickup = int.Parse(values[1]);
					break;

				// Block Probabilities
				case "blockProbStandart":
					blockProbStandart = float.Parse(values[1]);
					break;
				case "blockProbBonus":
					blockProbBonus = float.Parse(values[1]);
					break;
				case "blockProbFreezer":
					blockProbFreezer = float.Parse(values[1]);
					break;
				case "blockProbSpeeder":
					blockProbSpeeder = float.Parse(values[1]);
					break;

				// Pickup Duration
				case "pickupFreezerDuration":
					pickupFreezerDuration = float.Parse(values[1]);
					break;
				case "pickupSpeederDuration":
					pickupSpeederDuration = float.Parse(values[1]);
					break;

				default:
					break;
			}
		}
	}


	/// <summary>
	/// Constructor
	/// Reads configuration data from a file. If the file
	/// read fails, the object contains default values for
	/// the configuration data
	/// </summary>
	public ConfigurationData()
	{
		StreamReader input = null;
		try
		{
			// Opening File
			input = File.OpenText(Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName));
			// Setting config Data
			SetConfigurationDataFields(input);
		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
		}
		finally
		{// Close File
			if (input != null)
			{
				input.Close();
			}
		}
	}

	#endregion
}
