using System;
using Products.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
      //  public event EventHandler OnIncrementButtonPressed;

        public event Action OnIncrementButtonPressed;
        public event Action OnDecrementButtonPressed;

       // public delegate void IncrementButtonDelegate(int totalProduct);
        public bool isIncrementing;
        public bool isDecreasing;
        
        [SerializeField] private GameObject takeProductPanel;
        [SerializeField] private Image productImage;
        [SerializeField] private TextMeshProUGUI productNameText;
       // [SerializeField] private TextMeshProUGUI productPriceText;
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private TextMeshProUGUI totalPriceText;
        [SerializeField] private Transform moneyIcon;

        private ProductData productInfo;
        private int totalProductCount;
        private float totalPrice;
        private int milkCountOnCart,
            jamBottleCountOnCart,
            wineBottleCountOnCart,
            mayonnaiseCountOnCart,
            ketchupCountOnCart,
            soupCountOnCart,
            cerealBoxCountOnCart;

        private void Start()
        {
            ShowInfoPanel(false);
        }

        public void IncrementProductCount()
        {
           // isIncrementing = true;
           OnIncrementButtonPressed?.Invoke();
           if (!isIncrementing) return;

           totalProductCount = SetTotalProductCount();
           totalPrice += productInfo.productPrice;
           UpdateProductInfoPanel();
           TweenAnimation();


        }
        
        public void ReduceProductCount()
        {
            OnDecrementButtonPressed?.Invoke();
            if (!isDecreasing) return;
            if (totalProductCount < 1) return;
            totalProductCount = SetTotalProductCount();
            totalPrice -= productInfo.productPrice;
            UpdateProductInfoPanel();
            TweenAnimation();


        }

        public void SetProductInfoPanel(ProductData pickedProductInfo)
        {
            if (pickedProductInfo == null) return;
            productInfo = pickedProductInfo;
            ShowInfoPanel(true);
            UpdateProductInfoPanel();
        }

        private void UpdateProductInfoPanel()
        {
            isDecreasing = false;
            isIncrementing = false;
            totalProductCount = SetTotalProductCount();
            totalPriceText.text = totalPrice.ToString("F");
            productImage.sprite = productInfo.productSprite;
            productNameText.text = productInfo.productName;
            countText.text = totalProductCount.ToString();

            

        }

        private void TweenAnimation()
        {
            moneyIcon.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f).SetEase(Ease.InOutBack).OnComplete(TweenResetScale);
            totalPriceText.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f).SetEase(Ease.InOutBack);
        }

        private void TweenResetScale()
        {
            moneyIcon.DOScale(new Vector3(1f, 1f, 1f), 0.15f).SetEase(Ease.InOutSine);
            totalPriceText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.15f).SetEase(Ease.InOutSine);
        }
        
        public void ShowInfoPanel(bool isActive)
        {
            takeProductPanel.SetActive(isActive);
        }

        public void ResetInfo()
        {
            totalProductCount = 0;
            productImage.sprite = null;
            productNameText.text = null;
            countText.text = null;
            ShowInfoPanel(false);
            productInfo = null;
            isIncrementing = false;
        }
        private int SetTotalProductCount()
        {
            switch (productInfo.productName)
            {
                case "MilkCarton":
                    if (isIncrementing)
                        milkCountOnCart++;
                    if (isDecreasing)
                        milkCountOnCart--;
                    return milkCountOnCart;
                
                case "CerealBox":
                    if (isIncrementing)
                        cerealBoxCountOnCart++;
                    if (isDecreasing)
                        cerealBoxCountOnCart--;
                    return cerealBoxCountOnCart;
                
                case "JamBottle":
                    if (isIncrementing)
                        jamBottleCountOnCart++;
                    if (isDecreasing)
                        jamBottleCountOnCart--;
                    return jamBottleCountOnCart;
                
                case "Ketchup":
                    if (isIncrementing)
                        ketchupCountOnCart++;
                    if (isDecreasing)
                        ketchupCountOnCart--;
                    return ketchupCountOnCart;
                
                case "Mayonnaise":
                    if (isIncrementing)
                        mayonnaiseCountOnCart++;
                    if (isDecreasing)
                        mayonnaiseCountOnCart--;
                    return mayonnaiseCountOnCart;
                
                case "Soup":
                    if (isIncrementing)
                        soupCountOnCart++;
                    if (isDecreasing)
                        soupCountOnCart--;
                    return soupCountOnCart;
                
                case "WineBottle":
                    if (isIncrementing)
                        wineBottleCountOnCart++;
                    if (isDecreasing)
                        wineBottleCountOnCart--;
                    return wineBottleCountOnCart;

            }
            return 0;
        }

       
    }
}
