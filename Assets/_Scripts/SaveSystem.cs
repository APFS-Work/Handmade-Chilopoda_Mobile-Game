using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        //Debug.Log("No Problem");
        PlayerData data = new PlayerData(PlayerStatus.purse, PlayerStatus.PlayerLength, PlayerStatus.PlayerPartModification, PlayerStatus.ModificationLevel);       
        //Debug.Log("No Problem");
        formatter.Serialize(stream, data);
        //Debug.Log("No Problem");
        stream.Close();

        Debug.Log("Saved sucessfully       path: " + path);
        Debug.Log(PlayerStatus.purse);
        Debug.Log(PlayerStatus.PlayerLength);
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();
            Debug.Log("loaded" + path);
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class PlayerData
{
    public int Money;
    public int lengthOfPlayer;
    public List<int> PartModification = new List<int>();
    public List<int> playerModificationLevel = new List<int>();

    public PlayerData(int Purse, int playerLength, List<int> playerPartModification, List<int> modificationLevel)
    {
        Money = Purse;
        lengthOfPlayer = playerLength;
        PartModification = playerPartModification;
        playerModificationLevel = modificationLevel;
    }
}
