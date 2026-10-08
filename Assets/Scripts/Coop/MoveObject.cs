using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    //[SerializeField] private GameObject _objectToMove;
    
    [SerializeField] private Transform _objectToMoveTransform;
    [SerializeField] private Transform _objectToMoveTransform2;
    
    private float _moveSpeed = 5f;

    private float _stopTime = 10f;

    private void Start()
    {
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {

            while ((transform.position - _objectToMoveTransform.position).magnitude >= 0.1)
            {
                //Debug.Log("움직임 시작1");
                transform.position = Vector3.MoveTowards(gameObject.transform.position, _objectToMoveTransform.transform.position, _moveSpeed * Time.deltaTime);
                yield return null;
            }
            //Debug.Log("움직임 종료1");
            transform.position = _objectToMoveTransform.transform.position;

            yield return new WaitForSeconds(_stopTime);



            while ((transform.position - _objectToMoveTransform2.position).magnitude >= 0.1)
            {
                //Debug.Log("움직임 시작2");
                transform.position = Vector3.MoveTowards(gameObject.transform.position, _objectToMoveTransform2.transform.position, _moveSpeed * Time.deltaTime);
                yield return null;
                //Debug.Log("움직임 종료2");
            }

            transform.position = _objectToMoveTransform2.transform.position;

            yield return new WaitForSeconds(_stopTime);

        }


    }

    
    
}
