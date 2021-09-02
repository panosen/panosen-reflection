using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panosen.Reflection.Model
{
    /// <summary>
    /// AssemblyModel
    /// </summary>
    public class AssemblyModel
    {
        /// <summary>
        /// ClassNodeList
        /// </summary>
        public List<ClassNode> ClassNodeList { get; set; }

        /// <summary>
        /// InterfaceNodeList
        /// </summary>
        public List<InterfaceNode> InterfaceNodeList { get; set; }
    }
}
