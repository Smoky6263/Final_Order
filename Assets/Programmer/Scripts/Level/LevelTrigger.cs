using UnityEngine;
using UnityEngine.Events;

public class LevelTrigger : MonoBehaviour
{
    public UnityEvent TriggerEvent;

    private string _player = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == _player)
            TriggerEvent?.Invoke();
    }
}
