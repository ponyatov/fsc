/// @file
/// @brief Abstract Syntax Tree

module AST

type Expr = 
| Int of int
| Str of string
| Var of string
