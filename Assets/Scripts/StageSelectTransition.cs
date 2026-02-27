using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectTransition : MonoBehaviour
{
    public SelectCarUIHandler[] selectCarUIHandlers;
    public void GoToStageSelect()
    {
         
        foreach (SelectCarUIHandler uiHandler in selectCarUIHandlers)
        {
            if (!uiHandler.LockInChoice()) { Debug.Log(uiHandler.name); return; }
        }

        SceneManager.LoadScene("IzborStaze");
    }
}
