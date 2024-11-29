using Cinemachine;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private float _parallaxEffect;
    
    private Camera _camera;

    private float _startPosition;

    private void Awake() => _camera = GetComponentInParent<Parallax>().Camera;

    private void Start() => _startPosition = transform.position.x;

    private void LateUpdate()
    {
        float distance = _camera.transform.position.x * _parallaxEffect;
        transform.position = new Vector3(_startPosition + distance, transform.position.y, transform.position.z);
    }

    public void ChangeCamera(Camera cameraTransform)
    {
        _camera = cameraTransform;
    }
}
