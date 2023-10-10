using System.Collections;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private float knockback = 5;
    [SerializeField] private float strength = 20;
    [SerializeField] private float delay = 0.1f;
    public bool IsCollision { get; set; }
    private Rigidbody rb;

    private void Start()
    {
        this.IsCollision = false;
        this.rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object_Heavy"))
        {
            AudioSystem.Instance.PlaySFX(AudioSystem.Instance.collision);

            this.IsCollision = true;
            Vector3 direction = (this.transform.position - other.transform.position).normalized;
            this.rb.AddForce(direction * this.knockback, ForceMode.Impulse);
            StartCoroutine(Reset());
        }
        else if (other.CompareTag("Object_Weak"))
        {
            AudioSystem.Instance.PlaySFX(AudioSystem.Instance.collision);

            this.IsCollision = true;
            Vector3 direction = (other.transform.position - this.transform.position).normalized;

            var otherRB = other.GetComponent<Rigidbody>();
            otherRB.AddForce(direction * this.strength, ForceMode.Impulse);
            StartCoroutine(Reset());
        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(this.delay);
        this.rb.velocity = Vector3.zero;
        IsCollision = false;
    }
}
