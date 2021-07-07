using UnityEngine;

public class ChangeBodySide : MonoBehaviour
{
   [SerializeField] private PhysicsMovement _physicsMovement;

   [SerializeField] private Transform _body;

   private float _size;
   //Todo: сделать вызов по ивенту из PlayerInput
   private void Start()
   {
      _size = Mathf.Abs(_body.localScale.x);
   }

   private void Update()
   {
      if (_physicsMovement.Velocity.x != 0)
      {
         if (_physicsMovement.Velocity.x > 0)
         {
            _body.localScale = Vector3.one * _size;
         }
         else
         {
            _body.localScale = new Vector3(_size * -1, _size, _size);
         }
      }
   }
}
