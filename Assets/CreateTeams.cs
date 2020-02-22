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

    private int max_team_size = 5;
    private int min_team_size = 2;
    private int max_number_of_teams = 5;
    private int min_number_of_teams = 2;

    public GameObject tank;


    // Start is called before the first frame update
    void Start()
    {
        team_size = Mathf.Clamp(team_size, min_team_size,max_team_size);
        number_of_teams = Mathf.Clamp(number_of_teams,min_number_of_teams,max_number_of_teams);

        for(int i = 1; i <= number_of_teams; i++) {
            Debug.Log("creating team: " + i);
            Color team_color = AssignColor();
            for (int k = 1; k <= team_size; k++) {
                Instantiate(tank);
                tank.GetComponent<TankPlayer>().player_color = team_color;
                Debug.Log("Team: " + i + " got member: " + k);
                Debug.Log("yolo: " + team_color);
                tank.GetComponent<TankPlayer>().team_id = i;
                tank.GetComponent<TankPlayer>().player_ID = k; 
                
            }
        }
    }

    Color AssignColor() {
        Color newColor = new Color(Random.Range(0,255.0f)/255.0f, Random.Range(0,255.0f)/255.0f, Random.Range(0,255.0f)/255.0f, 1.0f);
        return newColor;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
