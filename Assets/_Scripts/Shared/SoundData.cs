using UnityEngine;

public class SoundData
{
    public SoundData(int sourceId, AudioClip audioClip, bool loop = false)
    {
        this.SourceId = sourceId;
        this.AudioClip = audioClip;
        this.Loop = loop;
    }

    public bool SameClipAndSource(SoundData soundData)
    {
        bool sameClip = this.AudioClip == soundData.AudioClip;
        bool sameSource = this.SourceId == soundData.SourceId;
        return sameClip && sameSource;
    }

    public int SourceId { get; }
    public bool Loop { get; }
    public AudioClip AudioClip { get; }
}
