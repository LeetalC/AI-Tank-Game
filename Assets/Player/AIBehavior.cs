using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PUT A README REGARDING HOW TO CHANGE NUM OF TEAMS
public class AIBehavior : MonoBehaviour
{

    List<GameObject> enemy_tanks = new List<GameObject>();
    public static List<GameObject> all_tanks = new List<GameObject>();

    TankPlayer this_tank_component;
    private Quaternion look_at_enemy;
    public static Color winner_color;

    public static bool bullet_failed = false;
    public CreateTeams winner_script;


    // Start is called before the first frame update
    void Start()
    {
        this_tank_component = gameObject.GetComponent<TankPlayer>();
         foreach(GameObject tank in GameObject.FindGameObjectsWithTag("Player")) {
            if(all_tanks.Contains(tank) == false) {
                all_tanks.Add(tank);
            }
            if(tank.GetComponent<TankPlayer>().get_team_id() != this_tank_component.get_team_id()) {
                enemy_tanks.Add(tank);
            }
         }

        
        InvokeRepeating("FindEnemy", Random.Range(.5f,2.0f), Random.Range(.5f, 2.5f));
    }

    void Update() {
        GetEnemies();
        CheckForWinner();


    }

    void CheckForWinner()
    {
        if (enemy_tanks.Count == 0)
        {
            winner_color = gameObject.GetComponent<TankPlayer>().player_color;
            winner_script = FindObjectOfType<CreateTeams>();
            winner_script.DeclareWinner(winner_color);
            CancelInvoke("FindEnemy");
        }
    }


    void GetEnemies() {
        all_tanks.RemoveAll(GameObject => GameObject == null);
        enemy_tanks.RemoveAll(GameObject => GameObject == null);
    }
    
    void FindEnemy() {
        if(enemy_tanks.Count > 0) {
            int random_enemy_index = Random.Range(0, enemy_tanks.Count);
            Vector3 center_of_enemy =  (enemy_tanks[random_enemy_index].GetComponent<BoxCollider>().bounds.center - gameObject.transform.position);
            look_at_enemy = Quaternion.LookRotation(center_of_enemy);
            this_tank_component.transform.rotation = look_at_enemy;

            if(all_tanks.Count <= 3 && bullet_failed == false) {
                Debug.Log(gameObject.GetComponent<TankPlayer>().player_color + " shot failed");
                bullet_failed = true;
                CancelInvoke("FindEnemy");
            }
            else {
                this_tank_component.Shoot();
            }
        }
    }

    public void ChangeDirection() {
        this_tank_component.transform.rotation = Quaternion.Euler (0, this_tank_component.random_rotation_y + 185.0f, 0);
        this_tank_component.random_rotation_y = this_tank_component.random_rotation_y + 185.0f;
    } 

}
