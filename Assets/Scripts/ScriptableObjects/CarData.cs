using UnityEngine;

[CreateAssetMenu(fileName = "New Car Data", menuName = "Car Data", order = 51)]
public class CarData : ScriptableObject
{
    [SerializeField]
    private int carUniqueID = 0;

    [SerializeField]
    private Sprite carUISprite;

    [SerializeField]
    private Sprite teamLogoSprite;

    [SerializeField]
    private GameObject carPrefab;

    public int CarUniqueID
    {
        get { return carUniqueID; }
    }
    public Sprite CarUISprite
    {
        get { return carUISprite; }
    }
    public Sprite TeamLogoSprite
    {
        get { return teamLogoSprite; }
    }
    public GameObject CarPrefab
    {
        get { return carPrefab; }
    }

}
