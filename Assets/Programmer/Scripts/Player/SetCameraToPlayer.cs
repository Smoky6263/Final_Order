using UnityEngine;

public class SetCameraToPlayer : MonoBehaviour
{
    private void Awake() 
    {
        Camera.main.GetComponentInParent<CameraController>().Init(transform);
    }
}
