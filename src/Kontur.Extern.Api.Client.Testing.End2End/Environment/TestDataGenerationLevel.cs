namespace Kontur.Extern.Api.Client.Testing.End2End.Environment
{
    public enum TestDataGenerationLevel
    {
        /// <summary>
        /// Generate new test data once for each tests running session 
        /// </summary>
        TestRun,
        
        /// <summary>
        /// Cache test generated data in the tmp folder
        /// </summary>
        TempFolder,

        /// <summary>
        /// Cache test generated data in current directory of a tests running session
        /// </summary>
        CurrentDirectory
    }
}