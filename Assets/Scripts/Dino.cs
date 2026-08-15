using UnityEngine;
using System;

public class Dino : MonoBehaviour
{
    public Rigidbody2D dinozor;
    private float jumpForce = 20f; // zıplama gücü
    private float originY;
    public Animator animation;

    private void Start() {
        originY = dinozor.linearVelocity.y; // karakterin başlangıçtaki yüksekliğini bir float değişkene (originY) atıyor
    }

    void Update()
    {
        float odds = MathF.Abs(dinozor.linearVelocity.y - originY); // zemin ile karakterin anlık yüksekliği arasındaki farkın mutlak değerini alıyor
        if (odds < 0.5f) {
            animation.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                Jump();
            } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                Jump();
            }
        } else {
            animation.enabled = false;
        }
    }

    private void Jump()
    { // zıplama fonksiyonu. belirlediğimiz zıplama gücünü (jumpForce) karakterin yükseklik değeri yerine koyuyor
        dinozor.linearVelocity = new Vector2(dinozor.linearVelocity.x, jumpForce);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cactus"))
        {
            Time.timeScale = 0f;
            Debug.Log("OYUN BİTTİ!");
        }
    }

}
