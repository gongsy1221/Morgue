using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    //private Transform _cam;
    //private InputManager _inputManager;
    //private PlayerUI _playerUI;
    //private int _interactMask;
    //[SerializeField] private float _distance = 2;
    //private void Awake()
    //{
    //    _cam = Camera.main.transform;
    //    _playerUI = GetComponent<PlayerUI>();
    //    _interactMask = LayerMask.NameToLayer("Interact");
    //    _inputManager = GetComponent<InputManager>();

    //}

    //void Update()
    //{
    //    _playerUI.UpdateText(string.Empty);
    //    Ray _ray = new Ray(_cam.transform.position, _cam.transform.forward);
    //    RaycastHit _hitInfo;
    //    if (Physics.Raycast(_ray, out _hitInfo, _distance, 1 << _interactMask))
    //    {

    //        if (_hitInfo.collider.GetComponent<Outline>() != null)
    //        {
    //            _outline = _hitInfo.collider.GetComponent<Outline>();
    //            _outline.OnOutline();
    //        }


    //        if (_hitInfo.collider.GetComponent<Interactable>() != null)
    //        {
    //            Interactable _interactable = _hitInfo.collider.GetComponent<Interactable>();
    //            _playerUI.UpdateText(_interactable.promptMessage);
    //            if (_inputManager.OnFloor.Interact.triggered)
    //            {
    //                _interactable.BassInteract();
    //            }
    //        }
    //    }
    //    else if (_outline != null)
    //    {
    //        _outline.OffOutline();
    //    }

    //}
}
