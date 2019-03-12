using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace managers
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
                CreateLoadedAsset(new Vector3(0f, 0f, 8f), Quaternion.identity);
            }
            else
                Debug.Log("Asset name is null");
        }
        #endregion

        #region CreateModelFunctions
        void CreateLoadedAsset(Vector3 position, Quaternion rotation)
        {
            createdObjects.Add(Instantiate(loadedAsset, position, rotation));
        }
        #endregion
    }
}
