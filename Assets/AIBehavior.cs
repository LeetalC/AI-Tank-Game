using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PUT A README REGARDING HOW TO CHANGE NUM OF TEAMS
public class AIBehavior : MonoBehaviour
{
   // private GameObject[] EnemyTanks;
    List<GameObject> EnemyTanks = new List<GameObject>();
    TankPlayer thisTank;

    // Start is called before the first frame update
    void Start()
    {
        thisTank = gameObject.GetComponent<TankPlayer>();
         foreach(GameObject tank in GameObject.FindGameObjectsWithTag("Player")) {
            if(tank.GetComponent<TankPlayer>().get_team_id() != thisTank.get_team_id()) {
             EnemyTanks.Add(tank);
            }
         }
         FindEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FindEnemy() {
        if(EnemyTanks.Count != 0) {
            Debug.Log("Tank: " + thisTank.get_team_id() + " found: " + EnemyTanks[0].GetComponent<TankPlayer>().get_team_id());
        }
      //  thisTank.transform.rotation = EnemyTanks[0].GetComponent<BoxCollider>.bounds.center;
    }

    void Move() {

    }

}
