public class PlayEffectMessage
{
    public SoundData SoundData { get; }

    public PlayEffectMessage(SoundData soundData)
    {
        this.SoundData = soundData;
    }
}
