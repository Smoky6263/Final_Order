using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    public void LoadLevel(int nextLevel)
    {
        SceneManager.LoadScene(nextLevel);
    }
}
