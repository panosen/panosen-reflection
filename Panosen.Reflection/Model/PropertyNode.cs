using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panosen.Reflection.Model
{
    /// <summary>
    /// 属性
    /// </summary>
    public class PropertyNode : Node
    {
        /// <summary>
        /// 类型
        /// </summary>
        public Type PropertyType { get; set; }

        /// <summary>
        /// 特性
        /// </summary>
        public List<Attribute> Attributes { get; set; }
    }
}
