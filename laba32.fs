open System

// 3/2
let readSequenceFromConsole () =
    let rec loop acc =
        printf "Введите элемент (или оставьте пустым для завершения): "
        let input = Console.ReadLine()
        if input = "" then
            Seq.rev acc 
        else
            match Int32.TryParse(input) with
            | (true, number) when number > 0 -> 
                loop (number :: acc) 
            | (false, _) ->
                printfn "Ошибка: '%s' не является числом." input
                loop acc 
            | _ -> 
                printfn "Некорректное число."
                loop acc 
    loop [] 


let printSequence seq =
    printfn "Содержимое последовательности:"
    seq |> Seq.iter (fun element -> printfn "- %d" element) 


let rec containsDigit (number: int) (digit: int) : bool =
    if number = 0 then 
        false 
    else
        let currentDigit = number % 10 
        if currentDigit = digit then 
            true 
        else 
            containsDigit (number / 10) digit 


let countElementsWithDigit (numbers: seq<int>) (digit: int) =
    Seq.fold (fun acc number -> if containsDigit number digit then acc + 1 else acc) 0 numbers


let readNaturalNumberFromConsole () =
    let rec loop () =
        printf "Введите натуральное число (или оставьте пустым для завершения): "
        let input = Console.ReadLine()
        if input = "" then
            None 
        else
            match Int32.TryParse(input) with
            | (true, number) when number > 0 -> Some number 
            | (true, _) -> 
                printfn "Ошибка: '%s' не является натуральным числом." input
                loop () 
            | _ -> 
                printfn "Некорректный ввод. Введите целое число."
                loop ()
    loop () 


let rec readDigitFromConsole () =
    printf "Введите цифру для поиска (0-9): "
    let digitInput = Console.ReadLine()
    match Int32.TryParse(digitInput) with
    | (true, digit) when digit >= 0 && digit <= 9 -> 
        Some digit
    | _ ->
        printfn "Ошибка: '%s' не является цифрой." digitInput
        readDigitFromConsole() 

[<EntryPoint>]
let main argv =
    let numbers = readSequenceFromConsole() 
    printSequence numbers 

    let digit = readDigitFromConsole() 
    match digit with
    | Some d -> 
        let count = lazy (countElementsWithDigit numbers d)
        printfn "Количество элементов, содержащих цифру %d: %d" d (count.Value)
    | None -> 
        printfn "Запрос цифры завершён."
    
    0
