using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Rigidbody _rigidbody;
    [SerializeField] private GameObject _object;
    private Vector3 _position;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _position = _object.GetComponent<Transform>().position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        var position = eventData.pointerCurrentRaycast.worldPosition;
        var delta = position - transform.position;
        delta.z = 0;

        transform.position += delta;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _rigidbody.isKinematic = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _rigidbody.isKinematic = false;
    }

    public void UpdatePosition()
    {
        _object.transform.position = _position;
    }
}
