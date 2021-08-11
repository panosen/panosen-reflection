using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panosen.Reflection.Model
{
    /// <summary>
    /// NodeType
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// None
        /// </summary>
        None = 1,

        /// <summary>
        /// Class
        /// </summary>
        Class = 2,

        /// <summary>
        /// Enum
        /// </summary>
        Enum = 3,

        /// <summary>
        /// Property
        /// </summary>
        Property = 4,

        /// <summary>
        /// Method
        /// </summary>
        Method = 5,

        /// <summary>
        /// Namespace
        /// </summary>
        Namespace = 7,

        /// <summary>
        /// Root
        /// </summary>
        Root = 8
    }
}
