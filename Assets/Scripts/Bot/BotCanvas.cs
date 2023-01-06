using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bot
{
    public class BotCanvas : MonoBehaviour
    {
        private const byte StartedDialogue = 0;

        [SerializeField] private GameObject cloudPanel;
        [SerializeField] private Button cloudButton;
        [SerializeField] private TextMeshProUGUI textInDialog;

        private int _numDialog;
        private float _interval = 2f;
        private Sequence _sequence;

        private readonly string[] _phrases = new[]
        {
            "Сәлем! Менің атым АНДРОГҮЛ!",
            "Сіз Орталық Азияның алғашқы Метаәлем ішіндесіз! <sprite=2>",
            "Құттықтаймыз және қош келдіңіз!",
            "Приветствую! Меня зовут АНДРОГҮЛ!",
            "Вы находитесь внутри первой Метавселенной Центральной Азии! <sprite=2>",
            "Поздравляем Вас и Добро пожаловать!"
        };

        private void Awake()
        {
            transform.GetComponent<Canvas>().worldCamera = Camera.main;
            Debug.Log("Awake");
        }

        private void OnEnable()
        {
            _numDialog = -1;
            _sequence = DOTween.Sequence();
            _sequence.Append(cloudPanel.transform.DOScale(0.01f, 0.01f).OnComplete(NextPhrase));
            _sequence.Append(cloudPanel.transform.DOScale(1, 1));
            _sequence.AppendInterval(_interval);
            _sequence.SetLoops(-1, LoopType.Restart);

            cloudButton.onClick.AddListener(() =>
            {
                Debug.Log("Clicked");
                _sequence.Restart();
            });
        }

        private void NextPhrase()
        {
            if (_phrases == null)
            {
                textInDialog.text = "Bye-Bye!";
                return;
            }

            _numDialog = (_numDialog + 1) % _phrases.Length;
            textInDialog.text = _phrases[_numDialog];
        }

        private void OnDisable()
        {
            DOTween.Clear();
            cloudButton.onClick.RemoveAllListeners();
            // _numDialog = StartedDialogue;
            // textInDialog.text = _phrases[_numDialog];
            cloudPanel.transform.localScale = Vector3.zero;
        }
    }
}