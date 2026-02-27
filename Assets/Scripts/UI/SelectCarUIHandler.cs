using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCarUIHandler : MonoBehaviour
{
    [Header("Car prefab")]
    public GameObject carPrefab;

    [Header("Spawn on")]
    public Transform spawnOnTransform;

    [Header("Saves player's choice to...")]
    public string PlayerPrefsKey;

    [Header("Controls")]
    public KeyCode Left, Right;

    bool isChangingCar = false;

    CarData[] carDatas;

    int selectedCarIndex = 0;

    //Other components
    CarUIHandler carUIHandler = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Load the car data
        carDatas = Resources.LoadAll<CarData>("CarData/");
        
        StartCoroutine(SpawnCarCO(true)); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(Left))
        {
            OnPreviousCar();
        }
        else if (Input.GetKey(Right))
        {
            OnNextCar();
        }

    }
    public bool LockInChoice()
    {
        if (isChangingCar) return false;
        PlayerPrefs.SetString(PlayerPrefsKey, carDatas[selectedCarIndex].name);

        return true;
    }
   
    public void OnPreviousCar()
    {
        if (isChangingCar)
            return;

        selectedCarIndex--;

        if (selectedCarIndex < 0)
            selectedCarIndex = carDatas.Length - 1;

        StartCoroutine(SpawnCarCO(true));
    }

    public void OnNextCar()
    {
        if (isChangingCar)
            return;

        selectedCarIndex++;

        if (selectedCarIndex > carDatas.Length - 1)
            selectedCarIndex = 0;

        StartCoroutine(SpawnCarCO(false));
    }

    IEnumerator SpawnCarCO(bool isCarAppearingOnRightSide)
    {
        isChangingCar = true;

        if (carUIHandler != null)
            carUIHandler.StartCarExitAnimation(!isCarAppearingOnRightSide);

        GameObject instantiatedCar = Instantiate(carPrefab, spawnOnTransform);

        carUIHandler = instantiatedCar.GetComponent<CarUIHandler>();
         
        carUIHandler.SetupCar(carDatas[selectedCarIndex]);
        carUIHandler.StartCarEntranceAnimation(isCarAppearingOnRightSide);

        yield return new WaitForSeconds(0.4f);

        isChangingCar = false;
    }
}
