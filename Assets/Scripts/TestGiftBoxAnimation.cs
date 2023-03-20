using UnityEngine;
using DG.Tweening;

public class TestGiftBoxAnimation : MonoBehaviour
{
   [SerializeField] private Transform cover;
   [SerializeField] private Transform southBox;
   [SerializeField] private Transform northBox;
   [SerializeField] private Transform eastBox;
   [SerializeField] private Transform westBox;
   [SerializeField] private Transform westJoint;
   [SerializeField] private Transform eastJoint;
   [SerializeField] private Transform northJoint;
   [SerializeField] private Transform southJoint;

   private float duration = 0.3f;
   
   
   public void OpenBoxAnim()
   {
      cover.DOLocalMove(new Vector3(0f, 1.5f, 0f), duration).SetEase(Ease.OutExpo);
      southBox.DOLocalRotate(new Vector3(0f, 90f, 0f), duration).SetEase(Ease.OutSine);
      northBox.DOLocalRotate(new Vector3(0f, -90f, 0f), duration).SetEase(Ease.OutSine);
      eastBox.DOLocalRotate(new Vector3(0f, 0f, 0f), duration).SetEase(Ease.OutSine);
      westBox.DOLocalRotate(new Vector3(0f, 180f, 0f), duration).SetEase(Ease.OutSine);
      
      southJoint.DOLocalRotate(new Vector3(0f, 90f, 0f), duration);
      northJoint.DOLocalRotate(new Vector3(0f, -90f, 0f), duration);
      westJoint.DOLocalRotate(new Vector3(0f, 180f, 0f), duration);
      eastJoint.DOLocalRotate(new Vector3(0f, 0f, 0f), duration);
   }

   public void CloseBoxAnim()
   {
      cover.DOLocalMove(new Vector3(0f, 0.5145221f, 0f), duration).SetEase(Ease.OutExpo);
       southBox.DOLocalRotate(new Vector3(0f, 90f, -90f), duration).SetEase(Ease.OutSine);
       northBox.DOLocalRotate(new Vector3(0f, -90f, -90f), duration).SetEase(Ease.OutSine);
       eastBox.DOLocalRotate(new Vector3(0f, 0f, -90f), duration).SetEase(Ease.OutSine);
       westBox.DOLocalRotate(new Vector3(0f, 180f, -90f), duration).SetEase(Ease.OutSine);
      
       southJoint.DOLocalRotate(new Vector3(0f, 90f, -90f), duration);
       northJoint.DOLocalRotate(new Vector3(0f, -90f, -90f), duration);
       westJoint.DOLocalRotate(new Vector3(0f, 180f, -90f), duration);
       eastJoint.DOLocalRotate(new Vector3(0f, 0f, -90f), duration);
       
      /* transform.DOLocalJump(new Vector3((transform1 = transform).position.x, 0.06f, transform1.position.z), 0.5f, 1,
           duration);
           */
   }
}
