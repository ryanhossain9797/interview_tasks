open System

let validatePerfectSquare n =
    (float n) |> sqrt |> int |> fun m -> m * m = n

let run sum =
    [ 0 .. (sum / 2
            - (match sum % 2 with
               | 0 -> 1
               | _ -> 0)) ]
    |> Seq.map
        (fun c ->
            ((sum - c), (sum * (sum - 2 * c) / 2))
            |> fun (aPlusB, aTimesB) -> (aPlusB, aTimesB, ((aPlusB * aPlusB) - 4 * aTimesB), c))
    |> Seq.map
        (fun (aPlusB, aTimesB, bMinusASquared, c) ->
            match bMinusASquared >= 0
                  && validatePerfectSquare bMinusASquared
                  && (aPlusB * aPlusB) - 2 * aTimesB = c * c with
            | true -> aTimesB * c
            | false -> -1)
    |> Seq.max

{ 1 .. (Console.ReadLine() |> int) }
|> Seq.map (fun _ -> (Console.ReadLine() |> int))
|> Seq.map run
|> Seq.iter (printfn "%i")
