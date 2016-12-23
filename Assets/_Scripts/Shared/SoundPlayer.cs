using PathologicalGames;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Resources = SRResources.Audio;

public class SoundPlayer : MonoBehaviourEx
{
    public SoundPlayer Initialize()
    {
        this.effects = Resources.Effects.Instantiate().GetComponent<AudioSource>();
        this.effects.transform.SetParent(this.gameObject.transform, false);

        this.music = Resources.Music.Instantiate().GetComponent<AudioSource>();
        this.music.transform.SetParent(this.gameObject.transform, false);

        this.effectsPool = Resources.EffectPool.Instantiate().GetComponent<SpawnPool>();
        this.effectsPool.transform.SetParent(this.gameObject.transform, false);

        this.audioMixer = this.music.outputAudioMixerGroup.audioMixer;
        return this;
    }

    public void Handle(int playEffectMessage)
    {

    }

    public void Handle(bool stopEffectMessage)
    {

    }

    public void Handle(float playMusicMessage)
    {

    }

    private List<PlayingSound> playingSounds = new List<PlayingSound>();

    private AudioMixer audioMixer;

    private AudioSource effects;
    private AudioSource music;

    private SpawnPool effectsPool;
}
