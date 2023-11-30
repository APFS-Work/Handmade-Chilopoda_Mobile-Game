using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    public List<GameObject> BodyPart = new List<GameObject>();

    public float RotateStrength = 5.0f;
    public float FllowSpeed = 10.0f;
    public float FllowDis = 2.5f;
    Quaternion TargetRotate;

    // Start is called before the first frame update
    void Start()
    {
        if (BodyPart[0] == null)
        {
            Debug.Log("No Body Part");
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int x = 0; x < BodyPart.Count; x++)
        {
            if (x != 0)
            {
                if (gameObject.transform.position != BodyPart[1].transform.position)
                { 
                //TargetRotate = Quaternion.Lerp(BodyPart[x].transform.rotation, BodyPart[x - 1].transform.rotation, 0.2f);
                TargetRotate = Quaternion.LookRotation(BodyPart[x - 1].transform.position - BodyPart[x].transform.position, BodyPart[x].transform.up);        
                //TargetRotate = new Quaternion(BodyPart[x].transform.rotation.x, Quaternion.LookRotation(BodyPart[x - 1].transform.position - BodyPart[x].transform.position, BodyPart[x].transform.up).y, BodyPart[x].transform.rotation.z, BodyPart[x].transform.rotation.w);
                //Debug.Log(Quaternion.LookRotation(BodyPart[x - 1].transform.position - BodyPart[x].transform.position, BodyPart[x].transform.up).y);
                BodyPart[x].transform.rotation = TargetRotate;
                float speed = FllowSpeed * Time.deltaTime;
                //if (Vector3.Distance(BodyPart[x].transform.position, Vector3.Lerp(BodyPart[x].transform.position, BodyPart[x - 1].transform.position, 0.5f)) > FllowDis)
                if (Vector3.Distance(BodyPart[x].transform.position, BodyPart[x - 1].transform.position) > FllowDis)
                {
                    BodyPart[x].transform.position = Vector3.MoveTowards(BodyPart[x].transform.position, Vector3.Lerp(BodyPart[x].transform.position, BodyPart[x - 1].transform.position, 0.5f), speed);
                }

                }
            }
        }
    }

}
