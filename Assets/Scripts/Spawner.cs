using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] arrayOfObstacles = new GameObject[5];
    private float spawningTime = 2f;
    private float currentTime = 0f;

    private void Update() {
        currentTime += Time.deltaTime;

        if (currentTime >= spawningTime) {
            int index = Random.Range(0, 5);
            Instantiate(arrayOfObstacles[index], transform.position, Quaternion.identity);
            currentTime = 0f;
        }
    }
}
