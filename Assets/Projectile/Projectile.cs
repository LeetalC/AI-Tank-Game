using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody body;
    public float speed = 5.0f;
    private int player_id = 0;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * speed;
        // print("firing projectile with pid: " + player_id.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Wall")){
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player")){
            if(other.gameObject.GetComponent<TankPlayer>().get_pid() != player_id)
            Destroy(gameObject);
        }
    }

    public void set_pid(int pid){
        player_id = pid;
    }

    public int get_pid(){
        return player_id;
    }
}
