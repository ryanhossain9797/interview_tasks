open System


let run target =
    Seq.unfold
        (fun (num: uint64, div: uint64, maxFactor: uint64) ->
            if div > num then
                None
            elif num % div = 0UL then
                Some((num, div, div), (num / div, div + 1UL, div))
            else
                Some((num, div, maxFactor), (num, div + 1UL, maxFactor)))
        (target, 1UL, 0UL)
    |> Seq.maxBy (fun (_, _, maxFactor) -> maxFactor)
    |> fun (_, _, factor) -> factor

{ 1UL .. (Console.ReadLine() |> uint64) }
|> Seq.map (fun _ -> (Console.ReadLine() |> uint64))
|> Seq.map run
|> Seq.iter (printfn "%i")
