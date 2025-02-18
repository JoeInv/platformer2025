using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GhostController : MonoBehaviour
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
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 4f);
        RaycastHit2D wallHitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1f);
        RaycastHit2D wallHitRight = Physics2D.Raycast(transform.position, Vector2.right, 1f);
        //Debug.DrawRay(transform.position, new Vector2(0, -3), Color.red, 0.5f);

        
         if (((((wallHitLeft.collider != null) && !wallHitLeft.collider.CompareTag("Player")) //checks if it hits wall and makes sure it isn't player
         || (wallHitRight.collider != null) && !wallHitRight.collider.CompareTag("Player"))) && Time.time >= cooldown)
        {
            direction = direction * -1;
            rend.flipX = !rend.flipX;
            cooldown = Time.time + cooldownRate; //copied cooldown format for gun cooldown because enemy would get stuck
        }
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - 2 * direction, transform.position.y), Time.deltaTime);
        
     }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("fire"))
            Destroy (gameObject);
            

    }
}
