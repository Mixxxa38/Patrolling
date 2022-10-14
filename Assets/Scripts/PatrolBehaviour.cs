using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] private List<Transform> _anyPoints;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _waitingTime = 1f;
    
    private float _time;
    private float _stopTimer;
    private int _index;
    
   private void Update()
   {
       if (_stopTimer < _waitingTime) 
       {
           _stopTimer += Time.deltaTime; 
           return;
       }

       var firstPoint = _index %_anyPoints.Count;
       var lastPoint = (firstPoint + 1) % _anyPoints.Count;
       var distance = Vector3.Distance(_anyPoints[firstPoint].position, _anyPoints[lastPoint].position);
       var travelTime = distance / _speed;

       if (_time < travelTime)
       {
           _time += Time.deltaTime;
           var distanceTraveled = _time * _speed; 
           var _progress = distanceTraveled / distance; 
           var newPosition= Vector3.Lerp(_anyPoints[firstPoint].position, _anyPoints[lastPoint].position, _progress);
           transform.position = newPosition;
       }
       else
       {
           _time = 0;
           _stopTimer = 0;
           _index++;
       }
   }
}
