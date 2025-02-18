using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
   private int distance = 18;
    private int movement = 1;
    // Start is called before the first frame update
    void Start()
    {
      StartCoroutine("MoveObject");
    }

    IEnumerator MoveObject()
    {
        while (true)
        {
            transform.Translate(new Vector2(0,.2f) * movement);
            distance -= 1;
            if (distance <= 0)
            {
                distance = 18;
                movement *= -1;
            }
            yield return new WaitForSeconds(0.25f);
        }

    }
}

