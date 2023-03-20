using UnityEngine;

namespace Products.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObject/Product")]
    public class ProductData : ScriptableObject
    {
        public string productName;
        public Sprite productSprite;
        public GameObject productPrefab;
        public float productPrice;
    }

   
}