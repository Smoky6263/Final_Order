using UnityEngine;
using UnityEngine.Events;

public class LevelCompleteTrigger : MonoBehaviour
{
    public UnityEvent OnLevelComplete;

    private string _player = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == _player)
            OnLevelComplete?.Invoke();
    }
}
