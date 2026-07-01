using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInspect : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler
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

    [Header("Start Animation")]
    [SerializeField] private bool play = true;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float animationZoom = 1.5f;
    [SerializeField] private Vector3 animationRotation = new Vector3(0, 45, 0);
    
    private bool isHovering = false;
    private float currentZoom = 0;
    private float smoothCurrentZoom = 0;

    private bool isAnimating = false;
    
    
    
    public void OnDrag(PointerEventData eventData)
    {
        if (isAnimating)
            return;
        
        objectTransform.Rotate(Vector3.down, eventData.delta.x * xMultiplier, Space.World);
        cameraPivotTransform.Rotate(Vector3.left, eventData.delta.y * yMultiplier, Space.World);
        
        visualObject.transform.DORotate(objectTransform.eulerAngles, tweenDuration).SetEase(Ease.OutCubic);
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
        if (!isAnimating && isHovering)
        {
            currentZoom += Input.GetAxisRaw("Mouse ScrollWheel") * zoomMultiplier;
            currentZoom = Math.Clamp(currentZoom, minimumZoom, maximumZoom);
        }
        
        
        // Zooming
        smoothCurrentZoom = Mathf.Lerp(smoothCurrentZoom, currentZoom, Time.deltaTime * 10f);
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
        if (!item.model) return;
        
        visualObject = Instantiate(item.model, visualObjectParent);
        visualObject.transform.localPosition = Vector3.zero;
        visualObject.transform.localRotation = Quaternion.Euler(item.defaultRotation);
        visualObject.transform.localScale = visualObject.transform.localScale * item.defaultScale;
        SetLayerRecursively(visualObject, LayerMask.NameToLayer("Inventory"));

        currentZoom = 0;
        smoothCurrentZoom = 0;
        objectTransform.localRotation = Quaternion.Euler(item.defaultRotation);
        cameraPivotTransform.localRotation = Quaternion.identity;
        
        if (play)
            PlayStartAnimation(item);
    }

    public void SetEmpty()
    {
        if (visualObject) Destroy(visualObject);

        currentZoom = 0;
        smoothCurrentZoom = 0;
        cameraPivotTransform.localRotation = Quaternion.identity;
    }

    private void PlayStartAnimation(Item item)
    {
        isAnimating = true;

        visualObject.transform.DOKill();

        currentZoom = 0;
        smoothCurrentZoom = animationZoom;

        visualObject.transform.localRotation = Quaternion.Euler(item.defaultRotation + animationRotation);

        Sequence sequence = DOTween.Sequence();

        sequence.Join(visualObject.transform.DOLocalRotate(item.defaultRotation, animationDuration).SetEase(Ease.OutBack));

        sequence.Join(DOTween.To(() => smoothCurrentZoom, x => smoothCurrentZoom = x, currentZoom, animationDuration).SetEase(Ease.OutCubic));

        sequence.OnComplete(() =>
        {
            objectTransform.localRotation = Quaternion.Euler(item.defaultRotation);
            isAnimating = false;
        });
    }
    
    private void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
            SetLayerRecursively(child.gameObject, layer);
    }
}
