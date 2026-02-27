using UnityEngine;

public class CarInputHandler : MonoBehaviour
{

    [Header("Kontrole")]
    public KeyCode upKey = KeyCode.UpArrow;
    public KeyCode downKey = KeyCode.DownArrow;
    public KeyCode leftKey = KeyCode.LeftArrow;
    public KeyCode rightKey = KeyCode.RightArrow;

    bgigli CarControler;

    void Awake()
    {
        CarControler = GetComponent<bgigli>();
    }

    public CarInputHandler SetControls(KeyCode up, KeyCode left, KeyCode down, KeyCode right) { upKey = up;leftKey = left;downKey = down;rightKey = right; return this; }

    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        // Horizontal input (levo/desno)
        if (Input.GetKey(leftKey))
            inputVector.x = -1f;
        else if (Input.GetKey(rightKey))
            inputVector.x = 1f;

        // Vertical input (napred/nazad)
        if (Input.GetKey(upKey))
            inputVector.y = 1f;
        else if (Input.GetKey(downKey))
            inputVector.y = -1f;

        // Send the input to the car controller
        CarControler.SetInputVector(inputVector);
    }
}
