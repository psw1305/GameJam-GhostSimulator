using System.Collections;
using UnityEngine;

public class Target : BehaviourSingleton<Target>
{
    [Header("Value")]
    [SerializeField] private float[] levelSense;
    [SerializeField] private float maxSensitivity = 100;
    public TargetLevel Level { get; set; }
    public float Sensitivity { get; set; }

    private bool isSeek = false;
    private bool isFind = false;

    [Header("Image")]
    [SerializeField] private SpriteRenderer target;
    [SerializeField] private Sprite[] targetLevels;
    [SerializeField] private GameObject bubble;
    [SerializeField] private SpriteRenderer bubbleIcon;
    [SerializeField] private Sprite[] bubbleIcons;

    protected override void Awake()
    {
        base.Awake();

        this.Level = TargetLevel.Level_1;
        this.Sensitivity = 30;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1.0f);

        while (this.Level != TargetLevel.Level_End)
        {
            if (GameStage.Instance.GamePlay == GameState.Play)
            {
                if (!this.isSeek)
                {
                    switch (this.Level)
                    {
                        case TargetLevel.Level_1:
                            yield return StartCoroutine(TargetSensitivity(this.levelSense[0], 1.0f));
                            break;
                        case TargetLevel.Level_2:
                            yield return StartCoroutine(TargetSensitivity(this.levelSense[1], 0.8f));
                            break;
                        case TargetLevel.Level_3:
                            yield return StartCoroutine(TargetSensitivity(this.levelSense[2], 0.6f));
                            break;
                    }
                }
                else
                {
                    yield return StartCoroutine(TargetSeeking());
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    private void Update()
    {
        if (this.isSeek)
        {
            if (Player.Instance.IsHide == false && !this.isFind)
            {
                this.isFind = true;
                GameResult.Instance.GameOver();
            }
        }
    }

    private IEnumerator TargetSensitivity(float plus, float delay)
    {
        if (this.Sensitivity > this.maxSensitivity)
        {
            this.target.sprite = this.targetLevels[2];
            this.isSeek = true;
            this.bubble.SetActive(false);
        }
        else if (this.Sensitivity > this.maxSensitivity * 0.75f)
        {
            this.target.sprite = this.targetLevels[1];

            this.bubbleIcon.sprite = this.bubbleIcons[Random.Range(0, this.bubbleIcons.Length)];
            this.bubble.SetActive(true);
        }
        else
        {
            this.target.sprite = this.targetLevels[0];
            this.bubble.SetActive(false);
        }

        yield return new WaitForSeconds(delay);

        this.Sensitivity += Random.Range(-10 + plus, 20 + plus);

        if (this.Sensitivity < 0) this.Sensitivity = 0;
    }

    private IEnumerator TargetSeeking()
    {
        var seekTime = Random.Range(0.4f, 1.3f);

        yield return new WaitForSeconds(seekTime);

        this.target.sprite = this.targetLevels[0];
        this.Sensitivity = 0;
        this.isSeek = false;
    }
}
