using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers {
    public class CanvasAnimationController : MonoBehaviour
    {
        #region Variables
        [Header ("Info")]
        [SerializeField] private bool panelOpen;

        [Header("References")]
        [SerializeField] private Animator canvasAnimationController;
        #endregion

        #region Events
        public delegate void MenuInteraction();
        public static event MenuInteraction onMenuOpen;
        #endregion

        #region Init
        void Awake()
        {
            Subscribe();
        }

        void Subscribe()
        {
            TileController.onNewAugmentedContentSelected += HandleEvent;
        }
        #endregion

        #region DeInit
        void OnDestroy()
        {
            TileController.onNewAugmentedContentSelected -= HandleEvent;
        }
            
        #endregion

        #region EventHandler
        void HandleEvent(string contentName)
        {
            ClosePanel();
        }
        #endregion

        #region Functions
        public void TogglePanel()
        {
            // Trigger event when panel is opening
            if (!panelOpen && onMenuOpen != null)
                onMenuOpen();

            panelOpen = !panelOpen;
            canvasAnimationController.SetBool("Open", panelOpen);
        }

        public void ClosePanel()
        {
            panelOpen = false;
            canvasAnimationController.SetBool("Open", false);
        }
        #endregion
    }
}