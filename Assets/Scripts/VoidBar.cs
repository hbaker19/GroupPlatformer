using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidBar : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable component = (IDamageable)collision.gameObject.GetComponent(typeof(IDamageable));
        if (component != null) { collision.gameObject.GetComponent<IDamageable>().GameOver(); }
        else { Destroy(collision.gameObject); }
    }
}
