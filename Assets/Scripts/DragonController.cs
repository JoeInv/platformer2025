using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    private float cooldown = 0;
    private float cooldownRate = 0.6f;
    private int direction = 1;
    private SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Debug.Log(cooldown);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        RaycastHit2D wallHitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1f);
        RaycastHit2D wallHitRight = Physics2D.Raycast(transform.position, Vector2.right, 1f);
        //Debug.DrawRay(transform.position, new Vector2(0, -3), Color.red, 0.5f);

        if (((hit.collider == null ||
         ((wallHitLeft.collider != null) && !wallHitLeft.collider.CompareTag("Player")) //checks if it hits wall and makes sure it isn't player
         || (wallHitRight.collider != null) && !wallHitRight.collider.CompareTag("Player"))) && Time.time >= cooldown)
        {
            direction = direction * -1;
            rend.flipX = !rend.flipX;
            cooldown = Time.time + cooldownRate; //copied cooldown format for gun cooldown because enemy would get stuck
        }
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - 1 * direction, transform.position.y), Time.deltaTime);
        RaycastHit2D headHit = Physics2D.Raycast(transform.position, Vector2.up, 1f);
        if (hit.collider != null || wallHitRight.collider != null || wallHitRight.collider != null)
        {
            if (headHit.collider != null && headHit.collider.gameObject.CompareTag("Player")) //Kill enemy by jumping on it
            {
                Destroy(gameObject);
            }
        }
        
     }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("fire"))
            Destroy (gameObject);
            

    }
}
