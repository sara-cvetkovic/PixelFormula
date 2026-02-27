using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelector : MonoBehaviour
{
    
    public string SelectedStage;
    public RectTransform Cursor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GoToStage()
    { 
        SceneManager.LoadScene(SelectedStage);
    }
    public void MoveCursor(Vector2 postion)
    {
        Cursor.localPosition = postion;
    }
}
