using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Minimap
{
    public class MinimapMark : MonoBehaviour
    {

        [SerializeField] float yHeight = 5;

        Transform _target;

        SpriteRenderer _rendere;


        // Start is called before the first frame update
        void Awake()
        {
            _rendere = GetComponent<SpriteRenderer>();
        }



        // Update is called once per frame
        void Update()
        {
            if (_target == null)
            {
                Destroy(gameObject);
                return;
            }
            
            transform.position = _target.position + Vector3.up * yHeight;
        }

        public void InIt(Color color, Transform target)
        {
            _target = target;
            color.a = 1;
            _rendere.color = color;

        }
    }
}
