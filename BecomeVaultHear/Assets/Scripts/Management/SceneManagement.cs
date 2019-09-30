using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Component of the GameMaster
//Transitions to different scenes when Event Signals are recieved
//This should be the only script capable of changing the scene
public enum Scenes
{
    Failure = -1,
    Start = 0,
    LevelSelect = 10,
    Tertiary = 99,
    Win = 100,
    Lose = 101,
    Test,
    Tutorial,
    Level1 = 1,
    Level2 = 2
}


public class SceneManagement : MonoBehaviour
{
    private Animator anim;
    private SaveMangement data;

    //Have some scene array and load its index upon certain events
    private readonly Dictionary<Scenes, string> scenes = new Dictionary<Scenes, string>() {
        {Scenes.Start, "StartScene" },
        {Scenes.LevelSelect, "LevelSelectScene" },
        {Scenes.Tertiary, "Tertiary" },
        {Scenes.Win, "WinScene" },
        {Scenes.Lose, "LoseScene" },
        {Scenes.Test, "TestScene" },
        {Scenes.Tutorial, "TutorialScene" },
        {Scenes.Level1, "Level1Scene" },
        {Scenes.Level2, "Level2Scene" }
    };

    private readonly Dictionary<string, Scenes> scenesInverse = new Dictionary<string, Scenes>()
    {
        {"StartScene", Scenes.Start },
        {"LevelSelectScene", Scenes.LevelSelect },
        {"Tertiary", Scenes.Tertiary },
        {"WinScene", Scenes.Win },
        {"LoseScene", Scenes.Lose },
        {"TestScene", Scenes.Test },
        {"TutorialScene", Scenes.Tutorial },
        {"Level1Scene", Scenes.Level1 },
        {"Level2Scene", Scenes.Level2 }
    };


    private void Start()
    {
        //anim = GameObject.FindGameObjectWithTag("Transition").GetComponentInChildren<Animator>();
        //Debug.Log(anim.gameObject.name);
    }

    private void OnEnable()
    {
        LineOfSight.OnDetected += Lose;
        CatchPlayer.OnDetected += Lose;
        Obstacle.OnObstacleCollision += Lose;
        Goal.OnClearLevel += Win;
    }

    private void OnDisable()
    {
        LineOfSight.OnDetected -= Lose;
        CatchPlayer.OnDetected -= Lose;
        Obstacle.OnObstacleCollision -= Lose;
        Goal.OnClearLevel -= Win;
    }

    public void PlayAnim()
    {
        anim.SetBool("TriggerTransition", true);
        StartCoroutine(AnimDriver());
    }

    //Play
    private IEnumerator AnimDriver()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("TriggerTransition", false);

    }



    public Scenes CurrentScene()
    {
        if (scenesInverse.ContainsKey(SceneManager.GetActiveScene().name))
        {
            return scenesInverse[SceneManager.GetActiveScene().name];
        }

        else return Scenes.Failure;
    }

    public void Lose()
    {
        //PlayAnim();
        //anim.SetBool("TriggerTransition", false);
        SceneManager.LoadScene(scenes[Scenes.Lose]);
    }

    public void Win()
    {
        //PlayAnim();
        //anim.SetBool("TriggerTransition", false);
        SceneManager.LoadScene(scenes[Scenes.Win]);
    }

    public void SwitchScene(int sceneValue)
    {
        if (Enum.IsDefined(typeof(Scenes), sceneValue))
        {
            Scenes switchScene = (Scenes)sceneValue;
            // PlayAnim();

            //Debug.Log(anim.GetBool("TriggerTransition"));
            SceneManager.LoadScene(scenes[switchScene]);


        }

        else
        {
            Debug.LogError("Invalid Scene. Check sceneValue input in Inpector.");
        }
    }
}
