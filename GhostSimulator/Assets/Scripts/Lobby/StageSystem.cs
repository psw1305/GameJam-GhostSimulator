using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageSystem : BehaviourSingleton<StageSystem>
{
    [SerializeField] private int maxStage = 4;
    private int stageSelect = 0;
    private string stageName = "Stage00";

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI stageTM;
    [SerializeField] private Button leftArrowButton;
    [SerializeField] private Button rightArrowButton;
    [SerializeField] private Button stageSelectButton;

    protected override void Awake()
    {
        base.Awake();

        this.leftArrowButton.onClick.AddListener(LeftStage);
        this.rightArrowButton.onClick.AddListener(RightStage);
        this.stageSelectButton.onClick.AddListener(StageSelect);
    }

    private void Start()
    {
        
    }

    private void LeftStage()
    {
        AudioSystem.Instance.PlaySFX(AudioSystem.Instance.click);

        if (this.stageSelect <= 0)
            this.stageSelect = this.maxStage - 1;
        else
            this.stageSelect--;

        StageChange(this.stageSelect);
    }

    private void RightStage()
    {
        AudioSystem.Instance.PlaySFX(AudioSystem.Instance.click);

        if (this.stageSelect >= this.maxStage - 1)
            this.stageSelect = 0;
        else
            this.stageSelect++;

        StageChange(this.stageSelect);
    }

    private void StageChange(int value)
    {
        this.stageName = "Stage0" + value;
        this.stageTM.text = "스테이지 " + value;
        this.transform.position = new Vector3(value * -25, 0, 0);
    }

    private void StageSelect()
    {
        AudioSystem.Instance.PlaySFX(AudioSystem.Instance.click);
        this.stageSelectButton.interactable = false;
        SceneLoader.Instance.LoadScene(this.stageName);
    }
}
