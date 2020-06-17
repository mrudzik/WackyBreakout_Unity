using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
	[SerializeField] Paddle _paddlePrefab;
	[SerializeField] Transform _objectHolder;

	[Space]
	[SerializeField] StandartBlock _blockStandartPrefab;
	[SerializeField] Sprite[] _blockStandartSprites;
	[Space]
	[SerializeField] BonusBlock _blockBonusPrefab;
	[SerializeField] Sprite[] _blockBonusSprites;
	[Space]
	[SerializeField] PickupBlock _blockPickupPrefab;

	[Space]
	[SerializeField] Transform _blockHolder;

	List<Block> _allBlockList = new List<Block>();


	private void Start()
	{
		Instantiate(_paddlePrefab, _objectHolder);
		BuildBlockMap();
	}


	private void BuildBlockMap()
	{
		// Get Block Sizes
		var tempBlockCollider = _blockStandartPrefab.GetComponent<BoxCollider2D>();
		float blockWidth = tempBlockCollider.size.x * tempBlockCollider.transform.localScale.x;
		float blockHeight = tempBlockCollider.size.y * tempBlockCollider.transform.localScale.y;
		// Space Between Blocks
		float verticalSpace = 0.1f;
		float horizontalSpace = 0.1f;
		// Screen Size
		float upBorder; // The Biggest Y Coordinate
		float leftBorder; // The Lowest X Coordinate
		float rightBorder; // The Biggest X Coordinate
		float startPoint;

		{// Setuping Screen size
		 	EdgeCollider2D[] camBorders = Camera.main.GetComponents<EdgeCollider2D>();
		 	if (camBorders.Length != 3)// Warning
		 		Debug.Log("Wrong ammount of Edge Colliders in main Camera (num: " + camBorders.Length + " )");

			// Setting Default Values
			upBorder = camBorders[2].points[0].y; // From Top Collider
			leftBorder = camBorders[0].points[0].x; // From Left Collider
			rightBorder = camBorders[1].points[0].x;// From Right Collider
													// c = b - (b - a) / 2
			startPoint = rightBorder - (rightBorder - leftBorder) / 2;

			Debug.Log("UpBorder Y = " + upBorder + " | LeftBorder X = " + leftBorder + " RightBorder X = " + rightBorder);
		}


		int rowCount = 5;
		for (int row = 0; row < rowCount; row++)
		{// What to do every row
			// Y Row Coordinate for brick
			float YRowCoord = upBorder - ((verticalSpace + blockHeight) * (row + 1));

			// Spacing Dist
			float dist = 0;
			while (startPoint + dist < rightBorder - blockWidth)
			{
				float XRowCoord = startPoint + dist;

				SpawnBlock(XRowCoord, YRowCoord);
				if (dist > 0)
					SpawnBlock(-XRowCoord, YRowCoord);

				// Making Spacing
				dist += horizontalSpace + blockWidth;
			}
		}
	}


	private void SpawnBlock(float xPos, float yPos)
	{
		int rand = Random.Range(0, 100);

		if (rand > ConfigurationUtils.BlockProbStandart)
		{// Standart Block
			var tempBlock = Instantiate<StandartBlock>(_blockStandartPrefab, new Vector3(xPos, yPos), new Quaternion(), _blockHolder);
			SetSprite(_blockStandartSprites, tempBlock, Random.Range(0, 3));
		}
		else if (rand > ConfigurationUtils.BlockProbBonus)
		{// Bonus Block
			var tempBlock = Instantiate<BonusBlock>(_blockBonusPrefab, new Vector3(xPos, yPos), new Quaternion(), _blockHolder);
			SetSprite(_blockBonusSprites, tempBlock, Random.Range(0, 3));
		}
		else if (rand > ConfigurationUtils.BlockProbFreezer)
		{// Freezer Block
			var tempBlock = Instantiate<PickupBlock>(_blockPickupPrefab, new Vector3(xPos, yPos), new Quaternion(), _blockHolder);
			tempBlock.effect = PickupEffect.Freezer;
		}
		else if (rand > ConfigurationUtils.BlockProbSpeeder)
		{// Speeder Block
			var tempBlock = Instantiate<PickupBlock>(_blockPickupPrefab, new Vector3(xPos, yPos), new Quaternion(), _blockHolder);
			tempBlock.effect = PickupEffect.Speedup;
		}
		else
		{// Standart Block
			var tempBlock = Instantiate<StandartBlock>(_blockStandartPrefab, new Vector3(xPos, yPos), new Quaternion(), _blockHolder);
			SetSprite(_blockStandartSprites, tempBlock, Random.Range(0, 3));
		}

	}

	private void SetSprite(Sprite[] spriteArr, Block blockObj, int spriteNum)
	{
		SpriteRenderer ownSpriteRend = blockObj.GetComponent<SpriteRenderer>();

		// Set sprite from arr		
		if (spriteArr.Length >= 2 // Cases when it should Work
			&& spriteNum < spriteArr.Length && spriteNum >= 0)
		{// Protection ^^^
			ownSpriteRend.sprite = spriteArr[spriteNum];
		}
	}

}
