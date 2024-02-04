//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /SSD1/Repositories/Unity/Heretical Solutions/Stanley Script Unity/Assets/Scripts/Stanley script/Grammars/StanleyLexer.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace HereticalSolutions.StanleyScript.Grammars {
using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public partial class StanleyLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		SPACE=1, COMMENT=2, COMMENT_INPUT=3, LINE_COMMENT=4, REFER_TO=5, STORY=6, 
		SHOULD_BE=7, A=8, AND=9, AT=10, AS=11, OF=12, TO=13, HAD=14, HAS=15, HAVE=16, 
		PASSED=17, WAS=18, WERE=19, CLOSIEST=20, FURTHEST=21, RANDOM=22, STRONGEST=23, 
		UNIQUE=24, WEAKEST=25, MINUTES=26, SECONDS=27, STAR=28, DIVIDE=29, MODULE=30, 
		PLUS=31, MINUS=32, EQUAL_SYMBOL=33, GREATER_SYMBOL=34, LESS_SYMBOL=35, 
		EXCLAMATION_SYMBOL=36, DOT=37, LR_BRACKET=38, RR_BRACKET=39, COMMA=40, 
		SEMI=41, AT_SIGN=42, ZERO_DECIMAL=43, ONE_DECIMAL=44, TWO_DECIMAL=45, 
		SINGLE_QUOTE_SYMB=46, DOUBLE_QUOTE_SYMB=47, REVERSE_QUOTE_SYMB=48, COLON_SYMB=49, 
		DOLLAR_SYMB=50, STRING_LITERAL=51, DECIMAL_LITERAL=52, REAL_LITERAL=53, 
		ID=54, REVERSE_QUOTE_ID=55, ERROR_RECONGNIGION=56;
	public const int
		STANLEY_COMMENT=2, ERRORCHANNEL=3;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN", "STANLEY_COMMENT", "ERRORCHANNEL"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"SPACE", "COMMENT", "COMMENT_INPUT", "LINE_COMMENT", "REFER_TO", "STORY", 
		"SHOULD_BE", "A", "AND", "AT", "AS", "OF", "TO", "HAD", "HAS", "HAVE", 
		"PASSED", "WAS", "WERE", "CLOSIEST", "FURTHEST", "RANDOM", "STRONGEST", 
		"UNIQUE", "WEAKEST", "MINUTES", "SECONDS", "STAR", "DIVIDE", "MODULE", 
		"PLUS", "MINUS", "EQUAL_SYMBOL", "GREATER_SYMBOL", "LESS_SYMBOL", "EXCLAMATION_SYMBOL", 
		"DOT", "LR_BRACKET", "RR_BRACKET", "COMMA", "SEMI", "AT_SIGN", "ZERO_DECIMAL", 
		"ONE_DECIMAL", "TWO_DECIMAL", "SINGLE_QUOTE_SYMB", "DOUBLE_QUOTE_SYMB", 
		"REVERSE_QUOTE_SYMB", "COLON_SYMB", "DOLLAR_SYMB", "QUOTE_SYMB", "STRING_LITERAL", 
		"DECIMAL_LITERAL", "REAL_LITERAL", "ID", "REVERSE_QUOTE_ID", "ID_LITERAL", 
		"DQUOTA_STRING", "SQUOTA_STRING", "BQUOTA_STRING", "DEC_DIGIT", "ERROR_RECONGNIGION"
	};


	public StanleyLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public StanleyLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, null, null, null, null, "'REFER TO'", "'STORY ABOUT'", "'SHOULD BE'", 
		"'A'", "'AND'", "'AT'", "'AS'", "'OF'", "'TO'", "'HAD'", "'HAS'", "'HAVE'", 
		"'PASSED'", "'WAS'", "'WERE'", "'CLOSIEST'", "'FURTHEST'", "'RANDOM'", 
		"'STRONGEST'", "'UNIQUE'", "'WEAKEST'", "'MINUTES'", "'SECONDS'", "'*'", 
		"'/'", "'%'", "'+'", "'-'", "'='", "'>'", "'<'", "'!'", "'.'", "'('", 
		"')'", "','", "';'", "'@'", "'0'", "'1'", "'2'", "'''", "'\"'", "'`'", 
		"':'", "'$'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "SPACE", "COMMENT", "COMMENT_INPUT", "LINE_COMMENT", "REFER_TO", 
		"STORY", "SHOULD_BE", "A", "AND", "AT", "AS", "OF", "TO", "HAD", "HAS", 
		"HAVE", "PASSED", "WAS", "WERE", "CLOSIEST", "FURTHEST", "RANDOM", "STRONGEST", 
		"UNIQUE", "WEAKEST", "MINUTES", "SECONDS", "STAR", "DIVIDE", "MODULE", 
		"PLUS", "MINUS", "EQUAL_SYMBOL", "GREATER_SYMBOL", "LESS_SYMBOL", "EXCLAMATION_SYMBOL", 
		"DOT", "LR_BRACKET", "RR_BRACKET", "COMMA", "SEMI", "AT_SIGN", "ZERO_DECIMAL", 
		"ONE_DECIMAL", "TWO_DECIMAL", "SINGLE_QUOTE_SYMB", "DOUBLE_QUOTE_SYMB", 
		"REVERSE_QUOTE_SYMB", "COLON_SYMB", "DOLLAR_SYMB", "STRING_LITERAL", "DECIMAL_LITERAL", 
		"REAL_LITERAL", "ID", "REVERSE_QUOTE_ID", "ERROR_RECONGNIGION"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "StanleyLexer.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static StanleyLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,56,478,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,7,20,2,21,
		7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,7,26,2,27,7,27,2,28,
		7,28,2,29,7,29,2,30,7,30,2,31,7,31,2,32,7,32,2,33,7,33,2,34,7,34,2,35,
		7,35,2,36,7,36,2,37,7,37,2,38,7,38,2,39,7,39,2,40,7,40,2,41,7,41,2,42,
		7,42,2,43,7,43,2,44,7,44,2,45,7,45,2,46,7,46,2,47,7,47,2,48,7,48,2,49,
		7,49,2,50,7,50,2,51,7,51,2,52,7,52,2,53,7,53,2,54,7,54,2,55,7,55,2,56,
		7,56,2,57,7,57,2,58,7,58,2,59,7,59,2,60,7,60,2,61,7,61,1,0,4,0,127,8,0,
		11,0,12,0,128,1,0,1,0,1,1,1,1,1,1,1,1,1,1,4,1,138,8,1,11,1,12,1,139,1,
		1,1,1,1,1,1,1,1,1,1,2,1,2,1,2,1,2,5,2,151,8,2,10,2,12,2,154,9,2,1,2,1,
		2,1,2,1,2,1,2,1,3,1,3,1,3,1,3,5,3,165,8,3,10,3,12,3,168,9,3,1,3,3,3,171,
		8,3,1,3,5,3,174,8,3,10,3,12,3,177,9,3,1,3,3,3,180,8,3,1,3,1,3,3,3,184,
		8,3,1,3,1,3,1,3,1,3,3,3,190,8,3,1,3,1,3,3,3,194,8,3,3,3,196,8,3,1,3,1,
		3,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,5,1,5,1,5,1,5,1,5,1,5,1,5,1,5,
		1,5,1,5,1,5,1,5,1,6,1,6,1,6,1,6,1,6,1,6,1,6,1,6,1,6,1,6,1,7,1,7,1,8,1,
		8,1,8,1,8,1,9,1,9,1,9,1,10,1,10,1,10,1,11,1,11,1,11,1,12,1,12,1,12,1,13,
		1,13,1,13,1,13,1,14,1,14,1,14,1,14,1,15,1,15,1,15,1,15,1,15,1,16,1,16,
		1,16,1,16,1,16,1,16,1,16,1,17,1,17,1,17,1,17,1,18,1,18,1,18,1,18,1,18,
		1,19,1,19,1,19,1,19,1,19,1,19,1,19,1,19,1,19,1,20,1,20,1,20,1,20,1,20,
		1,20,1,20,1,20,1,20,1,21,1,21,1,21,1,21,1,21,1,21,1,21,1,22,1,22,1,22,
		1,22,1,22,1,22,1,22,1,22,1,22,1,22,1,23,1,23,1,23,1,23,1,23,1,23,1,23,
		1,24,1,24,1,24,1,24,1,24,1,24,1,24,1,24,1,25,1,25,1,25,1,25,1,25,1,25,
		1,25,1,25,1,26,1,26,1,26,1,26,1,26,1,26,1,26,1,26,1,27,1,27,1,28,1,28,
		1,29,1,29,1,30,1,30,1,31,1,31,1,32,1,32,1,33,1,33,1,34,1,34,1,35,1,35,
		1,36,1,36,1,37,1,37,1,38,1,38,1,39,1,39,1,40,1,40,1,41,1,41,1,42,1,42,
		1,43,1,43,1,44,1,44,1,45,1,45,1,46,1,46,1,47,1,47,1,48,1,48,1,49,1,49,
		1,50,1,50,1,50,3,50,393,8,50,1,51,1,51,1,51,3,51,398,8,51,1,52,4,52,401,
		8,52,11,52,12,52,402,1,53,4,53,406,8,53,11,53,12,53,407,1,53,1,53,5,53,
		412,8,53,10,53,12,53,415,9,53,1,53,1,53,4,53,419,8,53,11,53,12,53,420,
		3,53,423,8,53,1,54,1,54,1,55,1,55,1,56,1,56,5,56,431,8,56,10,56,12,56,
		434,9,56,1,57,1,57,1,57,1,57,1,57,1,57,5,57,442,8,57,10,57,12,57,445,9,
		57,1,57,1,57,1,58,1,58,1,58,1,58,1,58,1,58,5,58,455,8,58,10,58,12,58,458,
		9,58,1,58,1,58,1,59,1,59,1,59,1,59,5,59,466,8,59,10,59,12,59,469,9,59,
		1,59,1,59,1,60,1,60,1,61,1,61,1,61,1,61,2,139,152,0,62,1,1,3,2,5,3,7,4,
		9,5,11,6,13,7,15,8,17,9,19,10,21,11,23,12,25,13,27,14,29,15,31,16,33,17,
		35,18,37,19,39,20,41,21,43,22,45,23,47,24,49,25,51,26,53,27,55,28,57,29,
		59,30,61,31,63,32,65,33,67,34,69,35,71,36,73,37,75,38,77,39,79,40,81,41,
		83,42,85,43,87,44,89,45,91,46,93,47,95,48,97,49,99,50,101,0,103,51,105,
		52,107,53,109,54,111,55,113,0,115,0,117,0,119,0,121,0,123,56,1,0,9,3,0,
		9,10,13,13,32,32,2,0,9,9,32,32,2,0,10,10,13,13,3,0,65,90,95,95,97,122,
		4,0,48,57,65,90,95,95,97,122,2,0,34,34,92,92,2,0,39,39,92,92,1,0,96,96,
		1,0,48,57,500,0,1,1,0,0,0,0,3,1,0,0,0,0,5,1,0,0,0,0,7,1,0,0,0,0,9,1,0,
		0,0,0,11,1,0,0,0,0,13,1,0,0,0,0,15,1,0,0,0,0,17,1,0,0,0,0,19,1,0,0,0,0,
		21,1,0,0,0,0,23,1,0,0,0,0,25,1,0,0,0,0,27,1,0,0,0,0,29,1,0,0,0,0,31,1,
		0,0,0,0,33,1,0,0,0,0,35,1,0,0,0,0,37,1,0,0,0,0,39,1,0,0,0,0,41,1,0,0,0,
		0,43,1,0,0,0,0,45,1,0,0,0,0,47,1,0,0,0,0,49,1,0,0,0,0,51,1,0,0,0,0,53,
		1,0,0,0,0,55,1,0,0,0,0,57,1,0,0,0,0,59,1,0,0,0,0,61,1,0,0,0,0,63,1,0,0,
		0,0,65,1,0,0,0,0,67,1,0,0,0,0,69,1,0,0,0,0,71,1,0,0,0,0,73,1,0,0,0,0,75,
		1,0,0,0,0,77,1,0,0,0,0,79,1,0,0,0,0,81,1,0,0,0,0,83,1,0,0,0,0,85,1,0,0,
		0,0,87,1,0,0,0,0,89,1,0,0,0,0,91,1,0,0,0,0,93,1,0,0,0,0,95,1,0,0,0,0,97,
		1,0,0,0,0,99,1,0,0,0,0,103,1,0,0,0,0,105,1,0,0,0,0,107,1,0,0,0,0,109,1,
		0,0,0,0,111,1,0,0,0,0,123,1,0,0,0,1,126,1,0,0,0,3,132,1,0,0,0,5,146,1,
		0,0,0,7,195,1,0,0,0,9,199,1,0,0,0,11,208,1,0,0,0,13,220,1,0,0,0,15,230,
		1,0,0,0,17,232,1,0,0,0,19,236,1,0,0,0,21,239,1,0,0,0,23,242,1,0,0,0,25,
		245,1,0,0,0,27,248,1,0,0,0,29,252,1,0,0,0,31,256,1,0,0,0,33,261,1,0,0,
		0,35,268,1,0,0,0,37,272,1,0,0,0,39,277,1,0,0,0,41,286,1,0,0,0,43,295,1,
		0,0,0,45,302,1,0,0,0,47,312,1,0,0,0,49,319,1,0,0,0,51,327,1,0,0,0,53,335,
		1,0,0,0,55,343,1,0,0,0,57,345,1,0,0,0,59,347,1,0,0,0,61,349,1,0,0,0,63,
		351,1,0,0,0,65,353,1,0,0,0,67,355,1,0,0,0,69,357,1,0,0,0,71,359,1,0,0,
		0,73,361,1,0,0,0,75,363,1,0,0,0,77,365,1,0,0,0,79,367,1,0,0,0,81,369,1,
		0,0,0,83,371,1,0,0,0,85,373,1,0,0,0,87,375,1,0,0,0,89,377,1,0,0,0,91,379,
		1,0,0,0,93,381,1,0,0,0,95,383,1,0,0,0,97,385,1,0,0,0,99,387,1,0,0,0,101,
		392,1,0,0,0,103,397,1,0,0,0,105,400,1,0,0,0,107,422,1,0,0,0,109,424,1,
		0,0,0,111,426,1,0,0,0,113,428,1,0,0,0,115,435,1,0,0,0,117,448,1,0,0,0,
		119,461,1,0,0,0,121,472,1,0,0,0,123,474,1,0,0,0,125,127,7,0,0,0,126,125,
		1,0,0,0,127,128,1,0,0,0,128,126,1,0,0,0,128,129,1,0,0,0,129,130,1,0,0,
		0,130,131,6,0,0,0,131,2,1,0,0,0,132,133,5,47,0,0,133,134,5,42,0,0,134,
		135,5,33,0,0,135,137,1,0,0,0,136,138,9,0,0,0,137,136,1,0,0,0,138,139,1,
		0,0,0,139,140,1,0,0,0,139,137,1,0,0,0,140,141,1,0,0,0,141,142,5,42,0,0,
		142,143,5,47,0,0,143,144,1,0,0,0,144,145,6,1,0,0,145,4,1,0,0,0,146,147,
		5,47,0,0,147,148,5,42,0,0,148,152,1,0,0,0,149,151,9,0,0,0,150,149,1,0,
		0,0,151,154,1,0,0,0,152,153,1,0,0,0,152,150,1,0,0,0,153,155,1,0,0,0,154,
		152,1,0,0,0,155,156,5,42,0,0,156,157,5,47,0,0,157,158,1,0,0,0,158,159,
		6,2,0,0,159,6,1,0,0,0,160,161,5,47,0,0,161,162,5,47,0,0,162,166,1,0,0,
		0,163,165,7,1,0,0,164,163,1,0,0,0,165,168,1,0,0,0,166,164,1,0,0,0,166,
		167,1,0,0,0,167,171,1,0,0,0,168,166,1,0,0,0,169,171,5,35,0,0,170,160,1,
		0,0,0,170,169,1,0,0,0,171,175,1,0,0,0,172,174,8,2,0,0,173,172,1,0,0,0,
		174,177,1,0,0,0,175,173,1,0,0,0,175,176,1,0,0,0,176,183,1,0,0,0,177,175,
		1,0,0,0,178,180,5,13,0,0,179,178,1,0,0,0,179,180,1,0,0,0,180,181,1,0,0,
		0,181,184,5,10,0,0,182,184,5,0,0,1,183,179,1,0,0,0,183,182,1,0,0,0,184,
		196,1,0,0,0,185,186,5,47,0,0,186,187,5,47,0,0,187,193,1,0,0,0,188,190,
		5,13,0,0,189,188,1,0,0,0,189,190,1,0,0,0,190,191,1,0,0,0,191,194,5,10,
		0,0,192,194,5,0,0,1,193,189,1,0,0,0,193,192,1,0,0,0,194,196,1,0,0,0,195,
		170,1,0,0,0,195,185,1,0,0,0,196,197,1,0,0,0,197,198,6,3,0,0,198,8,1,0,
		0,0,199,200,5,82,0,0,200,201,5,69,0,0,201,202,5,70,0,0,202,203,5,69,0,
		0,203,204,5,82,0,0,204,205,5,32,0,0,205,206,5,84,0,0,206,207,5,79,0,0,
		207,10,1,0,0,0,208,209,5,83,0,0,209,210,5,84,0,0,210,211,5,79,0,0,211,
		212,5,82,0,0,212,213,5,89,0,0,213,214,5,32,0,0,214,215,5,65,0,0,215,216,
		5,66,0,0,216,217,5,79,0,0,217,218,5,85,0,0,218,219,5,84,0,0,219,12,1,0,
		0,0,220,221,5,83,0,0,221,222,5,72,0,0,222,223,5,79,0,0,223,224,5,85,0,
		0,224,225,5,76,0,0,225,226,5,68,0,0,226,227,5,32,0,0,227,228,5,66,0,0,
		228,229,5,69,0,0,229,14,1,0,0,0,230,231,5,65,0,0,231,16,1,0,0,0,232,233,
		5,65,0,0,233,234,5,78,0,0,234,235,5,68,0,0,235,18,1,0,0,0,236,237,5,65,
		0,0,237,238,5,84,0,0,238,20,1,0,0,0,239,240,5,65,0,0,240,241,5,83,0,0,
		241,22,1,0,0,0,242,243,5,79,0,0,243,244,5,70,0,0,244,24,1,0,0,0,245,246,
		5,84,0,0,246,247,5,79,0,0,247,26,1,0,0,0,248,249,5,72,0,0,249,250,5,65,
		0,0,250,251,5,68,0,0,251,28,1,0,0,0,252,253,5,72,0,0,253,254,5,65,0,0,
		254,255,5,83,0,0,255,30,1,0,0,0,256,257,5,72,0,0,257,258,5,65,0,0,258,
		259,5,86,0,0,259,260,5,69,0,0,260,32,1,0,0,0,261,262,5,80,0,0,262,263,
		5,65,0,0,263,264,5,83,0,0,264,265,5,83,0,0,265,266,5,69,0,0,266,267,5,
		68,0,0,267,34,1,0,0,0,268,269,5,87,0,0,269,270,5,65,0,0,270,271,5,83,0,
		0,271,36,1,0,0,0,272,273,5,87,0,0,273,274,5,69,0,0,274,275,5,82,0,0,275,
		276,5,69,0,0,276,38,1,0,0,0,277,278,5,67,0,0,278,279,5,76,0,0,279,280,
		5,79,0,0,280,281,5,83,0,0,281,282,5,73,0,0,282,283,5,69,0,0,283,284,5,
		83,0,0,284,285,5,84,0,0,285,40,1,0,0,0,286,287,5,70,0,0,287,288,5,85,0,
		0,288,289,5,82,0,0,289,290,5,84,0,0,290,291,5,72,0,0,291,292,5,69,0,0,
		292,293,5,83,0,0,293,294,5,84,0,0,294,42,1,0,0,0,295,296,5,82,0,0,296,
		297,5,65,0,0,297,298,5,78,0,0,298,299,5,68,0,0,299,300,5,79,0,0,300,301,
		5,77,0,0,301,44,1,0,0,0,302,303,5,83,0,0,303,304,5,84,0,0,304,305,5,82,
		0,0,305,306,5,79,0,0,306,307,5,78,0,0,307,308,5,71,0,0,308,309,5,69,0,
		0,309,310,5,83,0,0,310,311,5,84,0,0,311,46,1,0,0,0,312,313,5,85,0,0,313,
		314,5,78,0,0,314,315,5,73,0,0,315,316,5,81,0,0,316,317,5,85,0,0,317,318,
		5,69,0,0,318,48,1,0,0,0,319,320,5,87,0,0,320,321,5,69,0,0,321,322,5,65,
		0,0,322,323,5,75,0,0,323,324,5,69,0,0,324,325,5,83,0,0,325,326,5,84,0,
		0,326,50,1,0,0,0,327,328,5,77,0,0,328,329,5,73,0,0,329,330,5,78,0,0,330,
		331,5,85,0,0,331,332,5,84,0,0,332,333,5,69,0,0,333,334,5,83,0,0,334,52,
		1,0,0,0,335,336,5,83,0,0,336,337,5,69,0,0,337,338,5,67,0,0,338,339,5,79,
		0,0,339,340,5,78,0,0,340,341,5,68,0,0,341,342,5,83,0,0,342,54,1,0,0,0,
		343,344,5,42,0,0,344,56,1,0,0,0,345,346,5,47,0,0,346,58,1,0,0,0,347,348,
		5,37,0,0,348,60,1,0,0,0,349,350,5,43,0,0,350,62,1,0,0,0,351,352,5,45,0,
		0,352,64,1,0,0,0,353,354,5,61,0,0,354,66,1,0,0,0,355,356,5,62,0,0,356,
		68,1,0,0,0,357,358,5,60,0,0,358,70,1,0,0,0,359,360,5,33,0,0,360,72,1,0,
		0,0,361,362,5,46,0,0,362,74,1,0,0,0,363,364,5,40,0,0,364,76,1,0,0,0,365,
		366,5,41,0,0,366,78,1,0,0,0,367,368,5,44,0,0,368,80,1,0,0,0,369,370,5,
		59,0,0,370,82,1,0,0,0,371,372,5,64,0,0,372,84,1,0,0,0,373,374,5,48,0,0,
		374,86,1,0,0,0,375,376,5,49,0,0,376,88,1,0,0,0,377,378,5,50,0,0,378,90,
		1,0,0,0,379,380,5,39,0,0,380,92,1,0,0,0,381,382,5,34,0,0,382,94,1,0,0,
		0,383,384,5,96,0,0,384,96,1,0,0,0,385,386,5,58,0,0,386,98,1,0,0,0,387,
		388,5,36,0,0,388,100,1,0,0,0,389,393,3,91,45,0,390,393,3,93,46,0,391,393,
		3,95,47,0,392,389,1,0,0,0,392,390,1,0,0,0,392,391,1,0,0,0,393,102,1,0,
		0,0,394,398,3,115,57,0,395,398,3,117,58,0,396,398,3,119,59,0,397,394,1,
		0,0,0,397,395,1,0,0,0,397,396,1,0,0,0,398,104,1,0,0,0,399,401,3,121,60,
		0,400,399,1,0,0,0,401,402,1,0,0,0,402,400,1,0,0,0,402,403,1,0,0,0,403,
		106,1,0,0,0,404,406,3,121,60,0,405,404,1,0,0,0,406,407,1,0,0,0,407,405,
		1,0,0,0,407,408,1,0,0,0,408,409,1,0,0,0,409,413,5,46,0,0,410,412,3,121,
		60,0,411,410,1,0,0,0,412,415,1,0,0,0,413,411,1,0,0,0,413,414,1,0,0,0,414,
		423,1,0,0,0,415,413,1,0,0,0,416,418,5,46,0,0,417,419,3,121,60,0,418,417,
		1,0,0,0,419,420,1,0,0,0,420,418,1,0,0,0,420,421,1,0,0,0,421,423,1,0,0,
		0,422,405,1,0,0,0,422,416,1,0,0,0,423,108,1,0,0,0,424,425,3,113,56,0,425,
		110,1,0,0,0,426,427,3,119,59,0,427,112,1,0,0,0,428,432,7,3,0,0,429,431,
		7,4,0,0,430,429,1,0,0,0,431,434,1,0,0,0,432,430,1,0,0,0,432,433,1,0,0,
		0,433,114,1,0,0,0,434,432,1,0,0,0,435,443,5,34,0,0,436,437,5,92,0,0,437,
		442,9,0,0,0,438,439,5,34,0,0,439,442,5,34,0,0,440,442,8,5,0,0,441,436,
		1,0,0,0,441,438,1,0,0,0,441,440,1,0,0,0,442,445,1,0,0,0,443,441,1,0,0,
		0,443,444,1,0,0,0,444,446,1,0,0,0,445,443,1,0,0,0,446,447,5,34,0,0,447,
		116,1,0,0,0,448,456,5,39,0,0,449,450,5,92,0,0,450,455,9,0,0,0,451,452,
		5,39,0,0,452,455,5,39,0,0,453,455,8,6,0,0,454,449,1,0,0,0,454,451,1,0,
		0,0,454,453,1,0,0,0,455,458,1,0,0,0,456,454,1,0,0,0,456,457,1,0,0,0,457,
		459,1,0,0,0,458,456,1,0,0,0,459,460,5,39,0,0,460,118,1,0,0,0,461,467,5,
		96,0,0,462,466,8,7,0,0,463,464,5,96,0,0,464,466,5,96,0,0,465,462,1,0,0,
		0,465,463,1,0,0,0,466,469,1,0,0,0,467,465,1,0,0,0,467,468,1,0,0,0,468,
		470,1,0,0,0,469,467,1,0,0,0,470,471,5,96,0,0,471,120,1,0,0,0,472,473,7,
		8,0,0,473,122,1,0,0,0,474,475,9,0,0,0,475,476,1,0,0,0,476,477,6,61,1,0,
		477,124,1,0,0,0,26,0,128,139,152,166,170,175,179,183,189,193,195,392,397,
		402,407,413,420,422,432,441,443,454,456,465,467,2,6,0,0,0,3,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace HereticalSolutions.StanleyScript.Grammars
