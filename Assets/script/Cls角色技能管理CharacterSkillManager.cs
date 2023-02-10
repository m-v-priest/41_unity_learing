using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个脚本, 挂载在哪个物体上呢? 哪个角色会发技能, 本脚本就给谁. 我们挂到"go玩家"物体身上.
public class Cls角色技能管理CharacterSkillManager : MonoBehaviour {
    //所有技能的数值列表. 具体数值会从外部的数据文件中来读取, 来赋值给该列表中的所有元素身上.
    public Cls技能相关数据skillData[] arr所有技能列表; //这个列表中, 存放了你所有的技能实例, 每个技能实例身上, 有一大堆字段数据.



    //对每条技能, 做数据上的初始化.
    public void fn初始化技能InitSkill(Cls技能相关数据skillData ins技能相关数据data) {


        //通过预制件的名字, 来找到该预制件的对象.
        ins技能相关数据data.field_技能预制件对象 = Cls资源管理ResourceManager.fn加载Load<GameObject>(ins技能相关数据data.field_技能预制件名称); //把从"资源映射表"txt中, 该"技能名字"所对应的"物体路径", 加载到的该技能的GameObject物体 , 返回回来.




        //ins技能相关数据data.field_技能预制件对象 = Resources.Load<GameObject>("Prebs/" + ins技能相关数据data.field_技能预制件名称); //(1)预制件物体, 你存放在 Resources/Prebs/目录下. (2)该预制件物体的名字, 从你的 "Cls技能相关数据skillData"类的实例对象的"field_技能预制件名称"字段的值中来读取出来. 然后和路径拼在一起, 让unity来帮你找到这个预制件物体, (3)然后再把找到的预制件物体, 赋值给"field_技能预制件对象"字段上. (4)注意: 你想使用 Resources.Load()函数, 来帮你加载资源文件的话, 你的文件, 就必须放在Resources目录下! 而不能放在其他目录中.

        //将本脚本挂载的组件, 赋值给 owner字段. 即本组件,就是技能的"释放者".
        ins技能相关数据data.field_技能所属的对象owner = gameObject;

    }




    //返回一个迭代器
    public IEnumerator fn技能冷却倒计时CoolTimeDown(Cls技能相关数据skillData ins技能相关数据data) {

        ins技能相关数据data.field_技能冷却剩余时间 = ins技能相关数据data.field_技能所需冷却时间; //先读出"技能所需冷却时间", 然后赋值给"技能冷却剩余时间".

        //只要"技能冷却剩余时间"还有, 就让它递减. 即剩余秒数越来越少. 秒数到0后, 就能重新释放下一次技能了(枪管就不热了, 就能重新开枪了)
        while (ins技能相关数据data.field_技能冷却剩余时间 > 0) {
            yield return new WaitForSeconds(1); //等待1秒钟.
            ins技能相关数据data.field_技能冷却剩余时间--;
        }
    }




    ////根据id,在技能列表中,查找到某种技能, 并返回该技能
    //public Cls技能相关数据skillData fn查找某技能Find(int num你要查找的技能id) {
    //    for (int i = 0; i < arr所有技能列表.Length; i++) {
    //        if (arr所有技能列表[i].field_num技能id == num你要查找的技能id) {
    //            return arr所有技能列表[i]; //如果列表中,发现了你要找的"那条技能的id值" 的话,就把该技能, 返回回去
    //        }
    //    }
    //    return null; //没找到, 就返回空.
    //}



