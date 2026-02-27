using TMPro;
using UnityEngine;

public class LapCounterUI : MonoBehaviour
{
    public TMP_Text lapText;
    public bool isPlayer1;

    private CarLapCounter trackedCar;

    void Start()
    {
        string playerName = isPlayer1 ?
            PlayerPrefs.GetString("ImePlayer1") :
            PlayerPrefs.GetString("ImePlayer2");

        GameObject carObject = GameObject.Find(playerName);
        if (carObject != null)
            trackedCar = carObject.GetComponent<CarLapCounter>();
    }

    void Update()
    {
        if (trackedCar == null) return;

        int preostalo = trackedCar.GetLapsToComplete() - trackedCar.GetLapsCompleted();
        lapText.text = "Krugovi: " + preostalo;
    }
}