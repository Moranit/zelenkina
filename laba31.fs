open System

// 3/1 нахожение мин цифры в числах
let readListFromConsole () =
    let rec loop acc =
        printf "Введите элемент (или оставьте пустым для завершения): "
        let input = Console.ReadLine()
        if input = "" then
            Seq.rev acc 
        else
            match Int32.TryParse(input) with
            | (true, number) when number > 0 -> 
                loop (number :: acc) 
            | (true, number) when number <= 0 ->
                printfn "Ошибка: '%s' не является натуральным числом." input
                loop acc 
            | (false, _) ->
                printfn "Ошибка: '%s' не является числом." input
                loop acc 
    loop []



let printList seq =
    printfn "Содержимое списка:"
    seq |> Seq.iter (fun element -> printfn "- %d" element) 


let findMinDigit (number: int) =
    let rec loop number acc =
        if number = 0 then
            acc
        else
            let digit = number % 10 
            let newMin = if digit < acc then digit else acc 
            loop (number / 10) newMin 
    loop number 9


let findMinDigitsInSeq (numbers: seq<int>) =
    numbers |> Seq.map (fun n -> lazy (findMinDigit n))

[<EntryPoint>]
let main argv =
    let numbers = readListFromConsole() 
    let minDigits = findMinDigitsInSeq numbers 
    printfn "Минимальные цифры в каждом числе: %A" (minDigits |> Seq.map (fun lazyValue -> lazyValue.Value)) 
    0 
