using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTeams : MonoBehaviour
{
    [SerializeField]
    public int number_of_teams;

    [SerializeField]
    public int team_size;

    private int team_id;

    public GameObject tank;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < number_of_teams; i++) {
            Debug.Log("creating team: " + i);
             //   Instantiate(tank); 
             //   tank.GetComponent<TankPlayer>().player_color = new Color

            
            for (int k = 0; k < team_size; k++) {
                Debug.Log("Team: " + i + " got member: " + k);
                
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
