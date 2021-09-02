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
        /// Class
        /// </summary>
        public override NodeType NodeType => NodeType.Class;

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
        /// 属性
        /// </summary>
        public List<PropertyNode> PropertyNodeList { get; set; }

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
    /// ClassNodeExtension
    /// </summary>
    public static class ClassNodeExtension
    {
        /// <summary>
        /// AddProperty
        /// </summary>
        public static ClassNode AddProperty(this ClassNode classNode, PropertyNode propertyNode)
        {
            if (classNode.PropertyNodeList == null)
            {
                classNode.PropertyNodeList = new List<PropertyNode>();
            }

            classNode.PropertyNodeList.Add(propertyNode);

            return classNode;
        }

        /// <summary>
        /// AddMethod
        /// </summary>
        public static ClassNode AddMethod(this ClassNode classNode, MethodNode methodNode)
        {
            if (classNode.MethodNodeList == null)
            {
                classNode.MethodNodeList = new List<MethodNode>();
            }

            classNode.MethodNodeList.Add(methodNode);

            return classNode;
        }

        /// <summary>
        /// AddAttribute
        /// </summary>
        public static ClassNode AddAttribute(this ClassNode classNode, Attribute attribute)
        {
            if (classNode.Attributes == null)
            {
                classNode.Attributes = new List<Attribute>();
            }

            classNode.Attributes.Add(attribute);

            return classNode;
        }
    }
}
