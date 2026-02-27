using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel; 

    public void ShowPanel()
    {
        panel.SetActive(true);
    }
}
