using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrPacMan : MonoBehaviour {

    public int score;
    public int pelletScore = 1;
    public int GhostEdibleScore = 4;
    public int lives;

    public int ghostsAte;

    public int HP;
    public bool GhostEdible;
    public float GhostEdibleTimer;

    public GameObject ghost1;
    public GameObject ghost2;
    public GameObject ghost3;
    public GameObject ghost4;

    private GameObject NavPoints;

    float speed = 1.0f;

    /*
    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;
    */
    private Animator animator;

	// Use this for initialization
	void Start () {
        NavPoints = GameObject.Find("NavPoints");

        animator = GetComponent<Animator>();
        ghostsAte = 1;
        lives = 3;
    }
	
	// Update is called once per frame
	void Update () {

        if (lives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Reloaded!");
        }
      
        if (Input.GetKeyDown("up")) //press button
            animator.SetTrigger("MoveUp");
        if (Input.GetKeyDown("down")) //press button
            animator.SetTrigger("MoveDown");
        if (Input.GetKeyDown("left")) //press button
            animator.SetTrigger("MoveLeft");
        if (Input.GetKeyDown("right")) //press button
            animator.SetTrigger("MoveRight");

        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;

        if (HP <= 0) {                                          //if dead
            animator.SetTrigger("Died");
        }

        if (GhostEdible == true) {
            GhostEdibleTimer = GhostEdibleTimer - 1 * Time.deltaTime;
            if (GhostEdibleTimer <= 0) {
                GhostEdible = false;
                GhostEdibleTimer = 5;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 9) {
            Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());
        }

        if (coll.gameObject.tag == "Ghost")// || (coll.gameObject.name == "Ghost2") || (coll.gameObject.name == "Ghost3") || (coll.gameObject.name == "Ghost4"))
        {
            if (GhostEdible == false)
            {
                lives--;
                transform.position = (new Vector3(0f, -0.8f, 0f)); //respawn
            }

            if(GhostEdible == true)
            {
                score = score + (GhostEdibleScore * ghostsAte);
                ghostsAte = ghostsAte + 1;
            }
        }

        if (coll.gameObject.tag == "PowerUpDot") {
            GhostEdible = true;
            Destroy(coll.gameObject);
        }

        if (coll.gameObject.tag == "pellet") {
            score = score + pelletScore;
            Destroy(coll.gameObject);
        }

        //if (coll.gameObject.tag == "Wall") {
            //transform.Translate(new Vector3(0f, -0.2f, 0f));   //move
            
        //}
    }

}
