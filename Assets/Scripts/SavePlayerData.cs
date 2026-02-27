using TMPro;
using UnityEngine;

public class SavePlayerData : MonoBehaviour
{
    public TMP_InputField player1, player2;

    public void SaveData()
    {
        string ime1 = player1.text;
        string ime2 = player2.text;

        string skraceno1 = (ime1.Length >= 3 ? ime1.Substring(0, 3) : ime1).ToUpper();
        string skraceno2 = (ime2.Length >= 3 ? ime2.Substring(0, 3) : ime2).ToUpper();

        PlayerPrefs.SetString("ImePlayer1", ime1); 
        PlayerPrefs.SetString("ImePlayer2", ime2);
        PlayerPrefs.SetString("SkracenoPlayer1", skraceno1);
        PlayerPrefs.SetString("SkracenoPlayer2", skraceno2);
        PlayerPrefs.Save();

        Debug.Log("Player1: " + ime1 + " (" + skraceno1 + ") Player2: " + ime2 + " (" + skraceno2 + ")");
    }
}
