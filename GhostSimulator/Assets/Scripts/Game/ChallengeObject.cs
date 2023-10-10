using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class ChallengeObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private Sprite normalImage;
    [SerializeField] private Sprite nearImage;
    [SerializeField] private ParticleSystem particle;
    
    private bool isTrigger = false;
    private bool isEnd = false;
    private Sequence sequence;

    private void Update()
    {
        if (this.isTrigger && !this.isEnd)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isEnd = true;

                GameChallenger.Instance.StarGet();

                this.sequence = DOTween.Sequence()
                    .Append(this.image.transform.DOScale(0, 0.5f).SetEase(Ease.OutBounce))
                    .OnComplete(() => StartCoroutine(AnimationCoroutine()));
            }
        }
    }

    private IEnumerator AnimationCoroutine()
    {
        this.particle.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.isTrigger = true;
            this.image.sprite = nearImage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.isTrigger = false;
            this.image.sprite = normalImage;
        }
    }
}
