using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    public int score = 100;
    public float speedMod = 0;
    public float jumpMod = 0;
    public bool changeHealth = false;
    public int healthSet = 1;
    public int damageMod = 0;
    public float projectileMod = 0;
    public Vector3 colourMod = new Vector3(1, 1, 1);

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
            if (changeHealth) { playerMain.health = healthSet; }
            Persistant.persistant.score += score;
            playerMain.GetComponentInChildren<Whack>(true).damage += damageMod;
            playerMain.projectileSpeed += projectileMod;
            playerMove.speed += speedMod;
            playerMove.jumpTakeOffSpeed += jumpMod;
            colourMod.Normalize();
            colourMod += (Vector3)playerMain.defaultColour.normalized;
            playerMain.defaultColour = new Vector4(colourMod.x, colourMod.y, colourMod.z, 1);
            playerMain.ChangeColour(playerMain.defaultColour);
            Persistant.persistant.spicesCollected++;
            Destroy(gameObject);
        }
    }
}
