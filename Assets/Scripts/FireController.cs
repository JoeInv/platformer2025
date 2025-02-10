using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float force = 100f;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = force * Vector2.right;
        Invoke("Die", 4f);
    } 

    void Die() 
    {
        Destroy(gameObject);
    }
}
