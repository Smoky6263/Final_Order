using System.Collections;
using UnityEngine;

public class ShakerScript : MonoBehaviour
{
    public IEnumerator ShakeSpriteRendererCoroutine(Transform transform, SpriteRenderer sprite, Material currentMaterial, float duration, float intensity)
    {
        sprite.material = GameManager.Instance.GetComponent<VFXManager>().EnemyDamageMaterial();
        Vector3 originalPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * intensity;
            float offsetY = Random.Range(-1f, 1f) * intensity;

            transform.position = originalPosition + new Vector3(offsetX, offsetY, 0f);
            elapsedTime += Time.deltaTime;


            yield return new WaitForFixedUpdate();
        }
        
        transform.position = originalPosition;
        sprite.material = currentMaterial;

        yield break;
    }
    public IEnumerator ShakeImageCoroutine(RectTransform transform, float duration, float intensity)
    {
        Vector3 originalPosition = transform.position;
        Vector3 originalScale = transform.localScale;
        transform.localScale *= 1.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Vector3 scale = Vector3.Lerp(transform.localScale, originalScale, elapsedTime / duration);
            intensity = Mathf.Lerp(5f, 0f, elapsedTime / duration);

            float offsetX = Random.Range(-1f, 1f) * intensity;
            float offsetY = Random.Range(-1f, 1f) * intensity;

            transform.position = originalPosition + new Vector3(offsetX, offsetY, 0f);
            transform.localScale = scale;
            elapsedTime += Time.deltaTime;


            yield return new WaitForFixedUpdate();
        }

        transform.position = originalPosition;

        yield break;
    }

    private void OnDestroy() => StopAllCoroutines();
}
