using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class CinemachineController : MonoBehaviour
{
    // Start is called before the first frame update
    public float TouchSensitivityX = 1f;
    public float TouchSensitivityY = 1f;

    /*public string TouchXInputMapTo = "Mouse X";
    public string TouchYInputMapTo = "Mouse Y";*/

    [SerializeField] float xmin, xmax, ymin, ymax;
    int InsideAreaTouchId = -1;
    bool Released = false;
    Touch AnalogTouch;

    void Start()
    {
        CinemachineCore.GetInputAxis = GetInputAxis;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Released)
            {
                InsideAreaTouchId = GetAnalogTouchIDInsideArea(); //-1 = none
            }

            if (InsideAreaTouchId != -1)
            {
                AnalogTouch = Input.GetTouch(InsideAreaTouchId);
                if (Released)
                {
                    if (AnalogTouch.phase == TouchPhase.Began)
                    {
                        Released = false;
                        TouchBegan();
                    }
                }
                else
                {
                    if (AnalogTouch.phase == TouchPhase.Ended) TouchEnd();
                }
            }
            else
            {
                Released = true;
            }
        }
        else
        {
            InsideAreaTouchId = -1;
            Released = true;
        }
    }

    private float GetInputAxis(string axisName)
    {
        switch (axisName)
        {
            case "Mouse X":
                if (Input.touchCount > 0 && InsideAreaTouchId != -1)
                {
                    return Input.touches[InsideAreaTouchId].deltaPosition.x / TouchSensitivityX;
                }
                else
                {
#if UNITY_EDITOR
                    return Input.GetAxis(axisName);
#else
                    return 0;
#endif
                }
            case "Mouse Y":
                if (Input.touchCount > 0 && InsideAreaTouchId != -1)
                {
                    return Input.touches[InsideAreaTouchId].deltaPosition.y / TouchSensitivityY;
                }
                else
                {
#if UNITY_EDITOR
                    return Input.GetAxis(axisName);
#else
                    return 0;
#endif
                }
            default:
                Debug.LogError("Input <" + axisName + "> not recognized.", this);
                break;
        }
        return 0f;
    }

    int GetAnalogTouchIDInsideArea()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (CheckArea(Input.GetTouch(i).position))
                return i;
        }
        return -1;
    }

    bool CheckArea(Vector2 pos)
    {
        Vector2 npos = new Vector2(pos.x / Screen.width, pos.y / Screen.height);
        /*Debug.Log("NPosX: " + npos.x.ToString());
        Debug.Log("NPosY: " + npos.y.ToString());*/
        if ((npos.x > xmin && npos.x < xmax) || (npos.y > ymin && npos.y < ymax))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void TouchBegan()
    {
        Released = false;
    }

    void TouchEnd()
    {
        Released = true;
        InsideAreaTouchId = -1;
    }

    public void InputOnImage()
    {
        Debug.Log("Im on Image");
    }

}
