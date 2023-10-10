using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoader : BehaviourSingleton<SceneLoader>
{
    [SerializeField] private Transform[] curtains;
    [SerializeField] private GameObject blind;

    public void LoadScene(string sceneName)
    {
        StopAllCoroutines();
        StartCoroutine(AnimationFadeScene(sceneName));
    }

    private IEnumerator SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        yield return new WaitForSeconds(1.0f);
    }

    private IEnumerator AnimationFadeScene(string sceneName)
    {
        yield return StartCoroutine(CurtainBeginAnimation());

        yield return StartCoroutine(SceneLoad(sceneName));

        yield return StartCoroutine(CurtainEndAnimation());
    }

    private IEnumerator CurtainBeginAnimation()
    {
        this.blind.SetActive(true);

        int moveX = 450;
        int max = this.curtains.Length - 1;

        for (int i = 0; i < this.curtains.Length; i++)
        {
            for (int k = max; k >= 0; k--)
            {
                this.curtains[k].DOLocalMoveX(moveX, 0.3f).SetEase(Ease.OutSine);
            }

            yield return new WaitForSeconds(0.3f);

            max -= 1;
            moveX += 450;
        }
    }

    private IEnumerator CurtainEndAnimation()
    {
        int moveX = 1800;
        int max = 1;

        for (int i = this.curtains.Length - 1; i >= 0; i--)
        {
            for (int k = 0; k < max; k++)
            {
                this.curtains[k].DOLocalMoveX(moveX, 0.3f).SetEase(Ease.OutSine);
            }

            yield return new WaitForSeconds(0.3f);

            max += 1;
            moveX -= 450;
        }

        this.blind.SetActive(false);
    }
}
