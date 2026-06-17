using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInspect : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    public GameObject visualObject;
    [SerializeField] private Transform visualObjectParent;
    [SerializeField] private Transform objectTransform;
    [SerializeField] private Transform cameraPivotTransform;
    [SerializeField] private Transform cameraTransform;
    
    [Header("Settings")]
    [SerializeField] private float xMultiplier = 1;
    [SerializeField] private float yMultiplier = 1;
    [SerializeField] private float tweenDuration = .15f;
    [SerializeField] private float yMin, yMax;
    
    [Header("Zoom Settings")]
    [SerializeField] private float zoomMultiplier = 10f;
    [SerializeField] private float defaultZ = -10;
    [SerializeField] private float minimumZoom = 0;
    [SerializeField] private float maximumZoom = 5;
    
    private bool isDragging = false;
    private bool isHovering = false;
    private float currentZoom = 0;
    private float smoothCurrentZoom = 0;
    
    
    
    public void OnDrag(PointerEventData eventData)
    {
        objectTransform.Rotate(Vector3.down, eventData.delta.x * xMultiplier, Space.World);
        cameraPivotTransform.Rotate(Vector3.left, eventData.delta.y * yMultiplier, Space.World);
        
        visualObject.transform.DORotate(objectTransform.eulerAngles, tweenDuration).SetEase(Ease.OutCubic);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    private void Update()
    {
        if (isHovering)
        {
            currentZoom += Input.GetAxisRaw("Mouse ScrollWheel") * zoomMultiplier;
            currentZoom = Math.Clamp(currentZoom, minimumZoom, maximumZoom);
        }
        
        
        // Zooming
        DOTween.To(() => smoothCurrentZoom, x => smoothCurrentZoom = x, currentZoom, tweenDuration);
        cameraTransform.localPosition = new Vector3(0, 0, defaultZ + smoothCurrentZoom);
        
        
        // Rotating
        float pitch = cameraPivotTransform.localEulerAngles.x;
        
        if (pitch > 180)
            pitch -= 360;
        
        pitch = math.clamp(pitch, yMin, yMax);
        cameraPivotTransform.rotation = Quaternion.Euler(pitch, 0, 0);
    }

    public void UpdateItem(Item item)
    {
        if (visualObject) Destroy(visualObject);
        
        visualObject = Instantiate(item.model, visualObjectParent);
        visualObject.transform.localPosition = Vector3.zero;
        visualObject.transform.localRotation = Quaternion.Euler(item.defaultRotation);
        visualObject.transform.localScale = Vector3.Scale(visualObject.transform.localScale, item.defaultScale);
        visualObject.layer = LayerMask.NameToLayer("Inventory");

        currentZoom = 0;
        smoothCurrentZoom = 0;
        objectTransform.localRotation = Quaternion.Euler(item.defaultRotation);
        cameraPivotTransform.localRotation = Quaternion.identity;
    }

    public void SetEmpty()
    {
        if (visualObject) Destroy(visualObject);

        currentZoom = 0;
        smoothCurrentZoom = 0;
        cameraPivotTransform.localRotation = Quaternion.identity;
    }
}
