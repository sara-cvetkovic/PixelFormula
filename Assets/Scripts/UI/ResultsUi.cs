using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsUi : MonoBehaviour
{
    public TMP_Text pobednikText;
    public TMP_Text gubitnikText;
    public Image logoPrvo;
    public Image logoDrugo;

    public TMP_Text stazaText;

    void Start()
    {
        stazaText.text = PlayerPrefs.GetString("NazivStaze");

        string pobednik = PlayerPrefs.GetString("Pobednik");
        string skraceno1 = PlayerPrefs.GetString("SkracenoPlayer1");
        string skraceno2 = PlayerPrefs.GetString("SkracenoPlayer2");
        string p1 = PlayerPrefs.GetString("ImePlayer1");
        string p2 = PlayerPrefs.GetString("ImePlayer2");

        CarData p1Car = Resources.Load<CarData>("CarData/" + PlayerPrefs.GetString("P1CAR"));
        CarData p2Car = Resources.Load<CarData>("CarData/" + PlayerPrefs.GetString("P2CAR"));


        if (pobednik == skraceno1)
        {
            pobednikText.text = "1. " + p1;
            gubitnikText.text = "2. " + p2;
            if (p1Car != null) logoPrvo.sprite = p1Car.TeamLogoSprite;
            if (p2Car != null) logoDrugo.sprite = p2Car.TeamLogoSprite;
        }
        else
        {
            pobednikText.text = "1. " + p2;
            gubitnikText.text = "2. " + p1;
            if (p2Car != null) logoPrvo.sprite = p2Car.TeamLogoSprite;
            if (p1Car != null) logoDrugo.sprite = p1Car.TeamLogoSprite;
        }
    }
}