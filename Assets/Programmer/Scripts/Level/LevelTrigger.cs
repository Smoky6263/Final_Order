using UnityEngine;
using UnityEngine.Events;

public class LevelTrigger : MonoBehaviour
{
    public UnityEvent OnEnterEvent;
    public UnityEvent OnExitEvent;

    private string _player = "PlayerMainTrigger";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == _player)
            OnEnterEvent?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == _player)
            OnExitEvent?.Invoke();
    }
}
