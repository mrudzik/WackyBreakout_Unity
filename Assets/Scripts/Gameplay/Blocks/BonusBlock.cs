using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : Block
{
	protected override void Start()
	{
		base.Start();
		scoreWorth = ConfigurationUtils.BlockValueBonus;
	}
}
