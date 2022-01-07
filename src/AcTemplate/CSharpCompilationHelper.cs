using Microsoft.CodeAnalysis;
using System.Reflection;

namespace AcTemplate
{
    public class CSharpCompilationHelper
    {
        static List<PortableExecutableReference> References;

        static CSharpCompilationHelper()
        {
            NatashaInitializer.InitializeAndPreheating().GetAwaiter().GetResult();
        }

        public static Assembly Compile(List<string> sourceCodes)
        {
            AssemblyCSharpBuilder oop = new AssemblyCSharpBuilder();
            foreach (var sourceCode in sourceCodes)
            {
                oop.Add(sourceCode);
            }

            Assembly assembly = oop.GetAssembly();
            return assembly;
        }

        public static Assembly Compile(string sourceCode)
        {
            AssemblyCSharpBuilder oop = new AssemblyCSharpBuilder();
            oop.Add(sourceCode);
            Assembly assembly = oop.GetAssembly();
            return assembly;
        }

        //public static Assembly Compile(string source)
        //{
        //    var referencePaths = DependencyContext.Default.CompileLibraries.SelectMany(cl => cl.ResolveReferencePaths()).Distinct().ToList();
        //    referencePaths = DependencyContext.Default.CompileLibraries.Select(a => a.Path).ToList();

        //    referencePaths = AppDomain.CurrentDomain.GetAssemblies().Select(a => a.Location).ToList();

        //    References = referencePaths.Select(referencePath => MetadataReference.CreateFromFile(referencePath)).ToList();

        //    var compilation = CSharpCompilation.Create(
        //                                            null,
        //                                            new[] { CSharpSyntaxTree.ParseText(source) },
        //                                           References,
        //                                            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        //    using var memSteam = new MemoryStream();

        //    var emitResult = compilation.Emit(memSteam);

        //    if (!emitResult.Success || emitResult.Diagnostics.FirstOrDefault(x => x.Severity > 0) != null)
        //    {
        //        throw new ArgumentException();
        //    }

        //    memSteam.Seek(0, SeekOrigin.Begin);

        //    return Assembly.Load(memSteam.ToArray());
        //}
    }
}
