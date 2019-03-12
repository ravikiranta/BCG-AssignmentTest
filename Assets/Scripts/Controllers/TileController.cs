using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class TileController : MonoBehaviour
    {
        #region Variables
        [Header("Dev Settings")]
        [SerializeField] private string contentName;
        [SerializeField] private Color tileHighlightedColor;
        [SerializeField] private Color tileNormalColor;

        [Header("References")]
        [SerializeField] private Image highlightImage;

        [Header("Info")]
        [SerializeField] private bool isSelected;

        #endregion
        #region Events
        public delegate void AugmentedContentSelection(string contentName);
        public static event AugmentedContentSelection onNewAugmentedContentSelected;
        #endregion

        #region Properties
        public string ContentName
        {
            get
            {
                return contentName;
            }
        }
        #endregion

        #region Init
        void Awake()
        {
            Subscribe();
        }

        void Subscribe()
        {
            onNewAugmentedContentSelected += UpdateHighlightColour;
        }
        #endregion

        #region DeInit
        void OnDestroy()
        {
            onNewAugmentedContentSelected -= UpdateHighlightColour;
        }
        #endregion

        #region ContentSelected
        public void ContentSelected()
        {
            if (onNewAugmentedContentSelected != null)
                onNewAugmentedContentSelected(contentName);
        }
        #endregion

        #region HighlightFunctions
        void UpdateHighlightColour(string contentName)
        {
            if (this.contentName == contentName)
            {
                isSelected = true;
                highlightImage.color = tileHighlightedColor;
            }
            else if (isSelected)
            {
                isSelected = false;
                highlightImage.color = tileNormalColor;
            }
        }
        #endregion
    }
}