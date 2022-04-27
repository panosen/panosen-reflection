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
        None,

        /// <summary>
        /// Class
        /// </summary>
        Class,

        /// <summary>
        /// Enum
        /// </summary>
        Enum,

        /// <summary>
        /// Property
        /// </summary>
        Property,

        /// <summary>
        /// Method
        /// </summary>
        Method,

        /// <summary>
        /// Root
        /// </summary>
        Interface,

        /// <summary>
        /// Field
        /// </summary>
        Field
    }
}
