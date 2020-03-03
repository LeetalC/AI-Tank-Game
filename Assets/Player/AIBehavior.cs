using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PUT A README REGARDING HOW TO CHANGE NUM OF TEAMS
public class AIBehavior : MonoBehaviour
{

    List<GameObject> EnemyTanks = new List<GameObject>();
    public static List<GameObject> AllTanks = new List<GameObject>();

    TankPlayer this_tank_component;
    private Quaternion look_at_enemy;
    private float time_varience = 0;
    public static bool winner_found = false;
    public static Color winner_color;

    public static bool bullet_failed = false;
    public CreateTeams winner_script;


    // Start is called before the first frame update
    void Start()
    {
        this_tank_component = gameObject.GetComponent<TankPlayer>();
         foreach(GameObject tank in GameObject.FindGameObjectsWithTag("Player")) {
            if(AllTanks.Contains(tank) == false) {
                AllTanks.Add(tank);
            }
            if(tank.GetComponent<TankPlayer>().get_team_id() != this_tank_component.get_team_id()) {
                EnemyTanks.Add(tank);
            }
         }
        InvokeRepeating("FindEnemy", Random.Range(.5f,2.0f), Random.Range(.5f, 3.0f));
    }

    void Update() {
        GetEnemies();
    }


    void GetEnemies() {
        AllTanks.RemoveAll(GameObject => GameObject == null);
        EnemyTanks.RemoveAll(GameObject => GameObject == null);
    }
    
    void FindEnemy() {
        if(EnemyTanks.Count > 0) {
            int random_enemy_index = Random.Range(0, EnemyTanks.Count);
            Vector3 center_of_enemy =  (EnemyTanks[random_enemy_index].GetComponent<BoxCollider>().bounds.center - gameObject.transform.position);
            look_at_enemy = Quaternion.LookRotation(center_of_enemy);
            this_tank_component.transform.rotation = look_at_enemy;
            if(AllTanks.Count <= 2 && bullet_failed == false) {
                Debug.Log(gameObject.GetComponent<TankPlayer>().player_color + " shot failed");
                bullet_failed = true;
            }
            else {
                this_tank_component.Shoot();
            }
        }
        else
        {

            winner_found = true;
            winner_color = gameObject.GetComponent<TankPlayer>().player_color;
            winner_script = FindObjectOfType<CreateTeams>();
            winner_script.DeclareWinner(winner_color);
            CancelInvoke("FindEnemy");
        }
    }

    public void ChangeDirection() {
        this_tank_component.transform.rotation = Quaternion.Euler (0, this_tank_component.current_y + 185.0f, 0);
        this_tank_component.current_y = this_tank_component.current_y + 185.0f;
    } 

}
