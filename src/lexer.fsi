module lexer

open FSharp.Text.Lexing
open parser/// Rule tokenize
val tokenize: lexbuf: LexBuffer<char> -> token
