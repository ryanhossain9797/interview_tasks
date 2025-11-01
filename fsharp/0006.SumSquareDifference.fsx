open System

let run n =
    [ 1 .. n ]
    |> List.fold (fun (squareSum, sum) num -> (squareSum + (num * num), sum + num)) (0, 0)
    |> fun (squareSum, sum) -> (sum * sum) - squareSum

{ 1 .. (Console.ReadLine() |> int) }
|> Seq.map (fun _ -> (Console.ReadLine() |> int))
|> Seq.map run
|> Seq.iter (printfn "%i")
