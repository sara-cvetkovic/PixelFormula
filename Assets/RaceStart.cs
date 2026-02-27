using System;
using UnityEngine;

public class RaceStart : MonoBehaviour
{
   public Vector2[] PlayerStartingPostions;
    public float[] PlayerStartingRotations;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitiateCars();
        Mediator.Instance.StartRace();  
    }

    private void InitiateCars()
    {
        CarData p1Car = Resources.Load<CarData>("CarData/" + PlayerPrefs.GetString("P1CAR"));
        CarData p2Car = Resources.Load<CarData>("CarData/" + PlayerPrefs.GetString("P2CAR"));
        if (p1Car == null || p2Car == null) { throw new UnityException("Resources Load did not find all the car scriptobjects. Please check if paths are correct.");
            return;
        }
        GameObject p1 = Instantiate(p1Car.CarPrefab, null);
        GameObject p2 = Instantiate(p2Car.CarPrefab, null);
        p1.GetComponent<bgigli>().rotationAngle = PlayerStartingRotations[0];
        p2.GetComponent<bgigli>().rotationAngle = PlayerStartingRotations[1];
        p1.GetComponent<CarInputHandler>().SetControls(KeyCode.W,KeyCode.A,KeyCode.S,KeyCode.D).transform.SetPositionAndRotation(PlayerStartingPostions[0],Quaternion.Euler(0,0, PlayerStartingRotations[0]));
        p2.GetComponent<CarInputHandler>().SetControls(KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow ).transform.SetPositionAndRotation(PlayerStartingPostions[1], Quaternion.Euler(0, 0, PlayerStartingRotations[1]));
        Mediator.Instance.Connect(p1);
        Mediator.Instance.Connect(p2);
       
    }
}
