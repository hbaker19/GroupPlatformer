using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    public int score = 100;
    public float speedMod = 0;
    public float jumpMod = 0;
    public int healthSet = 1;
    public int damageMod = 0;
    public float projectileMod = 0;
    public Vector4 colourMod = new Vector4(1, 1, 1, 1);

    private PlayerMain playerMain;
    private PlayerPhysObj playerMove;

    private void Awake()
    {
        playerMain = GameObject.Find("Player").GetComponent<PlayerMain>();
        playerMove = GameObject.Find("Player").GetComponent<PlayerPhysObj>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {

        }
    }
}
