using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class CreateTeams : MonoBehaviour
{

    private int number_of_teams;
    private int number_of_team_members;


    [Range(2, 8)]
    public int NumberOfTeams;

    [Range(1, 15)]
    public int NumberOfTeamMembers;

    public GameObject tank;
    public GameObject[] bounds;


    //used for congrats winner
    public TextMesh winner_text;
    public Light main_light_component;
    Light main_light;

    Color[] team_colors = { new Color(1, 0, .47f, 1), Color.red, new Color(0, .30f, 1, 1), new Color(.20f, 1, 0, 1), Color.yellow, Color.cyan, Color.white, new Color(1.0f, .30f, 0.0f, 1.0f)};

    void Start()
    {
        main_light = main_light_component.GetComponent<Light>();
        bounds = GameObject.FindGameObjectsWithTag("Respawn");
        CheckTeamSize();
        TeamInit();
    }


    void CheckTeamSize() {

        number_of_teams = NumberOfTeams;
        number_of_team_members = NumberOfTeamMembers;

    }

    void TeamInit() {
        
         for(int i = 1; i <= number_of_teams; i++) {
            Color team_color = team_colors[i-1];

            for (int k = 1; k <= number_of_team_members; k++) {
                TankPlayer this_tank_component = tank.GetComponent<TankPlayer>();
                this_tank_component.spawn_bounds = bounds[i-1];
                AssignColor(team_color, this_tank_component);
                this_tank_component.team_id = i;
                this_tank_component.player_ID = k;
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
    void Update(){
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        CheckForDraw();
    }

   public void CheckForDraw(){
        if(FindObjectOfType<AIBehavior>() == null)
        {
            DeclareWinner(Color.grey);
        }
   }

    public void DeclareWinner(Color color){
        if(color == Color.grey){
            winner_text.text = "Draw!";
        }
        else{
            winner_text.text = "Winner!";
        }
        main_light.color = color;
        main_light.intensity = .5f;
        winner_text.color = color;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }

}
