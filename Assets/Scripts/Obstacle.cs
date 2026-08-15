using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float speed = 10f;


    private void Update() {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -15f) {
            Destroy(gameObject);
        }
    }
}
