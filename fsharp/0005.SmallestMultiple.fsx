open System

let rec highestCommonFactor a b =
    match (a, b) with
    | (0, b) -> b
    | (a, b) -> highestCommonFactor (b % a) a

let leastCommonMultiple a b = (b / (highestCommonFactor b a)) * a

let run n =
    [ 1 .. n ] |> List.fold (leastCommonMultiple) 1

{ 1 .. (Console.ReadLine() |> int) }
|> Seq.map (fun _ -> (Console.ReadLine() |> int))
|> Seq.map run
|> Seq.iter (printfn "%i")
