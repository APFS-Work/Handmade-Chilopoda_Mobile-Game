using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour
{
    #region Modifications
    public GameObject OneShotTurret;
    public GameObject Sight;
    public GameObject speed;
    #endregion

    public GameObject PartOfPlayer;
    List<GameObject> parts;

    public static bool Moving;

    int fllowSpeed;
    
    Vector3 Move;

    [SerializeField]
    Transform top;
    [SerializeField]
    Transform bottom;
    [SerializeField]
    Transform left;
    [SerializeField]
    Transform right;

    public static int playerHP;

    public Text playerHealth;

    public static AudioSource PlayerHurtSound;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHurtSound = gameObject.GetComponent<AudioSource>();

        parts = gameObject.GetComponent<Segment>().BodyPart;
        parts.Add(gameObject);

        playerHP = PlayerStatus.PlayerLength * 10;

        for (int x = 0; x < PlayerStatus.PlayerLength; x++)
        {
            parts.Add(Instantiate(PartOfPlayer, new Vector3(parts[x].transform.position.x, parts[x].transform.position.y, parts[x].transform.position.z - 2), Quaternion.identity));
            Debug.Log(PlayerStatus.PlayerPartModification[x]);
            switch (PlayerStatus.PlayerPartModification[x])
            {
                case 0:
                    Instantiate(OneShotTurret, parts[x + 1].transform);
                    break;
                case 1:
                    Instantiate(Sight, parts[x + 1].transform);
                    Camera.main.orthographicSize += GameManager.GetDamage(1, PlayerStatus.ModificationLevel[1]);
                    //Debug.Log(PlayerStatus.ModificationLevel[1]);
                    break;
                case 2:
                    Instantiate(speed, parts[x + 1].transform);
                    fllowSpeed += GameManager.GetDamage(2, PlayerStatus.ModificationLevel[2]);
                    break;

            }
            
        }

        gameObject.GetComponent<Segment>().FllowSpeed += fllowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth.text = "HP : " + playerHP + "/" + PlayerStatus.PlayerLength * 10;


        if (playerHP <= 0)
        {
            GameManager.BackToMenu();
        }

        if (gameObject.transform.position.x < left.transform.position.x)
        {
            VirtualJoystick.inputVector = new Vector3(VirtualJoystick.inputVector.x *- 1, VirtualJoystick.inputVector.x*1, VirtualJoystick.inputVector.x*1);
        }

        if (gameObject.transform.position.x > right.transform.position.x)
        {
            VirtualJoystick.inputVector = new Vector3(VirtualJoystick.inputVector.x * -1, VirtualJoystick.inputVector.x * 1, VirtualJoystick.inputVector.x * 1);
        }

        if (gameObject.transform.position.z > top.transform.position.z)
        {
            VirtualJoystick.inputVector = new Vector3(VirtualJoystick.inputVector.x * 1, VirtualJoystick.inputVector.x * 1, VirtualJoystick.inputVector.x * -1);
        }

        if (gameObject.transform.position.z < bottom.transform.position.z)
        {
            VirtualJoystick.inputVector = new Vector3(VirtualJoystick.inputVector.x * 1, VirtualJoystick.inputVector.x * 1, VirtualJoystick.inputVector.x * -1);
        }
        Camera.main.transform.position = new Vector3(parts[1].transform.position.x, parts[1].transform.position.y + 45.0f, parts[1].transform.position.z);
        Move = new Vector3(parts[1].transform.position.x + VirtualJoystick.inputVector.x * 5 , gameObject.transform.position.y, parts[1].transform.position.z + VirtualJoystick.inputVector.z * 5 );
        if (VirtualJoystick.inputVector.x == 0.0f && VirtualJoystick.inputVector.z == 0.0f)
        {
            gameObject.transform.position = parts[1].transform.position;
            Move = parts[1].transform.position;
            Moving = false;
        }
        else
        { 
            gameObject.transform.position = Move;
            Moving = true;
            //Debug.Log(new Vector3((parts[1].transform.position.x + VirtualJoystick.inputVector.x) * 5, gameObject.transform.position.y, (parts[1].transform.position.z + VirtualJoystick.inputVector.z) * 5));
        }

    }
}
