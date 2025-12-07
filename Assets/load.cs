using UnityEngine;
using UnityEngine.SceneManagement; 

public class load : MonoBehaviour
{
    public void LoadTargetScene()
    {
        SceneManager.LoadScene("map");
    }
}