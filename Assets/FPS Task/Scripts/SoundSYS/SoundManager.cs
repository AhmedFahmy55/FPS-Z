using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FPS.Sounds
{
    public class SoundManager : Singelton<SoundManager>
    {



        [SerializeField] SoundInfo[] soundInfos;


        Dictionary<Sound,float> soundsDelay = new Dictionary<Sound,float>();





        public void PlaySound(Sound sound , Vector3 position)
        {
            GameObject obj = new GameObject("SoundFX");
            AudioSource audio = obj.AddComponent<AudioSource>();
            if(sound == Sound.Ambian)
            {
                audio.spatialBlend = 0;
                audio.volume = .8f;
                audio.loop = true;

            }
            else
            {
                audio.spatialBlend = .8f;
            }
            obj.transform.position = position;
            AudioClip clip = FindClip(sound);
            if(clip == null ) 
            {
                Destroy(obj);
                return;
            }
            audio.clip = clip;
            audio.playOnAwake = true;
            audio.Play();
            Destroy(obj, audio.clip.length);
        }

        private AudioClip FindClip(Sound sound) 
        {
            foreach (var soundinfo in soundInfos)
            {
                if (soundinfo.name != sound) continue;

                if(soundinfo.clips.Length > 1)
                {
                    int index = UnityEngine.Random.Range(0, soundinfo.clips.Length);
                    return soundinfo.clips[index];
                }
                else
                {
                    return soundinfo.clips[0];
                }
                
            }
            return null;
        }
    }

    [System.Serializable]
    struct SoundInfo
    {
        public Sound name;
        public AudioClip[] clips;
    }

}