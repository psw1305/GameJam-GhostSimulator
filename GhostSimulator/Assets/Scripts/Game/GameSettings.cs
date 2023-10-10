using UnityEngine;
using UnityEngine.UI;

public class GameSettings : BehaviourSingleton<GameSettings>
{
    [Header("Audio")]
    [SerializeField] private Button bgmButton;
    [SerializeField] private Button sfxButton;
    [SerializeField] private Image bgmSound;
    [SerializeField] private Image sfxSound;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;

    [Header("UI")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private Button lobbyButton;

    protected override void Awake()
    {
        base.Awake();

        this.bgmButton.onClick.AddListener(BGMClick);
        this.sfxButton.onClick.AddListener(SFXClick);
        this.pauseButton.onClick.AddListener(GamePause);
        this.returnButton.onClick.AddListener(GameReturn);
        this.lobbyButton.onClick.AddListener(GoLobby);

        this.settingPanel.SetActive(false);
    }

    private void Start()
    {
        SetBGM();
        SetSFX();
    }

    private void SetBGM()
    {
        if (PlayerPrefs.GetInt("BGM") == 1)
            this.bgmSound.sprite = this.soundOn;
        else
            this.bgmSound.sprite = this.soundOff;
    }

    private void SetSFX()
    {
        if (PlayerPrefs.GetInt("SFX") == 1)
            this.sfxSound.sprite = this.soundOn;
        else
            this.sfxSound.sprite = this.soundOff;
    }

    private void BGMClick()
    {
        if (PlayerPrefs.GetInt("BGM") == 1)
            AudioSystem.Instance.SetBGM(0);
        else
            AudioSystem.Instance.SetBGM(1);

        SetBGM();

        AudioSystem.Instance.PlaySFX(AudioSystem.Instance.click);
    }

    private void SFXClick()
    {
        if (PlayerPrefs.GetInt("SFX") == 1)
            AudioSystem.Instance.SetSFX(0);
        else
            AudioSystem.Instance.SetSFX(1);

        SetSFX();

        AudioSystem.Instance.PlaySFX(AudioSystem.Instance.click);
    }

    private void GamePause()
    {
        AudioSystem.Instance.PlaySFX(AudioSystem.Instance.click);
        GameStage.Instance.GamePlay = GameState.Pause;
        Time.timeScale = 0;

        this.settingPanel.SetActive(true);
    }

    private void GameReturn()
    {
        AudioSystem.Instance.PlaySFX(AudioSystem.Instance.click);
        GameStage.Instance.GamePlay = GameState.Play;
        Time.timeScale = 1;

        this.settingPanel.SetActive(false);
    }

    private void GoLobby()
    {
        AudioSystem.Instance.PlaySFX(AudioSystem.Instance.click);
        GameStage.Instance.GamePlay = GameState.Exit;
        Time.timeScale = 1;

        SceneLoader.Instance.LoadScene(SceneName.Lobby);
    }
}
