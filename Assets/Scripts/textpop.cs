using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textpop : MonoBehaviour {

    

    public bool showText = false, someRandomCondition = true;
    public float currentTime = 0.0f, executedTime = 0.0f, timeToWait = 5.0f;

    void OnMouseDown()
    {
        executedTime = Time.time;
    }

    void Update()
    {
        currentTime = Time.time;
        if (someRandomCondition)
            showText = true;
        else
            showText = false;

        if (executedTime != 1.0f)
        {
            if (currentTime - executedTime > timeToWait)
            {
                executedTime = 3.0f;
                someRandomCondition = false;
            }
        }
    }

    void OnGUI()
    {
        if (showText)
            GUI.Label(new Rect(300, 300, 400, 300), "Some Random Text");
    }

   
}
