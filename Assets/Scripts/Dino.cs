using UnityEngine;

public class Dino : MonoBehaviour
{

    public Rigidbody2D dinozor;
    public float jumpForce = 3.5f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump () {
        dinozor.linearVelocity = new Vector2(dinozor.linearVelocity.x, jumpForce);
    }

}
