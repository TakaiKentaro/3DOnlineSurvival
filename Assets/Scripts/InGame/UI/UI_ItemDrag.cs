using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemDrag : MonoBehaviour
{
    public static UI_ItemDrag Instance { get; private set; }

    private Canvas _canvas;
    private RectTransform _rectTransform;
    private RectTransform _parentRectTransform;
    private CanvasGroup _canvasGroup;
    private Image _image;
    private Item _item;
    private TextMeshProUGUI _amountText;

    private void Awake()
    {
        Instance = this;

        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GetComponentInParent<Canvas>();
        _image = transform.Find("Image").GetComponent<Image>();
        _amountText = transform.Find("AmountText").GetComponent<TextMeshProUGUI>();
        _parentRectTransform = transform.parent.GetComponent<RectTransform>();

        //Hide();
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        
    }

    public Item GetItem()
    {
        return _item;
    }

    public void SetItem(Item item)
    {
        this._item = item;
    }

    public void SetSprite(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void SetAmountText(int amount)
    {
        if (amount <= 1)
        {
            _amountText.text = "";
        }
        else
        {
            _amountText.text = amount.ToString();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(Item item)
    {
        gameObject.SetActive(true);
        
        SetItem(item);
        SetSprite(item.GetSprite());
        SetAmountText(item.amount);
        UpdatePosition();
    }
}
