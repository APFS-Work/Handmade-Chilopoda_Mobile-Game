using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Image OutImg;
    public Image InImg;
    Touch touch;

    Vector2 showPos;


    public static Vector3 inputVector;

    public virtual void OnPointerDown(PointerEventData ped)
    {

    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        InImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(OutImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / OutImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / OutImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2 - 1, 0.0f, pos.y * 2 - 1);

            inputVector = (inputVector.magnitude > 1.0f ? inputVector.normalized : inputVector);

            InImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (OutImg.rectTransform.sizeDelta.x / 2), inputVector.z * (OutImg.rectTransform.sizeDelta.y  / 2));


        }
        
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
        {
            return inputVector.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public float Vertical()
    {
        if (inputVector.z != 0)
        {
            return inputVector.z;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Screen.width + "              " + Screen.height); 
        //Debug.Log(OutImg.rectTransform.rect.width + "              " + OutImg.rectTransform.rect.height);
        OutImg.rectTransform.position = new Vector3(Screen.width / 4, Screen.height / 6, 0.0f);
        //Debug.Log(OutImg.rectTransform.position);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
