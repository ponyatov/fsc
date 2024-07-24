// https://thanos.codes/blog/using-fslexyacc-the-fsharp-lexer-and-parser/
// https://en.wikibooks.org/wiki/F_Sharp_Programming/Lexing_and_Parsing

module compiler

open AST

type ByteCode =
    // single-path sequence
    | Seq of ByteCode array
    // control
    | Nop
    | Halt
    // math
    | Add
    | Sub
    | Mul
    | Div
    // stack
    | Dup
    | Drop
    | Swap
    | Over

let test = //
    printfn "%A" (Seq([| Nop; Halt |]))

[<EntryPoint>]
let main (argv: string array) =
    for arg in argv do
        printfn "%s" arg

    test
    AST.test