    //上面的查找函数, 我们可以把它的参数, 写成一个"委托"类型的. Func<> 能用来引用"具有返回值的函数".Func<> 尖括号中的类型,就是其所指针指向的函数的返回值的类型.
    public Cls技能相关数据skillData fn查找某技能Find(Func<Cls技能相关数据skillData, bool> fn委托指针变量) { //这里, 我们的函数指针, 会指向一个函数, 这个函数接收一个"Cls技能相关数据skillData"类型的数据, 并返回一个bool类型的值.
        for (int i = 0; i < arr所有技能列表.Length; i++) {
            if (fn委托指针变量(arr所有技能列表[i]) == true) { //我们给函数指针变量(假设叫a),所指向的函数(假设叫fnB), 传了个参数进去, 这个参数就是"arr所有技能列表[i]", 这样, fnB()就会处理这个传进来的参数. 然后返回一个bool值. 然后,本"fn查找某技能Find"函数就会根据bool值情况, 来决定是否返回具体的某条技能. 即返回"arr所有技能列表[i]". 这里的具体示意图, 见下面的图片.
                return arr所有技能列表[i];
            }
        }
        return null;
    }





    //能技能释放, 是需要前提条件的,比如法力值够, 处在冷却状态中等.我们分三步做: (1)根据id,查找到某种技能, (2)判断该技能是否出满足"可释放"的条件. (3)若满足, 则返回该技能.
    public Cls技能相关数据skillData fn技能释放前的准备工作(int num你要查找的技能id) {

        Cls技能相关数据skillData ins找到的那条技能 = fn查找某技能Find(arg => arg.field_num技能id == num你要查找的技能id); //如果在技能列表中, 找到了你想找那条id的技能, 就把该技能实例, 返回回来, 拿过来.


        //进行条件判断, 只有下面三个条件都满足后, 才能进行技能释放. 这三个条件是: (1)根据id查找到技能, 存在. (2)剩余冷却时间是0, 即枪管已冷, 可以重新开枪. (3),角色身上的魔法值, 数量大于技能释放的魔法消耗值.
        if (ins找到的那条技能 != null && ins找到的那条技能.field_技能冷却剩余时间 <= 0 && ins找到的那条技能.field_魔法消耗 <= GetComponent<Cls角色当前状态CharacterStatus>().field_魔法值SP) { //GetComponent<脚本名>(); 这句话的意思就是, 从这个脚本所挂载的物体(类的实例)身上, 获取某字段的值.
            Debug.Log($"找到技能, id={num你要查找的技能id}");
            return ins找到的那条技能;
        }
        else {
            return null; //如果根据id,没找到相应技能, 就返回null
        }

    }






    public void fn生成技能GenerateSkill(Cls技能相关数据skillData ins技能相关数据data) {

        //将"技能预制件物体", 实例化显示到界面上. 第二个参数是坐标位置, 第三个参数是旋转角度. 下面的语句就是直接用当前的位置和当前的角度.
        GameObject go技能预制件物体skillGo = Instantiate(ins技能相关数据data.field_技能预制件对象, transform.position, transform.rotation); //Instantiate(),进行实例化. 也就是对一个对象进行复制克隆操作. 注意: 你在游戏运行前, 把"技能预制件"物体拖到本字段上来的话, 在运行unity后, 该"预制件"物体会丢失, 你要重新给本字段把"预制体"物体拖进来才行. 
        Debug.Log($"获取到技能预制件, 名字是:{go技能预制件物体skillGo.name}");


        //销毁"技能预制件物体". 比如你技能释放完毕后, 就在视觉上消失了, 所以要把"技能预制件物体"销毁掉.
        Destroy(go技能预制件物体skillGo, ins技能相关数据data.field_持续时间); //第二个参数是, 指定多长时间后销毁. 比如我们的技能, 会持续2秒钟, 然后消失. 则, 销毁该预制体, 就要在生成它2秒后再来销毁.


        //开启"技能冷却".因为"fn技能冷却倒计时CoolTimeDown"函数返回一个迭代器, 所以我们要用协程来开启它,让该函数执行.
        StartCoroutine(fn技能冷却倒计时CoolTimeDown(ins技能相关数据data));


    }





    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < arr所有技能列表.Length; i++) {
            fn初始化技能InitSkill(arr所有技能列表[i]); //将技能列表中的每一条技能, 都给它的数据做初始化操作.
        }

    }


    // Update is called once per frame
    void Update() {

    }
}
