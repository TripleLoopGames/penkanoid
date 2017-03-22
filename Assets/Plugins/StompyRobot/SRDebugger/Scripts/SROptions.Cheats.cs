using System.ComponentModel;

public partial class SROptions
{
    private bool godMode;

    [Category("Cheats")]
    public bool GodMode
    {
        get { return this.godMode; }
        set { this.godMode = value; }
    }
}