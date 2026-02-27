using TMPro;
using UnityEngine;

public class LoadImeIgraca : MonoBehaviour
{
    public TMP_Text playerText;
    public bool isPlayer1;

    void Start()
    {
        if (isPlayer1)
            playerText.text = PlayerPrefs.GetString("SkracenoPlayer1");
        else
            playerText.text = PlayerPrefs.GetString("SkracenoPlayer2");
    }
}
