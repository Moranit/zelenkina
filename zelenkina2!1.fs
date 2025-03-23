open  System

//2\1 получить список из мин цифр чисел
let readListFromConsole () =
    let rec loop acc =
        printf "Введите элемент (или оставьте пустым для завершения): "
        let input = Console.ReadLine()
        if input = "" then
            List.rev acc 
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
    seq |> List.iter (fun element -> printfn "- %d" element) 


let rec findMinDigit (number: int) (acc: int) =
    if number = 0 then
        acc
    else
        let digit = number % 10 
        let newMin = if digit < acc then digit else acc 
        findMinDigit (number / 10) newMin 


let findMinDigitsInSeq (numbers: list<int>) =
    numbers |> List.map (fun n -> findMinDigit n 9)


[<EntryPoint>]
let main argv =
    let numbers = readListFromConsole() 
    let minDigits = findMinDigitsInSeq numbers 
    printfn "Минимальные цифры в каждом числе: %A" minDigits 
    0 

