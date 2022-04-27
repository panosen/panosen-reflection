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

            Assert.IsNotNull(assemblyModel.InterfaceNodeList);
            Assert.AreEqual(1, assemblyModel.InterfaceNodeList.Count);

            {
                var student = assemblyModel.ClassNodeList[0];
                Assert.IsNotNull(student);

                Assert.IsNotNull(student.PropertyNodeList);
                Assert.AreEqual(1, student.PropertyNodeList.Count);

                {
                    var name = student.PropertyNodeList[0];
                    Assert.AreEqual("Name", name.Name);
                    Assert.AreEqual("System.String", name.PropertyType.FullName);
                }

                Assert.IsNotNull(student.MethodNodeList);
                Assert.AreEqual(1, student.MethodNodeList.Count);

                {
                    var sayhi = student.MethodNodeList[0];
                    Assert.AreEqual("SayHi", sayhi.Name);
                    Assert.AreEqual("System.Int32", sayhi.ReturnType.FullName);
                    Assert.IsTrue((sayhi.Attributes & MethodAttributes.Public) == MethodAttributes.Public);
                }
            }

            {
                var iSample = assemblyModel.InterfaceNodeList[0];
                Assert.IsNotNull(iSample);

                Assert.IsNotNull(iSample.MethodNodeList);
                Assert.AreEqual(1, iSample.MethodNodeList.Count);

                {
                    var sayHi = iSample.MethodNodeList[0];
                    Assert.IsNotNull(sayHi);

                    Assert.AreEqual("SayHi", sayHi.Name);

                    Assert.AreEqual(typeof(string), sayHi.ReturnType);

                    Assert.IsNotNull(sayHi.Parameters);
                    Assert.AreEqual(1, sayHi.Parameters.Count);
                    Assert.AreEqual(typeof(string), sayHi.Parameters[0].ParameterType);
                    Assert.AreEqual("name", sayHi.Parameters[0].Name);
                }
            }

            {
                var status = assemblyModel.EnumNodeList[0];
                Assert.IsNotNull(status);

                Assert.AreEqual("Status", status.Name);

                Assert.IsNotNull(status.FieldNodeList);
                Assert.AreEqual(4, status.FieldNodeList.Count);

                {
                    Assert.AreEqual(typeof(int), status.FieldNodeList[1].FieldType);
                    Assert.AreEqual(1, status.FieldNodeList[1].FieldValue);
                }
                {
                    Assert.AreEqual(typeof(int), status.FieldNodeList[2].FieldType);
                    Assert.AreEqual(3, status.FieldNodeList[2].FieldValue);
                }
                {
                    Assert.AreEqual(typeof(int), status.FieldNodeList[3].FieldType);
                    Assert.AreEqual(7, status.FieldNodeList[3].FieldValue);
                }
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

        public int SayHi()
        {
            return 1;
        }
    }

    public interface ISample
    {
        string SayHi(string name);
    }

    public enum Status
    {
        None = 1,

        Enabled = 3,

        Disabled = 7
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
