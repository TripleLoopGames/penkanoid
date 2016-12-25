using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private SoundData soundData;
    private Action despawnOwn;

    public SoundPlayer Initialize(SoundData soundData, Action despawnOwn)
    {
        this.soundData = soundData;
        this.audioSource = GetComponent<AudioSource>();
        this.despawnOwn = despawnOwn;
        ApplyData(soundData);
        return this;
    }

    public SoundPlayer Play()
    {
        this.audioSource.Play();
        return this;
    }

    public SoundPlayer Reset()
    {
        this.audioSource.Stop();
        this.audioSource.clip = null;
        this.audioSource.loop = false;
        this.despawnOwn();
        return this;
    }

    public bool TheSameAs(SoundData soundData)
    {
        return soundData.SameClipAndSource(soundData);
    }

    private SoundPlayer ApplyData(SoundData soundData)
    {
        this.audioSource.clip = soundData.AudioClip;
        this.audioSource.loop = soundData.Loop;
        return this;
    }

}
