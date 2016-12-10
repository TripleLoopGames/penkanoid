using UnityEngine;

public class ModifyTimeMessage
{
    public int Time { get; set; }

    public ModifyTimeMessage(int time)
    {
        this.Time = time;
    }
}