using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    int nextLevel;

    public void LoadLevel(int nextLevel)
    {
        SceneManager.LoadScene(nextLevel);
    }
}
