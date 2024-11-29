using Cinemachine;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    public Camera Camera { get { return _camera; } set { _camera = value; } }

    private void LateUpdate() => transform.position = new Vector3(transform.position.x, _camera.transform.position.y, transform.position.z);
}
