using UnityEngine;

public class RoomCameraFolow : MonoBehaviour
{
    private GameObject _virtualCam;

    private void Awake() => _virtualCam = transform.GetChild(0).gameObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _virtualCam.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _virtualCam.gameObject.SetActive(false);
        }
    }
}
