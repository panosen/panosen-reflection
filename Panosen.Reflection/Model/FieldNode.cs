using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Reflection.Model
{
    /// <summary>
    /// 字段
    /// </summary>
    public class FieldNode : Node
    {
        /// <summary>
        /// Field
        /// </summary>
        public override NodeType NodeType => NodeType.Field;

        /// <summary>
        /// 类型
        /// </summary>
        public Type FieldType { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public int FieldValue { get; set; }
    }
}
