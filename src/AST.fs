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
    /// variable
    | Var of string
    /// binary operator
    | BinOp of Op * Expr * Expr
    /// prefix
    | Pfx of Op * Expr

let i = (Int 123)
let s = (Int 456)
let a = BinOp(Add, i, s)
let p = Pfx(Sub, a)
let v = Var("zero")

/// global environment
let env =
    [ ("zero", 0)
      ("bl", int ' ')
      ("cr", int '\r')
      ("lf", int '\n')
      ("tab", int '\t') ]

/// search in `env`ironment with key `x`
let rec lookup env x =
    match env with
    | [] -> failwith $"%A{x} not found"
    | (k, v) :: r -> if x = k then v else lookup r x


/// evaluate Expr as calculator
let rec eval (e: Expr) : int =
    match e with
    | Int i -> i
    | BinOp(Add, e1, e2) -> (eval e1) + (eval e2)
    | Pfx(Sub, e) -> -(eval e)
    | Var v -> lookup env v
    | _ -> failwith "eval"

let test =
    printfn "<%A> -> %i" i (eval i)
    printfn "<%A> -> %i" a (eval a)
    printfn "<%A> -> %i" p (eval p)
    printfn "<%A> -> %i" v (eval v)
    0
