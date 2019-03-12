using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class ModelInteractionManager : MonoBehaviour
    {
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
    }
}
