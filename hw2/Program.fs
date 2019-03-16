open Processing

open LogicParser
open System.IO
open Microsoft.FSharp.Text.Lexing

let readLines (sr: StreamReader) = seq {
  while not sr.EndOfStream do
    yield sr.ReadLine()
 }


let solve header body: string =
    preprocess header

    let indexes = seq { 1..List.length body }
    //todo length
    
    (*printf "%d\n" (Seq.length indexes)
    printf "%d\n" (Seq.length body)*)
       
    Seq.zip indexes (Seq.distinct body) |> Seq.map (fun (i, x) ->
      let proof = process_expression x i
      post_process x proof i
      proof
      ) |> Seq.toList |> expand_proof

[<EntryPoint>]
let main argv =
    use input = File.OpenText("input.txt")
    use output = File.CreateText("output.txt")

    let header = input.ReadLine() |> LexBuffer<_>.FromString |> (LogicLexer.tokenize |> LogicParser.header)
    let body = readLines input |> Seq.map (fun s -> (s |> LexBuffer<_>.FromString |> LogicParser.body LogicLexer.tokenize)) |> Seq.toList  
    
    printf "%s" (solve header body)

    0 // return an integer exit code
