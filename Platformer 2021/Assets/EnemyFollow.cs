using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 5f; // Adjust the speed as needed
    public string playerTag = "Player"; // Tag of the player GameObject

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the tag: " + playerTag);
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
