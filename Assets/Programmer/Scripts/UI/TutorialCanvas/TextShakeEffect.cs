using TMPro;
using UnityEngine;

public class TextShakeEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text textMeshPro;      // Ссылка на TextMeshPro компонент
    [SerializeField] private float shakeInterval = 0.5f; // Интервал между сменой положения (в секундах)
    [SerializeField] private float shakeIntensity = 5f; // Максимальная амплитуда смещения вершин

    private TMP_TextInfo textInfo;
    private Vector3[][] originalVertices;
    private Vector3[][] modifiedVertices;
    private float timer;

    void Start()
    {
        if (textMeshPro == null)
            textMeshPro = GetComponent<TMP_Text>();

        textMeshPro.ForceMeshUpdate();
        textInfo = textMeshPro.textInfo;

        // Инициализация массивов для вершин
        originalVertices = new Vector3[textInfo.meshInfo.Length][];
        modifiedVertices = new Vector3[textInfo.meshInfo.Length][];
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            originalVertices[i] = textInfo.meshInfo[i].vertices.Clone() as Vector3[];
            modifiedVertices[i] = new Vector3[textInfo.meshInfo[i].vertices.Length];
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shakeInterval)
        {
            timer = 0f;
            ApplyRandomShake();
        }
    }

    void ApplyRandomShake()
    {
        // Перебираем все символы в тексте
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible)
                continue;

            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            int vertexIndex = charInfo.vertexIndex;
            int materialIndex = charInfo.materialReferenceIndex;

            // Получаем массив вершин символа
            Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

            // Генерируем случайное смещение для каждой вершины символа
            Vector3 randomOffset = new Vector3(
                Random.Range(-shakeIntensity, shakeIntensity),
                Random.Range(-shakeIntensity, shakeIntensity),
                0
            );

            for (int j = 0; j < 4; j++)
            {
                modifiedVertices[materialIndex][vertexIndex + j] = originalVertices[materialIndex][vertexIndex + j] + randomOffset;
            }
        }

        // Применяем изменения к тексту
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            TMP_MeshInfo meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = modifiedVertices[i];
            textMeshPro.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
