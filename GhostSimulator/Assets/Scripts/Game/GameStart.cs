using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Transform titleLogo;
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditButton;

    [Header("Panel")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject creditPanel;
    [SerializeField] private TextMeshProUGUI creditButtonText;

    private bool isCredit;
    private Sequence sequence;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        this.isCredit = false;

        this.startButton.onClick.AddListener(GameStartClick);
        this.creditButton.onClick.AddListener(GameCreditClick);

        this.sequence = DOTween.Sequence()
            .Append(this.titleLogo.DOLocalMoveY(60f, 1.6f).SetEase(Ease.OutSine))
            .Append(this.titleLogo.DOLocalMoveY(120f, 1.6f).SetEase(Ease.OutSine).SetDelay(0.1f))
            .SetLoops(-1, LoopType.Restart)
            .SetDelay(0.1f);
    }

    private void GameStartClick()
    {
        AudioSystem.Instance.PlaySFX(AudioSystem.Instance.click);

        this.startButton.interactable = false;
        this.creditButton.interactable = false;

        SceneLoader.Instance.LoadScene(SceneName.Lobby);
    }

    private void GameCreditClick()
    {
        AudioSystem.Instance.PlaySFX(AudioSystem.Instance.click);

        if (this.isCredit)
        {
            this.startPanel.SetActive(true);
            this.creditPanel.SetActive(false);

            this.creditButtonText.text = "CREDITS";
        }
        else
        {
            this.startPanel.SetActive(false);
            this.creditPanel.SetActive(true);

            this.creditButtonText.text = "³ª°¡±â";
        }

        this.isCredit = !this.isCredit;
    }


    private void OnDisable()
    {
        if (DOTween.instance != null)
        {
            this.sequence.Kill();
        }
    }
}
