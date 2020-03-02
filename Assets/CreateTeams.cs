using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTeams : MonoBehaviour
{
    [SerializeField]
    public int number_of_teams;

    [SerializeField]
    public int number_of_team_members;

    private int max_number_of_team_members = 8;
    private int min_number_of_team_members = 1;

    private int max_number_of_teams = 6;
    private int min_number_of_teams = 1;

    public GameObject tank;
    public GameObject[] bounds;

    static float randomColor;

    Color[] team_colors = {Color.magenta, Color.red, Color.blue, Color.green, Color.yellow, Color.cyan};

    // Start is called before the first frame update
    void Start()
    {
        bounds = GameObject.FindGameObjectsWithTag("Respawn");
        CheckTeamSize();
        TeamInit();
    }


    void CheckTeamSize() {
        number_of_team_members = Mathf.Clamp(number_of_team_members, min_number_of_team_members,max_number_of_team_members);
        number_of_teams = Mathf.Clamp(number_of_teams,min_number_of_teams,max_number_of_teams);
    }

    void TeamInit() {
        
         for(int i = 1; i <= number_of_teams; i++) {
            Color team_color = team_colors[i-1];

            for (int k = 1; k <= number_of_team_members; k++) {
                Debug.Log(team_color + "team id: " + i);
                TankPlayer this_tank = tank.GetComponent<TankPlayer>();
                this_tank.spawn_bounds = bounds[i-1];
                AssignColor(team_color, this_tank);
                this_tank.team_id = i;
                this_tank.player_ID = k;
                Instantiate(tank);

              
            }
         }
    }

    void AssignColor(Color color, TankPlayer tank) {
        tank.player_color = color;
        Material mat = new Material(Shader.Find("Standard"));
        mat.SetColor("_Color",color);
        mat.EnableKeyword("_EMISSION");
        mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;
        mat.SetColor("_EmissionColor", color);
        
        tank.GetComponent<Renderer>().material = mat;
        tank.transform.Find("Cylinder").GetComponent<Renderer>().material = mat;
        tank.transform.Find("Point Light").GetComponent<Light>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    // void ShuffleArr(float[] arr)
    // {
    //     // Knuth shuffle algorithm :: courtesy of Wikipedia :)
    //     for (int t = 0; t < arr.Length; t++ )
    //     {
    //         float tmp = arr[t];
    //         int r = Random.Range(t, arr.Length);
    //         arr[t] = arr[r];
    //         arr[r] = tmp;
    //     }
    // }

    
    // Color GetRandomColor() {
    //     randomColor = Random.Range(0.0f + i + 10.0f,30.0f + i)/255.0f;
    //     float[] arr = {0.0f, 1.0f, randomColor};  //helps keep colors vibrant
    //     i += 30.0f;
    //     ShuffleArr(arr);
    //     Color newColor = new Color(arr[0], arr[1], arr[2], 1.0f);
    //     return newColor;
        
    // }
}
