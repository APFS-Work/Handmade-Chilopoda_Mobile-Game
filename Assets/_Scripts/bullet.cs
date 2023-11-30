using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int damage;
    public bool player;

    Collider collide;

    float bulletPath;
    public bool shot;
    TrailRenderer trailRenderer;

    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = gameObject.GetComponent<TrailRenderer>();
        collide = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {            
        if (shot == true)
        {
            if (player == true)
            {
                bulletPath += Time.deltaTime;
            }
            else if (player == false)
            {
                bulletPath += (Time.deltaTime / 2);
            }
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position + gameObject.transform.up, bulletPath);
            if (trailRenderer.enabled == false)
            {
                trailRenderer.enabled = true;
            }
            if (bulletPath >= 1.0f)
            {
                HitOrFinish();
            }
        }
        else
        {
            trailRenderer.enabled = false;
        }
    }

    void HitOrFinish()
    {
        trailRenderer.enabled = false;
        gameObject.transform.position = new Vector3(500.0f, 50.0f, 500.0f);
        bulletPath = 0.0f;

        //Debug.Log("Keep on going");
        shot = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (player == true)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<Enemy>().HP -= damage;
                Enemy.EnemyHurtSound.Play();
                HitOrFinish();
                Debug.Log("Hit Enemy");
                //Debug.Log("HP   " + other.gameObject.GetComponent<Enemy>().HP);
            }
        }

        if (player == false)
        {
            //Debug.Log("Hit");
            if (other.gameObject.tag == "Player")
            {
                SpawnPlayer.playerHP -= damage;
                SpawnPlayer.PlayerHurtSound.Play();
                HitOrFinish();
                Debug.Log("Hit player");
            }
        }
    }
}
