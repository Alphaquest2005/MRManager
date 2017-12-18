using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp;
using System.Data.Entity;

namespace LinqLib.DynamicCodeGenerator
{
  internal class DynamicClassGenerator
  {
    private Assembly assembly;
    private string dynamicClassName;
    private string dynamicClassNamespace;

    public object GetInstance(params object[] args)
    {
      return this.assembly.CreateInstance(this.dynamicClassNamespace + "." + this.dynamicClassName, false, BindingFlags.Public | BindingFlags.Instance, null, args, null, null);
    }

    public DynamicClassGenerator(string className, Type baseType, IEnumerable<string> Properties, Type propertyType, EventHandler<ClassGenerationEventArgs> classGenerationEventHandler)
    {
      this.dynamicClassName = className;
      this.dynamicClassNamespace = baseType.Namespace;
      CodeNamespace dynamicGenNamespace = new CodeNamespace(this.dynamicClassNamespace);

      dynamicGenNamespace.Imports.Add(new CodeNamespaceImport("LinqLib.DynamicCodeGenerator"));
      dynamicGenNamespace.Imports.Add(new CodeNamespaceImport("System"));
    //  dynamicGenNamespace.Imports.Add(new CodeNamespaceImport(typeof(System.Data.Objects.DataClasses.EntityObject).Namespace));
      if (baseType.Namespace != propertyType.Namespace)
        dynamicGenNamespace.Imports.Add(new CodeNamespaceImport(propertyType.Namespace));

      CodeTypeDeclaration dynamicClass = CreateClass(this.dynamicClassName, baseType);
      dynamicGenNamespace.Types.Add(dynamicClass);

      int propertiesCount = Properties.Count();

      dynamicClass.Members.Add(CreateArrayField("properties", typeof(PropertyInfo[]), true, 0));
      dynamicClass.Members.Add(CreateField("propertiesMap", typeof(Dictionary<string, int>), true));
      dynamicClass.Members.Add(CreateStaticConstructor(propertiesCount, dynamicClassName));

      dynamicClass.Members.Add(CreateGetPropertyNameMethod());
      dynamicClass.Members.Add(CreateGetPropertyIndexMethod());
      dynamicClass.Members.Add(CreateGetPropertyTypeByIndexMethod());
      dynamicClass.Members.Add(CreateGetPropertyTypeByNameMethod());


      
      foreach (string property in Properties)
      {
        dynamicClass.Members.Add(CreateField(property, propertyType, false));
        dynamicClass.Members.Add(CreateProperty(property, propertyType));
      }

      dynamicClass.Members.Add(CreateStringIndexerProperty());
      dynamicClass.Members.Add(CreateNumericIndexerProperty());
      dynamicClass.Members.Add(CreatePropertiesCountProperty());
      dynamicClass.Members.Add(CreatePropertiesNamesNamesProperty());

      GenerateAssembly(dynamicGenNamespace, new Type[] { baseType, propertyType }, classGenerationEventHandler);
    }

    private void GenerateAssembly(CodeNamespace mynamespace, IEnumerable<Type> relatedTypes, EventHandler<ClassGenerationEventArgs> classGenerationEventHandler)
    {
      CodeCompileUnit myassembly = new CodeCompileUnit();
      myassembly.Namespaces.Add(mynamespace);
      string[] locations = relatedTypes.Select(T => T.Assembly.Location).Distinct().ToArray();

      CompilerParameters compParam = new CompilerParameters(locations);
      compParam.ReferencedAssemblies.Add("System.dll");
      compParam.ReferencedAssemblies.Add(this.GetType().Assembly.Location);

      compParam.ReferencedAssemblies.Add("System.Data.Entity.dll");
      compParam.ReferencedAssemblies.Add(this.GetType().Assembly.Location);

      compParam.ReferencedAssemblies.Add("WindowsBase.dll");
      compParam.ReferencedAssemblies.Add(this.GetType().Assembly.Location);
        

      compParam.GenerateInMemory = true;
      compParam.GenerateExecutable = false;

      compParam.CompilerOptions = "/Debug-";
      compParam.IncludeDebugInformation = false;

#if KEEP_TEMP && DEBUG
      compParam.TempFiles.KeepFiles = true;
#endif

      using (CSharpCodeProvider cscp = new CSharpCodeProvider())
      {
        CompilerResults compRes = cscp.CompileAssemblyFromDom(compParam, myassembly);
        if (compRes.Errors.HasErrors)
          this.assembly = null;
        else
          this.assembly = compRes.CompiledAssembly;

#if KEEP_TEMP && DEBUG
        foreach (string tempFile in compRes.TempFiles)
          if (!tempFile.EndsWith(".cs"))
            System.IO.File.Delete(tempFile);
#endif

        if (classGenerationEventHandler != null)
        {
          ClassGenerationEventArgs args = new ClassGenerationEventArgs(compRes.TempFiles.BasePath,
                                                                   compRes.TempFiles.OfType<string>().Where(F => F.EndsWith(".cs")).FirstOrDefault(),
                                                                   compRes.Output,
                                                                   compRes.Errors.HasErrors);
          classGenerationEventHandler(this, args);
        }
      }
    }
  
