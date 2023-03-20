using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Products.ScriptableObjects;

public class Shelf : MonoBehaviour
{
    public ProductData productData;
    public List<GameObject> productsOnShelf;
    public List<Transform> productSlots;
   // private const float JumpPower = 1f;
   // private const int NumJumps = 1;
   // private const float Duration = 0.3f;

  /*  
  public GameObject MoveProductToCart(List<Transform> pickupSlots)
  {
    if (productsOnShelf.Count <= 0) return null;
    var firstProduct = productsOnShelf[0].gameObject;

    foreach (var slot in pickupSlots)
    {
      if (slot.childCount != 0) continue;
      firstProduct.transform.SetParentNull();
      firstProduct.transform.DOJump(slot.transform.position, JumpPower, NumJumps,Duration).SetEase(Ease.OutSine);
      firstProduct.transform.SetParent(slot.transform,true);
      productsOnShelf.Remove(firstProduct);
      break;
    }
   
    
    return firstProduct;

  }

  
  public void MoveProductToShelf(GameObject product)
  {
    foreach (var slot in productSlots.Where(slot => slot.childCount == 0))
    {
      product.transform.SetParentNull();
      product.transform.DOJump(slot.transform.position, JumpPower, NumJumps,Duration).SetEase(Ease.OutSine);
      product.transform.SetParent(slot.transform,true);
      productsOnShelf.Add(product);
      break;
    }
  }
  */
}
