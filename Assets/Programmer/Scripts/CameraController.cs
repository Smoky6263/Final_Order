using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _heightOffsert;
    [SerializeField, Range(0f,5f)] private float _interpolation;
    private Transform _player;

    public void Init(Transform player) => _player = player;

    private void Update()
    {
        Vector3 targetPosition = new Vector3(_player.position.x, _player.position.y + _heightOffsert, -1f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _interpolation * Time.deltaTime);
    }
}
