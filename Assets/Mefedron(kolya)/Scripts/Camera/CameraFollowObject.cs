using System.Collections;
using UnityEngine;
public class CameraFollowObject : MonoBehaviour
{
    [Header("Start Reference")]
    [SerializeField] private Transform _referenceTransform;

    [Header("Flip Rotation Stats")]
    [SerializeField] private float _flipYRotationTime = 0.5f;
    
    private GameManager _gameManager;
    private EventBus _eventBus;

    private bool _isFacingRight;

    private void Start()
    {
        _gameManager = GameManager.Instance;

        _eventBus = _gameManager.EventBus;
        _eventBus.Subscribe<CinemachineSetReferenceSignal>(SetNewTransformReference);
        _eventBus.Subscribe<CinemachineCallTurnSignal>(CallTurn);

        _isFacingRight = true;
    }
    private void Update() => transform.position = _referenceTransform.position;
    public void CallTurn(CinemachineCallTurnSignal signal) => StartCoroutine(FlipYLerp());

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotation = DeterminateEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while(elapsedTime < _flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotation, (elapsedTime / _flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }

    private float DeterminateEndRotation()
    {
        _isFacingRight = !_isFacingRight;

        if (_isFacingRight) return 0f;
        else return 180f;
    }

    /// <summary>
    /// указать новый объект для Cinemachine камеры, за которым она будет следовать.
    /// </summary>
    /// <param name="newReferenceTransform">ссылка на новый объект преследования камерой.</param>
    /// <param name="isFacingRight">здесь надо указать в какую сторону смотрит спрайт в данный момент.</param>
    private void SetNewTransformReference(CinemachineSetReferenceSignal signal)
    {
        _referenceTransform = signal.NewReferenceTransform;
        _isFacingRight = signal.IsFacingRight;
    }


}
