open System

let run (limit: uint64) =
    Seq.unfold (fun (a, b) -> Some(b, (b, a + b))) (0UL, 1UL)
    |> Seq.takeWhile (fun num -> num < limit)
    |> Seq.filter (fun num -> num % 2UL = 0UL)
    |> Seq.sum

{ 1 .. (Console.ReadLine() |> int) }
|> Seq.map (fun _ -> (Console.ReadLine() |> uint64))
|> Seq.map run
|> Seq.iter (printfn "%i")
