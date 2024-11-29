public class TutorialPanel : PopUpPanel
{
    public override void DoSomethingOnEnable()
    {
        _canvasManager.EventBus.Invoke(new OnPauseEventSignal(true));
    }
    public override void DoSomethingOnDisable()
    {
        _canvasManager.EventBus.Invoke(new OnPauseEventSignal(false));
    }

}
