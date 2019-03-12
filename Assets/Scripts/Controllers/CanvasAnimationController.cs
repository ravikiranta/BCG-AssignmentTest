using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimationController : MonoBehaviour
{
    #region Variables
    [SerializeField] private bool panelOpen;
    [SerializeField] private Animator canvasAnimationController;
    #endregion

    #region Functions
    public void TogglePanel()
    {
        panelOpen = !panelOpen;
        canvasAnimationController.SetBool("Open", panelOpen);
    }
    #endregion
}
