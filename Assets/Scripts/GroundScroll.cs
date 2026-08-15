using UnityEngine;

public class GroundScroll : MonoBehaviour
{
    float limitX;
    float width;
    float speed = 10f;
    float defaultX;

    private void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        defaultX = transform.position.x;
        limitX = defaultX - width;
    }
    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < limitX)
        {
            float overshoot = transform.position.x - limitX;
            transform.position = new Vector2(defaultX + overshoot, transform.position.y);
        }
    }
}
