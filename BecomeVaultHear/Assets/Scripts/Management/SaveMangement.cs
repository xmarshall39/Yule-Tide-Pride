using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using LitJson;

public class SaveMangement : MonoBehaviour
{

    public SaveData data;
    private Scenes currentScene;
    private Collectable collectableScript;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        collectableScript = new Collectable();
        currentScene = new SceneManagement().CurrentScene();
        data = new SaveData();
        LoadGame();
    }

    //Serialization Method: Class (SaveData) -> JSON (String) -> Binary (.dat file)

    public void SaveGame()
    {
        //Update remaining save data
        data.clearedLevels.Add(currentScene);
        //Remove duplicates from list
        data.clearedLevels = data.clearedLevels.Distinct().ToList();
        data.test = "Saved";

        string JData = JsonMapper.ToJson(data);
        Debug.Log(JData);

        //Now I can serialize the whole class into binary
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite); //Might need OpenOrCreate

        formatter.Serialize(file, JData);
        file.Close();
        PrintSaveData();
    }

    public void LoadGame()
    {
        //Load in data from the dat file to the JSON string

        if (File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            string JData;

            Debug.Log("File Found");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            try
            {
                JData = (String)formatter.Deserialize(file);
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                Debug.Log("EOF error. Save File corrupted");
                file.Close(); return;
            }
            file.Close();
            data = JsonMapper.ToObject<SaveData>(JData);

            PrintSaveData();
        }

    }

    private void OnEnable()
    {
        Goal.OnClearLevel += SaveGame;
        Collectable.OnFirstCollection += FirstCollection;
        Collectable.OnSecondCollection += SecondCollection;
        Collectable.OnThirdCollection += ThirdCollection;

    }

    private void OnDisable()
    {
        Goal.OnClearLevel -= SaveGame;
        Collectable.OnFirstCollection -= FirstCollection;
        Collectable.OnSecondCollection -= SecondCollection;
        Collectable.OnThirdCollection -= ThirdCollection;

    }

    private void FirstCollection() { CollectionDriver(Collectables.First); }
    private void SecondCollection() { CollectionDriver(Collectables.Second); }
    private void ThirdCollection() { CollectionDriver(Collectables.Third); }

    private void CollectionDriver(Collectables coll)
    {
        if (data.obtainedCollectables.ContainsKey(currentScene.ToString()))
        {
            data.obtainedCollectables[currentScene.ToString()].Add(coll);
            //Remove Duplicates from list to mimic set
            data.obtainedCollectables[currentScene.ToString()] = data.obtainedCollectables[currentScene.ToString()].Distinct().ToList();
        }

        else
        {
            data.obtainedCollectables.Add(currentScene.ToString(), new List<Collectables> { coll });
        }
    }

    //Debugging script for monitoring what data is currently saved

    public void PrintSaveData()
    {

        Debug.Log(data.test);
        Debug.Log("Printing Save Data");
        //Print Cleared Levels
        if (data.clearedLevels.Count == 0) Debug.Log("No Cleared Levels");
        foreach (Scenes level in data.clearedLevels)
        {
            Debug.Log("Level Cleared: " + level.ToString());
        }
        if (data.obtainedCollectables.Keys.Count == 0) Debug.Log("No Obtained Collectables");
        foreach (string level in data.obtainedCollectables.Keys)
        {
            Debug.Log("Items collected in: " + level.ToString());
            foreach (Collectables coll in data.obtainedCollectables[level])
            {
                Debug.Log("Collected Type: " + coll.ToString());
            }
        }

    }

}

