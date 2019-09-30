using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{

    public List<Scenes> clearedLevels = new List<Scenes>();
    public Dictionary<string, List<Collectables>> obtainedCollectables = new Dictionary<string, List<Collectables>>();
    public string test = ""; //This is used to validate proper save/load use

}

