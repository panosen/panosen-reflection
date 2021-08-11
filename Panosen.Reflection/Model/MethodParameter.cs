using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panosen.Reflection.Model
{
    /// <summary>
    /// MethodParameter
    /// </summary>
    public class MethodParameter
    {
        /// <summary>
        /// ParameterType
        /// </summary>
        public Type ParameterType { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
    }
}
