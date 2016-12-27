using PathologicalGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Resources = SRResources.Audio;

public class SoundCentralPool : MonoBehaviourEx, IHandle<PlayEffectMessage>, IHandle<PlayMusicMessage>, IHandle<StopEffectMessage>, IHandle<StopMusicMessage>
{
    public SoundCentralPool Initialize()
    {
        this.soundPlayerPool = Resources.SoundPlayerPool.Instantiate().GetComponent<SpawnPool>();
        this.soundPlayerPool.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    public SoundCentralPool Reset()
    {
        // go trough all playing soudns and stop them
        this.soundPlayers.Select((soundPlayer) => soundPlayer.Reset());
        this.soundPlayers.RemoveAll((soundPlayer) => true);
        return this;
    }

    public void Handle(PlayEffectMessage message)
    {
        SoundPlayer playingSound = FindSoundPlayer(message.SoundData);
        if (playingSound != null)
        {
            Debug.LogWarning("Ignored play effect sound, already one playing");
            return;
        }
        SoundPlayer soundPlayer = this.soundPlayerPool.Spawn(Resources.EffectPlayer).GetComponent<SoundPlayer>();
        soundPlayer.Initialize(message.SoundData, () =>
        {
            this.soundPlayers.Remove(soundPlayer);
            this.soundPlayerPool.Despawn(soundPlayer.transform);
        });
        soundPlayer.Play();
        this.soundPlayers.Add(soundPlayer);
    }

    public void Handle(PlayMusicMessage message)
    {
        SoundPlayer playingSound = FindSoundPlayer(message.SoundData);
        if (playingSound != null)
        {
            Debug.LogWarning("Ignored play music sound, already one playing");
            return;
        }
        SoundPlayer soundPlayer = this.soundPlayerPool.Spawn(Resources.MusicPlayer).GetComponent<SoundPlayer>();
        soundPlayer.Initialize(message.SoundData, () =>
        {
            this.soundPlayers.Remove(soundPlayer);
            this.soundPlayerPool.Despawn(soundPlayer.transform);
        });
        soundPlayer.Play();
        this.soundPlayers.Add(soundPlayer);
    }

    public void Handle(StopEffectMessage message)
    {
        SoundPlayer playingSound = FindSoundPlayer(message.SoundData);
        if (playingSound == null)
        {
            Debug.LogWarning("Could not find Effect to stop");
            return;
        }
        playingSound.Reset();
    }

    public void Handle(StopMusicMessage message)
    {
        SoundPlayer playingSound = FindSoundPlayer(message.SoundData);
        if (playingSound == null)
        {
            Debug.LogWarning("Could not find Music to stop");
            return;
        }
        playingSound.Reset();
    }

    private SoundPlayer FindSoundPlayer(SoundData soundData)
    {
        return this.soundPlayers.Find(playing => playing.TheSameAs(soundData));
    }

    private List<SoundPlayer> soundPlayers = new List<SoundPlayer>();

    public AudioMixer audioMixer;

    private SpawnPool soundPlayerPool;

}
