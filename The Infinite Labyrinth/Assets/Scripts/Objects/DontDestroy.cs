using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public string dontDestroyObjectTag;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(dontDestroyObjectTag);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
