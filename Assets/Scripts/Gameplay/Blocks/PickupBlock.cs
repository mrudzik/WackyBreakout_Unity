using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PickupBlock : Block
{
	public PickupEffect effect;
	[Tooltip("Must be sprites here for Freezer and Speeder")]
	[SerializeField] Sprite[] _sprites;

	float _effectDuration = 0;
	FloatEventInvoker _eventInvoker = null;

	protected override void Start()
	{
		base.Start();

		scoreWorth = ConfigurationUtils.BlockValuePickup;
		var tempRend = GetComponent<SpriteRenderer>();
		tempRend.sprite = _sprites[(int) effect];

		// Event stuff
		_eventInvoker = new FloatEventInvoker();

		switch (effect)
		{
			case PickupEffect.Freezer:
				_effectDuration = ConfigurationUtils.PickupFreezerDuration;
				_eventInvoker.AddSupportedEvent(EventName.FreezerEvent);
				break;
			case PickupEffect.Speedup:
				_effectDuration = ConfigurationUtils.PickupSpeederDuration;
				_eventInvoker.AddSupportedEvent(EventName.SpeederEvent);
				break;
		}
	}

	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ball"))
		{
			switch (effect)
			{
				case PickupEffect.Freezer:
					_eventInvoker.InvokeEvent(EventName.FreezerEvent, _effectDuration);
					AudioManager.Play(AudioClipName.IceHit);
					break;
				case PickupEffect.Speedup:
					_eventInvoker.InvokeEvent(EventName.SpeederEvent, _effectDuration);
					AudioManager.Play(AudioClipName.NitroHit);
					break;
			}

			// Score and Destroy
			base.OnCollisionEnter2D(collision);
		}
	}
}
