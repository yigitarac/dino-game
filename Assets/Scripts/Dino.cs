using UnityEngine;

public class Dino : MonoBehaviour
{
    public Rigidbody2D dinozor;
    [SerializeField] private float jumpForce = 20f; // zıplama gücü (Serialize Field ekleyip Unity ortamından değiştirmeyi sağladım)
    public Animator animation;
    private BoxCollider2D collider;
    [SerializeField] private LayerMask Ground;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>(); // objeye ait BoxCollider lazım olduğunda referans almayı sağlar
    }

    void Update()
    {// mathf gerek kalmadı zaten Jump sadece yerde aktifleşeceği için
        if (GameManager.Instance == null || !GameManager.Instance.IsPlaying) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
            animation.enabled = true;
    }

    private void Jump()
    { // zıplama fonksiyonu. belirlediğimiz zıplama gücünü (jumpForce) karakterin yükseklik değeri yerine koyuyor
        if (isGrounded())
        {
            dinozor.linearVelocity = new Vector2(dinozor.linearVelocity.x, jumpForce);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {// Oyunun bitmesini sağlar
        if (collision.gameObject.CompareTag("Cactus"))
        {
            GameManager.Instance.EndGame();
            Debug.Log("OYUN BİTTİ!");
        }
    }

    private bool isGrounded()
    {// objenin altında layerı Ground olan obje var mı diye bakıyor (ZeminYuzey), eğer varsa true dönüyor
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, Ground);
        return raycastHit.collider != null;
    }
}
