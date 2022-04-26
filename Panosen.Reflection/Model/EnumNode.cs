using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Reflection.Model
{
    /// <summary>
    /// 枚举
    /// </summary>
    public class EnumNode : Node
    {
        /// <summary>
        /// Enum
        /// </summary>
        public override NodeType NodeType => NodeType.Enum;
    }
}
