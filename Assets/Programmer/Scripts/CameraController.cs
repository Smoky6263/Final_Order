using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _heightOffsert;
    [SerializeField, Range(0f,1f)] private float _interpolation;

    private void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(_player.position.x, _player.position.y + _heightOffsert, -1f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _interpolation);
    }
}
