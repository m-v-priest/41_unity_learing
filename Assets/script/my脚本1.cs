using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.Device;
using static UnityEngine.UI.Image;
using System;


public class my脚本1 : MonoBehaviour {

    private void Start() {

        //第1步:加载AB包
        AssetBundle ab包 = AssetBundle.LoadFromFile(UnityEngine.Application.streamingAssetsPath + "/" + "头像"); //因为我们的包, 同时拷贝到了streamingAssetsPath目录下, 所以,本例,  我们就加载这个目录中的包文件, 即"头像"包.


        //第2步: 加载AB包中的资源
        GameObject go对象 = ab包.LoadAsset<GameObject>("Img/小猴子.png");  //指定LoadAsset()方法, 加载的是GameObject类型的东西.

        //上面的代码, 也可以写成这种形式 : GameObject go对象2 = ab包.LoadAsset("小猴子", typeof(GameObject)) as GameObject;

        //将加载出来的包里的资源, 实例化到屏幕上
        Instantiate(go对象);





    }

    void Update() {

    }

}
