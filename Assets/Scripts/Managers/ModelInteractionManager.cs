using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Controllers;

namespace Managers
{
    public class ModelInteractionManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private bool isObjectSelected;
        [SerializeField] private GameObject selectedGameObject;

        [SerializeField] private float xSpeed = 120.0f;
        [SerializeField] private float ySpeed = 120.0f;
        [SerializeField] private float yMinLimit = -20f;
        [SerializeField] private float yMaxLimit = 80f;

        private float x = 0.0f;
        private float y = 0.0f;
        private Quaternion rotation;
        #endregion

        #region Properties
        public bool IsObjectSelected
        {
            get
            {
                return isObjectSelected;
            }
        }
        #endregion

        #region Singleton
        private static ModelInteractionManager instance;
        public static ModelInteractionManager Instance
        {
            get
            {
                if (instance == null)
                    instance = GameObject.FindObjectOfType<ModelInteractionManager>();

                return instance;
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
            ObjectController.onObjectSelected += SetSelectedObject;
        }
        #endregion

        #region DeInit
        void OnDestroy()
        {
            ObjectController.onObjectSelected -= SetSelectedObject;
        }
        #endregion

        #region ObjectSelection
        void SetSelectedObject(GameObject obj)
        {
            selectedGameObject = obj;

            if (obj != null)
                isObjectSelected = true;
            else
                isObjectSelected = false;
        }
        #endregion

        #region SelectedObjectMovementAndRotation
        public void MoveSelectedObject(Vector3 position)
        {
            selectedGameObject.transform.position = position;
        }

        public void RotateSelectedObject(Vector3 rotation)
        {

        }
        #endregion

        #region Input
        void LateUpdate()
        {
            if (isObjectSelected)
            {
#if (UNITY_ANDROID || UNITY_IOS)
                //TouchDetection.Calculate();
#endif
                //--- Rotation ---
#if (UNITY_ANDROID || UNITY_IOS)
                if (Input.touchCount == 2)
#elif(UNITY_EDITOR)
                if (Input.GetMouseButton(1))
#endif
                {
#if (UNITY_ANDROID || UNITY_IOS)
                    x += Input.GetTouch(0).deltaPosition.x * xSpeed * 0.01f;
                    y -= Input.GetTouch(0).deltaPosition.y * ySpeed * 0.01f;
#elif (UNITY_EDITOR)
                    x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                    y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
#endif
                    y = ClampAngle(y, yMinLimit, yMaxLimit);
                    rotation = Quaternion.Euler(
                        selectedGameObject.transform.rotation.eulerAngles.x, x, selectedGameObject.transform.rotation.z);
                    selectedGameObject.transform.rotation = rotation;
                }
            }
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
        #endregion
    }
}
