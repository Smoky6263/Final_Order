using AYellowpaper.SerializedCollections;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class TutorialCanvas : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;

    [SerializeField] private GameObject Canvas;

    [SerializedDictionary("Word", "Start and Target position")]
    public SerializedDictionary<GameObject, Vector3[]> _words;

    [SerializeField, Range(0f, 5f)] private float _overshoot = 1.7f;
    [SerializeField, Range(0f,1f)] private float _moveDuration = 0.3f;
    [SerializeField, Range(0f,1f)] private float _alphaDuration = 0.2f;

    private GameObject[] textGO;


    private void Start()
    {
        Canvas.SetActive(false);

        textGO = new GameObject[_words.Count];
        _words.Keys.CopyTo(textGO, 0);

        foreach (GameObject item in textGO)
        {
            TutorialText text = item.GetComponent<TutorialText>();
            text.Init(_alphaDuration);
        }
    }

    private void LateUpdate()
    {
        if(_player != null)
            transform.position = _player.position + _offset;
    }

    public void OnEnabled(Transform player)
    {
        _player = player;
        Canvas.SetActive(true);


        for (int i = 0; i < _words.Count; i++)
        {

            textGO[i].transform.localPosition = (Vector3)_words[textGO[i].gameObject].GetValue(0);
            textGO[i].transform.DOLocalMove((Vector3)_words[textGO[i].gameObject].GetValue(1), _moveDuration).SetEase(Ease.OutBack, _overshoot);
        }
    }

    public void OnDisabled()
    {
        _player = null;

        for (int i = 0; i < _words.Count; i++)
        {
            TutorialText text = textGO[i].GetComponent<TutorialText>();
            text.DoFadeIn(_alphaDuration);

            //textGO[i].transform.DOLocalMove((Vector3)_words[textGO[i].gameObject].GetValue(0), _moveDuration / 2f).SetEase(Ease.OutBack, _overshoot).OnComplete(() =>
            //{
            //});

        }
        StartCoroutine(TurnOffCanvas(_alphaDuration));

    }

    private IEnumerator TurnOffCanvas(float time)
    {
        yield return new WaitForSeconds(time);

        Canvas.SetActive(false);

        yield break;
    }
}
