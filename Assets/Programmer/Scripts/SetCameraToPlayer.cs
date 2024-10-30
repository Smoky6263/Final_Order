using UnityEngine;

public class SetCameraToPlayer : MonoBehaviour
{
    private void Awake() 
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        Camera.main.GetComponent<CameraController>().Init(transform);
    }
}
