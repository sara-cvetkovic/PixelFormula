using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsButtons : MonoBehaviour
{
    public void GoToScene1()
    {
        Debug.Log("GoToScene1 pozvano");
        SceneManager.LoadScene(0);
    }

    public void GoToScene2()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToScene3()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Rematch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}