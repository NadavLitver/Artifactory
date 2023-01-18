using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
    public void LoadNextScene()
    {
      LeanTween.delayedCall(1 ,() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void LoadPrevScene()
    {
        LeanTween.delayedCall(1, () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1));
    }
    public void LoadTutorial()
    {
        LeanTween.delayedCall(1, () => SceneManager.LoadScene(2));

    }

    public void QuitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();

    }
}
