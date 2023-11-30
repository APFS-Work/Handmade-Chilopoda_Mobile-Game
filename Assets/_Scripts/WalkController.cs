using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkController : MonoBehaviour
{
    [SerializeField]
    GameObject LeftForelegIKTarget, RightForelegIKTarget;
    [SerializeField]
    GameObject LeftLegTarget, RightLegTarget;
    [SerializeField]
    GameObject LeftLegHint, RightLegHint;
    [SerializeField]
    GameObject Body;

    Vector3 LeftHintDis, RightHintDis;
    Vector3 LeftLegPos, RightLegPos;
    Vector3 LeftStartPos, RightStartPos;

    float BodyStartZ, BodyAfterZ;
    public float BodyMove = 0.25f;
    public float LegMoveDis = 1.5f;
    public float LegMoveHeight = 1.0f;
    public float LiftLegTime = 0.3f, PutLegTime = 0.2f;
    public float LeftLegTime, RightLegTime;
    public float FootPlacementHeight;
    public float RotateAngle;

    public bool LeftLegMoving, RightLegMoving;
    bool LeftLegLifting, RightLegLifting;
    bool LeftLegPutting, RightLegPutting;

    // Start is called before the first frame update
    void Start()
    {
        LeftLegPos = LeftForelegIKTarget.transform.position;
        RightLegPos = RightForelegIKTarget.transform.position;

        LeftHintDis = LeftLegHint.transform.position - new Vector3(LeftForelegIKTarget.transform.position.x, LeftForelegIKTarget.transform.position.y, LeftForelegIKTarget.transform.position.z);
        RightHintDis = RightLegHint.transform.position - new Vector3(RightForelegIKTarget.transform.position.x, RightForelegIKTarget.transform.position.y, RightForelegIKTarget.transform.position.z);


    }

    // Update is called once per frame
    void Update()
    {
        IKRay();

        LeftForelegIKTarget.transform.position = LeftLegPos;
        RightForelegIKTarget.transform.position = RightLegPos;

        LeftLegHint.transform.position = LeftLegPos + LeftHintDis;
        RightLegHint.transform.position = RightLegPos + RightHintDis;


        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Left Leg Dis : " + Vector3.Distance(LeftLegTarget.transform.position, LeftForelegIKTarget.transform.position));
            Debug.Log("Right Leg Dis : " + Vector3.Distance(RightLegTarget.transform.position, RightForelegIKTarget.transform.position));
        }*/

        if (Vector3.Distance(LeftLegTarget.transform.position, LeftForelegIKTarget.transform.position) > LegMoveDis && LeftLegMoving == false && RightLegMoving == false)
        {
            LeftLegLifting = true;
            LeftStartPos = LeftForelegIKTarget.transform.position;
            BodyStartZ = gameObject.transform.position.z;
            LeftLegMoving = true;
        }
        if (Vector3.Distance(RightLegTarget.transform.position, RightForelegIKTarget.transform.position) > LegMoveDis && RightLegMoving == false && LeftLegMoving == false)
        {
            RightLegLifting = true;
            RightStartPos = RightForelegIKTarget.transform.position;
            BodyStartZ = gameObject.transform.position.z;
            RightLegMoving = true;
        }
    

        if (LeftLegMoving == true)
        {
            if (LeftLegLifting == true)
            {
                LeftLegTime += Time.deltaTime * (1 / LiftLegTime);
                LeftLegPos = Vector3.Lerp(LeftStartPos, new Vector3(LeftLegTarget.transform.position.x, LeftLegTarget.transform.position.y + LegMoveHeight, LeftLegTarget.transform.position.z), LeftLegTime);
                //Debug.Log(BodyRotation[0] + "," + BodyRotation[1] + "," + BodyRotation[2] + "," + BodyRotation[3]);
                //Body.transform.localPosition = Vector3.Lerp(new Vector3(Body.transform.localPosition.x, Body.transform.localPosition.y, BodyStartZ), new Vector3(Body.transform.localPosition.x, Body.transform.localPosition.y, BodyStartZ + BodyMove), LeftLegTime);
                Body.transform.localPosition = new Vector3(BodyMove, 2.0f, 0.0f);
                if (LeftLegTime >= 1.0f)
                {
                    LeftLegTime = 0.0f;
                    LeftStartPos = LeftForelegIKTarget.transform.position;
                    //BodyAfterZ = Body.transform.position.z;
                    LeftLegPutting = true;
                    LeftLegLifting = false;
                }
            }

            if (LeftLegPutting == true)
            {
                LeftLegTime += Time.deltaTime * (1 / PutLegTime);
                LeftLegPos = Vector3.Lerp(LeftStartPos, new Vector3(LeftLegTarget.transform.position.x, LeftLegTarget.transform.position.y, LeftLegTarget.transform.position.z), LeftLegTime);         

                Body.transform.localPosition = new Vector3(0.0f, 2.0f, 0.0f);

                if (LeftLegTime >= 1.0f)
                {
                    LeftLegTime = 0.0f;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, (LeftLegTarget.transform.position.y + RightLegTarget.transform.position.y) / 2, gameObject.transform.position.z);
                    LeftLegPutting = false;
                    LeftLegMoving = false;
                }
            }
        }
        if (RightLegMoving == true)
        {
            if (RightLegLifting == true)
            {
                RightLegTime += Time.deltaTime * (1 / LiftLegTime);
                RightLegPos = Vector3.Lerp(RightStartPos, new Vector3(RightLegTarget.transform.position.x, RightLegTarget.transform.position.y + LegMoveHeight, RightLegTarget.transform.position.z), RightLegTime);
                //Body.transform.localPosition = Vector3.Lerp(new Vector3(Body.transform.localPosition.x, Body.transform.localPosition.y, BodyStartZ), new Vector3(Body.transform.localPosition.x, Body.transform.localPosition.y, BodyStartZ - BodyMove), RightLegTime);
                //Body.transform.position = Vector3.Lerp(new Vector3(Body.transform.position.x, Body.transform.position.y, BodyStartZ), new Vector3(Body.transform.position.x, Body.transform.position.y, BodyStartZ) - Vector3.forward, RightLegTime);
                Body.transform.localPosition = new Vector3( -BodyMove, 2.0f, 0.0f);
                if (RightLegTime >= 1.0f)
                {
                    RightLegTime = 0.0f;
                    RightStartPos = RightForelegIKTarget.transform.position;
                    //BodyAfterZ = Body.transform.position.z;
                    RightLegPutting = true;
                    RightLegLifting = false;
                }
            }

            if (RightLegPutting == true)
            {
                RightLegTime += Time.deltaTime * (1 / PutLegTime);
                RightLegPos = Vector3.Lerp(RightStartPos, new Vector3(RightLegTarget.transform.position.x, RightLegTarget.transform.position.y, RightLegTarget.transform.position.z), RightLegTime);

                Body.transform.localPosition = new Vector3(0.0f, 2.0f, 0.0f);

                if (RightLegTime >= 1.0f)
                {
                    RightLegTime = 0.0f;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, (LeftLegTarget.transform.position.y + RightLegTarget.transform.position.y) / 2, gameObject.transform.position.z);
                    RightLegPutting = false;
                    RightLegMoving = false;
                }
            }
        }


        /*if (Mathf.Min(LeftLegTarget.transform.position.y, RightLegTarget.transform.position.y) == LeftLegTarget.transform.position.y)
        {
            RotateAngle = Vector3.Angle(LeftLegTarget.transform.position - RightLegTarget.transform.position, new Vector3(LeftLegTarget.transform.position.x - RightLegTarget.transform.position.x, 0.0f, LeftLegTarget.transform.position.z - RightLegTarget.transform.position.z)) * -1;           
        }
        else if (Mathf.Min(LeftLegTarget.transform.position.y, RightLegTarget.transform.position.y) == RightLegTarget.transform.position.y)
        {
            RotateAngle = Vector3.Angle(RightLegTarget.transform.position - LeftLegTarget.transform.position, new Vector3(RightLegTarget.transform.position.x - LeftLegTarget.transform.position.x, 0.0f, RightLegTarget.transform.position.z - LeftLegTarget.transform.position.z));       
        }

        if (RotateAngle <= 30.0f && RotateAngle >= -30.0f)
        {
                gameObject.transform.localRotation = Quaternion.Euler(RotateAngle, gameObject.transform.rotation.eulerAngles.y, gameObject.transform.rotation.eulerAngles.z);
                //Debug.Log("Left Leg Higher  :  " + RotateAngle);
        }*/


        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, (LeftLegTarget.transform.position.y + RightLegTarget.transform.position.y) / 2, gameObject.transform.position.z);


        //Debug.Log(RotateAngle);

    }
    void IKRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(new Vector3(LeftLegTarget.transform.position.x, FootPlacementHeight, LeftLegTarget.transform.position.z), Vector3.down, out hit) == true)
        {
            LeftLegTarget.transform.position = hit.point;
            //Debug.Log("hit" + hit.point);
            //Debug.DrawLine(new Vector3(LeftLegTarget.transform.position.x, FootPlacementHeight, LeftLegTarget.transform.position.z), hit.point, Color.red);
        }

        if (Physics.Raycast(new Vector3(RightLegTarget.transform.position.x, FootPlacementHeight, RightLegTarget.transform.position.z), Vector3.down, out hit) == true)
        {
            RightLegTarget.transform.position = hit.point;
            //Debug.Log("hit" + hit.point);
            //Debug.DrawLine(new Vector3(LeftLegTarget.transform.position.x, FootPlacementHeight, LeftLegTarget.transform.position.z), hit.point, Color.red);
        }
    }


}
