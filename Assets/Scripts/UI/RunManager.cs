
using UnityEngine.SceneManagement;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    public void ClickPlay()
    {
        SceneManager.LoadScene("GameRun");
    }
}
