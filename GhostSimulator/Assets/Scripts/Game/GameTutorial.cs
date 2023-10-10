using UnityEngine;

public class GameTutorial : BehaviourSingleton<GameTutorial>
{
    [SerializeField] private GameObject tutorialImage;
    [SerializeField] private GameStage gameStage;

    protected override void Awake()
    {
        base.Awake();

        this.gameStage.GamePlay = GameState.Tutorial;
    }

    public void Update()
    {
        if (this.gameStage.GamePlay == GameState.Tutorial)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                this.tutorialImage.SetActive(false);
                this.gameStage.TimerStart();
            }
        }
    }
}
