using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PUT A README REGARDING HOW TO CHANGE NUM OF TEAMS
public class AIBehavior : MonoBehaviour
{

    List<GameObject> EnemyTanks = new List<GameObject>();
    public static List<GameObject> AllTanks = new List<GameObject>();
    TankPlayer this_tank;
    bool enemy_targetted = false;
    bool is_valid_enemy = false;
    public static int i = 0;

    public GameObject pole;
    public Quaternion lookdir;

    // Start is called before the first frame update
    void Start()
    {
        this_tank = gameObject.GetComponent<TankPlayer>();
         foreach(GameObject tank in GameObject.FindGameObjectsWithTag("Player")) {
            AllTanks.Add(tank);
            if(tank.GetComponent<TankPlayer>().get_team_id() != this_tank.get_team_id()) {
                EnemyTanks.Add(tank);
            }
         }
        InvokeRepeating("FindEnemy", 2.0f, 3.0f);
    }

    void GetEnemies() {
        EnemyTanks.Clear();
        foreach(GameObject tank in AllTanks) {
            if(tank.GetComponent<TankPlayer>().get_team_id() != this_tank.get_team_id()) {
                EnemyTanks.Add(tank);
            }
        }
    }
    
    void FindEnemy() {
        if(EnemyTanks.Count > 0) {
            //if there is still an enemy remaining

        }
        enemy_targetted = true;
        Vector3 find_center =  (EnemyTanks[0].GetComponent<BoxCollider>().bounds.center - gameObject.transform.position).normalized;
        lookdir = Quaternion.LookRotation(find_center);
        this_tank.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, lookdir, Time.deltaTime * this_tank.rotation_speed);
        this_tank.Shoot();
    }

    public void RemoveTankFromList(GameObject tank) {
        AllTanks.Remove(tank);
        GetEnemies();
    }
        

    public void ChangeDirection() {
        this_tank.transform.rotation = Quaternion.Euler (0, this_tank.current_y + 185.0f, 0);
        this_tank.current_y = this_tank.current_y + 185.0f;
    } 

        // int i = 0;
        // if(AllTanks.length != 0) {
        //     foreach(GameObject tank in AllTanks) {
        //         if(tank.GetComponent<TankPlayer>().get_team_id() != this_tank.get_team_id())
        //     }
            
        // }

        // if(EnemyTanks.Count != 0) { //if array 
        //     // Debug.Log("Tank: " + this_tank.get_team_id() + " found: " + EnemyTanks[0].GetComponent<TankPlayer>().get_team_id());
        //     // while(i < EnemyTanks.length) {
        //     //     if(EnemyTanks[i] == null){
        //     //         i++;
        //     //     }
        //     //     else {
        //     //         break;
        //     //     }
        // }

}
