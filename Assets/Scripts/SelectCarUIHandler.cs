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

    bool isChangingCar = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnCarCO(true)); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCarCO(bool isCarAppearingOnRightSide)
    {
        isChangingCar = true;



        GameObject instantiatedCar = Instantiate(carPrefab, spawnOnTransform);

        

        yield return new WaitForSeconds(0.4f);

        isChangingCar = false;
    }
}
