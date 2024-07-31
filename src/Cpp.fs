type S =
    | bare of string
    | obj  of {
            pfx: optional S
            nest: S list
            sfx: optional S
        }

sigil C"s" = Cpp.parser s

const tabsize = 4

let dump (src:S) (depth: int = 0): string =
    match S with
    | S.bare s -> (String.replicate (depth*tabsize) " ") @ s
    | S.obj pfx nest sfx ->
        