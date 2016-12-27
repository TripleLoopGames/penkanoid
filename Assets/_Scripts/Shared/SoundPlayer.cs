using System;
using System.Collections;
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
        if (this.audioSource.loop == false)
        {
            StartCoroutine(DelayedCallback(this.audioSource.clip.length, this.despawnOwn));
        }
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

    public bool TheSameAs(SoundData soundDataToCompare)
    {
        return this.soundData.SameClipAndSource(soundDataToCompare);
    }

    private SoundPlayer ApplyData(SoundData soundData)
    {
        this.audioSource.clip = soundData.AudioClip;
        this.audioSource.loop = soundData.Loop;
        return this;
    }

    private IEnumerator DelayedCallback(float time, Action callback)
    {
       yield return new WaitForSeconds(time);
       this.despawnOwn();
    }
}
