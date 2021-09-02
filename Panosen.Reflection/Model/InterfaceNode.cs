using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Reflection.Model
{
    /// <summary>
    /// InterfaceNode
    /// </summary>
    public class InterfaceNode : Node
    {
        /// <summary>
        /// Interface
        /// </summary>
        public override NodeType NodeType => NodeType.Interface;

        /// <summary>
        /// 实际类型
        /// </summary>
        public Type InterfaceType { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// 全名称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 基类名称
        /// </summary>
        public string BaseTypeName { get; set; }

        /// <summary>
        /// 基类全名称
        /// </summary>
        public string BaseTypeFullName { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public List<MethodNode> MethodNodeList { get; set; }

        /// <summary>
        /// 特性
        /// </summary>
        public List<Attribute> Attributes { get; set; }
    }

    /// <summary>
    /// InterfaceNodeExtension
    /// </summary>
    public static class InterfaceNodeExtension
    {
        /// <summary>
        /// AddMethod
        /// </summary>
        public static InterfaceNode AddMethod(this InterfaceNode interfaceNode, MethodNode methodNode)
        {
            if (interfaceNode.MethodNodeList == null)
            {
                interfaceNode.MethodNodeList = new List<MethodNode>();
            }

            interfaceNode.MethodNodeList.Add(methodNode);

            return interfaceNode;
        }

        /// <summary>
        /// AddAttribute
        /// </summary>
        public static InterfaceNode AddAttribute(this InterfaceNode interfaceNode, Attribute attribute)
        {
            if (interfaceNode.Attributes == null)
            {
                interfaceNode.Attributes = new List<Attribute>();
            }

            interfaceNode.Attributes.Add(attribute);

            return interfaceNode;
        }
    }
}
