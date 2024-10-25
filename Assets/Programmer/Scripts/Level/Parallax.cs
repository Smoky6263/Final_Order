using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private float _parallaxEffect;
    private float _startPosition;

    private void Start() => _startPosition = transform.position.x;

    private void FixedUpdate()
    {
        float distance = _camera.transform.position.x * _parallaxEffect;
        transform.position = new Vector3(_startPosition + distance, transform.position.y, transform.position.z);
    }
}
