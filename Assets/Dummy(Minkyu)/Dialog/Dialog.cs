using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class MsgInfo
{
    public string Msg;
    public float Delay;
}
public class Dialog : MonoBehaviour
{
    [SerializeField]
    public List<MsgInfo> MsgQueue;

     Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }
    private void Start()
    {
       StartCoroutine(MsgQueue_Start(MsgQueue[0]));
    }
    private void Update()
    {
        
    }

    public IEnumerator MsgQueue_Start(MsgInfo Info)
    {
        string Temp_Msg = Info.Msg; 
        float Delay = Info.Delay;

        for(int i =0; i< Temp_Msg.Length; i++)
        {
            yield return new WaitForSeconds(Delay);
            text.text += Temp_Msg[i];
        }
        yield return null;
    }
    
}