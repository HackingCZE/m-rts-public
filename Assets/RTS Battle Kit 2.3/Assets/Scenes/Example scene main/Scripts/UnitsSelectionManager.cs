using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsSelectionManager : MonoBehaviour
{
    
    [SerializeField] Camera _camera;
    private Ray _lastRay;
    private RaycastHit _lastHit;
    private bool _hitSomething;
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnMouseClick();
        }
    }

    private void OnMouseClick()
    {
        RaycastHit hit;
        _lastRay = _camera.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(_lastRay, out _lastHit, 1000f)) 
        {
            if(_lastHit.transform.CompareTag("Knight"))
            {
                _hitSomething = true;
                Debug.Log("Selected Knight");
                
                _lastHit.transform.GetComponent<UnitSelectionController>().ChangeSelectionState(true);
            }
            else
            {
                _hitSomething = false;
                UnitSelectionController.SelectedUnit?.ChangeSelectionState(false);
                Debug.Log("Deselected Knight");
            }
        }
    }

    
    void OnDrawGizmos()
    {
        if (_lastRay.origin != Vector3.zero)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawRay(_lastRay.origin, _lastRay.direction * 1000f);

            if (_hitSomething)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(_lastHit.point, 0.1f);
            }
        }
    }
}
