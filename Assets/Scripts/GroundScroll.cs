using UnityEngine;

public class GroundScroll : MonoBehaviour
{
    int limitX = -12;
    float speed = 10f;
    float defaultX;

    private void Start()
    {
        defaultX = transform.position.x;
    }
    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < limitX) {
            float overshoot = transform.position.x - limitX;
            transform.position = new Vector2 (defaultX + overshoot, transform.position.y);
        }
    }

}
