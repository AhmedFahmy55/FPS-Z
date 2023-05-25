using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Minimap
{
    public class MiniMapManager : MonoBehaviour
    {

        public List<MinimapMarkInfo> marksToTrack = new List<MinimapMarkInfo>();

        List<MinimapMark> spwanedmarkes = new List<MinimapMark>();



        private void Start()
        {
            AssignMarks();
        }


        void AssignMarks()
        {
            if (spwanedmarkes != null && spwanedmarkes.Count > 0)
            {
                DestroyActiveMarks();
            }


            MinimapMark mark;


            foreach (MinimapMarkInfo markInfo in marksToTrack)
            {
                GameObject[] targets = GameObject.FindGameObjectsWithTag(markInfo.tag);
                if (targets.Length == 0) continue;

                foreach (GameObject target in targets)
                {
                    mark = Instantiate(markInfo.prefap).GetComponent<MinimapMark>();
                    mark.transform.SetParent(transform);
                    mark.InIt(markInfo.color, target.transform);
                    spwanedmarkes.Add(mark);
                }
            }
        }


        private void DestroyActiveMarks()
        {
            foreach (var mark in spwanedmarkes)
            {
                Destroy(mark.gameObject);
            }
        }
        [System.Serializable]
        public struct MinimapMarkInfo
        {
            public string tag;
            public Color color;
            public GameObject prefap;
        }
    }
}
