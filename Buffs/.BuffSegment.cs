using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteriousKnives.Buffs
{
    /// <summary>
    /// BUFF分段的计数器.
    /// </summary>
    public struct BuffSegment   //结构体
    {
        public int Time;

        public int NPCSegment;

        public int PlayerSegment;

        /// <summary>
        /// 定义方法-获取npc所处分段-返回int
        /// </summary>
        /// <returns></returns>
        public int GetNpcBuffSegment()
        {
            return Time / NPCSegment;
        }

        /// <summary>
        /// 定义方法-获取player所处分段-返回int
        /// </summary>
        /// <returns></returns>
        public int GetPlayerBuffSegment()
        {
            return Time / PlayerSegment;
        }
    }
}