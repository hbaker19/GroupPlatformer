using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth2 : MonoBehaviour {

    public int lives = 10;
	public int health = 10;

	void Start () {
        //When a new game begins, next line should run:
        //PlayerPrefs.SetInt("Lives", lives);
        lives = PlayerPrefs.GetInt("Lives");
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
            //lose a life and reload the level
            PlayerPrefs.SetInt("Lives", lives - 1);
			Scene scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene (scene.name);
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
        float yVelocity = GetComponent<Rigidbody2D>().velocity.y;
		if (collision.gameObject.tag == "Enemy" && yVelocity >= 0) {
			health -= 1;
		}else if (collision.gameObject.tag == "Enemy" && yVelocity < 0)
        {
            Destroy(collision.gameObject);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 600));
        }
	}
}
