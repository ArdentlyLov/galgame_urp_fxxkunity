using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileJoystick : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private RectTransform joystickKnob;

    [Header(" Settings ")]
    [SerializeField] private float moveFactor;
    public Animator animator; // 引用动画组件
    private Vector3 clickedPosition;
    private Vector3 move;
    private bool canControl;

    void Start()
    {
        animator = joystickOutline.GetComponent<Animator>();
        HideJoystick();
    }
    void Update()
    {
        if(canControl)
            ControlJoystick();
    }

    public void CilickOnMobileJoysitickZoneCollBack()
    {
        clickedPosition = Input.mousePosition;
        joystickOutline.position = clickedPosition;
        ShowJoystick();
    }    
    private void ShowJoystick()
    {
        animator.SetTrigger("MobileJoystickAnimationShow");
        joystickOutline.position = clickedPosition;

        joystickOutline.gameObject.SetActive(true);
        canControl = true;
    }
    private void HideJoystick()
    {
        joystickOutline.gameObject.SetActive(false);
        canControl = false;
        move = Vector3.zero;
    }
    private void ControlJoystick()
    {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 direction = currentPosition - clickedPosition;
    
        float canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x;
        float moveMagnitude = direction.magnitude * moveFactor * canvasScale;
        float absoluteWidth = joystickOutline.rect.width / 2;
        float realWidth = absoluteWidth * canvasScale;
    
        moveMagnitude = Mathf.Min(moveMagnitude, realWidth);
        move = direction.normalized * moveMagnitude;
        Vector3 targetPosition = clickedPosition + move;
    
        joystickKnob.position = targetPosition;
        if (Input.GetMouseButtonUp(0))
            HideJoystick();
    }
    
    public Vector3 GetMoveVector()
    {
        float canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x;
        return move / canvasScale;
    }
}
