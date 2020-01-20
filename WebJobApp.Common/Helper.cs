namespace WebJobApp.Common
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class Helper
    {
        #region Methods

        #region Public Methods

        /// <summary>
        ///     Loads the specified assembly by name and then instantiates an instance of the specified className.
        ///     Note, the assembly file MUST be in the same directory as the executing application or registered
        ///     in the GAC to be loaded.
        ///     If the assembly or class cannot be found an exception will be thrown to the caller.
        /// </summary>
        /// <param name="assemblyName">
        ///     The name of the assembly without it's file extension.  For example,
        ///     to load the "SomeAssembly.dll" library, the assemblyName parameter would be "SomeAssembly".
        /// </param>
        /// <param name="className">
        ///     The full namespace and class name to load from the assembly.  For example,
        ///     to load the "SomeClass" class in the "SomeAssembly" assembly, the className parameter would be
        ///     "SomeNamespace.SomeClass"
        /// </param>
        /// .
        /// <returns>A reference to the instantiated class.</returns>
        public static object LoadObject(string assemblyName, string className)
        {
            if (string.IsNullOrWhiteSpace(assemblyName))
            {
                throw new ArgumentNullException("assemblyName");
            }

            if (string.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentNullException("className");
            }

            //use type name to get type from asm; note we WANT case specificity 
            Assembly asm = Assembly.GetExecutingAssembly();
            AssemblyName[] asmNames = asm.GetReferencedAssemblies();
            //check to see if the assembly the caller wants to load is already a referenced assembly
            //in memory so we can reuse that assembly instance
            AssemblyName refAsm = asmNames.FirstOrDefault(aname => aname.Name == assemblyName);

            asm = refAsm == null ? Assembly.Load(assemblyName) : Assembly.Load(refAsm);

            //Create an instance of the class needed from the loaded assembly - we are loading the class name case sensitive!
            Type classTypeInstance = asm.GetType(className.Trim(), true, false);

            //Create and return an instance of the class.
            return Activator.CreateInstance(classTypeInstance);
        }

        public static object LoadObject(string fullTypeName)
        {
            if (string.IsNullOrWhiteSpace(fullTypeName))
            {
                throw new ArgumentNullException("fullTypeName");
            }

            string[] args = fullTypeName.Split(new[] {','});
            if (args.Length != 2)
            {
                throw new ArgumentException("fullTypeName is Invalid Assembly name and class name");
            }

            //use type name to get type from asm; note we WANT case specificity 
            Assembly asm = Assembly.GetExecutingAssembly();
            AssemblyName[] asmNames = asm.GetReferencedAssemblies();
            string assemblyName = args[1].Trim();
            string className = args[0];
            //check to see if the assembly the caller wants to load is already a referenced assembly
            //in memory so we can reuse that assembly instance
            AssemblyName refAsm = asmNames.FirstOrDefault(aname => aname.Name == assemblyName);

            asm = refAsm == null ? Assembly.Load(assemblyName) : Assembly.Load(refAsm);

            //Create an instance of the class needed from the loaded assembly - we are loading the class name case sensitive!
            Type classTypeInstance = asm.GetType(className.Trim(), true, false);

            //Create and return an instance of the class.
            return Activator.CreateInstance(classTypeInstance);
        }

        #endregion

        #endregion
    }
}