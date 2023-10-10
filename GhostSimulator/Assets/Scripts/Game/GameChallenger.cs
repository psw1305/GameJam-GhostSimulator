using UnityEngine;
using UnityEngine.UI;

public class GameChallenger : BehaviourSingleton<GameChallenger>
{
    [Header("Sprite")]
    [SerializeField] private Sprite starOn;

    [Header("UI")]
    [SerializeField] private Image[] stars;
    [SerializeField] private Image[] resultStars;

    private int starCount = 0;

    protected override void Awake()
    {
        base.Awake();
        this.starCount = 0;
    }

    public void StarGet()
    {
        AudioSystem.Instance.PlaySFX(AudioSystem.Instance.collect);

        this.stars[this.starCount].sprite = this.starOn;
        this.resultStars[this.starCount].sprite = this.starOn;
        this.starCount++;
    }
}
