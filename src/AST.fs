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

let test = //
    let i = (Int 123)
    printfn "<%A>" i
    let s = (Str "hello")
    printfn "<%A>" s
    printfn "<%A>" (BinOp("+", i, s))
    0
