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
        SoundPlayer soundPlayer = this.soundPlayerPool.Spawn(Resources.EffectPlayer).GetComponent<SoundPlayer>();
        soundPlayer.Initialize(message.SoundData, () => this.soundPlayerPool.Despawn(soundPlayer.transform));
        soundPlayer.Play();
        this.soundPlayers.Add(soundPlayer);
    }

    public void Handle(PlayMusicMessage message)
    {
        SoundPlayer soundPlayer = this.soundPlayerPool.Spawn(Resources.MusicPlayer).GetComponent<SoundPlayer>();
        soundPlayer.Initialize(message.SoundData, () => this.soundPlayerPool.Despawn(soundPlayer.transform));
        soundPlayer.Play();
        this.soundPlayers.Add(soundPlayer);
    }

    public void Handle(StopEffectMessage message)
    {
        SoundPlayer playingSound = this.soundPlayers.Find(playing => playing.TheSameAs(message.SoundData));
        if(playingSound == null)
        {
            Debug.LogWarning("Could not find Effect to stop");
            return;
        }
        playingSound.Reset();
        this.soundPlayers.Remove(playingSound);
    }

    public void Handle(StopMusicMessage message)
    {
        SoundPlayer playingSound = this.soundPlayers.Find(playing => playing.TheSameAs(message.SoundData));
        if (playingSound == null)
        {
            Debug.LogWarning("Could not find Music to stop");
            return;
        }
        playingSound.Reset();
        this.soundPlayers.Remove(playingSound);
    }

    private List<SoundPlayer> soundPlayers = new List<SoundPlayer>();

    public AudioMixer audioMixer;

    private AudioSource music;

    private SpawnPool soundPlayerPool;

}
