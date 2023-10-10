using UnityEngine;

public class TargetComplete : MonoBehaviour
{
    private bool isComplete = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.isComplete) return;

            this.isComplete = true;
            GameResult.Instance.GameClear();
        }
        else if (other.CompareTag("Object_Weak"))
        {
            if (this.isComplete) return;

            this.isComplete = true;
            GameResult.Instance.GameOver();
        }
    }
}
