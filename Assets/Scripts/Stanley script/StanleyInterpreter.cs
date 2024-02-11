using System.Collections.Generic;

using HereticalSolutions.StanleyScript.Grammars;

using Antlr4.Runtime;

namespace HereticalSolutions.StanleyScript
{
    public class StanleyInterpreter
    {
        private readonly StanleyASTWalker walker;

        private readonly IRuntimeEnvironment environment;

        public StanleyInterpreter(
            StanleyASTWalker walker,
            IRuntimeEnvironment environment)
        {
            this.walker = walker;

            this.environment = environment;
        }

        public IRuntimeEnvironment Environment
        {
            get
            {
                return environment;
            }
        }

        public IExecutable Executable
        {
            get
            {
                return environment as IExecutable;
            }
        }

        public string[] Interpret(string script)
        {
            AntlrInputStream inputStream = new AntlrInputStream(script);

            StanleyLexer StanleyLexer = new StanleyLexer(inputStream);

            CommonTokenStream commonTokenStream = new CommonTokenStream(StanleyLexer);

            StanleyParser StanleyParser = new StanleyParser(commonTokenStream);

            StanleyParser.ScriptContext context = StanleyParser.script();

            walker.Initialize();

            walker.Visit(context);

            var instructions = walker.GetInstructions();

            walker.Initialize();

            return instructions;
        }

        public void Execute(string script)
        {
            var instructions = Interpret(script);

            UnityEngine.Debug.Log("--------");

            UnityEngine.Debug.Log("INSTRUCTIONS:");

            UnityEngine.Debug.Log("--------");

            for (int i = 0; i < instructions.Length; i++)
            {
                UnityEngine.Debug.Log($"{i}: {instructions[i]}");
            }

            UnityEngine.Debug.Log("--------");

            var executable = environment as IExecutable;

            executable.LoadProgram(instructions);

            executable.Start();
        }
    }
}