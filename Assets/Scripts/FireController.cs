using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float force = 8f;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Player");
        direction = player.GetComponent<PlayerController>().GetDirection();
        rb.velocity = force * direction;
        Invoke("Die", 4f);
    } 

    void Die() 
    {
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("enemy"))
            Die();
    }
}
