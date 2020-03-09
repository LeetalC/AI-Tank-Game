using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayer : MonoBehaviour
{
    public Rigidbody body;

    private GameObject proj_ref;
    
    public float speed = 10.0f;
    public float rotation_speed = 10.0f;

    public GameObject projectile;
    public float proj_offset = 1;


    private bool alive = true;
    private Vector3 player_position;
    public Material player_mat;
    public Color player_color;

    public int team_id;
    public int player_ID;
    public GameObject spawn_bounds;
    public float random_rotation_y;


    void SpawnPlayer() {
        Bounds bounds = spawn_bounds.GetComponent<BoxCollider>().bounds;
        GetComponent<Transform>().position = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            0.55f,
            Random.Range(bounds.min.z, bounds.max.z)
        );
        random_rotation_y = Random.Range(0,360);
        transform.rotation = Quaternion.Euler( 0, random_rotation_y, 0); //chooses a random rotation at the start
    }

    void Start() { 
        SpawnPlayer();
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        Move();
    }

    public void Move() {

        body.velocity = speed * transform.forward;

    }


    void Update()
    {
        if (Input.GetButtonDown("SHOOT_")){
            Shoot();
        }

    }


    //LEETAL: The logic in this function is copy/pasted from Update()
    public void Shoot() {
            proj_ref = Instantiate(projectile, transform.position + transform.forward * proj_offset, transform.rotation);
          
            proj_ref.GetComponent<Projectile>().set_pid(player_ID);
            proj_ref.GetComponent<Projectile>().set_team_id(team_id);
            proj_ref.GetComponent<Renderer>().material.SetColor("_Color", player_color);
            proj_ref.GetComponent<Renderer>().material.SetColor("_EmmisionColor", player_color);
            proj_ref.transform.Find("Point Light").GetComponent<Light>().color = player_color;
            
            proj_ref.SetActive(true);
    }


    //LEETAL: made this function because sometimes a tank would continue to collide with a wall
    void OnCollisionStay(Collision other) {
        if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player")) {
            gameObject.GetComponent<AIBehavior>().ChangeDirection();
            body.AddForce(Random.Range(0.0f, 40.0f), 0, Random.Range(0.0f,40.0f), ForceMode.Impulse);
        }
    }

    

    string get_pid_str(){
        return player_ID.ToString();
    }

    public int get_pid(){
        return player_ID;
    }


    //LEETAL
    public int get_team_id() {
        return team_id;
    }

    public Vector3 get_player_position() {
        return player_position;
    }

    public void set_is_alive(bool b) {
        alive = b;
    }

    public bool get_is_alive(){
        return alive;
    }
 
}

//LEETAL: This function is for respawning, but the game doesn't need this -- refactored the logic for 'spawn_bounds instead!

// Called when player is hit from another bullet
//    This will wait "wait" seconds before randomly placing the player
//    within some BoxCollider. Re-enable it's lights and set alive to true.
//    Re-enables player control.

//IEnumerator respawn_delayed(int wait)
//{
//    yield return new WaitForSeconds(wait);
//    Bounds rb_bounds = respawn_bounds.GetComponent<BoxCollider>().bounds;

//    GetComponent<Transform>().position = new Vector3(
//        Random.Range(rb_bounds.min.x, rb_bounds.max.x),
//        0.55f,
//        Random.Range(rb_bounds.min.z, rb_bounds.max.z)
//    );

//    GetComponent<TankPlayer>().enabled = true;
//    light_comp.enabled = true;
//    alive = true;

//    gameObject.SetActive(true);
//}

