open System

let sumOfMultiplesTill limit num =
    ((limit - 1UL) / num)
    |> (fun x -> num * (x * (x + 1UL) / 2UL))

let run limit =
    limit
    |> sumOfMultiplesTill
    |> (fun x -> (x 3UL) + (x 5UL) - (x 15UL) )

[ 1UL .. (Console.ReadLine() |> uint64) ] 
|> Seq.map 
    (fun _ -> 
        (Console.ReadLine() 
        |> uint64)
        |> run)
|> Seq.iter (printfn "%i")
