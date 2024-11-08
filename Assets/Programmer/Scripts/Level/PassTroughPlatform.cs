using System.Collections;
using UnityEngine;

public class PassTroughPlatform : MonoBehaviour
{
    private Collider2D _collider;
    private Collider2D _playerBody;
    private Collider2D _playerFeet;

    private void Awake() => _collider = GetComponent<Collider2D>();
    public void TurnOffCollision(Collider2D playerCollider, Collider2D playerFeet)
    {
        _playerBody = playerCollider;
        _playerFeet = playerFeet;
        Physics2D.IgnoreCollision(_collider, _playerBody);
        Physics2D.IgnoreCollision(_collider, _playerFeet);
        Debug.Log("Turn Off Collision");
        StartCoroutine(CollisionTimer());
    }

    private IEnumerator CollisionTimer()
    {
        yield return new WaitForSeconds(0.5f);

        Physics2D.IgnoreCollision(_collider, _playerBody, false);
        Physics2D.IgnoreCollision(_collider, _playerFeet, false);
        _playerBody = null;
        _playerFeet = null;
        Debug.Log("Turn On Collision");
        yield break;
    }
}
