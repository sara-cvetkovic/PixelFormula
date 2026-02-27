using UnityEngine;

public class StageUI : MonoBehaviour
{
    public string DisplayName, SceneName;
    private StageSelector selector;
    public Vector3 CursorPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        selector = FindFirstObjectByType<StageSelector>();
        
    }
    public void Selected()
    {
        selector.SelectedStage = SceneName;
        selector.MoveCursor(CursorPosition);
    }
}
