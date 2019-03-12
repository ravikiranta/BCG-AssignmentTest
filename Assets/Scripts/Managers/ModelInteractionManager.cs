using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using UnityEngine.UI;

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
        [SerializeField] private float scaleSpeed;
        [SerializeField] private float touchDeltaToZoomThreshold;

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

        #region OBJECT ROTATION
        //Rotate the object if object is selected and no item in menu selected for creation and tow touch counts
        void LateUpdate()
        {
            if (isObjectSelected)
            {
                //--- Scale ---
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                //float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                float deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag;

                //Calculate fov changes
                if (deltaMagnitudeDiff > touchDeltaToZoomThreshold || deltaMagnitudeDiff < -touchDeltaToZoomThreshold)
                {
                    selectedGameObject.transform.localScale = new Vector3(
                        selectedGameObject.transform.localScale.x + deltaMagnitudeDiff * scaleSpeed,
                        selectedGameObject.transform.localScale.y + deltaMagnitudeDiff * scaleSpeed,
                        selectedGameObject.transform.localScale.z + deltaMagnitudeDiff * scaleSpeed);
                }
                else {
                    //--- Rotation ---
                    if (Input.touchCount == 2)
                    {
                        x += Input.GetTouch(0).deltaPosition.x * xSpeed * 0.01f;
                        y -= Input.GetTouch(0).deltaPosition.y * ySpeed * 0.01f;
                        y = ClampAngle(y, yMinLimit, yMaxLimit);

                        rotation = Quaternion.Euler(
                            selectedGameObject.transform.rotation.eulerAngles.x,
                            x, selectedGameObject.transform.rotation.z);

                        selectedGameObject.transform.rotation = rotation;
                    }
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
