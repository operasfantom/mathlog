open LogicTree

open System.IO
open Microsoft.FSharp.Text.Lexing

let (|>) x f = f x;;

type op with
        member x.format() = match x with | Conj -> "&" | Disj -> "|" | Impl -> "->"

let tree_from_text text = text |> LexBuffer<_>.FromString |> (LogicLexer.tokenize |> LogicParser.parse)

let expression_to_tree text =
  let rec analyse tree = seq {
    match tree with
    | Var v ->
      yield v
    | Neg t ->
      yield "(!"
      yield! (analyse t)
      yield ")"
    | Binop(op, t1, t2) ->
      yield "("
      yield op.format()
      yield ","
      yield! (analyse t1)
      yield ","
      yield! (analyse t2)
      yield ")"
  }

  text |> tree_from_text |> analyse |> String.concat "" 

[<EntryPoint>]
let main argv =
  use input = File.OpenText("input.txt")
  use output = File.CreateText("output.txt")
  input.ReadLine() |> expression_to_tree |> output.WriteLine
  0 // return an integer exit code
