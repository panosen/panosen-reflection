﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Panosen.Reflection.Model
{
    /// <summary>
    /// MethodNode
    /// </summary>
    public class MethodNode : Node
    {
        /// <summary>
        /// Method
        /// </summary>
        public override NodeType NodeType => NodeType.Method;

        /// <summary>
        /// MethodAttributes
        /// </summary>
        public MethodAttributes Attributes { get; set; }

        /// <summary>
        /// 方法返回类型
        /// </summary>
        public Type ReturnType { get; set; }

        /// <summary>
        /// 方法的参数
        /// </summary>
        public List<MethodParameter> Parameters { get; set; }

        /// <summary>
        /// 属性名称
        /// example: Name
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 属性名称
        /// example: nameProperty
        /// </summary>
        public string PropertyJavaName { get; set; }
    }
}