    private static CodeTypeDeclaration CreateClass(string className, Type baseClass)
    {
      CodeTypeDeclaration dynamicClass = new CodeTypeDeclaration();
      CodeTypeReference dynamicClassBase = new CodeTypeReference(baseClass);

      dynamicClass.IsClass = true;
      dynamicClass.Name = className;
      dynamicClass.BaseTypes.Add(dynamicClassBase);
      dynamicClass.BaseTypes.Add(new CodeTypeReference("IDynamicPivotObject"));
      return dynamicClass;
    }

    private static CodeTypeConstructor CreateStaticConstructor(int propertyCount, string className)
    {
      CodeTypeConstructor constructor = new CodeTypeConstructor();
      //constructor.Attributes = MemberAttributes.Public;

      //Line 1: _propertiesMap = new Dictionary<string, int>();            
      CodeAssignStatement line1 = new CodeAssignStatement();
      line1.Left = new CodeVariableReferenceExpression("_propertiesMap");
      line1.Right = new CodeObjectCreateExpression(new CodeTypeReference(typeof(Dictionary<string, int>)));
      constructor.Statements.Add(line1);

      //Line 2: _properties = this.GetType().GetProperties();
      CodeAssignStatement line2 = new CodeAssignStatement();
      line2.Left = new CodeVariableReferenceExpression("_properties");
      line2.Right = new CodeMethodInvokeExpression(
                        new CodeTypeOfExpression(className), "GetProperties");
      constructor.Statements.Add(line2);

      //Statement 1: int p = 0
      CodeVariableDeclarationStatement stmt1 = new CodeVariableDeclarationStatement(typeof(int), "p");
      stmt1.InitExpression = new CodePrimitiveExpression(0);

      //Statement 2: p + 1
      CodeBinaryOperatorExpression stmt2 = new CodeBinaryOperatorExpression();
      stmt2.Left = new CodeVariableReferenceExpression("p");
      stmt2.Operator = CodeBinaryOperatorType.Add;
      stmt2.Right = new CodePrimitiveExpression(1);

      //Statement 3: p = p + 1
      CodeAssignStatement stmt3 = new CodeAssignStatement();
      stmt3.Left = new CodeVariableReferenceExpression("p");
      stmt3.Right = stmt2;

      //Statement 4: p < propertyCount
      CodeBinaryOperatorExpression stmt4 = new CodeBinaryOperatorExpression();
      stmt4.Left = new CodeVariableReferenceExpression("p");
      stmt4.Operator = CodeBinaryOperatorType.LessThan;
      stmt4.Right = new CodePrimitiveExpression(propertyCount);

      //Statement 5: _properties[p]
      CodeArrayIndexerExpression stmt5 = new CodeArrayIndexerExpression(
                                           new CodeVariableReferenceExpression("_properties"),
                                           new CodeVariableReferenceExpression("p"));

      //Line 3: for (p = 0; p < propertyCount; p++)
      CodeIterationStatement line3 = new CodeIterationStatement();
      line3.InitStatement = stmt1;
      line3.TestExpression = stmt4;
      line3.IncrementStatement = stmt3;

      //Line 3.1 : _propertiesMap.add(_properties[p].Name, p)
      CodeMethodInvokeExpression line3x1 = new CodeMethodInvokeExpression(
                                          new CodeVariableReferenceExpression("_propertiesMap"),
                                          "Add",
                                          new CodePropertyReferenceExpression(stmt5, "Name"),
                                          new CodeVariableReferenceExpression("p"));
      line3.Statements.Add(line3x1);
      constructor.Statements.Add(line3);
      return constructor;
    }

    private static CodeMemberField CreateArrayField(string name, Type type, bool createStatic, int size)
    {
      CodeMemberField fld = new CodeMemberField();
      fld.Name = "_" + name;
      fld.Type = new CodeTypeReference(type);

      if (size > 0)
        fld.InitExpression = new CodeArrayCreateExpression(new CodeTypeReference(type), size);

      fld.Attributes = MemberAttributes.Private;

      if (createStatic)
        fld.Attributes |= MemberAttributes.Static;
      return fld;
    }

