using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cls玩家控制CharacterInputController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        fn按下按钮_释放技能();
    }


    public void fn按下按钮_释放技能()
    {
        //"技能的释放"管理, 在"Cls角色技能管理CharacterSkillManager"类里面
        Cls角色技能管理CharacterSkillManager ins角色技能管理器 = GetComponent<Cls角色技能管理CharacterSkillManager>(); //先获取到"Cls角色技能管理"组件




        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A键已按下");
            //根据技能id, 来让"Cls角色技能管理"类 告诉我们, 该技能是否可以施放? 若可以, 就把该技能的实例返回给我们.
            Cls技能相关数据skillData ins找到的技能 = ins角色技能管理器.fn技能释放前的准备工作(num你要查找的技能id: 1002);

            if (ins找到的技能 != null)
            {
                ins角色技能管理器.fn生成技能GenerateSkill(ins找到的技能); //依然叫让"Cls角色技能管理"类,来帮我们释放这条技能.
                ;
            }
        }






    }
}
