using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : BehaviourSingleton<Player>
{
    [Header("Move Action")]
    [SerializeField] private Rigidbody moveObject;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 300f;
    private Vector3 movement;

    [Header("Hide Action")]
    [SerializeField] private float hideMaxFloat = 100;
    [SerializeField] private float hideUseSpeed = 20;
    [SerializeField] private float hideChargeSpeed = 10;
    private float hideFloat;
    public bool IsHide { get; set; }
    private bool isBurnOut = false;

    [Header("Hide UI")]
    [SerializeField] private Image hideGage;
    [SerializeField] private Image hideIcon;
    [SerializeField] private Sprite hideNormalSprite;
    [SerializeField] private Sprite hideBurnOutSprite;

    [Header("Sprite")]
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    private Sequence sequence;

    [Header("Script")]
    [SerializeField] private PlayerCollider playerCollider;

    protected override void Awake()
    {
        base.Awake();

        this.hideFloat = this.hideMaxFloat;
        this.IsHide = false;

        this.sequence = DOTween.Sequence()
            .Append(this.sprite.transform.DOLocalMoveY(0.13f, 0.8f).SetEase(Ease.OutSine))
            .Append(this.sprite.transform.DOLocalMoveY(0.0f, 0.8f).SetEase(Ease.OutSine).SetDelay(0.2f))
            .SetLoops(-1, LoopType.Restart)
            .SetDelay(0.2f);
    }

    private void Update()
    {
        if (GameStage.Instance.GamePlay != GameState.Play) return;

        GatherInput();
        Look();
        Hide();
    }

    private void FixedUpdate()
    {
        if (GameStage.Instance.GamePlay != GameState.Play) return;

        this.spriteTransform.position = this.moveObject.position;

        if (this.playerCollider.IsCollision == false)
        {
            if (this.IsHide) 
                this.sprite.color = new Color32(255, 255, 255, 128);
            else
                this.sprite.color = Color.white;

            Move();
        }
    }

    private void GatherInput()
    {
        var hAxis = Input.GetAxisRaw("Horizontal");
        var vAxis = Input.GetAxisRaw("Vertical");

        this.movement = new Vector3(hAxis, 0, vAxis);

        this.animator.SetFloat("PosX", hAxis);
        this.animator.SetFloat("PosY", vAxis);
    }

    private void Move()
    {
        var currentSpeed = this.moveSpeed * (this.IsHide ? 0.0f : 1.0f);
        this.moveObject.MovePosition(this.moveObject.position + this.moveObject.transform.forward * this.movement.normalized.magnitude * currentSpeed * Time.deltaTime);
    }

    private void Look()
    {
        if (this.movement == Vector3.zero) return;

        var rot = Quaternion.LookRotation(this.movement.ToIso(), Vector3.up);
        this.moveObject.rotation = Quaternion.RotateTowards(this.moveObject.rotation, rot, this.turnSpeed * Time.deltaTime);     
    }

    private void Hide()
    {
        this.hideGage.fillAmount = this.hideFloat / this.hideMaxFloat;

        if (Input.GetKey(KeyCode.Space) && !this.isBurnOut)
        {
            this.IsHide = true;

            if (this.hideFloat > 0)
                this.hideFloat -= Time.deltaTime * this.hideUseSpeed;
            else if (this.hideFloat <= 0)
            {
                this.hideFloat = 0;
                StartCoroutine(BurnOut());
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) && !this.isBurnOut)
        {
            this.IsHide = false;
        }
        else
        {
            if (this.hideFloat < this.hideMaxFloat)
            {
                if (!this.isBurnOut)
                    this.hideFloat += Time.deltaTime * this.hideChargeSpeed;
                else
                    this.hideFloat += Time.deltaTime * this.hideChargeSpeed * 2.5f;
            }
            else
                this.hideFloat = this.hideMaxFloat;
        }
    }

    private IEnumerator BurnOut()
    {
        this.isBurnOut = true;
        this.hideIcon.sprite = this.hideBurnOutSprite;

        this.IsHide = false;
        yield return new WaitForSeconds(2);
        
        this.isBurnOut = false;
        this.hideIcon.sprite = this.hideNormalSprite;
    }

    private void OnDisable()
    {
        if (DOTween.instance != null)
        {
            this.sequence.Kill();
        }
    }
}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