    private static CodeMemberField CreateField(string name, Type type, bool createStatic)
    {
      CodeMemberField fld = new CodeMemberField();
      fld.Name = "_" + name;
      fld.Type = new CodeTypeReference(type);
      fld.Attributes = MemberAttributes.Private;
      if (createStatic)
        fld.Attributes |= MemberAttributes.Static;
      return fld;
    }

    private static CodeMemberProperty CreateProperty(string name, Type type)
    {
      CodeMemberProperty prop = new CodeMemberProperty();

      prop.Name = name;
      prop.Type = new CodeTypeReference(type);
      prop.Attributes = MemberAttributes.Public;
      prop.HasGet = true;
      prop.HasSet = true;

      //Get Statement of the property
      CodeFieldReferenceExpression propReference = new CodeFieldReferenceExpression();
      propReference.FieldName = "_" + name;
      prop.GetStatements.Add(new CodeMethodReturnStatement(propReference));

      //Set Statement of the property
      CodeAssignStatement propAssignment = new CodeAssignStatement();
      propAssignment.Left = propReference;
      propAssignment.Right = new CodeArgumentReferenceExpression("value");
      prop.SetStatements.Add(propAssignment);

      return prop;
    }

    private static CodeMemberProperty CreateStringIndexerProperty()
    {
      CodeMemberProperty prop = new CodeMemberProperty();

      prop.Name = "Item";
      prop.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), "index"));
      prop.Type = new CodeTypeReference(typeof(object));
      prop.Attributes = MemberAttributes.Public;
      prop.HasGet = true;
      prop.HasSet = true;

      //Get Statement of the property      
      CodeMethodReturnStatement getStmt = new CodeMethodReturnStatement(
                                         new CodeMethodInvokeExpression(
                                           new CodeArrayIndexerExpression(
                                             new CodeVariableReferenceExpression("_properties"),
                                             new CodeArrayIndexerExpression(
                                               new CodeVariableReferenceExpression("_propertiesMap"),
                                               new CodeArgumentReferenceExpression("index"))),
                                           "GetValue",
                                           new CodeThisReferenceExpression(),
                                           new CodePrimitiveExpression(null)));
      prop.GetStatements.Add(getStmt);


      CodeMethodInvokeExpression setStmt = new CodeMethodInvokeExpression(
                                       new CodeArrayIndexerExpression(
                                         new CodeVariableReferenceExpression("_properties"),
                                         new CodeArrayIndexerExpression(
                                           new CodeVariableReferenceExpression("_propertiesMap"),
                                             new CodeArgumentReferenceExpression("index"))),
                                         "SetValue",
                                         new CodeThisReferenceExpression(),
                                         new CodeArgumentReferenceExpression("value"),
                                         new CodePrimitiveExpression(null));
      prop.SetStatements.Add(setStmt);
      return prop;
    }

    private static CodeMemberProperty CreateNumericIndexerProperty()
    {
      CodeMemberProperty prop = new CodeMemberProperty();

      prop.Name = "Item";
      prop.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "index"));
      prop.Type = new CodeTypeReference(typeof(object));
      prop.Attributes = MemberAttributes.Public;
      prop.HasGet = true;
      prop.HasSet = true;


      //Get Statement of the property      
      CodeMethodReturnStatement getStmt = new CodeMethodReturnStatement(
                                               new CodeMethodInvokeExpression(
                                               new CodeArrayIndexerExpression(
                                                   new CodeVariableReferenceExpression("_properties"),
                                                   new CodeArgumentReferenceExpression("index")),
                                                 "GetValue",
                                                 new CodeThisReferenceExpression(),
                                                 new CodePrimitiveExpression(null)));
      prop.GetStatements.Add(getStmt);


      CodeMethodInvokeExpression setStmt = new CodeMethodInvokeExpression(
                                            new CodeArrayIndexerExpression(
                                              new CodeVariableReferenceExpression("_properties"),
                                              new CodeArgumentReferenceExpression("index")),
                                            "SetValue",
                                            new CodeThisReferenceExpression(),
                                            new CodeArgumentReferenceExpression("value"),
                                            new CodePrimitiveExpression(null));
      prop.SetStatements.Add(setStmt);
      return prop;
    }

    private static CodeMemberMethod CreateGetPropertyNameMethod()
    {
      //object GetPropertyName(int propertyIndex)
      CodeMemberMethod method = new CodeMemberMethod();

      method.Name = "GetPropertyName";
      method.ReturnType = new CodeTypeReference(typeof(string));
      method.Attributes = MemberAttributes.Public;
      method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "propertyIndex"));

      //return _properties[propertyIndex].Name;
      CodeMethodReturnStatement stmt = new CodeMethodReturnStatement(
                                         new CodePropertyReferenceExpression(
                                           new CodeArrayIndexerExpression(
                                             new CodeVariableReferenceExpression("_properties"),
                                             new CodeArgumentReferenceExpression("propertyIndex")),
                                           "Name"));
      method.Statements.Add(stmt);

      return method;
    }

    private static CodeMemberMethod CreateGetPropertyIndexMethod()
    {
      //object GetPropertyName(string propertyName)
      CodeMemberMethod method = new CodeMemberMethod();

      method.Name = "GetPropertyIndex";
      method.ReturnType = new CodeTypeReference(typeof(int));
      method.Attributes = MemberAttributes.Public;
      method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), "propertyName"));

      //return _propertiesMap[propertyName];
      CodeMethodReturnStatement stmt = new CodeMethodReturnStatement(
                                         new CodeArrayIndexerExpression(
                                           new CodeVariableReferenceExpression("_propertiesMap"),
                                           new CodeArgumentReferenceExpression("propertyName")));
      method.Statements.Add(stmt);

      return method;
    }

    private static CodeMemberMethod CreateGetPropertyTypeByIndexMethod()
    {
      //Type GetPropertyType(int propertyIndex)
      CodeMemberMethod method = new CodeMemberMethod();

      method.Name = "GetPropertyType";
      method.ReturnType = new CodeTypeReference(typeof(Type));
      method.Attributes = MemberAttributes.Public;
      method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "propertyIndex"));

      //return _properties[propertyIndex].GetType;
      CodeMethodReturnStatement stmt = new CodeMethodReturnStatement(
                                         new CodePropertyReferenceExpression(
                                           new CodeArrayIndexerExpression(
                                             new CodeVariableReferenceExpression("_properties"),
                                             new CodeArgumentReferenceExpression("propertyIndex")),
                                           "PropertyType"));
      method.Statements.Add(stmt);

      return method;
    }

    private static CodeMemberMethod CreateGetPropertyTypeByNameMethod()
    {
      //Type GetPropertyType(string propertyName)
      CodeMemberMethod method = new CodeMemberMethod();

      method.Name = "GetPropertyType";
      method.ReturnType = new CodeTypeReference(typeof(Type));
      method.Attributes = MemberAttributes.Public;
      method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), "propertyName"));

      //return _properties[_propertiesMap[propertyName]].GetType;
      CodeMethodReturnStatement stmt = new CodeMethodReturnStatement(
                                         new CodePropertyReferenceExpression(
                                           new CodeArrayIndexerExpression(
                                             new CodeVariableReferenceExpression("_properties"),
                                             new CodeArrayIndexerExpression(
                                               new CodeVariableReferenceExpression("_propertiesMap"),
                                               new CodeArgumentReferenceExpression("propertyName"))),
                                           "PropertyType"));
      method.Statements.Add(stmt);

      return method;
    }

    private static CodeMemberProperty CreatePropertiesCountProperty()
    {
      CodeMemberProperty prop = new CodeMemberProperty();

      prop.Name = "PropertiesCount";
      prop.Type = new CodeTypeReference(typeof(int));
      prop.Attributes = MemberAttributes.Public;
      prop.HasGet = true;
      prop.HasSet = false;

      CodeMethodReturnStatement stmt = new CodeMethodReturnStatement(
                                         new CodePropertyReferenceExpression(
                                           new CodeVariableReferenceExpression("_propertiesMap"), "Count"));
      prop.GetStatements.Add(stmt);

      return prop;
    }

    private static CodeMemberProperty CreatePropertiesNamesNamesProperty()
    {
      CodeMemberProperty prop = new CodeMemberProperty();

      prop.Name = "PropertiesNames";
      prop.Type = new CodeTypeReference(typeof(IEnumerable<string>));
      prop.Attributes = MemberAttributes.Public;
      prop.HasGet = true;
      prop.HasSet = false;

      CodeMethodReturnStatement stmt = new CodeMethodReturnStatement(
                                         new CodePropertyReferenceExpression(
                                           new CodeVariableReferenceExpression("_propertiesMap"), "Keys"));
      prop.GetStatements.Add(stmt);

      return prop;
    }
  }
}
