using System.Collections;
using UnityEngine;
public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;

    [Header("Flip Rotation Stats")]
    [SerializeField] private float _flipYRotationTime = 0.5f;

    private Coroutine _turnCoroutine;

    private PlayerStateMachine _player;

    private bool _isFacingRight;

    private void Awake()
    {
        _player = _playerTransform.gameObject.GetComponent<PlayerStateMachine>();

        _isFacingRight = false;
    }

    private void Update()
    {
        transform.position = _playerTransform.position;
    }

    public void CallTurn()
    {
        LeanTween.rotateY(gameObject, DeterminateEndRotation(), _flipYRotationTime).setEaseInOutSine();
    }

    private IEnumerator FlipYLerp() 
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotation = DeterminateEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while(elapsedTime < _flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotation, (elapsedTime / _flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }

    private float DeterminateEndRotation()
    {
        _isFacingRight = !_isFacingRight;

        if (_isFacingRight)
        {
            return 180f;
        }

        else
        {
            return 0f;
        }
    }
}