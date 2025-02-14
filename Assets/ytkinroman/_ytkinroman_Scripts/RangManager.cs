using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using VContainer;


public class RangManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public StringListDescruption _description;
    public int _inxDescription;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _textStory;
    [SerializeField] private TextMeshProUGUI _textTotal;
    [SerializeField] private TextMeshProUGUI _textRang;
    [SerializeField] private LevelIndex _levelIndex;
    private EventBus _eventBus;

    private void Awake ()
    {
        _eventBus = _gameManager.EventBus;
        _eventBus.Subscribe<RangValueChangeSignal>(OnRangValueSet);
    }


    public void OnPointerEnter (PointerEventData eventData)
    {
        _textStory.gameObject.SetActive(true);
        _textTotal.gameObject.SetActive(true);
        _textRang.gameObject.SetActive(true);

        _eventBus.Invoke(new RangEnableSignal(_levelIndex));

        _textStory.text = _description.stringList[_inxDescription];
    }


    public void OnPointerExit (PointerEventData eventData)
    {
        _textStory.gameObject.SetActive(false);
        _textTotal.gameObject.SetActive(false);
        _textRang.gameObject.SetActive(false);
    }


    public void OnRangValueSet (RangValueChangeSignal signal)
    {
        LevelData levelData = signal.LevelData;

        string _totalScoreText = levelData.totalScore.ToString();
        string _totalRangText = levelData.totalRang;

        _textTotal.text = $"Combo score: {_totalScoreText}";
        _textRang.text = $"YOUR RANG: {_totalRangText}";
    }
}
