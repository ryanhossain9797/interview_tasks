open System

let threeDigits = [ 999 .. -1 .. 100 ]

let isPalindrome num =
    num
    |> string
    |> (fun num ->
        [ 0 .. (num.Length / 2) ]
        |> Seq.forall (fun n -> num.[n] = num.[num.Length - 1 - n]))


let run limit =
    List.collect (fun m -> (threeDigits |> List.map (fun n -> m * n))) threeDigits
    |> List.filter isPalindrome
    |> List.filter (fun n -> n < limit)
    |> List.max

{ 1 .. (Console.ReadLine() |> int) }
|> Seq.map (fun _ -> (Console.ReadLine() |> int))
|> Seq.map run
|> Seq.iter (printfn "%i")
