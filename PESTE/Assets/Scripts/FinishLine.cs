using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public GameManagerScript gameManager;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
            gameManager.victory();
        }
    }
}
