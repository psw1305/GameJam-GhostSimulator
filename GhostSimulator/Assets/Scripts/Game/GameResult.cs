using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameResult : BehaviourSingleton<GameResult>
{
    public bool isResult { get; set; }
    [SerializeField] private string sceneName;

    [Header("Button")]
    [SerializeField] private Button lobbyButton;
    [SerializeField] private Button replayButton;

    [Header("Result")]
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private Transform resultBoard;
    [SerializeField] private Image resultLogo;
    [SerializeField] private TextMeshProUGUI resultTimerTM;
    [SerializeField] private Sprite clearLogo;
    [SerializeField] private Sprite overLogo;
    private Sequence sequence;

    protected override void Awake()
    {
        base.Awake();

        this.resultPanel.SetActive(false);
        this.lobbyButton.onClick.AddListener(Lobby);
        this.replayButton.onClick.AddListener(Replay);

        this.resultBoard.localScale = Vector3.zero;
        this.resultBoard.GetComponent<CanvasGroup>().alpha = 0;
    }

    private void ShowAnimation()
    {
        this.sequence = DOTween.Sequence()
            .Append(this.resultBoard.DOScale(1, 1).SetEase(Ease.OutBounce))
            .Join(this.resultBoard.GetComponent<CanvasGroup>().DOFade(1, 1))
            .OnComplete(() => Time.timeScale = 0);
    }

    private void Lobby()
    {
        AudioSystem.Instance.PlaySFX(AudioSystem.Instance.click);

        Time.timeScale = 1.0f;
        this.lobbyButton.interactable = false;
        SceneLoader.Instance.LoadScene(SceneName.Lobby);
    }

    private void Replay()
    {
        AudioSystem.Instance.PlaySFX(AudioSystem.Instance.click);

        Time.timeScale = 1.0f;
        this.replayButton.interactable = false;
        SceneLoader.Instance.LoadScene(this.sceneName);
    }

    public void GameClear()
    {
        if (this.isResult) return;

        AudioSystem.Instance.PlaySFX(AudioSystem.Instance.gameclear);
        GameStage.Instance.GamePlay = GameState.End;

        this.isResult = true;
        this.resultLogo.sprite = this.clearLogo;
        this.resultTimerTM.text = $"{GameStage.Instance.TimeCurrent:N1}" + "√ ";
        this.resultPanel.SetActive(true);
        ShowAnimation();
    }

    public void GameOver()
    {
        if (this.isResult) return;

        AudioSystem.Instance.PlaySFX(AudioSystem.Instance.gameover);
        GameStage.Instance.GamePlay = GameState.End;

        this.isResult = true;
        this.resultLogo.sprite = this.overLogo;
        this.resultTimerTM.text = "";
        this.resultPanel.SetActive(true);
        ShowAnimation();
    }

    private void OnDisable()
    {
        if (DOTween.instance != null)
        {
            this.sequence.Kill();
        }
    }
}
