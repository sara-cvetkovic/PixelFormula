using UnityEngine;

public class StartLight : MonoBehaviour
{
    public Sprite On, Off;
    public int Id;
    public void TurnOn()
    {

        GetComponent<SpriteRenderer>().sprite = On;
    }
    void Start()
    {

        GetComponent<SpriteRenderer>().sprite = Off;
    }

}
