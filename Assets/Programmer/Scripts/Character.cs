using UnityEngine;

public class Character : MonoBehaviour, IControlable
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField, Range(0, 1)] private float _inputInterpolation;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _jumpForce;
    
    private Rigidbody2D _rb;
    private BoxCollider2D _collider;

    private float _groundRayDistance;
    private float _moveInput;

    private bool _isGrounded;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();

        _groundRayDistance = (_collider.bounds.size.y / 2) + 0.02f;
    }

    private void Update()
    {
        RaycastHit2D();
    }

    private void RaycastHit2D()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, _groundRayDistance, _layerMask);

        if (hitInfo.collider != null) 
        {
            Debug.Log(hitInfo.collider.name);
            _isGrounded = true;
        }
        else
            _isGrounded = false;

    }

    public void DoMove(float x)
    {
        _moveInput = Mathf.Lerp(_moveInput, x, _inputInterpolation);
        Vector3 direction = new Vector3(_moveInput * _playerSpeed * Time.deltaTime, 0f, 0f);
        transform.position += direction;
    }
    public void DoJump()
    {
        if(_isGrounded == true)
            _rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - new Vector3(0f, _groundRayDistance, 0f));
    }
}