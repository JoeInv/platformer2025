using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesController : MonoBehaviour
{
    public TMP_Text livesTxt;

    // Start is called before the first frame update
    void Start()
    {
        livesTxt.SetText("Lives: " + GameManager.instance.GetLives());  
    }

    // Update is called once per frame
    void Update()
    {
        livesTxt.SetText("Lives: " + GameManager.instance.GetLives());  
    }
}
