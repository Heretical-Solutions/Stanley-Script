parser grammar StanleyParser;

options
{
	tokenVocab = StanleyLexer;
}

// Top level description

script
	: storyHeader statement+
	;

storyHeader
	: STORY subject
	;

statement
	: defineStatement 
	| commandStatement
	;
	//| eventStatement
	//| assertStatement;

// Define statements

defineStatement
	: REFER_TO relatableSubjectExpression AS subject
	;

// Command statements

commandStatement
	: actionStatement
	| timeStatement;

timeStatement
	: timeExpression (AND timeExpression)* PASSED
	;

// Event statements


// Assert statements

//assertStatement
//	: subjectExpression SHOULD_BE objectExpression
//	;

// Action statements

actionStatement
	: actionWithSubject
	| actionWithArguments;

actionWithSubject
	: pluralSubjectsExpression WERE actionWithArguments
	| subjectExpression WAS actionWithArguments
	| pluralSubjectsExpression HAVE actionWithArguments
	| subjectExpression (HAVE | HAS) actionWithArguments
	;

actionWithArguments
	//: action numberArgument
	//| action locationArgument
	//| action subjectArgument
	//| action objectArgument
	: action objectArgument
	| action
	;

// Action arguments


//numberArgument
//	: TO pluralObjectsExpression
//	;

//locationArgument
//	: TO objectExpression
//	;

//subjectArgument
//	: AT subjectExpression
//	;

objectArgument
	: (TO | AT)? pluralObjectsExpression
	| (TO | AT)? objectExpression
	;

// Subject expressions

pluralSubjectsExpression
	: integer subject
	| subject
	;

relatableSubjectExpression
	: relatablePluralSubjectsExpression
	| relatableSingleSubjectExpression
	;

relatablePluralSubjectsExpression
	: relatableSingleSubjectExpression (AND relatableSingleSubjectExpression)+
	;

relatableSingleSubjectExpression
	: importVariableLiteral
	| subject
	;

subjectExpression
	: selectedSubject
	| subject
	;

selectedSubject
	: A selectionAdjective assertAdjective* OF subject
	;

// Object expressions

pluralObjectsExpression
	: integer object
	| float object
	;

objectExpression
	: selectedObject
	| object
	;

selectedObject
	: A selectionAdjective assertAdjective* OF object
	;

// Selection adjectives

selectionAdjective
	: CLOSIEST
	| FURTHEST
	| RANDOM
	| WEAKEST
	| STRONGEST
	;

assertAdjective
	: UNIQUE
	;

// Time expressions

timeExpression
	: integer timeStep
	| float timeStep
	;

timeStep
	: SECONDS
	| MINUTES
	;

// Simple subjects, objects and actions

subject
	: STRING_LITERAL //QUOTE_SYMB idLiteral (SUFFIX_S)? QUOTE_SYMB
	;

object
	: STRING_LITERAL //QUOTE_SYMB idLiteral (SUFFIX_S)? QUOTE_SYMB
	;

action
	: idLiteral //(SUFFIX_N | SUFFIX_D | SUFFIX_ED)? //Suffixes in LL parsers just don't work. Gonna have to root em out in codegen
	;

// Literals

importVariableLiteral
	: DOLLAR_SYMB idLiteral
	;

integer
	: DECIMAL_LITERAL
	;

float
	: REAL_LITERAL
	;

idLiteral
	: ID
	;