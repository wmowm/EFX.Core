using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    public class Rule
    {

        /// <summary>
        /// 获取牌型
        /// </summary>
        /// <param name="list_card"></param>
        /// <returns></returns>
        public CardType GetCardType(List<Card> list_card)
        {
            if (list_card.Any())
            {
                return CardType.Invalid;
            }
            //先排序
            list_card = list_card.OrderBy(m => m.Number).ToList();
            if (list_card.Count() == 1) return CardType.SingleCard;
            if(list_card.Count() == 2)
            {
                if (list_card[0].Number + list_card[1].Number == 29)
                {
                    return CardType.WangFeng;
                }
                else if(list_card[0].Number != list_card[1].Number)
                {
                    return CardType.Invalid;
                }
                else
                {
                    return CardType.Pair;
                }
            }
            if(list_card.Count() == 3)
            {
                if (list_card[0].Number == list_card[1].Number && list_card[1].Number == list_card[2].Number)
                {
                    return CardType.ThreeNotBelt;
                }
                else
                {
                    return CardType.Invalid;
                }
            }
            if(list_card.Count() == 4)
            {
                if (list_card[0].Number == list_card[1].Number && list_card[1].Number == list_card[2].Number && list_card[2].Number == list_card[3].Number)
                {
                    return CardType.Bomb;
                }
                else if (list_card[0].Number == list_card[1].Number && list_card[1].Number == list_card[2].Number)
                {
                    return CardType.ThreeBeltOne;
                }
                else
                {
                    return CardType.Invalid;
                }
            }
            if(list_card.Count == 5)
            {
                //三带二
                if(list_card[0].Number == list_card[1].Number && list_card[1].Number == list_card[2].Number && list_card[3].Number == list_card[4].Number)
                {
                    return CardType.ThreeBeltTwo;
                }
                //顺子

            }
            if (list_card.Count() == 2) return CardType.Pair;
            return CardType.Aircraft;
        }

        /// <summary>
        /// 验证顺子
        /// </summary>
        /// <returns></returns>
        private bool CheckStraight(List<Card> list_card)
        {
            if (list_card.Count() < 5 || list_card.LastOrDefault().Number > 13) return false;
            for (int i = 0; i < list_card.Count(); i++)
            {
                if (i == list_card.Count() - 1) continue;
                if (list_card[i + 1].Number - list_card[i].Number != 1) return false;
            }
            return true;
        }

        /// <summary>
        /// 验证连对
        /// </summary>
        /// <param name="list_card"></param>
        /// <returns></returns>
        private bool CheckEven(List<Card> list_card)
        {
            if (list_card.Count()<6 || list_card.Count() % 2 != 0 || list_card.LastOrDefault().Number > 13) return false;
            for (int i = 0; i < list_card.Count(); i++)
            {
                if (i == list_card.Count() - 1) continue;
                if ((i+1) % 2 == 0) continue;
                if (list_card[i + 1].Number - list_card[i].Number != 0) return false;
            }
            return true;
        }

        /// <summary>
        /// 验证三顺
        /// </summary>
        /// <param name="list_card"></param>
        /// <returns></returns>
        private bool CheckSanshun(List<Card> list_card)
        {
            if (list_card.Count() < 6 || list_card.Count() % 3 != 0 || list_card.LastOrDefault().Number > 13) return false;
            for (int i = 0; i < list_card.Count(); i++)
            {
                if (i == list_card.Count() - 1) continue;
                if ((i + 1) % 3 == 0) continue;
                if (list_card[i + 1].Number - list_card[i].Number != 0) return false;
            }
            return true;
        }

        /// <summary>
        /// 验证飞机
        /// </summary>
        /// <param name="list_card"></param>
        /// <returns></returns>
        private bool CheckAircraft(List<Card> list_card)
        {
            var list = list_card.GroupBy(m => m.Number).ToList();
            return false;
        }
    }
}
