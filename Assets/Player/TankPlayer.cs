using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayer : MonoBehaviour
{
    public Rigidbody body;  //i made this public
    public Vector3 moveVect = Vector3.zero; //i made this public


    private GameObject proj_ref;

    private Light light_comp;
    
    public int player_id = 0;   //the one that matters is player_ID, deleting this later
    public float speed = 10.0f;
    public float rotation_speed = 10.0f;
    public GameObject respawn_bounds;

    public GameObject projectile;
    public float proj_offset = 1;


    private bool alive = true;
    private Vector3 player_position;
    public Material player_mat;
    public int team_id;
    public int player_ID;
    public Color player_color;
    public GameObject spawn_bounds;
    public float current_y;


    void SpawnPlayer() {
        Bounds bounds = spawn_bounds.GetComponent<BoxCollider>().bounds;
        GetComponent<Transform>().position = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            0.55f,
            Random.Range(bounds.min.z, bounds.max.z)
        );
        current_y = Random.Range(0,360);
        transform.rotation = Quaternion.Euler( 0, current_y, 0); //chooses a random rotation at the start
    }

    // Standardizes the colors of the player and assigns variables to be used later
    void Start()
    { 
        //player_mat = GetComponent<Renderer>().material;
        SpawnPlayer();
        body = GetComponent<Rigidbody>();
       // light_comp = transform.Find("Point Light").GetComponent<Light>();
        //  light_comp.color = player_color;
       // player_mat.SetColor("_EmmisionColor", player_color);
       // player_mat.SetColor("_Color", player_color);
        


    }

    // Physics are applied here
    void FixedUpdate(){
        Move();
    }

    public void Move() {
        // Apply motion
      //   body.velocity = moveVect;

        body.velocity = speed * transform.forward;

        // Apply Rotation based on current velocity
        //  if (moveVect.x != 0 || moveVect.z != 0)
        //     transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, Mathf.Rad2Deg * Mathf.Atan2(moveVect.x, moveVect.z), 0.0f), rotation_speed);
    }


    void Update()
    {
        // Calculate Movement based on raw axis input
        //    Raw because Unity does some interpolation behind the scenes when
        //    the player changes their input. GetAxisRaw will ignore the interpolation
        //moveVect = new Vector3(Input.GetAxisRaw("HORIZONTAL_MOVE_"+get_pid_str()) * speed, body.velocity.y, Input.GetAxisRaw("VERTICAL_MOVE_"+get_pid_str()) * speed);        

        // Check for shoot button down
        //    For the sake of reusability, Input map has SHOOT_0 and SHOOT_1
        //    We just need to append the player's ID to SHOOT_ to get
        //    the appropriate check for input
        //
        //    Instances a projectile object slightly offset forward of the player,
        //    projectile is instanced disabled, and we assign variables to it.
        //    When it is ready, it will set it to active and will travel.
        if (Input.GetButtonDown("SHOOT_" + get_pid_str())){
            Shoot();
        }

    }


    //LEETAL: The logic in this function is copy/pasted from Update()
    public void Shoot() {
            proj_ref = Instantiate(projectile, transform.position + transform.forward * proj_offset, transform.rotation);
            
            proj_ref.GetComponent<Projectile>().set_pid(player_id);
            proj_ref.GetComponent<Projectile>().set_team_id(team_id);
            proj_ref.GetComponent<Renderer>().material.SetColor("_Color", player_color);
            proj_ref.GetComponent<Renderer>().material.SetColor("_EmmisionColor", player_color);
            proj_ref.transform.Find("Point Light").GetComponent<Light>().color = player_color;
            
            proj_ref.SetActive(true);
    }


    //LEETAL: This function isn't needed, taking care of 'death' in Projectle.cs
    // Called when any collision enters this object's collision
    void OnCollisionEnter(Collision other){

        if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player")) {
            gameObject.GetComponent<AIBehavior>().ChangeDirection();
        }


        if (other.gameObject.CompareTag("PlayerProjectile")){
            // Ensure that this projectile wasn't spawned by the player themselves
            if (other.gameObject.GetComponent<Projectile>().get_pid() != player_id){
                // If the player was alive when this occured, turn off their light and
                // start an asychronous call to respawn_delayed(wait_time).
                // Also disable the player control.
               if(alive){
                    
                    alive = false;
                    GetComponent<TankPlayer>().enabled = false;
                    light_comp.enabled = false;
                    StartCoroutine(respawn_delayed(3));
                }
            }
        }
    }

    void OnCollisionStay(Collision other) {
        if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player")) {
            gameObject.GetComponent<AIBehavior>().ChangeDirection();
        }
    }

    //LEETAL: This function is for respawning, but the game doesn't need this
    // Called when player is hit from another bullet
    //    This will wait "wait" seconds before randomly placing the player
    //    within some BoxCollider. Re-enable it's lights and set alive to true.
    //    Re-enables player control.
    IEnumerator respawn_delayed(int wait){
        yield return new WaitForSeconds(wait);
        Bounds rb_bounds = respawn_bounds.GetComponent<BoxCollider>().bounds;

        GetComponent<Transform>().position = new Vector3(
            Random.Range(rb_bounds.min.x, rb_bounds.max.x),
            0.55f,
            Random.Range(rb_bounds.min.z, rb_bounds.max.z)
        );

        GetComponent<TankPlayer>().enabled = true;
        light_comp.enabled = true;
        alive = true;

        //gameObject.SetActive(true);
    }


    string get_pid_str(){
        return player_id.ToString();
    }

    public int get_pid(){
        return player_id;
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

