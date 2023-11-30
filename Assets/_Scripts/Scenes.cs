using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;


public class Scenes : MonoBehaviour
{
    public GameObject Menu;
    public GameObject ModifyPage;
    public Text ModifyPage_Part_Text;
    [SerializeField]   
    Text damageText;
    [SerializeField]
    Text rateOfFireText;
    [SerializeField]
    Text DescriText;
    [SerializeField]
    Text PurseText;
    [SerializeField]
    Text LevelText;
    [SerializeField]
    Text UpgradeCostText;
    [SerializeField]
    Text AddPartText;

    int partNumber = 1;
    float RotationAngle;

    int modificationIndex;
    public List<GameObject> modifications;

    public int damage;
    public int rateOfFire;
    public int cost;
    string descri;

    [SerializeField]
    CanvasScaler ModifyCanvasScaler;
    [SerializeField]
    CanvasScaler MenuCanvasScaler;

    public GameObject Loading;
    public Slider LoadSlider;

    private void Awake()
    {
        //Debug.Log(Application.persistentDataPath + "/player.data");
        //PlayerStatus.purse = 100000;
        //Debug.Log(PlayerStatus.purse);
        //Debug.Log(PlayerStatus.PlayerLength);
    }

    // Start is called before the first frame update
    void Start()
    {
        //partNumber = 1;
        modificationIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ModifyCanvasScaler.dynamicPixelsPerUnit <= 3.0f)
        {
            ModifyCanvasScaler.dynamicPixelsPerUnit += Time.deltaTime;
        }
        else
        {
            ModifyCanvasScaler.dynamicPixelsPerUnit = 2.0f;
        }
        if (MenuCanvasScaler.dynamicPixelsPerUnit <= 2.0f)
        {
            MenuCanvasScaler.dynamicPixelsPerUnit += Time.deltaTime;
        }
        else
        {
            MenuCanvasScaler.dynamicPixelsPerUnit = 0.5f;
        }

        if (ModifyPage.activeInHierarchy == true)
        {
            Modify();
        }
    }

    public void ChangeToGame()
    {
        SaveSystem.SavePlayer();
        SaveSystem.LoadPlayer();
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game");

        Menu.SetActive(false);
        Loading.SetActive(true);

        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            LoadSlider.value = progress;

            yield return null;
        }
    }

    public void ChangeToModifyPage()
    {
        Menu.SetActive(false);
        ModifyPage.SetActive(true);
    }

    public void ModifyBackToMenu()
    {
        ModifyPage.SetActive(false);
        Menu.SetActive(true);
    }

    public void RightPart()
    {
        //Debug.Log("Clicked");
        if (partNumber == PlayerStatus.PlayerLength)
        {
            partNumber = 1;
            Debug.Log("Here");
        }
        else
        {
            partNumber++;
            Debug.Log("Here");
        }
        modificationIndex = PlayerStatus.PlayerPartModification[partNumber - 1];
    }

    public void LeftPart()
    {
        if (partNumber == 1)
        {
            partNumber = PlayerStatus.PlayerLength;
            Debug.Log("Here");
        }
        else
        {
            partNumber --;
            Debug.Log("Here");
        }
        modificationIndex = PlayerStatus.PlayerPartModification[partNumber - 1];
    }

    public void LeftModificationPart()
    {
        if (modificationIndex == 0)
        {
            modificationIndex = modifications.Count - 1;
        }
        else
        {
            modificationIndex--;
        }
        foreach (GameObject Object in modifications)
        {
            Object.SetActive(false);
        }
    }

    public void RightModificationPart()
    {
        if (modificationIndex == modifications.Count - 1)
        {
            modificationIndex = 0;
        }
        else
        {
            modificationIndex++;
        }      
        foreach (GameObject Object in modifications)
        {
            Object.SetActive(false);
        }
    }

    public void UpgradeBtn()
    {
        if (PlayerStatus.purse >= cost)
        {
            if (PlayerStatus.ModificationLevel[modificationIndex] < 10)
            {
                PlayerStatus.purse -= cost;
                PlayerStatus.ModificationLevel[modificationIndex]++;
                SaveSystem.SavePlayer();
            }
        }
        else
        {
            Debug.Log("You don't have enough money to upgrade part");
        }
    }

    public void AddPartBtn()
    {
        if (PlayerStatus.purse >= (PlayerStatus.PlayerLength * PlayerStatus.PlayerLength * 100))
        {
            PlayerStatus.purse -= (PlayerStatus.PlayerLength * PlayerStatus.PlayerLength * 100);
            PlayerStatus.PlayerLength++;
            if (PlayerStatus.PlayerPartModification.Count < PlayerStatus.PlayerLength)
            {
                PlayerStatus.PlayerPartModification.Add(0);
            }
            SaveSystem.SavePlayer();
        }
        else
        {
            Debug.Log("You don't have enough money to add part");
        }
    }

    void Modify()
    {
            PurseText.text = "Purse: " + PlayerStatus.purse;
            ModifyPage_Part_Text.text = "Part " + partNumber;
            LevelText.text = "Level " + PlayerStatus.ModificationLevel[modificationIndex];
            UpgradeCostText.text = "Cost: " + cost;
            AddPartText.text = "Cost: " + (PlayerStatus.PlayerLength * PlayerStatus.PlayerLength * 100);

            foreach (GameObject modification in GameObject.FindGameObjectsWithTag("Demo"))
            {
                if (modification.activeInHierarchy == true)
                { 
                    RotationAngle += Time.deltaTime * 10;
                    modification.transform.localRotation = Quaternion.Euler(modification.transform.localRotation.x, RotationAngle, modification.transform.localRotation.z);
                }
            }

            switch (modificationIndex)
            {
                case 0:              
                    damage = 1 + (PlayerStatus.ModificationLevel[modificationIndex] - 1);
                    rateOfFire = 1;
                    descri = "Basic weapon";
                    cost = PlayerStatus.ModificationLevel[modificationIndex] * 100;
                    break;
                case 1:
                    damage = 0;
                    rateOfFire = 0;
                    descri = "Enlarge your vision";
                    cost = PlayerStatus.ModificationLevel[modificationIndex] * 50;
                    break;
                case 2:
                    damage = 0;
                    rateOfFire = 0;
                    descri = "Increase moving speed";
                    cost = PlayerStatus.ModificationLevel[modificationIndex] * 200;
                    break;
            }

            if (modifications[modificationIndex].activeInHierarchy == false)
            {
                   for (int x = 0; x < GameManager.numberOfModifications; x++)
                   {
                       if (x != modificationIndex)
                       {
                           if (modifications[x].activeInHierarchy == true)
                           {
                               modifications[x].SetActive(false);
                           }
                       }
                   }
                modifications[modificationIndex].SetActive(true);
                Debug.Log(modificationIndex);
            }
            damageText.text = "Damage: " + damage;
            rateOfFireText.text = "Rate of fire: " + rateOfFire + "/s";
            DescriText.text = descri;

            PlayerStatus.PlayerPartModification[partNumber - 1] = modificationIndex;
            //Debug.Log(PlayerStatus.PlayerPartModification[0]);
    }
}
