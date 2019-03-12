using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace Managers
{
    public class ModelsManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject loadedAsset;
        [SerializeField] private List<GameObject> createdObjects;
        #endregion

        #region Singleton
        private static ModelsManager instance;
        public static ModelsManager Instance
        {
            get
            {
                if (instance == null)
                    instance = GameObject.FindObjectOfType<ModelsManager>();

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
            TileController.onNewAugmentedContentSelected += LoadGameObject;
        }
        #endregion

        #region DeInit
        void OnDestroy()
        {
            TileController.onNewAugmentedContentSelected -= LoadGameObject;
        }
        #endregion

        #region ModelLoadFunctions
        void LoadGameObject(string assetName)
        {
            Debug.Log(assetName);

            if (assetName != null)
            {
                loadedAsset = Resources.Load(assetName, typeof(GameObject)) as GameObject;
                
            }
            else
                Debug.Log("Asset name is null");
        }
        #endregion

        #region CreateModelFunctions
        public GameObject CreateLoadedAsset(Vector3 position, Quaternion rotation)
        {
            GameObject createdObject = Instantiate(loadedAsset, position, rotation);
            createdObjects.Add(createdObject);
            return createdObject;
        }
        #endregion
    }
}
