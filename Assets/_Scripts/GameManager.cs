using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static int numberOfModifications;

    private void Awake()
    {
        numberOfModifications = 3;

        if (SaveSystem.LoadPlayer() == null)
        {
            PlayerStatus.PlayerLength = 1;
            //Debug.Log("NoProblem");
            PlayerStatus.purse = 0;
            //Debug.Log("NoProblem");
            for (int x = 0; x < PlayerStatus.PlayerLength; x++)
            {
                PlayerStatus.PlayerPartModification.Add(0);
            }
            //Debug.Log("NoProblem");
            for (int x = 0; x < numberOfModifications; x++)
            {
                PlayerStatus.ModificationLevel.Add(1);
            }
            //Debug.Log("NoProblem");
        }
        else
        {
            PlayerData data = SaveSystem.LoadPlayer();
            PlayerStatus.purse = data.Money;
            PlayerStatus.PlayerLength = data.lengthOfPlayer;
            if (data.PartModification.Count < data.lengthOfPlayer)
            {
                for (int x = data.PartModification.Count; x < data.lengthOfPlayer; x++)
                {
                    data.PartModification.Add(0);
                }
            }
            PlayerStatus.PlayerPartModification = data.PartModification;
            if (data.playerModificationLevel.Count < numberOfModifications)
            {
                for (int x = data.playerModificationLevel.Count; x < numberOfModifications; x++)
                { 
                    data.playerModificationLevel.Add(1);
                }
            }
            PlayerStatus.ModificationLevel = data.playerModificationLevel;
            //Debug.Log("Loaded");          
            //Debug.Log(PlayerStatus.purse);
            //Debug.Log(PlayerStatus.PlayerLength);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerStatus.PlayerLength == 0)
        {
            PlayerStatus.PlayerLength = 1;
        }
        if (PlayerStatus.PlayerPartModification.Count == 0)
        {
            for (int x = 0; x < PlayerStatus.PlayerLength; x++)
            { 
                PlayerStatus.PlayerPartModification.Add(0);
            }
        }
        if (PlayerStatus.ModificationLevel.Count == 0)
        {
            for (int x = 0; x < 2; x++)
            { 
                PlayerStatus.ModificationLevel.Add(1);
            }
        }
    }


    public static void BackToMenu()
    {
        SaveSystem.SavePlayer();
        SceneManager.LoadScene("Menu");
    }


    public static int GetDamage(int Modification, int Level)
    {
        int damage;
        switch (Modification) 
        {      
            case 0:
                damage = 1 + (Level - 1);
                return damage;
            case 1:
                damage = Level;
                return damage;
            case 2:
                damage = Level;
                return damage;
        }
        return 0;
    }
}

public class PlayerStatus
{
    public static int purse;
    public static int PlayerLength;
    public static List<int> PlayerPartModification = new List<int>();
    public static List<int> ModificationLevel = new List<int>();
}
