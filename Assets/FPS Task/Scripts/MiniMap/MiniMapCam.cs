using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Minimap
{
    public class MiniMapCam : MonoBehaviour
    {
        Transform player;





        private void Awake()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            Vector3 position = player.position;
            position.y = transform.position.y;
            transform.position = position;

            Quaternion rotation = player.rotation;
            transform.rotation = Quaternion.Euler(90, rotation.eulerAngles.y, 0);

        }
    }
}
