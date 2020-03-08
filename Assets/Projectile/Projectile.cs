using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody body;
    [SerializeField]
    private float speed = 10.0f;

    private int player_id;
    private int team_id;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Wall")){
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("PlayerProjectile")) {
           Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Player")) {
           if(other.gameObject.GetComponent<TankPlayer>().get_team_id() != team_id) {
                Destroy(other.gameObject);
           }
           Destroy(gameObject);
        }
    }


    public void set_team_id(int tid){
        team_id = tid;
    }

    public int get_team_id(int tid){
        return team_id;
    }
    public void set_pid(int pid){
        player_id = pid;
    }

    public int get_pid(){
        return player_id;
    }
}
