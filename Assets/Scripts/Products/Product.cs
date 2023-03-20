using Products.ScriptableObjects;
using UnityEngine;

namespace Products
{
   public class Product : MonoBehaviour
   {
       [SerializeField] private ProductData productData;
       /*
       public string GetProductName()
      {
         return productData.productName;
      }

      public float GetProductPrice()
      {
         return productData.productPrice;
      }

      public Sprite GetProductSprite()
      {
         return productData.productSprite;
      }
      */
   }
}
