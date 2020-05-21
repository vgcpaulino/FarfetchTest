using System;
using System.Reflection;

namespace SeleniumDotNetTest.Helpers
{
    public class Paths
    {

        private string solutionName;
        private string solutionPath;
        public string solutionRuntimeFilesPath;
        private string projectPath;
        public string projectResourcesFilesPath;

        public Paths()
        {
            GetProjectAndSolutionInfo();

            solutionRuntimeFilesPath = $"{solutionPath}\\RuntimeTestFiles";
            projectResourcesFilesPath = $"{projectPath}\\Resources";
        }
        
        private void GetProjectAndSolutionInfo()
        {
            AssemblyProductAttribute myProject = (AssemblyProductAttribute)AssemblyProductAttribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyProductAttribute));
            string runningPath = AppDomain.CurrentDomain.BaseDirectory;
            int indexProjectName = runningPath.IndexOf(myProject.Product);
             
            solutionName = myProject.Product;
            solutionPath = runningPath.Substring(0, indexProjectName + myProject.Product.Length);

            int indexBinFolder = runningPath.IndexOf("\\bin");
            projectPath = runningPath.Substring(0, indexBinFolder);
        }
        

    }
}
