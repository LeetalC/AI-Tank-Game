using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PUT A README REGARDING HOW TO CHANGE NUM OF TEAMS
public class AIBehavior : MonoBehaviour
{

    List<GameObject> EnemyTanks = new List<GameObject>();
    public static List<GameObject> AllTanks = new List<GameObject>();
    TankPlayer this_tank;
    public int k = 0;
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

    void FixedUpdate() {
        GetEnemies();
    }

    void GetEnemies() {
        EnemyTanks.Clear();
        AllTanks.RemoveAll(GameObject => GameObject == null);
        foreach(GameObject tank in AllTanks) {
            if(tank.GetComponent<TankPlayer>().get_team_id() != this_tank.get_team_id()) {
                EnemyTanks.Add(tank);
            }
        }
    }
    
    void FindEnemy() {
        if(EnemyTanks.Count > 0) {
            Vector3 find_center =  (EnemyTanks[k].GetComponent<BoxCollider>().bounds.center - gameObject.transform.position);
            lookdir = Quaternion.LookRotation(find_center);
            this_tank.transform.rotation = lookdir;
            this_tank.Shoot();
        }
        
    }

    public void ChangeDirection() {
        this_tank.transform.rotation = Quaternion.Euler (0, this_tank.current_y + 185.0f, 0);
        this_tank.current_y = this_tank.current_y + 185.0f;
    } 

}
