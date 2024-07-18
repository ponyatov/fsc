﻿// https://thanos.codes/blog/using-fslexyacc-the-fsharp-lexer-and-parser/
// https://en.wikibooks.org/wiki/F_Sharp_Programming/Lexing_and_Parsing

open AST

[<EntryPoint>]
let main (argv: string array) =
    for arg in argv do
        printfn "%s" arg

    AST.test
