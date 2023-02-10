using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Cls资源管理ResourceManager
{
    static Dictionary<string, string> dict字典集合ConfigMap =new Dictionary<string, string>(); //这个字典, 会用来存放我们从"资源映射表"txt中,将里面的 string 转成 Dictionary类型 的数据. 注意: 这里不能只声明字典, 而不创建实例. 即,必须创建出字典实例, 否则, 下面给它添加元素时, 就会报错, 提示空引用. 这是很明显的, 如果字典连具象的身体都没有呢, 怎么添加元素呢?


    //下载并解析"资源映射表"txt文件. 这个我们要在本类的"静态构造方法"里来做. 类的"静态构造方法", 作用是: 初始化类中的静态成员. 它什么时候会做呢? 是在类被加载时, 就执行一次.
    static Cls资源管理ResourceManager()
    { //静态构造方法

        //先加载"资源映射表"文件
        string str资源映射表的文件名 = "ConfigMap资源映射表.txt";
        string str资源映射表文件中的内容 = fn加载映射表文件GetConfigFile(str资源映射表的文件名);


        //在解析该"资源映射表"文件, 把里面的内容(键值对), 装到一个字典中.
        fn构建出字典集合BuildMap(str资源映射表文件中的内容);

    }




    //下载"资源映射表"txt文件. 这个文件在StreamingAssets目录中,只能通过 UnityWebRequest 类, 来读取. 要使用这个类, 必须先引入它所在的命名空间: using UnityEngine.Networking;
    public static string fn加载映射表文件GetConfigFile(string str资源映射表的文件名)
    {

        //string url映射表所在路径 = "file://" + Application.streamingAssetsPath + "/ConfigMap资源映射表.txt"; //前面加上的"file://",表示加载的是本地的文件, 而不是http这种网络上的文件. 其实, 这行代码, 在某些手机里面, 可能也是不起作用的, 即读不到这个路径. 所以, 我们要分平台来操作, 重写写成:

        //if(Application.platform == RuntimePlatform.WindowsEditor) { ... }  //这句代码好理解, 但我们一般不会这样写, 而是用 unity宏来写. 如下:



        string url映射表所在路径;  //先声明一个字符串, 之后会给这个字符串赋值.

        //如果在编译器,或pc下, 怎么做...
#if UNITY_EDITOR || UNITY_STANDALONE //注意: 这些语句, 不是c#程序, 而是unity自带的宏标签
        url映射表所在路径 = "file://" + Application.dataPath + "/StreamingAssets/" + str资源映射表的文件名; //Application.dataPath此属性, 用于返回程序的数据文件所在文件夹的路径. 在pc上, 就是指 Assets目录


#elif UNITY_IPHONE   //否则如果在Iphone下
            url映射表所在路径 = "file://" + Application.dataPath + "/Raw/"+str资源映射表的文件名;

            
#elif UNITY_ANDROID  //否则如果在android下
            url映射表所在路径 = "jar:file://" + Application.dataPath + "!/assets/"+str资源映射表的文件名;
#endif



        //下面, 我们通过该映射表所在的文件路径,来拿到该txt文件里的字内容.
        UnityWebRequest ins网络请求 = UnityWebRequest.Get(url映射表所在路径);
        ins网络请求.SendWebRequest();//发起通信, 即发送所请求的要求

        while (true)
        {
            if (ins网络请求.downloadHandler.isDone)
            { //DownloadHandler u是从服务器接收(即下载)数据的对象. 这句代码的意思是, 如果读取完了数据的话
                return ins网络请求.downloadHandler.text;
            }
        }

    }


    //解析"资源映射表"txt文件. 即把 string 变成 Dictionary<string,string>
    public static void fn构建出字典集合BuildMap(string str资源映射表文件内容)
    {

        //这里, 我们要把读到的txt内容, 把它装到字典里. 即存到本类的静态字段 dict字典集合ConfigMap 中.

        //下面的这块代码, 是没问题的, 可以用. 不过教学里提供了另一种方法. 我们就来使用新方法了.
        //string[] arrStr = str资源映射表文件内容.Split("\r\n");

        //foreach (var str单条的映射 in arrStr) {
        //    string[] arr单条的映射键值对 = str单条的映射.Split("=");
        //    dict字典集合ConfigMap.Add(arr单条的映射键值对[0], arr单条的映射键值对[1]);
        //}



        Debug.Log(dict字典集合ConfigMap);


        //教学里的新方法如下. 我们来用 StringReader类, 即字符串读取器.改里面的 ReadLine()方法, 为我们提供了"逐行读取字符串"功能.
        //我们也会用到 using{}, 当退出using的{}代码块时,它能帮我们自动释放小括号()里的"ins字符串读取器"变量(既然是写在小括号里的, 就是说, 该变量即是作为 using的参数来用的). 即退出using{}时, 它会自动帮我们调用 "ins字符串读取器.Dispose()" 方法, 就释放它.
        using (StringReader ins字符串读取器 = new StringReader(str资源映射表文件内容))
        {

            //先读一行
            string str当前读取到的一行 = ins字符串读取器.ReadLine(); //该方法, 会每次只读取字符串(一个字符串可能包含很多行)中的一行内容. 再调一次, 就继续读取下一行. 现在, 读取到的一行, 内容是形如:"my预制体=Prebs/my预制体", 即类似键值对的形式.}

            while (str当前读取到的一行 != null)
            { //当读取到数据的时候,就继续做下面的操作. 如果读取到null时, 就说明整个文件已经都读完了,就跳出该while循环.               
                string[] arr存有单行中的键值对 = str当前读取到的一行.Split("=");  
                dict字典集合ConfigMap.Add(arr存有单行中的键值对[0], arr存有单行中的键值对[1]);

                str当前读取到的一行 = ins字符串读取器.ReadLine(); //继续读取下一行
            }


        }
    }



    //下面的函数, 根据你传入的预制件的名字, 来找到它所在的路径, 然后把该预制件物体, 返回回去. 注意: 下面的方法, 是静态方法, 而unity脚本生命周期函数, 都是实例来用的.
    public static T fn加载Load<T>(string str预制件名字) where T : Object
    {
        Debug.Log($"str预制件名字=>{str预制件名字}");

        foreach (var item in dict字典集合ConfigMap)
        {
            Debug.Log($"{item.Key}:{item.Value}");
        }

        //我们要把
        string path预制体物体的路径 = dict字典集合ConfigMap[str预制件名字]; //对字典, 以键取值. key就是"预制体的名字", value就是"该名字的预制体的所在路径"
        return Resources.Load<T>(path预制体物体的路径);
    }


}
