using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {

    private void Update()
    {
        gameObject.GetComponent<Text>().text = "Score: " + Persistant.persistant.score;
    }
}
