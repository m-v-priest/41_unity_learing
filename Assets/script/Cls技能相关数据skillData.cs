using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Cls技能相关数据skillData  //注意: 本类不需要继承自 MonoBehaviour类 !
{
    public int field_num技能id;
    public string field_str技能名称;

    [Tooltip("对技能详情的多行描述")] //添加一个tooltip属性, 可以在 instpector面板上提示注释
    public string field_技能介绍描述;

    public int field_技能所需冷却时间;
    public int field_技能冷却剩余时间;
    public int field_魔法消耗;
    public int field_攻击距离;
    public float field_攻击角度;

    public string[] field_arr攻击目标tags = { "敌人" };
    public Transform[] field_arr攻击目标对象数组;

    public string[] field_使用该技能会付出的成本 = { "魔法值", "健康度" };

    public float field_伤害比例;
    public float field_持续时间;
    public float field_伤害间隔;

    public GameObject field_技能所属的对象owner;
    public string field_技能预制件名称;
    public GameObject field_技能预制件对象;

    public string field_受到打击的预制件名称;
    public GameObject field_受到打击的预制件对象;

    public int field_技能等级;

    [Tooltip("攻击类型是指: 单攻, 还是群攻")]
    public enm技能攻击类型skillAttackType field_攻击类型;

    [Tooltip("选择类型是指: 技能释放出来的攻击形状, 是扇形, 还是矩形")]
    public enm技能形状选择类型 field_技能形状选择类型;






}



public enum enm技能攻击类型skillAttackType { }
public enum enm技能形状选择类型 { }