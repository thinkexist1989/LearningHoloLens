using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour {
    KeywordRecognizer keywordRecognizer = null;
    //创建一个keywords字典，里面每个string对应一个System.Action
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action> ();

    // Start is called before the first frame update
    void Start () {
        keywords.Add ("Reset world", () => {
            //调用OnReset方法在每个子对象(descendant object)
            this.BroadcastMessage ("OnReset");
        });

        keywords.Add ("Drop Sphere", () => {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null) {
                //在focused object上调用OnDrop方法，但为何在这里是SendMessage，而在GazeGestureMananger里面是SendMessageUpwards
                focusObject.SendMessage ("OnDrop", SendMessageOptions.DontRequireReceiver);
            }
        });
        //告知KeywordRecognizer添加的关键词
        keywordRecognizer = new KeywordRecognizer (keywords.Keys.ToArray ());
        //为KeywordRecognizer注册一个回调函数
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        //开始识别
        keywordRecognizer.Start ();

    }

    private void KeywordRecognizer_OnPhraseRecognized (PhraseRecognizedEventArgs args) {
        System.Action keywordAction;
        if (keywords.TryGetValue (args.text, out keywordAction)) {
            keywordAction.Invoke ();
        }
    }
}