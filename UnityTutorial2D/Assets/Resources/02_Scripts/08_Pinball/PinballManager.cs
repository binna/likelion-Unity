using UnityEngine;

public class PinballManager : MonoBehaviour
{
    public Rigidbody2D leftStickRigidbody2D;
    public Rigidbody2D rightStickRigidbody2D;

    public int totalScore = 0;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            leftStickRigidbody2D.AddTorque(30f);
        else
            leftStickRigidbody2D.AddTorque(-25f);
        
        if (Input.GetKey(KeyCode.RightArrow))
            rightStickRigidbody2D.AddTorque(-30f);
        else
            rightStickRigidbody2D.AddTorque(25f);
    }
}
