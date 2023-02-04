using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_UI : MonoBehaviour //, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public Action ClickFunc = null;
    public Action MouseRightClickFunc = null;
    public Action MouseMiddleClickFunc = null;
    public Action MouseDownOnceFunc = null;
    public Action MouseUpFunc = null;
    public Action MouseOverOnceTooltipFunc = null;
    public Action MouseOutOnceTooltipFunc = null;
    public Action MouseOverOnceFunc = null;
    public Action MouseOutOnceFunc = null;
    public Action MouseOverFunc = null;
    public Action MouseOverPerSecFunc = null; 
    public Action MouseUpdate = null;
    public Action<PointerEventData> OnPointerClickFunc;

}
