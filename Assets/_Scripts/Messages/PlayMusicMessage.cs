public class PlayMusicMessage
{
    public SoundData SoundData { get; }

    public PlayMusicMessage(SoundData soundData)
    {
        this.SoundData = soundData;
    }
}
