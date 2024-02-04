using System.Collections.Generic;

using HereticalSolutions.StanleyScript.Grammars;

using Antlr4.Runtime;

namespace HereticalSolutions.StanleyScript
{
    public class StanleyInterpreter
    {
        public void Interpret(string script)
        {
            AntlrInputStream inputStream = new AntlrInputStream(script);

            StanleyLexer StanleyLexer = new StanleyLexer(inputStream);

            CommonTokenStream commonTokenStream = new CommonTokenStream(StanleyLexer);

            StanleyParser StanleyParser = new StanleyParser(commonTokenStream);

            StanleyParser.ScriptContext context = StanleyParser.script();

            StanleyASTWalker walker = new StanleyASTWalker(
                new StanleyEnvironment(
                    new Dictionary<string, IStanleyVariable>(),
                    new Dictionary<string, List<IStanleyOperation>>(),
                    new Dictionary<string, IStanleyVariable>(),
                    new Stack<IStanleyVariable>(),
                    new List<string>()),
                new List<string>());

            walker.Visit(context);

            var instructions = walker.GetInstructions();

            //foreach (var instruction in instructions)
            //{
            //    UnityEngine.Debug.Log(instruction);
            //}
        }
    }
}