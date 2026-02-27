using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarLapCounter : MonoBehaviour
{
    public Text carPositionText;

    int passedCheckPointNumber = 0;
    float timeAtLastPassedCheckPoint = 0;

    int numberOfPassedCheckpoints = 0;

    int lapsCompleted = 0;
    const int lapsToComplete = 1;

    bool isRaceCompleted = false;

    int carPosition = 0;

    bool isHideRoutineRunning = false;
    float hideUIDelayTime;

    //Events
    public event Action<CarLapCounter> OnPassCheckpoint;

    public void SetCarPosition(int position)
    {
        carPosition = position;
    }

    public int GetNumberOfCheckpointsPassed()
    {
        return numberOfPassedCheckpoints;
    }

    public float GetTimeAtLastCheckPoint()
    {
        return timeAtLastPassedCheckPoint;
    }

    public int GetLapsCompleted() => lapsCompleted;
    public int GetLapsToComplete() => lapsToComplete;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("CheckPoint"))
        {

            if (isRaceCompleted)
            {
                Debug.Log("Trka vec zavrsena, ignorisem");
                return;
            }

            CheckPoint checkPoint = collider2D.GetComponent<CheckPoint>();

            if (passedCheckPointNumber + 1 == checkPoint.checkPointNumber)
            {
                passedCheckPointNumber = checkPoint.checkPointNumber;
                numberOfPassedCheckpoints++;
                timeAtLastPassedCheckPoint = Time.time;

                if (checkPoint.isFinishLine)
                {
                    passedCheckPointNumber = 0;
                    lapsCompleted++;

                    if (lapsCompleted >= lapsToComplete)
                    {
                        isRaceCompleted = true;
                        PlayerPrefs.SetString("Pobednik", PlayerPrefs.GetString(
                            gameObject.name == PlayerPrefs.GetString("SkracenoPlayer1") ? "SkracenoPlayer1" : "SkracenoPlayer2"));
                        PlayerPrefs.Save();
                        StartCoroutine(LoadNextSceneDelay());
                    }
                }

                OnPassCheckpoint?.Invoke(this);
            }
        }
    }

    IEnumerator LoadNextSceneDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Rezultat");
    }
}
