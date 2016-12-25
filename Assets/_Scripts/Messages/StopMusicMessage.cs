public class StopMusicMessage
{
    public SoundData SoundData { get; }

    public StopMusicMessage(SoundData soundData)
    {
        this.SoundData = soundData;
    }
}
