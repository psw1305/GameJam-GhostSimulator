using UnityEngine;

public class TargetAlert : MonoBehaviour
{
    [SerializeField] private TargetLevel targetIdle;
    private bool isPass = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.isPass) return;
            this.isPass = true;
            Target.Instance.Level = this.targetIdle;
        }
    }
}

