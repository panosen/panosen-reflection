using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Panosen.Reflection.MSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assembly assembly = PrepareAssembly();

            var assemblyModel = AssemblyLoader.LoadAssembly(assembly);

            Assert.IsNotNull(assemblyModel);
            Assert.IsNotNull(assemblyModel.ClassNodeList);
            Assert.AreEqual(1, assemblyModel.ClassNodeList.Count);

            var student = assemblyModel.ClassNodeList[0];
            Assert.IsNotNull(student);

            Assert.IsNotNull(student.PropertyNodeList);
            Assert.AreEqual(1, student.PropertyNodeList.Count);

            {
                var name = student.PropertyNodeList[0];
                Assert.AreEqual("Name", name.Name);
                Assert.AreEqual("System.String", name.PropertyType.FullName);
            }
        }

        private Assembly PrepareAssembly()
        {
            var code = @"
namespace Sample
{
    public class Student
    {
        public string Name { get; set; }
    }
}
";

            Assembly assembly = Compile(code, Assembly.Load(new AssemblyName("System.Runtime")), typeof(object).Assembly);

            return assembly;
        }

        public Assembly Compile(string text, params Assembly[] referencedAssemblies)
        {
            var references = referencedAssemblies.Select(it => MetadataReference.CreateFromFile(it.Location));
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var assemblyName = "_" + Guid.NewGuid().ToString("D");
            var syntaxTrees = new SyntaxTree[] { CSharpSyntaxTree.ParseText(text) };
            var compilation = CSharpCompilation.Create(assemblyName, syntaxTrees, references, options);
            using (var stream = new MemoryStream())
            {
                var compilationResult = compilation.Emit(stream);
                if (compilationResult.Success)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    return Assembly.Load(stream.ToArray());
                }
                throw new InvalidOperationException("Compilation error");
            }
        }
    }
}
