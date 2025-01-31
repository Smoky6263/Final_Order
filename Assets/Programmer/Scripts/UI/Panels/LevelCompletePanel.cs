using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelCompletePanel : PopUpPanel
{
    public override void DoSomethingOnEnable()
    {

    }

    public override void DoSomethingOnDisable()
    {
        LoadLevel(_canvasManager.NextLevel);
    }

    private void LoadLevel(string nextLevel)
    {
        SceneManager.LoadScene(nextLevel);
    }
}
