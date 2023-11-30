using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    public GameObject turret;
    [SerializeField]
    public GameObject shootingPoint;
    [SerializeField]
    public GameObject bullet;
    [SerializeField]
    GameObject player;
    GameObject target;

    [SerializeField]
    float distance;

    bool shot;
    int bulletCount;

    public int HP;

    public static AudioSource EnemyHurtSound;
    public AudioSource[] EnemyAudioSource;
    public static AudioSource EnemyShootSound;

    List<GameObject> bulletPool = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        EnemyHurtSound = gameObject.GetComponent<AudioSource>();
        Debug.Log(EnemyHurtSound.gameObject.name);
        EnemyAudioSource = gameObject.GetComponentsInChildren<AudioSource>();
        EnemyShootSound = EnemyAudioSource[1];
        Debug.Log(EnemyShootSound.gameObject.name);

        bulletCount = 0;
        for (int x = 0; x < 5; x++)
        {
            bulletPool.Add(Instantiate(bullet, new Vector3(500, 50, 500), new Quaternion(1.0f, 0.0f, 0.0f, 1.0f)));
            bulletPool[x].GetComponent<bullet>().damage = 1;
            bulletPool[x].GetComponent<bullet>().player = false;
            Debug.Log("Make Bullet  ");
        }
        //Debug.Log("Start  ");

        HP = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Target");
            return;
        }
        if (target == null)
        {
            target = player.GetComponent<Segment>().BodyPart[1];
            Debug.Log("target null");
            return;
        }

        if (HP <= 0)
        {
            PlayerStatus.purse += 10;
            Debug.Log("Gain 10 dollars");
            Debug.Log(PlayerStatus.purse);
            gameObject.transform.position = new Vector3(-1000,gameObject.transform.position.y,-1000);
            SpawnEnemy.EnemyNumber--;
            gameObject.SetActive(false);
        }



        turret.transform.rotation = Quaternion.LookRotation(target.transform.position - turret.transform.position, Vector3.up);
        if (Vector3.Distance(shootingPoint.transform.position, target.transform.position) < distance)
        {
            if (shot == false)
            {
                StartCoroutine(shooting(2.0f));
            }
            //Debug.Log("Close");
        }

    }


    IEnumerator shooting(float time)
    {
        shot = true;
        yield return new WaitForSeconds(time);
        //Debug.Log("Ienumerator Working");
        bulletPool[bulletCount].transform.position = shootingPoint.transform.position;
        bulletPool[bulletCount].transform.localEulerAngles = new Vector3(90.0f, turret.transform.localRotation.eulerAngles.y, 0.0f);
        EnemyShootSound.Play();
        bulletPool[bulletCount].GetComponent<bullet>().shot = true;
        if (bulletCount < bulletPool.Count - 1)
        {
            bulletCount++;
        }
        else
        {
            bulletCount = 0;
        }
        shot = false;
    }
}
