public class TutorialMode
{
    public bool isTutorialMode { get; private set; }

    public bool TutorialModeOn() => isTutorialMode = true;
    public bool TutorialModeOff() => isTutorialMode = false;
}