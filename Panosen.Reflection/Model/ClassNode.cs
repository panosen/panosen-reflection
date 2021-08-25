using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panosen.Reflection.Model
{
    /// <summary>
    /// 类
    /// </summary>
    public class ClassNode : Node
    {
        /// <summary>
        /// 实际类型
        /// </summary>
        public Type ClassType { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// 全名称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 是否是抽象类
        /// </summary>
        public bool IsAbstract { get; set; }

        /// <summary>
        /// 基类名称
        /// </summary>
        public string BaseTypeName { get; set; }

        /// <summary>
        /// 基类全名称
        /// </summary>
        public string BaseTypeFullName { get; set; }

        /// <summary>
        /// 服务原始命名空间
        /// </summary>
        public string OriginalServiceNamespace { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public List<PropertyNode> PropertyNodeList { get; private set; } = new List<PropertyNode>();

        /// <summary>
        /// 方法
        /// </summary>
        public List<MethodNode> MethodNodeList { get; private set; } = new List<MethodNode>();

        /// <summary>
        /// 特性
        /// </summary>
        public List<Attribute> Attributes { get; set; }
    }
}
