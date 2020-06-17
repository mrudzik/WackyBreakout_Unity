using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AudioClipName
{
	ButtonHighLight,
	ButtonSelect,
	GameOver,
	BlockHit,
	PaddleHit,
	IceHit,
	NitroHit
}

public static class AudioManager
{
	static bool _initialized = false;
	static AudioSource _audioSource;
	static Dictionary<AudioClipName, AudioClip> audioClips =
		new Dictionary<AudioClipName, AudioClip>();

	public static bool Initialized => _initialized;

	public static void Initialize(AudioSource source)
	{
		_initialized = true;
		_audioSource = source;

		audioClips.Add(AudioClipName.ButtonHighLight, Resources.Load<AudioClip>("Audio/button_highlight"));
		audioClips.Add(AudioClipName.ButtonSelect, Resources.Load<AudioClip>("Audio/button_select"));
		audioClips.Add(AudioClipName.GameOver, Resources.Load<AudioClip>("Audio/GameOver"));
		audioClips.Add(AudioClipName.BlockHit, Resources.Load<AudioClip>("Audio/hit-a-ball"));
		audioClips.Add(AudioClipName.PaddleHit, Resources.Load<AudioClip>("Audio/ping-pong-ball-hit"));
		audioClips.Add(AudioClipName.IceHit, Resources.Load<AudioClip>("Audio/Ice_Hit"));
		audioClips.Add(AudioClipName.NitroHit, Resources.Load<AudioClip>("Audio/nitro"));

	}

	public static void Play(AudioClipName name)
	{
		if (!_initialized)
			return;

		_audioSource.PlayOneShot(audioClips[name]);
	}
}
