using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Parallax_Kolya : MonoBehaviour
{
    private float distance;
    private float startpos;
    private GameObject cam;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPos;
    [SerializeField] private float parallax;
    public float smoothTime = 0.05f;

    private void Awake()
    {
        cam = GameObject.Find("MainCamera");
        startpos = transform.position.x;
    }

    void Update()
    {
        distance = cam.transform.position.x * parallax;
        targetPos = new Vector3(startpos + distance, cam.transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
