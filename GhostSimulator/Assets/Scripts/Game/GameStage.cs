using UnityEngine;
using TMPro;
using System.Collections;

public enum GameState
{
    Tutorial, Play, Pause, Exit, End
}

public class GameStage : BehaviourSingleton<GameStage>
{
    public GameState GamePlay { get; set; }
    public float TimeCurrent { get; set; }

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI stageTM;
    [SerializeField] private TextMeshProUGUI timerTM;

    private float timeStart;

    private void Start()
    {
        if (GameTutorial.Instance != null) return;

        StartCoroutine(StageStart());
    }

    private void Update()
    {
        if (this.GamePlay == GameState.Play)
        {
            this.TimeCurrent = Time.time - this.timeStart;
            this.timerTM.text = $"{this.TimeCurrent:N1}" + "√ ";
        }
    }

    public void TimerStart()
    {
        this.GamePlay = GameState.Play;

        this.timeStart = Time.time;
        this.TimeCurrent = 0;
        this.timerTM.text = $"{this.TimeCurrent:N1}" + "√ ";
    }

    private IEnumerator StageStart()
    {
        yield return new WaitForSeconds(2.5f);

        TimerStart();
    }
}
