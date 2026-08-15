using UnityEngine;
using System;

public class Dino : MonoBehaviour
{
    public Rigidbody2D dinozor;
    public float jumpForce = 6f;
    private float originY;

    private void Start() {
        originY = dinozor.linearVelocity.y;
    }

    void Update()
    {
        float odds = MathF.Abs(dinozor.linearVelocity.y - originY);
        if (odds < 0.5f) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                Jump();
            } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                Jump();
            }
        }
    }

    private void Jump () {
        dinozor.linearVelocity = new Vector2(dinozor.linearVelocity.x, jumpForce);
    }
}
