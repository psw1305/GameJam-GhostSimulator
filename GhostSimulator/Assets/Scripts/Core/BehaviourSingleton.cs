using UnityEngine;

public abstract class BehaviourSingleton<T> : MonoBehaviour where T : BehaviourSingleton<T>
{
    private static T instance = null;
    public static T Instance
    {
        get { return instance; }
    }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogError(string.Format("������ ���� �ߺ� �ν��Ͻ� => {0}", typeof(T)));
            Destroy(this);
            return;
        }

        instance = (T)this;
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}