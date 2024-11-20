using System.Collections;
using UnityEngine;

public class BreakebleWallController : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private VFXManager _vFXManager;

    [Header("Статы для тряски стены")]
    [SerializeField] private float _duration = 0.5f;
    [SerializeField, Range(0f, 1f)] float _intensity = 0.5f;


    public void GetDamage(float value)
    {
        _health -= value;

        StartCoroutine(ShakeWallCoroutine(_duration, _intensity));

        if (_health < 0)
            Destroy(gameObject);
    }

    private IEnumerator ShakeWallCoroutine(float duration, float intensity)
    {
        Material currentMaterial = GetComponent<SpriteRenderer>().material;
        Vector3 originalPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * intensity;
            float offsetY = Random.Range(-1f, 1f) * intensity;

            transform.position = originalPosition + new Vector3(offsetX, offsetY, 0f);
            elapsedTime += Time.deltaTime;

            GetComponent<SpriteRenderer>().material = _vFXManager.GetDamageMaterial();

            yield return new WaitForFixedUpdate();
        }
        
        transform.position = originalPosition;
        GetComponent<SpriteRenderer>().material = currentMaterial;

        yield break;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
