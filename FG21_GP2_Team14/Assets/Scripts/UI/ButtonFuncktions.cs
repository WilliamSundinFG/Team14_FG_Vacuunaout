using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFuncktions : MonoBehaviour
{
    
    public void Quit()
    {
        Application.Quit();
    }

    public void Starting()
    {
        SceneManager.LoadScene(1);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(0);
    }

    public void WinningScene()
    {
        SceneManager.LoadScene(2);
    }
}
