open System

let run digits digitCount maxOp =
    digits
    |> Seq.map (fun n -> (uint64 n) - (uint64 '0'))
    |> Seq.filter (fun n -> (n >= 0UL && n <= 9UL))
    |> Seq.toList
    |> Seq.windowed digitCount
    |> Seq.maxBy (Seq.reduce maxOp)
    |> Seq.reduce maxOp

{ 1 .. (Console.ReadLine() |> int) }
|> Seq.map
    (fun _ ->
        let config = Console.ReadLine()
        let data = Console.ReadLine()

        let (length, digitCount) =
            (config.Split ' ')
            |> (fun a -> (int a.[0], int a.[1]))

        (data, digitCount))
|> Seq.map (fun (line, digitCount) -> run line digitCount (*))
|> Seq.iter (printfn "%i")
