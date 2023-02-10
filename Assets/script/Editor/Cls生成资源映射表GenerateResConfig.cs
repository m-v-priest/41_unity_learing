using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Cls生成资源映射表GenerateResConfig : Editor //本类, 必须继承自 Editor类
{
    [MenuItem("Tools/Resources/生成资源映射文件")] //这个特性, 能在你unity菜单上, 生成这个字符串中路径的菜单.
    public static void fn生成资源映射文件Generate() { //这个函数, 用来生成"资源配置文件"

        //第1步: 查找 Resources目录下, 所有预制件的完整路径
        string[] arr路径; //下面会找到的所有物体的路径, 会存到这个数组里.

        arr路径 = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Resources" }); //第1个参数, t用来表示要查找类型, 类型是什么呢? 就是冒号后面的 扩展名是 .prefab 的所有文件. 即预制件文件. 第2个参数,就是在什么目录中查找. 该函数, 会返回找到的所有物体的 GUID值(全局唯一标识符), 就是ID值.

      

        //我们还要继续把该物体的GUID值,转成该物体所在的路径,后者才是我们想要的
        for (int i = 0; i < arr路径.Length; i++) {
            arr路径[i] = AssetDatabase.GUIDToAssetPath(arr路径[i]); //把guid值, 转成路径后, 重新覆盖掉数组中的当前元素值

            //现在, "arr路径"这个数组中, 每条元素的值就是如: "Assets/Resources/Prebs/my预制体.prefab". 
            //那么它的"资源名称"是什么呢? 就是不带扩展名的"my预制体".
            //该资源的路径是什么呢? 从Resources下开始的路径,即 Prebs目录. 注意: Resources本身是不需要带在路径里的! 


            //第2步: 生成对应关系, 即: 资源名称=其路径
            string str文件名 = Path.GetFileNameWithoutExtension(arr路径[i]); //拿到不带扩展名的文件名


            string str文件路径 = arr路径[i].Replace("Assets/Resources/", string.Empty).Replace(".prefab", string.Empty); //路径怎么提取出来呢? 直接把完整的路径, 把里面开头的"Assets/Resources"这部分字符串删了就行. 然后继续把末尾的".prefab"扩展名字符串也删了.


            arr路径[i] = str文件名 +"="+ str文件路径; //这里, 我们就以"my预制体=Prebs"的字符串形式, 来重新存到这个数组中, 覆盖掉"处理前的路径值".
            //现在, 数组中的元素, 就行如"my预制体=Prebs/my预制体"这种字符串了. 这正是我们想要的形式. 等号前面是物体文件名, 等号后面是它的路径.

        }



        //第3步: 把上面的映射关系, 写入文件中. 下面的 "File.WriteAllLines(写入路径, 数组)" 方法, 能将你给的数组, 每个元素写在一行上, 写到该路径中的文件中.
        File.WriteAllLines("Assets/StreamingAssets/ConfigMap资源映射表.txt", arr路径); //注意: 如果你想让你的资源, 被pc, ios, 安卓都能识别的话, 就必须存在 StreamingAssets 目录中. 像这种 unity特殊目录, 还有那些, 你可以去搜索.
        //下面, 如何生成这个txt呢? 就在你 unity中 菜单 Tools -> Resources -> 生成资源映射文件, 点击它, 就能生成txt了. 如果你看不到txt文件, 是因为unity刷新慢. 你可以在win10的自己的资源管理器中, 打开该目录, 就能看到这个txt.
        //每次你 unity中"预制件物体"数量有变化时, 就要点击这个按钮来更新本"资源映射表"txt文件.

        AssetDatabase.Refresh(); //也可以手动刷新unity. 加上这句代码即可.

    }



}
