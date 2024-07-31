/// @file
/// @brief Abstract Syntax Tree

module AST

type Op =
    | Plus
    | Minus
    | Star
    | Slash

/// language expression
type Expr =
    /// integer
    | Int of i: int
    /// variable
    | Var of v: string
    /// binary operator
    | BinOp of Op * e1: Expr * e2: Expr
    /// prefix
    | Pfx of Op * Expr
    /// let in
    | Let of v: string * rhs: Expr * ebody: Expr

let i = (Int 123)
let s = (Int 456)
let a = BinOp(Plus, i, s)
let p = Pfx(Minus, a)
let v = Var("zero")
let l = Let("x", i, Pfx(Minus, Var("x")))

/// global environment
let env =
    [ ("zero", 0)
      ("bl", int ' ')
      ("cr", int '\r')
      ("lf", int '\n')
      ("tab", int '\t') ]

/// search in `env`ironment with key `x`
let rec lookup (env: (string * int) list) (x: string) =
    match env with
    | [] -> failwith $"%A{x} not found"
    | (k, v) :: r -> if x = k then v else lookup r x

/// evaluate Expr as calculator
let rec eval (e: Expr) (env: (string * int) list) : int =
    match e with
    | Int(i: int) -> i
    | BinOp(Plus, e1, e2) -> (eval e1 env) + (eval e2 env)
    | Pfx(Minus, e) -> -(eval e env)
    | Var(v: string) -> lookup env v
    | Let(v: string, rhs: Expr, ebody) ->
        let x = eval rhs env
        let env' = (v, x) :: env
        eval ebody env'
    | _ -> failwith "eval"

let test =
    assert ($" {i} /{eval i env} " = """ Int 123 /123 """)
    assert ($" {a} /{eval a env} " = """ BinOp (Plus, Int 123, Int 456) /579 """)
    assert ($" {p} /{eval p env} " = """ Pfx (Minus, BinOp (Plus, Int 123, Int 456)) /-579 """)
    assert ($" {v} /{eval v env} " = """ Var "zero" /0 """)
    assert ($" {l} /{eval l env} " = """ Let ("x", Int 123, Pfx (Minus, Var "x")) /-123 """)
    0
