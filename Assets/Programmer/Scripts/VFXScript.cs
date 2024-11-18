using UnityEngine;

public class VFXScript : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 2f;

    private void FixedUpdate()
    {
        _lifeTime -= Time.fixedDeltaTime;

        if(_lifeTime < 0 )
            Destroy(gameObject);
    }
}
