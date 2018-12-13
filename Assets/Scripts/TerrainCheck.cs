using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCheck : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D collision)
    {
        Persistant.persistant.canChange = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Persistant.persistant.canChange = true;
    }
}
