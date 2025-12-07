using UnityEngine;

public class EndingController : MonoBehaviour
{
    public float endTime = 10f;

    void Start()
    {
        Invoke("Quit", endTime);
    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
