using System;
using System.Collections.Specialized;

namespace LinqLib.DynamicCodeGenerator
{
  /// <summary>
  /// Provides information 
  /// </summary>
  public class ClassGenerationEventArgs : EventArgs
  {
    /// <summary>
    /// Initializes a new instance of ClassGenerationEventArgs class.
    /// </summary>
    /// <param name="basePath">The base path of all the generated files.</param>
    /// <param name="codeFile">The path of the generated CS file.</param>
    /// <param name="output">Compiler output information.</param>
    /// <param name="hasErrors">Indicates if errors occurred during compilation.</param>
    public ClassGenerationEventArgs(string basePath, string codeFile, StringCollection output, bool hasErrors)
    {
      this.BasePath = basePath;
      this.CodeFile = codeFile;
      this.Output = output;
      this.HasError = hasErrors;
    }

    /// <summary>
    /// The base path of all the generated files.
    /// </summary>
    public string BasePath { get; private set; }

    /// <summary>
    /// The path of the generated CS file.
    /// </summary>
    public string CodeFile { get; private set; }

    /// <summary>
    /// Gets a value that indicates whether the generation process contains errors.
    /// </summary>
    public bool HasError { get; private set; }

    /// <summary>
    /// The compiler output information.
    /// </summary>
    public StringCollection Output { get; private set; }
  }
}
