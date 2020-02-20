using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject projectile;
    public float offset;
    public int player_id = 0;
    // Start is called before the first frame update
    void Start() 
    {
        projectile.GetComponent<Projectile>().set_pid(player_id);
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetButtonDown("SHOOT_"+get_pid_str()) ){
            Instantiate(projectile, transform.position + transform.forward * offset, transform.rotation);
        }
    }

    System.String get_pid_str(){
        return player_id.ToString();
    }

    int get_pid(){
        return player_id;
    }
}
