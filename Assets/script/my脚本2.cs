using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class my脚本2 : MonoBehaviour {

    Coroutine ins协程1;

    IEnumerator fn获取迭代器1() {
        Debug.Log("迭代器1前");
        yield return new WaitForSeconds(2);
        Debug.Log("迭代器1后");
    }


    IEnumerator fn获取迭代器2() {
        Debug.Log("迭代器2前");
        yield return ins协程1; //会先执行"协程1"中的工作, 执行完后, 再返回来执行"fn获取迭代器2()"剩下的内容.
        Debug.Log("迭代器2后");
    }


    // Start is called before the first frame update
    void Start() {
        ins协程1 = StartCoroutine(fn获取迭代器1());
        System.Console.WriteLine(Time.frameCount);
        StartCoroutine(fn获取迭代器2());
    }


    // Update is called once per frame
    void Update() {

    }

}
