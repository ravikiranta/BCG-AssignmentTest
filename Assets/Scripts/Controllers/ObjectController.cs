using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    #region Variables
    [SerializeField] private bool selected;
    [SerializeField] private Material originalmaterial;
    [SerializeField] private Material selectionHighlightMaterial;
    [SerializeField] private bool isSkinnedMeshRenderer;
    [SerializeField] private List<GameObject> objectsToChangeMaterial;
    #endregion

    #region Events
    public delegate void ObjectSelected(GameObject obj);
    public static event ObjectSelected onObjectSelected;
    #endregion

    #region Properties
    public bool IsSelected
    {
        get
        {
            return selected;
        }
    }
    #endregion

    #region Init
    public void Init()
    {
        selected = false;
        //---- Subscribe to events ----
        Subscribe();

        //---- Set Selected ---
        SetSelection();
    }

    void Subscribe()
    {
        onObjectSelected += CheckSelection;
    }
    #endregion

    #region DeInit
    void OnDestroy()
    {
        onObjectSelected -= CheckSelection;
    }
    #endregion

    #region SelectionFunctions
    void CheckSelection(GameObject selectedObject)
    {
        if (selectedObject != gameObject)
        {
            selected = false;
        }
    }

    private void OnMouseDown()
    {
        SetSelection();
    }

    public void DeSelect()
    {
        selected = false;
        if (onObjectSelected != null)
            onObjectSelected(null);
    }

    public void SetSelection()
    {
        if (!selected)
        {
            selected = true;
            if (onObjectSelected != null)
                onObjectSelected(gameObject);
        }
        else if (selected)
        {
            selected = false;
            if (onObjectSelected != null)
                onObjectSelected(null);
        }

        ChangeMaterial(selected);
    }

    void ChangeMaterial(bool isSelected)
    {
        for(int i = 0; i < objectsToChangeMaterial.Count; i++)
        {
            if (isSkinnedMeshRenderer)
            {
                if (isSelected)
                    objectsToChangeMaterial[i].GetComponent<SkinnedMeshRenderer>().material = selectionHighlightMaterial;
                else
                    objectsToChangeMaterial[i].GetComponent<SkinnedMeshRenderer>().material = originalmaterial;
            }
            else
            {
                if (isSelected)
                    objectsToChangeMaterial[i].GetComponent<MeshRenderer>().material = selectionHighlightMaterial;
                else
                    objectsToChangeMaterial[i].GetComponent<MeshRenderer>().material = originalmaterial;
            }
        }
    }

    #endregion
}