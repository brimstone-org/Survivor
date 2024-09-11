using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



/// <summary>
/// Handles player movement input
/// </summary>
public class Joystick : MonoBehaviour
{

    [SerializeField]
    private GameObject joystick;
    [SerializeField]
    private GameObject joystickBG;
    public Vector2 joystickVec; //direction of joystick
    private Vector2 joystickPos; //position of joystick
    private Vector2 joystickOrigPos; // initial joystick position
    private float joystickRadius; //radius of the joystick to keep it inside the background
    private bool isBeingTouched; //if the joystick is in motion

    void Start()
    {
        joystickOrigPos = joystickBG.transform.position; //initial start position for the joystick
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y*2; //get the radious of the joystick
    }

    /// <summary>
    /// starts the dragging sequence when finger touches the joystick
    /// </summary>
    public void PointerDown()
    {
        Debug.Log(joystickRadius);
        joystick.transform.position = Input.mousePosition;
        joystickPos = Input.mousePosition;
        isBeingTouched = true;
    }

    /// <summary>
    /// resets joystick when finger is up
    /// </summary>
    public void PointerUp()
    {
        joystickVec = Vector2.zero;
        joystick.transform.position = joystickOrigPos;
        isBeingTouched=false;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = (PointerEventData)baseEventData;
        Vector2 dragPos = pointerEventData.position; //the position of the finger during the drag
        joystickVec = (dragPos - joystickPos).normalized; //the direction from the position of the joystick to the finger

        float joystickDist = Vector2.Distance(dragPos, joystickOrigPos);

        //Keep joystick inside the joystick BG
        if (joystickDist< joystickRadius)
        {
            joystick.transform.position = joystickOrigPos + joystickVec * joystickDist;
        }
        else
        {
            joystick.transform.position = joystickOrigPos + joystickVec * joystickRadius;
        }
        
    }

    public bool IsBeingtouched()
    {
        return isBeingTouched;
    }
}
