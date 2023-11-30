using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Turret : MonoBehaviour
{
    public GameObject OneShotBullet;
    public List<GameObject> OneShotBulletPool;

    [SerializeField]
    bool OneShotTurret;
    bool OneShotShoot;
    public int OneShotPeriod;
    int OneShotBulletCount;
    
    [SerializeField]
    bool sight;

    public AudioSource shoot;

    public AudioManager audioManager;


    /*#region NoOptimize
    float time;

    #endregion*/
    private void Awake()
    {
        AudioManager.PlayerShootingSound.Clear();
    }


    // Start is called before the first frame update
    void Start()
    {
        shoot = gameObject.GetComponent<AudioSource>();
        if (audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
        }
        if (OneShotTurret == true)
        { 
            for (int x = 0; x < 5; x++)
            { 
                OneShotBulletPool.Add(Instantiate(OneShotBullet, new Vector3(500, 50, 500), new Quaternion(1.0f, 0.0f, 0.0f, 1.0f)));
                OneShotBulletPool[x].GetComponent<bullet>().damage = GameManager.GetDamage(0, PlayerStatus.ModificationLevel[0]);
                OneShotBulletPool[x].GetComponent<bullet>().player = true;
                //Debug.Log(OneShotBulletPool[x].GetComponent<bullet>().damage);
            }
        }
        AudioManager.PlayerShootingSound.Add(shoot);
    }

    // Update is called once per frame
    void Update()
    {
        /*#region NoOptimize
        GameObject ShotBullet;
        if (SpawnPlayer.Moving == true)
        {
            if (time < 1.0f)
            {
                time += Time.deltaTime;
            }
            else
            {
                ShotBullet = Instantiate(OneShotBullet, gameObject.transform.position, new Quaternion(1.0f, 0.0f, 0.0f, 1.0f));
                ShotBullet.GetComponent<bullet>().damage = GameManager.GetDamage(0, PlayerStatus.ModificationLevel[0]);
                ShotBullet.GetComponent<bullet>().player = true;
                ShotBullet.GetComponent<bullet>().shot = true;
                Destroy(ShotBullet, 1.0f);
                time = 0.0f;
            }
        }
        else
        {
            time = 0.0f;
        }
        #endregion*/



        if (OneShotTurret == true)
        {
            if (SpawnPlayer.Moving == true)
            {
                if (OneShotShoot == false)
                {
                    StartCoroutine(OneShotShooting(1.0f));
                }
            }
            else
            {
                //StopAllCoroutines();
            }
        }
        //Debug.DrawLine(gameObject.transform.position,gameObject.transform.position + gameObject.transform.forward * 50.0f, Color.red);
    }

    IEnumerator OneShotShooting(float time)
    {
        OneShotShoot = true;
        yield return new WaitForSeconds(time);
        //Debug.Log("Ienumerator Working");
        OneShotBulletPool[OneShotBulletCount].transform.position = gameObject.transform.position;
        OneShotBulletPool[OneShotBulletCount].transform.eulerAngles = new Vector3(90.0f, gameObject.transform.eulerAngles.y, 0.0f) ;
        OneShotBulletPool[OneShotBulletCount].GetComponent<bullet>().shot = true;
        AudioManager.PlayPlayerShooting(shoot);
        //shoot.Play();
        if (OneShotBulletCount < OneShotBulletPool.Count - 1)
        {
            OneShotBulletCount++;
        }
        else
        {
            OneShotBulletCount = 0;
        }
        OneShotShoot = false;
    }
}


