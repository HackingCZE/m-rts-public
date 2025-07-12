#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionController : MonoBehaviour
{
    private GameObject _selection;
    private bool _selected;

    public static UnitSelectionController? SelectedUnit {get; private set;}

    private void Awake()
    {
        _selection = transform.Find("selected object").gameObject;
    }

    public void ChangeSelectionState(bool toSelect)
    {
        if(SelectedUnit != null && this != SelectedUnit) SelectedUnit.ChangeSelectionState(false);
        _selected = toSelect;
        _selection.SetActive(toSelect);

        if(_selected)SelectedUnit = this;
        else SelectedUnit = null;
       
    }
}
