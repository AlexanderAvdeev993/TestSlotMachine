using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItemScroll : MonoBehaviour
{
    [SerializeField] private List<RectTransform> _items;
    [SerializeField] private float _speedCloser;                 // скорость доводчика ряда
    [SerializeField] private float _stopPosition;
    private float _itemHeight;
    private int _itemCount;
    private int _firstItemIndex = 0;
    private float _scrollSpeed;
    
    private void Start()
    {
        if (_items.Count == 0)
        {
            Debug.LogError("Items list is empty!");
            return;
        }

        _itemHeight = _items[0].rect.height;
        _itemCount = _items.Count;
    }

    private void Update()
    {
        ScrollItems();
        CheckForReset();
    }

    private void ScrollItems()
    {
        foreach (RectTransform item in _items)
        {
            item.anchoredPosition -= new Vector2(0, _scrollSpeed * Time.deltaTime);
        }
    }

    private void CheckForReset()
    {
        RectTransform firstItem = _items[_firstItemIndex];
        RectTransform lastItem = _items[(_firstItemIndex + _itemCount - 1) % _itemCount];

        if (firstItem.anchoredPosition.y <= -_itemHeight)
        {
            firstItem.anchoredPosition = new Vector2(firstItem.anchoredPosition.x, lastItem.anchoredPosition.y + _itemHeight);
            _firstItemIndex = (_firstItemIndex + 1) % _itemCount;
        }
    }

    public void StopScrollSmoothly(float duration)
    {
        StartCoroutine(SmoothStopItem(duration));
    }

    private IEnumerator SmoothStopItem(float duration)
    {
        float initialSpeed = _scrollSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _scrollSpeed = Mathf.Lerp(initialSpeed, _speedCloser, elapsedTime / duration);
            yield return null;
        }
        _scrollSpeed = _speedCloser;
        
        while (true)    // доводчик
        {   
            RectTransform lastItem = _items[(_firstItemIndex + _itemCount - 1) % _itemCount];
            if (Mathf.Abs(lastItem.anchoredPosition.y - _stopPosition) < 0.3f)
            {
                _scrollSpeed = 0f; 
                yield break;
            }
            yield return null;
        }
    }

    public void StartScrollAcceleration(float targetSpeed, float duration)
    {
        StartCoroutine(ScrollAccelerationCoroutine(targetSpeed, duration));
    }

    private IEnumerator ScrollAccelerationCoroutine(float targetSpeed, float duration)
    {
        float initialSpeed = _scrollSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _scrollSpeed = Mathf.Lerp(initialSpeed, targetSpeed, elapsedTime / duration);
            yield return null;
        }

        _scrollSpeed = targetSpeed;
    }
}