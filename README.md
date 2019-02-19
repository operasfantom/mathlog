##Instruction for build & run Logical Expressions parser/lexer

1. Install one of the proposed IDEs: Rider(cross-platform) or Visual Studio(Windows only)
2. Create .NET Core console application project
3. Download _FsLexYacc_ nuget package via Nuget package manager in your IDE
4. Run **fsyacc.exe parser.fsp --module LogicParser**
5. Run **fslex.exe lexer.fsl --unicode**
6. Add following files to the project: **LogicParser.fsi LogicParser.fs LogicLexer.fs**
7. Arrange files in solution explorer as shown below(recommend):
    * **LogicTree.fsi**
    * **LogicParser.fs**
    * **LogicLexer.fs**
    * **Program.fs**
    * **other not .fs? files**
    
8. Run default configuration for project