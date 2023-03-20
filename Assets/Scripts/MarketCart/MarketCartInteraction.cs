using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UI;
using UnityEngine;
using DG.Tweening;

namespace MarketCart
{
   public class MarketCartInteraction : MonoBehaviour,IInteractable
   {
      
      [SerializeField] private List<Transform> pickupSlots;
      [SerializeField] private List<GameObject> cartProductsList;
      [SerializeField] private Transform packingBasketPosition;
      [SerializeField] private float radius;
      
      private MarketCartController marketCartController;
      
      [Header("Tween Settings")]
      private const float JumpPower = 1.5f;
      private const int NumJumps = 1;
      private const float Duration = 0.3f;
      
      [Header("Coroutine Settings")]
      private float delayTime = 0.45f;
      private WaitForSeconds routineWaitForSeconds;

      private Shelf shelf;
      private int pickUpSlotIndex;
     

      private void Start()
      {
         marketCartController = GetComponent<MarketCartController>();
        UIManager.Instance.OnIncrementButtonPressed += IncrementButtonEvent;
        UIManager.Instance.OnDecrementButtonPressed += DecrementButtonEvent;
        routineWaitForSeconds = new WaitForSeconds(delayTime);
      }
      
      
      private void OnTriggerEnter(Collider other)
      {

         if (other.CompareTag("PackingArea"))
         {
            if (cartProductsList.Count == 0) return;
            other.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(MoveProductsToBasket());

         }
         
      }

      private void OnTriggerStay(Collider other)
      {
         if (!other.CompareTag("Shelf")) return;
         
         if(shelf == null)
            shelf = other.gameObject.GetComponent<Shelf>();
         UIManager.Instance.SetProductInfoPanel(shelf.productData);
         UIManager.Instance.ShowInfoPanel(true);
      }

      private void OnTriggerExit(Collider other)
      {
         if (!other.CompareTag("Shelf")) return;
         UIManager.Instance.ResetInfo();
         shelf = null;

      }

      //Products moving into the packing basket.
      private IEnumerator MoveProductsToBasket()
      {
         //Check if there are any products in the cart
         if (cartProductsList.Count <= 0 || cartProductsList == null)  yield break ;
         
         marketCartController.CanMoveSetter(false);
         yield return routineWaitForSeconds;
         for (var i = 0; i < cartProductsList.Count; i++)
         {
            var angle = i * Mathf.PI * 2 / cartProductsList.Count;
            var x = Mathf.Cos(angle) * radius;
            var z = Mathf.Sin(angle) * radius;
            var pos = packingBasketPosition.position + new Vector3(x, 0, z);
            var angleDegrees = -angle*Mathf.Rad2Deg;
            var rot = Quaternion.Euler(0, angleDegrees, 0);
            
            cartProductsList[i].transform.SetParentNull();
            cartProductsList[i].transform.DOJump(pos, JumpPower, NumJumps,Duration).SetEase(Ease.OutQuint);
            cartProductsList[i].transform.rotation = rot;
            cartProductsList[i].transform.SetParent(packingBasketPosition,true);
            yield return routineWaitForSeconds;
         }
         
         yield return routineWaitForSeconds;
         marketCartController.CanMoveSetter(true);
         // cartProductsList = null;

       
      }
      public void Interact(bool isIncrementing)
      {

         if (isIncrementing)
         {
            cartProductsList.Add(MoveProductToCart());
         }
         else
         {
            foreach (var product in cartProductsList.Where(product => product.name == shelf.productData.name))
            {
               //Move object to shelf again
               MoveProductToShelf(product);
               cartProductsList.Remove(product);
               break;
            }
         }
      }
      
      private GameObject MoveProductToCart()
      {
         if (shelf.productsOnShelf.Count <= 0) return null;
         var firstProduct = shelf.productsOnShelf[0].gameObject;
         marketCartController.CanMoveSetter(false);
         foreach (var slot in pickupSlots.Where(slot => slot.childCount == 0))
         {
            firstProduct.transform.SetParentNull();
            
            firstProduct.transform.SetParent(slot.transform,true);
            shelf.productsOnShelf.Remove(firstProduct);
            firstProduct.transform.DOJump(slot.transform.position, JumpPower, NumJumps, Duration).SetEase(Ease.OutSine)
               .OnComplete(() =>  marketCartController.CanMoveSetter(true));
            break;
         }
         return firstProduct;

      }
      
      private void MoveProductToShelf(GameObject product)
      {
         foreach (var slot in shelf.productSlots.Where(slot => slot.childCount == 0))
         {
            marketCartController.CanMoveSetter(false);
            product.transform.SetParentNull();
            product.transform.DOJump(slot.transform.position, JumpPower, NumJumps, Duration).SetEase(Ease.OutSine)
               .OnComplete(() =>  marketCartController.CanMoveSetter(true));
            product.transform.SetParent(slot.transform,true);
            shelf.productsOnShelf.Add(product);
            break;
         }
      }

      public void Interact()
      {
         throw new System.NotImplementedException();
      }

      //Event that listens to the UIManager.Instance.IncrementProductCount function
      private void IncrementButtonEvent()
      {
         if (shelf.productsOnShelf.Count == 0 || pickUpSlotIndex >= pickupSlots.Count )
         {
            //Return a message for that on UI.
            UIManager.Instance.isIncrementing = false;
            return;
         }
        Interact(true);
         pickUpSlotIndex++;
         UIManager.Instance.isIncrementing = true;
        
      }

      private void DecrementButtonEvent()
      {
         if (cartProductsList.Count == 0) return;
         
         Interact(false);        
         pickUpSlotIndex--;
         UIManager.Instance.isDecreasing = true;
      }
      
   
   }
   
}
