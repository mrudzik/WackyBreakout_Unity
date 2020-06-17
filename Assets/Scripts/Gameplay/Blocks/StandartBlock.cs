using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartBlock : Block
{
	protected override void Start()
	{
		base.Start();

		scoreWorth = ConfigurationUtils.BlockValueStandart;
	}
}
