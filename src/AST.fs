/// @file
/// @brief Abstract Syntax Tree

module AST

type Op =
    | Add
    | Sub
    | Mul
    | Div

/// language expression
type Expr =
    /// integer
    | Int of int
    /// string
    | Str of string
    /// variable
    | Var of string
    /// binary operator
    | BinOp of Op * Expr * Expr

let i = (Int 123)
let s = (Str "hello")
let a = BinOp(Add, i, s)

let rec eval (e: Expr) : int =
    match e with
    | Int i -> i
    | Str s -> 0
    | BinOp(Add, e1, e2) -> eval e1 + eval e2
    | _ -> failwith "eval"

let test = //
    printfn "<%A> -> %i" i (eval i)
    printfn "<%A> -> %i" s (eval s)
    printfn "<%A> -> %i" a (eval a)
    0
