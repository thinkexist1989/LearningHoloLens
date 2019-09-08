using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSounds : MonoBehaviour {
    AudioSource impactAudioSource = null; //会添加impact的音效，位于Assets/Resources文件夹
    AudioSource rollingAudioSource = null; //会添加rolling的音效，位于Assets/Resorces文件夹

    bool rolling = false;
    // Start is called before the first frame update
    void Start () {
        impactAudioSource = gameObject.AddComponent<AudioSource> ();
        impactAudioSource.playOnAwake = false;
        impactAudioSource.spatialize = true;
        impactAudioSource.spatialBlend = 1.0f; //2d->3D
        impactAudioSource.dopplerLevel = 0.0f; //doppler等级
        impactAudioSource.rolloffMode = AudioRolloffMode.Logarithmic; //对数形式
        impactAudioSource.maxDistance = 20f; //最大距离

        rollingAudioSource = gameObject.AddComponent<AudioSource> ();
        rollingAudioSource.playOnAwake = false;
        rollingAudioSource.spatialize = true;
        rollingAudioSource.spatialBlend = 1.0f;
        rollingAudioSource.dopplerLevel = 0.0f;
        rollingAudioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        rollingAudioSource.loop = true;

        //从Assets/Resources文件夹加载Sphere Sounds
        impactAudioSource.clip = Resources.Load<AudioClip> ("Impact");
        rollingAudioSource.clip = Resources.Load<AudioClip> ("Rolling");
    }

    //当this指向的object与其他object刚要开始发生碰撞时进入OnCollisionEnter()
    void OnCollisionEnter (Collision collision) {
        //当冲击力足够大，则播放impact声音
        if (collision.relativeVelocity.magnitude >= 0.1f) {
            impactAudioSource.Play ();
        }
    }
    //在每一帧中，若this指向的object持续与其他物体碰撞时则进入OnCollisionStay()
    void OnCollisionStay (Collision collision) {
        Rigidbody rigid = gameObject.GetComponent<Rigidbody> ();
        //当滚动的足够快时，则播放滚动声音
        if (!rolling && rigid.velocity.magnitude >= 0.01f) {
            rolling = true;
            rollingAudioSource.Play ();
        }
        //当滚动慢下来时，停止播放声音
        else if (rolling && rigid.velocity.magnitude < 0.01f) {
            rolling = false;
            rollingAudioSource.Stop ();
        }
    }
    //当this指向的object与其他object结束碰撞时进入OnCollisionExit()
    void OnCollisionExit (Collision collision) {
        //当物体掉下去了，停止碰撞，则停止播放所有声音
        if (rolling) {
            rolling = false;
            impactAudioSource.Stop ();
            rollingAudioSource.Stop ();
        }
    }
}