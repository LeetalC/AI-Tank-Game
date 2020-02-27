using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PUT A README REGARDING HOW TO CHANGE NUM OF TEAMS
public class AIBehavior : MonoBehaviour
{

    List<GameObject> EnemyTanks = new List<GameObject>();
    //public static List<GameObject> AllTanks = GameObject.FindGameObjectsWithTag("Player");
    TankPlayer this_tank;
    bool enemy_targetted = false;
    bool is_valid_enemy = false;
    public static int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        this_tank = gameObject.GetComponent<TankPlayer>();
        //  foreach(GameObject tank in GameObject.FindGameObjectsWithTag("Player")) {
        //     if(tank.GetComponent<TankPlayer>().get_team_id() != this_tank.get_team_id()) {
        //         EnemyTanks.Add(tank);
        //     }
        //  }
        FindEnemy();
    }

    // Update is called once per frame
    void Update()
    {
       this_tank.Move();

    }
    void FixedUpdate() {
       // FindEnemy();
    }
    void TargetEnemy() {
        enemy_targetted = true;
       // this_tank.transform.rotation = Quaternion.Euler (find_center);
    }
    void FindEnemy() {
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
       // Vector3 find_center =  EnemyTanks[i].GetComponent<BoxCollider>().bounds.center;
        TargetEnemy();
    }
        
        

    public void ChangeDirection() {
        this_tank.transform.rotation = Quaternion.Euler (0, this_tank.current_y + 185.0f, 0);
        this_tank.current_y = this_tank.current_y + 185.0f;
    } 



}
