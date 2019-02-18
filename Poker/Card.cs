using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    public class Card
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 花色
        /// </summary>
        public Color Color { get; set; }
    }


    /// <summary>
    /// 花色
    /// </summary>
    public enum Color
    {
        /// <summary>
        /// 黑桃
        /// </summary>
        spade=1,
        /// <summary>
        /// 红桃
        /// </summary>
        hearts=2,
        /// <summary>
        /// 梅花
        /// </summary>
        plum=3,
        /// <summary>
        /// 方块
        /// </summary>
        diamonds=4
    }

    /// <summary>
    /// 牌型
    /// </summary>
    public enum CardType
    {
        /// <summary>
        /// 无效牌型
        /// </summary>
        Invalid = 0,
        /// <summary>
        /// 单牌
        /// </summary>
        SingleCard = 1,
        /// <summary>
        /// 对子
        /// </summary>
        Pair=2,
        /// <summary>
        /// 顺子
        /// </summary>
        Straight=3,
        /// <summary>
        /// 连对
        /// </summary>
        Even=4,
        /// <summary>
        /// 三带一
        /// </summary>
        ThreeBeltOne=5,
        /// <summary>
        /// 三带二
        /// </summary>
        ThreeBeltTwo=6,
        /// <summary>
        /// 飞机
        /// </summary>
        Aircraft=7,
        /// <summary>
        /// 炸弹
        /// </summary>
        Bomb=8,
        /// <summary>
        /// 王炸
        /// </summary>
        WangFeng=9,
        /// <summary>
        /// 三不带
        /// </summary>
        ThreeNotBelt=10,
        /// <summary>
        /// 三顺
        /// </summary>
        Sanshun=11,
        /// <summary>
        /// 四带二
        /// </summary>
        FourBeltTwo

    }
}
