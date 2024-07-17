/// @file
/// @brief Abstract Syntax Tree

module AST

/// language expression
type Expr =
    /// integer
    | Int of int
    /// string
    | Str of string
    /// variable
    | Var of string
    /// binary operator
    | BinOp of string * Expr * Expr
