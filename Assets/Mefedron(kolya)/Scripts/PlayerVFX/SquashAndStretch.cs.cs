using UnityEngine;

public class SquashAndStretch : MonoBehaviour
{
    [SerializeField] private Transform _sprite;
    [SerializeField] private float _stretch = 0.1f;
    [SerializeField] private Transform _squashParent;

    private Rigidbody2D _rigidbody;
    private Vector3 _originalScale;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _originalScale = _sprite.transform.localScale;

        if(!_squashParent)
            _squashParent = new GameObject(string.Format("_squash_{0}", name)).transform;
    }

    private void Update()
    {
        _sprite.parent = transform;
        _sprite.localPosition = Vector3.zero;
        _sprite.localScale = _originalScale;
        _sprite.localRotation = Quaternion.identity;

        _squashParent.localScale = Vector3.one;
        _squashParent.position = transform.position;

        Vector3 velocity = _rigidbody.velocity;
        if (velocity.sqrMagnitude > 0.01f)
        {
            _squashParent.rotation = Quaternion.FromToRotation(Vector3.right, velocity);
        }

        var scaleX = 1.0f + (velocity.magnitude * _stretch);
        var scaleY = 1.0f / scaleX;
        _sprite.parent = _squashParent;
        _squashParent.localScale = new Vector3(scaleX, scaleY, 1.0f);
    }
}
