using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTeams : MonoBehaviour
{
    [SerializeField]
    public int number_of_teams;

    [SerializeField]
    public int number_of_team_members;

    private int team_id;

    private int max_number_of_team_members = 5;
    private int min_number_of_team_members = 2;

    private int max_number_of_teams = 6;
    private int min_number_of_teams = 3;

    public GameObject tank;
    public GameObject[] bounds;

    static float randomColor;
    static float i = 0;


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
            Color team_color = GetRandomColor();
            Debug.Log("Current team Color: " + team_color);

            for (int k = 1; k <= number_of_team_members; k++) {

                TankPlayer thisTank = tank.GetComponent<TankPlayer>();
                thisTank.spawn_bounds = bounds[i-1];
                AssignColor(team_color, thisTank);
                Instantiate(tank);
                thisTank.team_id = i;
                thisTank.player_ID = k; 

               // thisTank.player_mat.SetColor("_Color",team_color);
              //  thisTank.player_mat.SetColor("_EmissionColor",team_color);
                        

                
            }

        }
    }
    void ShuffleArr(float[] arr)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < arr.Length; t++ )
        {
            float tmp = arr[t];
            int r = Random.Range(t, arr.Length);
            arr[t] = arr[r];
            arr[r] = tmp;
        }
    }
    Color GetRandomColor() {
        randomColor = Random.Range(0.0f + i + 10.0f,30.0f + i)/255.0f;
        float[] arr = {0.0f, 1.0f, randomColor};  //helps keep colors vibrant
        i += 30.0f;
        ShuffleArr(arr);
        Color newColor = new Color(arr[0], arr[1], arr[2], 1.0f);
        return newColor;
        
    }

    void AssignColor(Color color, TankPlayer tank) {
        tank.player_color = color;
        Material mat = new Material(Shader.Find("Standard"));
        mat.SetColor("_Color",color);
        mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.None;
        mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;
        mat.SetColor("_EmissionColor", color);
        DynamicGI.SetEmissive(tank.GetComponent<Renderer>(), color);
        
        tank.GetComponent<Renderer>().material = mat;
        tank.transform.Find("Cylinder").GetComponent<Renderer>().material = mat;
        tank.transform.Find("Point Light").GetComponent<Light>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
